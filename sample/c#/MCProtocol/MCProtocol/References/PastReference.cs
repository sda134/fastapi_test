using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    class PastReference
    {
        /*
        public void sendEnginnerRequestData(byte[] requestDataArray, out byte[] receivedData)
        {
            // Works2 から送られているデータを真似て作った要求データ

            // 戻り値を格納する変数
            int retCode = -1;
            receivedData = null;

            // 送信データを格納する変数
            List<byte> sendBytesList = null;


            // サブヘッダからアクセス経路までのデータ
            sendBytesList = new List<byte>(this.get_ByteData_BasicFormat());


            // 18.01.09 意味不明だがついているデータをそのまま真似る
            sendBytesList.AddRange(new byte[] {

                0x00,0xFE,0x03,0x00,0x00,
            });


            //要求データ長：2byte なので 16bit の数値型 ushort を使用
            // ※ +2 監視タイマ 2byte を含めたデータ長さな為
            ushort dataLength_request = (ushort)(requestDataArray.Length);

            // データ長さを追加
            sendBytesList.AddRange(BitConverter.GetBytes(dataLength_request));

            // 要求データを追加
            sendBytesList.AddRange(requestDataArray);


            this._sendData(sendBytesList.ToArray(), out receivedData);
        }
        */

        /*
        // デバイス一括読み出しの要求データ配列を返すメソッド
        private byte[] getRequestData_SetWordDevice2(string startDevice, int[] writeData)
        {
            byte[] ret = null;
            var sendBytesList = new List<byte>();

            int startNumber;
            int wordCount;


            try
            {
                // 文字列を自作型の DeviceFormat に変換　※例外発生の可能性あり
                var dev = (DeviceFormat)startDevice;

                switch (dev.DeviceType)
                {
                    // ビット系
                    case DeviceType.InputSignal:
                    case DeviceType.OutputSignal:
                    case DeviceType.InnerRelay:
                    case DeviceType.EdgeRelay:
                    case DeviceType.LinqRelay:
                    case DeviceType.IntegratedTimerContact:
                    case DeviceType.IntegratedTimer_Coil:
                    case DeviceType.SpecialRelay:
                        {
                            // 2byte ごとに区切って処理を行う
                            startNumber = dev.DeviceNumber / 16;
                            wordCount = (writeData.Length / 16) + 1;

                            int startBit = dev.DeviceNumber % 16;

                            // word 単位に書き換えた新しい書き込みデータ
                            var writeData_re = new List<short>();

                            var tempDev = new DeviceFormat
                            {
                                DeviceNumber = startNumber,
                                DeviceType = dev.DeviceType,
                            };

                            // デバッグ用 override した ToString がうまく機能しているかどうか
                            string devName = tempDev.ToString();

                            // 現在の word 単位での値がどうなってるかを格納する変数
                            byte[] wordDataArray;

                            // 対象範囲内を一斉取得
                            int ret_d = this.send_GetDevice(devName, wordCount, out wordDataArray);

                            // 読み込みでエラー発生　処理を中止
                            if (ret_d != 0) return null;


                            int loopCount = 0;

                            for (int byt = 0; byt < wordCount; byt++)
                            {
                                var currentBytes = wordDataArray.Skip(byt * 2).Take(2);
                                var currentBitArray = new System.Collections.BitArray(currentBytes.ToArray());


                                //short currentWord = BitConverter.ToInt16(currentBytes.ToArray(), 0);


                                for (int bit = startBit; bit < 16; bit++)
                                {
                                    if (loopCount >= writeData.Length)
                                        break;

                                    // ビットの場合は 0かそれ以外かで判断する
                                    currentBitArray[bit] = writeData[loopCount] != 0;
                                    
                                    // カウントアップ
                                    loopCount++;
                                }


                                // 変換後のバイト配列を格納する変数
                                byte[] convertedBytes = new byte[2];
                                
                                // バイト配列に変換しつつコピー
                                currentBitArray.CopyTo(convertedBytes, 0);

                                // Int16 に変換しつつ変換後の値として追加
                                writeData_re.Add(BitConverter.ToInt16(convertedBytes, 0));

                                //writeData_re.Add()
                                startBit = 0;
                            }

                            // 書き込みデータの上書き
                            writeData = writeData_re.ToArray();
                        }
                        break;


                    case DeviceType.DataRegister:
                    case DeviceType.Timer_Value:
                    case DeviceType.IntegratedTimer_Value:
                        {
                            #region region - word type
                            

                            startNumber = dev.DeviceNumber;
                            wordCount = writeData.Length;



                            

                            #endregion
                        }
                        break;


                    // よく分からない
                    case DeviceType.LatchRelay:
                    case DeviceType.Annunciator:
                    case DeviceType.UnitBuffer:
                    case DeviceType.UnitBufferHG:
                    case DeviceType.SpecialRegister:
                    case DeviceType.IndexRegister:
                    case DeviceType.FileRegister:
                    case DeviceType.FileRegister_ZR:
                    default:
                        {
                            startNumber = 0;
                            wordCount = 0;
                        }
                        break;
                }



                




                // 書き込みデータ
                var byte_write = new List<byte>();

                // 反転しながらバイト配列に変換
                foreach (var w in writeData)
                {
                    var w_r = BitConverter.GetBytes(w);

                    w_r.Reverse();

                    byte_write.AddRange(w_r);
                }

                



                // 先頭デバイス番号
                sendBytesList.AddRange(byte_startNum);

                // デバイスコード
                sendBytesList.Add((byte)dev.DeviceType);

                // ワード(2byte)数 = 
                sendBytesList.AddRange(byte_devCount);

                // 書き込みデータ
                sendBytesList.AddRange(byte_write);

                ret = sendBytesList.ToArray();

            }
            catch (Exception)
            {
                // 例外対応するならここに記述


                throw;
            }

            return ret;
        }

    */

        /*
        public int ReadDeviceRandom(string[] DeviceList, out short[] Data)
        {
            var ret = new List<short>();
            var requestData = new List<byte>();

            // 未対応の書式だったデバイス文字列の要素番号を格納する変数
            var errorIndecies = new List<int>(); ;


            #region region - 要求データの作成


            requestData.AddRange(new byte[]
            {
                // コマンド
                0x03,0x04,

                // サブコマンド
                0x00,0x00,
            });


            // Wordアクセス数 1byte
            requestData.Add((byte)DeviceList.Length);

            // DoubleWord アクセス数 1byte →　常に0 でいいと思う 18.01.08
            // ２つ必要ならパラメータ増やせばいいだけ
            requestData.Add((byte)0x00);


            for (int i = 0; i < DeviceList.Length; i++)
            {
                string s = DeviceList[i];

                // TryParse の結果を格納する変数
                DeviceFormat dev;


                if (!DeviceFormat.TryParse(s, out dev))
                {
                    // 変換失敗した要素番号を保持する
                    errorIndecies.Add(i);
                }
                else
                {
                    // ひとまず 4byte整数(int)を取得して、そこから 3byte だけ取り出す
                    byte[] byte_devNum = BitConverter.GetBytes(dev.DeviceNumber).Take(3).ToArray();

                    // 反転させる
                    byte_devNum.Reverse();

                    // 先頭デバイス番号
                    requestData.AddRange(byte_devNum);

                    // デバイスコード
                    requestData.Add((byte)dev.DeviceType);
                }
            }

            #endregion


            byte[] receivedData;

            int iRet = this.sendRequestData(requestData.ToArray(), out receivedData);

            if (receivedData != null)
            {
                int count = 0;

                for (int i = 0; i < DeviceList.Length; i++)
                {
                    // 変換失敗した文字列の分のデータは 0 とする
                    if (errorIndecies.FindIndex(x => x == i) != -1)
                        ret.Add((short)0);
                    else
                    {
                        // 値を示すバイト配列
                        byte[] valueByte = receivedData.Skip(count * 2).Take(2).ToArray();

                        // Int16 に変換しつつ戻り値に追加
                        ret.Add(BitConverter.ToInt16(valueByte, 0));

                        // カウントアップ
                        count++;
                    }
                }
            }

            Data = ret.ToArray();

            return iRet;
        }*/

        /*
        private int send_SetDevice(string startDevice, int[] writeData)
        {
            // このメソッドの戻り値
            int ret = -1;

            // 応答データを格納する変数　※ただし応答データは無い
            byte[] recievedData;


            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                //  基本となるバイトの配列：ワード単位一括書き込み
                sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.WriteWord));

                // 要求データ
                //sendData.AddRange(this.getRequestData_SetDevice_1E(startDevice, writeData));

                // エラーコードを格納する変数
                int errorCode;

                // データ送信               
                // ret = this._sendData_1E(sendData.ToArray(), out data, out errorCode);
            }
            else
            {
                // 要求データの作成
                byte[] requestData = this.getRequestData_SetWordDevice(startDevice, writeData);

                ret = this.sendRequestData(requestData, out recievedData);
            }

            return ret;
        }*/

        private byte[] randomRead()
        {
            return new byte[]
            {
                // コマンド
                0x03,0x04,

                // サブコマンド
                0x00,0x00,

                // Wordアクセス数
                0x02,

                // DoubleWord アクセス数 →常に0 でいいと思う 18.01.08
                0x00,

                // n1 アドレス
                0x01,0x00,0x00,0xA8,

                // n2 アドレス
                0x05,0x00,0x00,0xA8,
            };
        }

        private byte[] set_monitor()
        {
            return new byte[]
            {
                // コマンド
                0x03,0x04,

                // サブコマンド
                0x00,0x00,

                // Wordアクセス数
                0x02, 0x00,

                // DoubleWord アクセス数 →常に0 でいいと思う 18.01.08
                0x00, 0x00,

                // 先頭アドレス BMF#1
                0x01,0x00,0x00,0x00,

                // ワード長さ
                0x01,0x00,
            };
        }
    }
}
