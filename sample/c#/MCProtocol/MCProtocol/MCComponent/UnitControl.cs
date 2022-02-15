using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public partial class MCComponent
    {

        // 基本的に半角の文字列を送る事を想定しているらしいので、string のオーバーロードを作成
        public int Send_ResponseTest(string stringData, out string recievedString)
        {
            // 応答電文を格納する変数
            byte[] recievedData;

            // 送信
            int ret = this.Send_ResponseTest(System.Text.Encoding.UTF8.GetBytes(stringData), out recievedData);

            if (recievedData == null)
                recievedString = null;
            else
                /// 応答 byte配列を string に変換
                recievedString = System.Text.Encoding.UTF8.GetString(recievedData);

            return ret;
        }


        public int Send_ResponseTest(byte[] byteData, out byte[] recievedData)
        {
            //throw new NotImplementedException("未確認メソッドです。");


            // 戻り値
            int ret = -1;
            recievedData = null;


            var requestData = new List<byte>();

            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                //  基本となるバイトの配列：ワード単位一括
                sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.ResponseTest));

                // 最大254 バイト分の数値(00 ～ FFH) データ　とあるのでバイト数を調整する
                if (byteData.Length > 254)
                    byteData = byteData.Take(254).ToArray();

                // サブヘッダ以下のデータ長（バイト数）1byte
                // ※デバイス制御では word数 をパラメータとしている点が違う
                sendData.Add((byte)(byteData.Length + sendData.Count + 1));

                // 送信データの追加
                sendData.AddRange(byteData);

                // エラーコード格納用変数
                int errorCode;
               
                // 応答電文を格納する変数
                byte[] rcvData;

                // 送信 18.01.23 応答がない
                ret = this.sendData_1E(sendData.ToArray(), out rcvData, out errorCode);
            }
            else
            {


                requestData.AddRange(new byte[]            
                {
                    0x19, 0x06,
                    0x00, 0x00,
                });

                // 半角文字列(数字0～9，英字A～F)を，1～960バイトの範囲 　とあるのでバイト数を調整する
                if (byteData.Length > 960)
                    byteData = byteData.Take(960).ToArray();

                // 送信データのバイト数
                var byte_devCount = BitConverter.GetBytes((short)byteData.Length);

                // 反転させる
                byte_devCount.Reverse();

                // バイト数分の byte配列を追加
                requestData.AddRange(byte_devCount);

                // 送信データそのもの
                requestData.AddRange(byteData);

                // 応答電文を格納する変数
                byte[] rcvData;

                // 送信
                ret = this.sendRequestData(requestData.ToArray(), out rcvData);

                if (rcvData != null && rcvData.Length > 1)
                {
                    // 応答電文長
                    short rcvDataLength = BitConverter.ToInt16(rcvData.Take(2).ToArray(), 0);

                    // 最初の 2 バイトを除いた値を返す
                    recievedData = rcvData.Skip(2).ToArray();
                }
            }

            return ret;
        }


        public int Send_GetCPUName(out string cpuName)
        {
            // 戻り値
            int ret = -1;
            cpuName = null;

            // 応答データを格納する変数
            byte[] rcvData;

            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                //  基本となるバイトの配列：ワード単位一括
                sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.GetPCName));

                // エラーコード格納用変数
                int errorCode;

                // 送信
                ret = this.sendData_1E(sendData.ToArray(), out rcvData, out errorCode);
                
                // 受信成功した場合
                if (ret == 0 && rcvData != null)
                {
                    // CPU名を示すコード (2byte の数値) が返ってくるらしい 18.01.23
                    short cpuCode = BitConverter.ToInt16(rcvData, 0);

                    // コード番号からCPU 名を取得
                    cpuName = MCComponent.Get_CpuName_1E(cpuCode);
                }
            }
            else
            {
                // FX5U-32M: 成功

                // 要求データを格納する変数
                var requestData = new List<byte>();

                // 要求データを作成
                requestData.AddRange(new byte[]
                {
                    0x01, 0x01,
                    0x00, 0x00,
                });
                
                // 送信
                ret = this.sendRequestData(requestData.ToArray(), out rcvData);

                // 受信成功した場合
                if (ret == 0 && rcvData != null)
                {
                    cpuName = System.Text.Encoding.UTF8.GetString(rcvData);
                }
            }

            return ret;
        }


        public int Send_RemoteRun(bool forceRun, ClearMode clearMode)
        {
            int ret = -1;

            byte[] rcvData;


            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                //  基本となるバイトの配列：ワード単位一括
                sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.Remote_Run));

                // エラーコード格納用変数
                int errorCode;

                // 送信
                ret = this.sendData_1E(sendData.ToArray(), out rcvData, out errorCode);
            }
            else
            {
                // Q03UDVCPU: この Remote Run は成功しないが、X入力で run するように設定できる
                // FX5U-32M : forceRun:関係ない, ClearMode.NoClear で成功、それ以外では失敗  18.01.18


                var requestData = new List<byte>();

                requestData.AddRange(new byte[]
                {
                // コマンド
                0x01, 0x10,

                // サブコマンド
                0x00, 0x00,

                // モード
                (byte)(forceRun? 0x01: 0x03) , 0x00,

                // クリアモード
                (byte)clearMode,

                0x00
                });


                ret = this.sendRequestData(requestData.ToArray(), out rcvData);
            }

            return ret;
        }


        public int Send_RemoteStop()
        {
            int ret = -1;

            byte[] rcvData;


            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                //  基本となるバイトの配列：ワード単位一括
                sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.Remote_Stop));

                // エラーコード格納用変数
                int errorCode;

                // 送信
                ret = this.sendData_1E(sendData.ToArray(), out rcvData, out errorCode);
            }
            else
            {
                // FX5U-32M 成功

                //throw new NotImplementedException("未確認メソッドです。");

                var requestData = new List<byte>();

                requestData.AddRange(new byte[]
                {
                    0x02, 0x10,
                    0x00, 0x00,
                    0x01, 0x00,
                });

                
                ret = this.sendRequestData(requestData.ToArray(), out rcvData);
            }

            return ret;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 動作条件：
        /// リモートRESETを許可する設定にしてある。
        /// PLC が STOP 状態。
        /// </remarks>
        public int Send_RemoteReset()
        {
            // FX5U-32M 成功

            var requestData = new List<byte>();

            requestData.AddRange(new byte[]
            {
                // コマンド
                0x06, 0x10,

                // サブコマンド
                0x00, 0x00,


                // FX5U の取説 MELSEC iQ-F FX5ユーザーズマニュアル(MCプロトコル編) p81 には
                // 0x00 , 0x00 と書いてある → 実際に FX5U-32MT/ES ではそれで動作した 18.01.19

                // MELSECコミュニケーションプロトコルリファレンスマニュアル p174 には
                // 0x00 , 0x01 と書いてある

                0x00, 0x00,
            });
           

            byte[] rcvData;

            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            return ret;
        }


        public int Send_RatchClear()
        {
            // Stop 状態の時に行う必要があるらしい

            // FX5U-32M 成功 18.01.19

            var requestData = new List<byte>();

            requestData.AddRange(new byte[]
            {
                0x05, 0x10,
                0x00, 0x00,

                // 固定値
                0x01, 0x00,
            });

            byte[] rcvData;

            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            return ret;
        }


        public int Send_RemotePause(bool forceRun)
        {
            // FX5U-32M 成功

            //throw new NotImplementedException("未確認メソッドです。");

            var requestData = new List<byte>();


            requestData.AddRange(new byte[]
            {
                // コマンド
                0x03, 0x10,

                // サブコマンド
                0x00, 0x00,

                // モード
                (byte)(forceRun? 0x01: 0x03) , 0x00,
            });


            string sndMsg = string.Join(",", requestData.Select(x => x.ToString("X")));


            byte[] rcvData;

            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            return ret;
        }


        public int Send_ErrorReset()
        {
            //FX5U で成功

            var requestData = new List<byte>();

            // 5U の取説にはこう書いてあるので 
            requestData.AddRange(new byte[]
            {
                0x17, 0x16,
                0x00, 0x00,
            });


            // MELSECコミュニケーションプロトコルリファレンスマニュアル p138 11.5 エラー情報のクリア
            // には以下の byte[] を送信するように書いてあるが、成功したためしがない。
            /*
            requestData.AddRange(new byte[]
            {
                0x17, 0x16,
                0x05, 0x00,
                0xFF, 0x00,
                0x00, 0x00,
            });
            */


            byte[] rcvData;

            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            return ret;
        }


        public int Lock(string remotePassword)
        {
            // Q/L では 4文字固定、iQ-R シリーズでは 6-32文字
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(remotePassword);

            // 引数の値が異常（とくに文字列）な場合はArgumentException を throw するようにする。
            if (this.CpuSeries == CpuSeries.Melsec_QL && passwordBytes.Length != 4)
            {
                throw new ArgumentException("MELSEC-Q/Lシリーズではパスワード長は半角英数４文字固定です。");
            }


            var requestData = new List<byte>();

            requestData.AddRange(new byte[]
            {
                // コマンド
                0x31, 0x16,

                // サブコマンド
                0x00, 0x00,
            });

            // リモートパスワード長 2byte
            requestData.AddRange(BitConverter.GetBytes((short)passwordBytes.Length));

            // パスワードを示す byte配列を追加
            requestData.AddRange(passwordBytes);

            // 応答データ　但し応答データは返らない
            byte[] rcvData;

            // 送信
            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            // 結果を返す
            return ret;

        }


        public int UnLock(string remotePassword)
        {            
            // Q/L では 4文字固定、iQ-R シリーズでは 6-32文字
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(remotePassword);

            // 引数の値が異常（とくに文字列）な場合はArgumentException を throw するようにする。
            if(this.CpuSeries == CpuSeries.Melsec_QL && passwordBytes.Length != 4)
            {
                throw new ArgumentException("MELSEC-Q/Lシリーズではパスワード長は半角英数４文字固定です。");
            }


            var requestData = new List<byte>();

            requestData.AddRange(new byte[]
            {
                // コマンド
                0x30, 0x16,

                // サブコマンド
                0x00, 0x00,
            });
           
            // リモートパスワード長 2byte
            requestData.AddRange(BitConverter.GetBytes((short)passwordBytes.Length));

            // パスワードを示す byte配列を追加
            requestData.AddRange(passwordBytes);

            // 応答データ　但し応答データは返らない
            byte[] rcvData;

            // 送信
            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            // 結果を返す
            return ret;
        }


        // 可能なら一つのメソッドにまとめたい　18.01.08
        public int SendUnitControl(UnitControlType controlType, bool forceRun)
        {
            var requestData = new List<byte>();

            // コマンド
            requestData.AddRange(BitConverter.GetBytes((ushort)controlType));

            // サブコマンド
            requestData.AddRange(new byte[] { 0x00, 0x00, });



            // モード（強制実行可否）
            if (forceRun)
                requestData.AddRange(new byte[] { 0x03, 0x00 });
            else
                requestData.AddRange(new byte[] { 0x01, 0x00 });


            // クリアモード　＋　固定値
            requestData.AddRange(new byte[] { 0x00, 0x00 });


            string sndMsg = string.Join(",", requestData.Select(x => x.ToString("X")));

            Console.WriteLine("要求データ:{0}", sndMsg);

            byte[] rcvData;

            int ret = this.sendRequestData(requestData.ToArray(), out rcvData);

            return ret;
        }
    }
}
