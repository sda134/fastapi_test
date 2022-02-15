using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mtec.UtilityLibrary.Mitsubishi;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public partial class MCComponent
    {

        #region region - Get Device


        /// <summary>
        /// GetDevice 16bit int の一括読み込みメソッド
        /// </summary>
        public int GetDevice(string startDevice, int deviceCount, out short[] readData)
        {
            // 応答データを格納する変数
            byte[] recievedData;

            // このメソッドの戻り値
            int ret = this.send_GetWordDevice(startDevice, deviceCount, out recievedData);

            readData = null;

            if (ret == 0 && recievedData != null)
            {
                var valueList = new List<short>();

                for (int i = 0; i < recievedData.Length; i += 2)
                {
                    short sVal = BitConverter.ToInt16(recievedData.Skip(i).Take(2).ToArray(), 0);

                    valueList.Add(sVal);
                }

                readData = valueList.ToArray();
            }

            return ret;
        }


        /// <summary>
        /// GetDevice 32bit int の一括読み込みメソッド　※注意！三菱シーケンサ上ではダブルとなる
        /// </summary>
        public int GetDevice(string startDevice, int deviceCount, out int[] readData)
        {            
            // 応答データを格納する変数
            byte[] recievedData;





            // このメソッドの戻り値
            int ret = this.send_GetWordDevice(startDevice, deviceCount * 2, out recievedData);
            readData = null;






            if (ret == 0 && recievedData != null)
            {
                var valueList = new List<int>();

                for (int i = 0; i < recievedData.Length; i += 4)
                {
                    int iVal = BitConverter.ToInt32(recievedData.Skip(i).Take(4).ToArray(), 0);

                    valueList.Add(iVal);
                }

                readData = valueList.ToArray();
            }

            return ret;
        }



        /// <summary>
        /// GetDevice 32bit float の一括読み込みメソッド　三菱シーケンサ上ではダブル
        /// </summary>
        public int GetDevice(string startDevice, int deviceCount, out float[] readData)
        {            
            // 応答データを格納する変数
            byte[] recievedData;

            // このメソッドの戻り値
            int ret = this.send_GetWordDevice(startDevice, deviceCount * 2, out recievedData);
            readData = null;

            if (ret == 0 && recievedData != null)
            {
                var valueList = new List<float>();

                for (int i = 0; i < recievedData.Length; i += 4)
                {
                    float flVal = BitConverter.ToSingle(recievedData.Skip(i).Take(4).ToArray(), 0);

                    valueList.Add(flVal);
                }

                readData = valueList.ToArray();
            }

            return ret;
        }

        

        /// <summary>
        /// GetDevice bool ビット単位の一括読み込み
        /// </summary>
        /// <param name="bitLength">デバイス数＝ビット数を指定します。0 を指定すると256点になります。</param>
        public int GetDevice(string startDevice, int bitLength, out bool[] readData)
        {
            // このメソッドの戻り値
            int ret = -1;
            readData = null;

            var reqDataList = new List<byte>();

            byte[] recievedByte;

            try
            {
                // 文字列を自作型の DeviceFormat に変換　※例外発生の可能性あり
                var dev = (FxDevice)startDevice;

                if (this.Frame == MCProtocolFrame._1EFrame)
                {
                    #region region - 1E Frame
                    
                    // 1E フレームはフォーマットがまったく異なるので、独自で行う

                    // 送信するデータを格納する変数　基本＋要求が格納される
                    var sendData = new List<byte>();

                    //  基本となるバイトの配列：ビット単位一括読み出し
                    sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.ReadBit));


                    // ================= 以下、要求データ =================

                    // 開始デバイス番号 6byte
                    sendData.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._1EFrame));

                    // デバイス点数 1byte  bit数らしい。未確認　18.01.22 
                    sendData.Add((byte)bitLength);

                    // よくわからない 18.01.05  → 固定値らしい 18.01.08
                    sendData.Add(0x00);

                    // エラーコード格納用変数
                    int errorCode;

                    // 送信
                    ret = this.sendData_1E(sendData.ToArray(), out recievedByte, out errorCode);

#if DEBUG
                    if (errorCode != 0)
                        Console.WriteLine("1Eフレームでエラー:0x{0:X}", errorCode);
#endif

                    #endregion
                }
                else
                {
                    #region region - 3E, 4E Frame

                    // 要求データの作成

                    // コマンド：ビット単位の一括読出し(コマンド: 0401)
                    reqDataList.AddRange(new byte[] { 0x01, 0x04 });

                    // サブコマンド
                    switch (this.MELSECType)
                    {
                        case MELSECType.MELSEC_QL:
                            reqDataList.AddRange(new byte[] { 0x01, 0x00 });
                            break;

                        case MELSECType.MELSEC_iQ_R:    // 未テスト 18.01.19
                            reqDataList.AddRange(new byte[] { 0x03, 0x00 });
                            break;
                        default:
                            break;
                    }

                    // 開始デバイス番号＋デバイスコード
                    reqDataList.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._3EFrame));

                    // デバイス数
                    var byte_devCount = BitConverter.GetBytes((short)bitLength);

                    // 反転させる
                    byte_devCount.Reverse();

                    // 追加
                    reqDataList.AddRange(byte_devCount);

                    ret = this.sendRequestData(reqDataList.ToArray(), out recievedByte);

                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }


            if (ret == 0 && recievedByte != null)
            {
                #region region - 応答電文データの加工

                
                // 各ビットの情報を 4bit ずつに分けて応答電文を返す仕様らしい。
                // しかも　第２bit 第１bit 第４bit 第３bit と入る
                // なぜこんな仕様に？？ 18.01.23

                // 要素数を変更
                readData = new bool[bitLength];

                for (int b = 0; b < recievedByte.Length; b++)
                {
                    // 第1 byte
                    bool first = (recievedByte[b] & 0x10) == 0x10;

                    // 第2 byte
                    bool second = (recievedByte[b] & 0x01) == 0x01;

                    // 第1 byte はそのまま代入
                    readData[b * 2] = first;

                    // 第2 byte は戻り値の要素数を気にしつつ代入する
                    if (bitLength > b * 2 + 1)
                        readData[(b * 2) + 1] = second;
                }

                #endregion
            }

            return ret;
        }


        // byte 配列を取得する親メソッド。16bit int, 32bit int, float と取得の子メソッドに対応する
        private int send_GetWordDevice(string startDevice, int wordCount, out byte[] recievedData)
        {
            // このメソッドの戻り値
            int ret = -1;

            // 戻り値の初期値
            recievedData = null;

            try
            {
                // 文字列を自作型の DeviceFormat に変換　※例外発生の可能性あり
                var dev = (FxDevice)startDevice;

                if (this.Frame == MCProtocolFrame._1EFrame)
                {
                    #region region - 1E Frame

                    // 1E フレームはフォーマットがまったく異なるので、独自で行う

                    // 送信するデータを格納する変数　基本＋要求が格納される
                    var sendData = new List<byte>();

                    //  基本となるバイトの配列：ワード単位一括
                    sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.ReadWord));


                    // ================= 以下、要求データ =================

                    // 開始デバイス番号
                    var deviceBytes = Tools.GetDeviceByteData(dev, MCProtocolFrame._1EFrame);

                    // 1E フレームは非対応なデバイスがある
                    if (deviceBytes == null) return -1;

                    // 開始デバイス分の byte 配列を追加
                    sendData.AddRange(deviceBytes);

                    // デバイス点数 1byte
                    sendData.Add((byte)wordCount);

                    // よくわからない 18.01.05  → 固定値らしい 18.01.08
                    sendData.Add(0x00);

                    // エラーコード格納用変数
                    int errorCode;

                    // 送信
                    ret = this.sendData_1E(sendData.ToArray(), out recievedData, out errorCode);

                    #endregion
                }
                else
                {
                    #region region - 3E, 4E Frame

                    // 要求データを格納す変数
                    var reqDataList = new List<byte>();

                    // コマンド：一括読み込み
                    reqDataList.AddRange(new byte[] { 0x01, 0x04 });

                    // サブコマンド
                    switch (this.MELSECType)
                    {
                        case MELSECType.MELSEC_QL:
                            reqDataList.AddRange(new byte[] { 0x00, 0x00 });
                            break;

                        case MELSECType.MELSEC_iQ_R:    // 未テスト 18.01.19
                            reqDataList.AddRange(new byte[] { 0x02, 0x00 });
                            break;
                        default:
                            break;
                    }

                    // 開始デバイス番号
                    reqDataList.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._3EFrame));

                    // デバイス数
                    var byte_devCount = BitConverter.GetBytes((short)wordCount);

                    // 反転させる
                    byte_devCount.Reverse();

                    // 追加
                    reqDataList.AddRange(byte_devCount);

                    ret = this.sendRequestData(reqDataList.ToArray(), out recievedData);

                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        #endregion


        #region region - Set Device


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>range: 2147483647 ~ -2147483648</remarks>
        /// <returns></returns>
        public int SetDevice(string startDevice, params int[] args)
        {
            var writeByteList = new List<byte>();

            // 実際の16bit の数値データ：反転しながらバイト配列に変換
            foreach (int iVal in args)
            {
                // バイト配列へ変換 .NET のint は32bit　※Mitsubishi シーケンサでは ダブルとなる
                var reversedArg = BitConverter.GetBytes(iVal);

                // 反転させる
                reversedArg.Reverse();

                // 反転させた byte 配列を追加
                writeByteList.AddRange(reversedArg);
            }

            // 送信専用メソッドを用いて送信
            return this.send_SetWordDevice(startDevice, writeByteList.ToArray());
        }


        public int SetDevice(string startDevice, params short[] args)
        {
            var writeByteList = new List<byte>();

            // 実際の16bit の数値データ：反転しながらバイト配列に変換
            foreach (short shVal in args)
            {
                // short(16bit int) のバイト配列へ変換
                var reversedArg = BitConverter.GetBytes(shVal);

                // 反転させる
                reversedArg.Reverse();

                // 反転させた byte 配列を追加
                writeByteList.AddRange(reversedArg);
            }

            // 送信専用メソッドを用いて送信
            return this.send_SetWordDevice(startDevice, writeByteList.ToArray());
        }


        public int SetDevice(string startDevice, params float[] args)
        {
            var writeByteList = new List<byte>();

            // 実際の16bit の数値データ：反転しながらバイト配列に変換
            foreach (float fVal in args)
            {
                //.NET のint は32bit なので  

                // BitConverterでバイト配列へ変換    ※浮動小数点は反転させなくていいらしい 18.01.22
                var reversedArg = BitConverter.GetBytes(fVal);

                // 追加
                writeByteList.AddRange(reversedArg);
            }

            // 送信専用メソッドを用いて送信
            return this.send_SetWordDevice(startDevice, writeByteList.ToArray());
        }


        // ビット単位の書き込み
        public int SetDevice(string startDevice, params bool[] args)
        {
            // このメソッドの戻り値
            int ret = -1;
            
            var reqDataList = new List<byte>();

            byte[] recievedByte;


            try
            {
                // 文字列を自作型の DeviceFormat に変換　※例外発生の可能性あり
                var dev = (FxDevice)startDevice;

                // 実際の書き込みデータ
                var bitValueDataList = new List<byte>();


                if (args != null)
                {
                    #region region - 書き込みデータの加工

                    // 各ビットの情報を 4bit ずつに分けて応答電文を返す仕様らしい。
                    // しかも　第２bit 第１bit 第４bit 第３bit と入る
                    // なぜこんな仕様に？？ 18.01.23

                    for (int b = 0; b < (args.Length + 1) / 2; b++)
                    {
                        // 第1 bit
                        byte currentByte = (byte)(args[b * 2] ? 0x10 : 0x00);

                        // 第2 bit は戻り値の要素数を気にしつつ代入する
                        if (args.Length > b * 2 + 1 && args[b * 2 + 1])
                            currentByte = (byte)(currentByte | 0x01);

                        // 書き込みデータに追加
                        bitValueDataList.Add(currentByte);
                    }

                    #endregion
                }



                if (this.Frame == MCProtocolFrame._1EFrame)
                {
                    #region region - 1E Frame

                    // 1E フレームはフォーマットがまったく異なるので、独自で行う

                    // 送信するデータを格納する変数　基本＋要求が格納される
                    var sendData = new List<byte>();

                    //  基本となるバイトの配列：ビット単位一括読み出し
                    sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.WriteBit));


                    // ================= 以下、要求データ =================

                    // 開始デバイス番号
                    sendData.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._1EFrame));

                    // デバイス点数 1byte  bit数
                    sendData.Add((byte)args.Length);

                    // よくわからない 18.01.05  → 固定値らしい 18.01.08
                    sendData.Add(0x00);

                    // 実際の書き込みデータ
                    sendData.AddRange(bitValueDataList);

                    // エラーコード格納用変数
                    int errorCode;

                    // 送信
                    ret = this.sendData_1E(sendData.ToArray(), out recievedByte, out errorCode);
                    
                    #endregion
                }
                else
                {
                    #region region - 3E, 4E Frame


                    // 要求データの作成

                    // コマンド：一括書き込み
                    reqDataList.AddRange(new byte[] { 0x01, 0x14 });

                    // サブコマンド
                    switch (this.MELSECType)
                    {
                        case MELSECType.MELSEC_QL:
                            reqDataList.AddRange(new byte[] { 0x01, 0x00 });
                            break;

                        case MELSECType.MELSEC_iQ_R:    // 未テスト 18.01.19
                            reqDataList.AddRange(new byte[] { 0x03, 0x00 });
                            break;
                        default:
                            break;
                    }

                    // 開始デバイス番号
                    reqDataList.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._3EFrame));

                    // デバイス数    bit数
                    var byte_devCount = BitConverter.GetBytes((short)args.Length);

                    // 反転させる
                    byte_devCount.Reverse();

                    // デバイス数分のbyte配列の追加
                    reqDataList.AddRange(byte_devCount);

                    // 実際の書き込みデータ
                    reqDataList.AddRange(bitValueDataList);

                    // 送信
                    ret = this.sendRequestData(reqDataList.ToArray(), out recievedByte);

                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
           
            return ret;
        }


        // byte 配列を送信する親メソッド。16bit int, 32bit int, float と書込みの子メソッドに対応する
        public int send_SetWordDevice(string startDevice, byte[] writeDataByte)
        {
            // このメソッドの戻り値
            int ret = -1;

            // 応答データを格納する変数　※ただし応答データは無い
            byte[] recievedData;
             
            try
            {
                // 文字列を自作型の DeviceFormat に変換　※例外発生の可能性あり
                var dev = (FxDevice)startDevice;

                if (this.Frame == MCProtocolFrame._1EFrame)
                {
                    #region region - 1E Frame

                    // 1E フレームはフォーマットがまったく異なるので、独自で行う

                    // 送信するデータを格納する変数　基本＋要求が格納される
                    var sendData = new List<byte>();
                    
                    //  基本となるバイトの配列：ワード単位一括
                    sendData.AddRange(this.get_ByteData_BasicFormat_1E(SubHeader_1E.WriteWord));


                    // ================= 以下、要求データ =================

                    // 開始デバイス番号
                    sendData.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._1EFrame));

                    // 書き込みデータの word (2byte) 数
                    int wordCount = (writeDataByte.Length / 2);

                    // デバイス点数 1byte
                    sendData.Add((byte)wordCount);

                    // よくわからない 18.01.05  → 固定値らしい 18.01.08
                    sendData.Add(0x00);

                    // 書き込みデータそのもの
                    sendData.AddRange(writeDataByte);

                    // エラーコード格納用変数
                    int errorCode;

                    // 送信
                    ret = this.sendData_1E(sendData.ToArray(), out recievedData, out errorCode);

                    #endregion
                }
                else
                {
                    #region region - 3E, 4E Frame

                    // 要求データを格納する変数
                    var reqDataList = new List<byte>();

                    // コマンド：ワード単位一括書き込み
                    reqDataList.AddRange(new byte[] { 0x01, 0x14 });

                    // サブコマンド
                    switch (this.MELSECType)
                    {
                        case MELSECType.MELSEC_QL:
                            reqDataList.AddRange(new byte[] { 0x00, 0x00 });
                            break;

                        case MELSECType.MELSEC_iQ_R:    // 未テスト 18.01.19
                            reqDataList.AddRange(new byte[] { 0x02, 0x00 });
                            break;
                        default:
                            break;
                    }

                    // 開始デバイス番号
                    reqDataList.AddRange(Tools.GetDeviceByteData(dev, MCProtocolFrame._3EFrame));

                    // 書き込みデータの word (2byte) 数
                    int wordCount = (writeDataByte.Length / 2);

                    // デバイス数を示すバイト配列
                    var byte_devCount = BitConverter.GetBytes((short)wordCount);

                    // 反転させる
                    byte_devCount.Reverse();

                    // デバイス数を示すバイト配列を追加
                    reqDataList.AddRange(byte_devCount);

                    // 実際の書き込みデータの追加
                    reqDataList.AddRange(writeDataByte);

                    // 送信
                    ret = this.sendRequestData(reqDataList.ToArray(), out recievedData);
                    
                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        #endregion

    }
}
