using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public partial class MCComponent
    {

        #region region - constructor, destructor


        public MCComponent()
        {
        }

        // デストラクタ　※初めて書いた気がするので注意する
        ~MCComponent()
        {
            this.Dispose();
        }


        // https://clickan.click/idisposable/
        public void Dispose()
        {
        }


        #endregion


        public event MCComponentEventHandler MCComponentEventInvoked = (obj, e) => { };

        /// <summary>
        /// サブヘッダからアクセス岐路までの基本データを返します。
        /// </summary>
        private byte[] get_ByteData_BasicFormat()
        {
            var ret = new List<byte>();

            switch (this.Frame)
            {
                case MCProtocolFrame._3EFrame:
                    {
                        // 要求電文：こちらから送信する際はすべて要求電文になる
                        ret.AddRange(new byte[] { 0x50, 0x00 });


                        // アクセス経路 2/5 byte：ネットワーク番号、PC番号
                        ret.AddRange(new byte[] { (byte)this._netWorkNumber, (byte)this._pCNumber });


                        // アクセス経路 2/5 byte ：要求先ユニットI/O 番号　※2byte なのでまず Int16である ushort に変換
                        ret.AddRange(BitConverter.GetBytes((ushort)this._targetIONumber));


                        // アクセス経路 1/5 byte ：要求先ユニット局番
                        ret.Add((byte)this._targetUnitNumber);
                    }
                    break;

                default:
                    break;
            }

            return ret.ToArray();
        }




        private bool sendData(byte[] sendingData, out byte[] receivedData)
        {
            // 参考資料： https://dobon.net/vb/dotnet/internet/udpclient.html


            // 戻り値の初期値
            receivedData = null;
           
            if (this.Protocol == CommunicationProtocol.TCP_IP)
            {
                #region region - TCP

                try
                {
                    // リモートIP のオブジェクトを生成
                    var remoteIP = System.Net.IPAddress.Parse(this._remoteIPAddress);

                    // リモートエンドポイントにバインド
                    var remoteEP = new System.Net.IPEndPoint(remoteIP, this._remotePortNumber);

                    // TCP Client のインスタンスを生成
                    using (var tcpClient = new System.Net.Sockets.TcpClient())
                    {
                        // Connectメソッド、もしくはコンストラクタでハンドシェイクを行う。
                        // コンストラクタ：TcpClient(string, int) または TcpClient (IPEndPoint)
                        tcpClient.Connect(remoteEP);


                        // NetworkStreamを取得
                        // MemoryStreamを生成  18.01.26 今まで using を２重にできる事を知らなかった
                        using (var ns = tcpClient.GetStream())
                        using (var ms = new System.IO.MemoryStream())
                        {
                            // 応答時間のタイムアウトを設定 (mSec)　※タイムアウトすると例外が発生する
                            ns.ReadTimeout = this.WatchTimerMiliseconds;

                            //データを送信する
                            ns.Write(sendingData, 0, sendingData.Length);

                            byte[] resBytes = new byte[256];

                            int resSize = 0;

                            do
                            {
                                //データの一部を受信する
                                resSize = ns.Read(resBytes, 0, resBytes.Length);

                                //Readが0を返した時はサーバーが切断したと判断
                                if (resSize == 0)
                                {
                                    Console.WriteLine("サーバーが切断しました。");
                                    break;
                                }

                                //受信したデータをMemoryStream に蓄積する
                                ms.Write(resBytes, 0, resSize);

                            } while (ns.DataAvailable);

                            // MemoryStream から配列を取得
                            receivedData = ms.ToArray();
                        }                       
                    }
                }
                
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                    receivedData = null;

                    return false;
                }

                #endregion
            }
            else if (this.Protocol == CommunicationProtocol.UDP_IP)
            {
                #region region - UDP

                try
                {

                    //UdpClientオブジェクトを生成
                    using (var udpClient = new System.Net.Sockets.UdpClient())
                    {
                        // 受信データを格納する変数
                        byte[] rcvData = null;

                        //リモートホストを指定してデータを送信する
                        udpClient.Send(sendingData, sendingData.Length, this._remoteIPAddress, this._remotePortNumber);
                        
                        // 接続されてない時のタイムアウトの為、別スレッドでコールバックを待つ
                        Task.Run(async () =>
                        {
                            var ar = await udpClient.ReceiveAsync();

                            rcvData = ar.Buffer;                            
                        });

                        // タイムアウト用ストップウォッチ
                        // UDP はハンドシェイクもなしに byte配列を投げるので、タイムアウト処理が必要
                        var sw_timeout = new System.Diagnostics.Stopwatch();

                        // ストップウォッチ開始
                        sw_timeout.Start();

                        // データ受信、もしくはタイムアウトするまでまつ
                        while (rcvData == null && (sw_timeout.ElapsedMilliseconds < WatchTimerMiliseconds)) { }

                        // ストップウォッチ停止
                        sw_timeout.Stop();

                        if (rcvData != null)
                            receivedData = rcvData;
                        else
                            receivedData = null;
                    }

                    #region region - 備忘録

                    // IPアドレスをListenする場合： UdpClientコンストラクタを呼び出す時、ポート番号だけを渡す
                    //this.udpClient = new System.Net.Sockets.UdpClient(this._localPortNumber);
                    // もしくは　InterNetworkを渡す
                    //this.udpClient = new System.Net.Sockets.UdpClient(this._localPortNumber, System.Net.Sockets.AddressFamily.InterNetwork);

                    #endregion

                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                    receivedData = null;
                    return false;
                }

                #endregion
            }


            #region - デバッグ用送信受信データの確認

#if DEBUG
            string sndMsg = string.Join(",", sendingData.Select(x => x.ToString("X")));
            Console.WriteLine("Sending Data:{0}", sndMsg);
#endif
            this.MCComponentEventInvoked.Invoke(this, new MCComponentEventArgs
            {
                SentData = sendingData,
                ReceivedData = receivedData,
            });

            #endregion 


            return true;
        }


        private int sendRequestData(byte[] requestDataArray, out byte[] receivedData)
        {
#if DEBUG
            //要求データを文字列に変換する
            string reqMsg = string.Join(",", requestDataArray.Select(x => x.ToString("X")));
            Console.WriteLine("Request Data:{0}", reqMsg);
#endif

            // 戻り値を格納する変数
            int retCode = -1;
            receivedData = null;

            // 送信データを格納する変数
            List<byte> sendBytesList = null;

            // 各フレームごとに処理を変更する　※実際は3E フレーム以外は実装する気はない 17.12.22
            switch (this.Frame)
            {
                case MCProtocolFrame._3EFrame:
                    {
                        #region region

                        // サブヘッダからアクセス経路までのデータ
                        sendBytesList = new List<byte>(this.get_ByteData_BasicFormat());

                        // 基本部分のデータ長さ
                        int basicDataLength = sendBytesList.Count;

                        //要求データ長：2byte なので 16bit の数値型 ushort を使用
                        // ※ +2 監視タイマ 2byte を含めたデータ長さな為
                        ushort dataLength_request = (ushort)(requestDataArray.Length + 2);

                        // データ長さを追加
                        sendBytesList.AddRange(BitConverter.GetBytes(dataLength_request));

                        // 監視タイマ　同じように16bit に変換してから加算。　※変換しないと32bit 分のデータが加算されてしまう。
                        sendBytesList.AddRange(BitConverter.GetBytes((ushort)this._watchTimer));

                        // 要求データを追加
                        sendBytesList.AddRange(requestDataArray);



                        if (sendBytesList != null)
                        {
                            // 受信データを格納する変数
                            byte[] rcvData;

                            this.sendData(sendBytesList.ToArray(), out rcvData);

                            if (rcvData != null)
                            {
                                // データ長さ分のデータバイト
                                var retBytes_length = rcvData.Skip(basicDataLength).Take(2).ToArray();

                                // データ長さ（終了コード 2byte + 受信データ長さ）
                                int dataLength_recieve = (int)BitConverter.ToInt16(retBytes_length, 0);

                                // 終了コード分のデータバイト
                                var retBytes_code = rcvData.Skip(basicDataLength + 2).Take(2).ToArray();

                                // 終了コード
                                retCode = (int)BitConverter.ToInt16(retBytes_code, 0);

#if DEBUG
                                #region region - error code

                                Console.WriteLine("Return Code:{0:X}", retCode);

                                if (retCode != 0)
                                {
                                    string msg = ErrorMessage.GetErrorMessage(retCode);
                                    if (msg != null)
                                        Console.WriteLine("Return Code:{0:X}:{1}", retCode, msg);
                                }



                                #endregion
#endif

                                // 受信データ
                                receivedData = rcvData.Skip(basicDataLength + 4).ToArray();

#if DEBUG
                                Console.WriteLine("Received Data:{0:X}", string.Join(",", receivedData.Select(x => x.ToString("X"))));
#endif

                            }
                            else
                            {
                                retCode = -1;
                                receivedData = null;
                            }
                        }
                        #endregion
                    }
                    break;

                default:
                    break;
            }

            return retCode;
        }



        // 開発用に外に公開する
        public void SendData(byte[] sendData, out byte[] receivedData) => this.sendData(sendData, out receivedData);
        

        // 開発用に外に公開する
        public int SendRequestData(byte[] equestDataArray, out byte[] receivedData) => this.sendRequestData(equestDataArray, out receivedData);
    }


    #region - delegate

    
    public delegate void MCComponentEventHandler(object sender, MCComponentEventArgs e);


    public class MCComponentEventArgs : EventArgs
    {
        public byte[] SentData;
        public byte[] ReceivedData;
    }

    #endregion

}
