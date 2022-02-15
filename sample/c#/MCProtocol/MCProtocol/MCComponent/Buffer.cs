using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public partial class MCComponent
    {
        public int ReadBuffer(int startAddress, int wordCount, out ushort[] readData)
        {
            // このメソッドの戻り値
            int ret = -1;
            readData = null;

            byte[] recievedData;

            if (this.Frame == MCProtocolFrame._1EFrame)
            {
                // 1E フレームはフォーマットがまったく異なるので、独自で行う

                // 送信するデータを格納する変数　基本＋要求が格納される
                var sendData = new List<byte>();

                // エラーコード格納用変数
                int errorCode;

                // 送信
                ret = this.sendData_1E(sendData.ToArray(), out recievedData, out errorCode);
            }
            else
            {
                // 要求データを格納する変数
                var reqDataList = new List<byte>();

                // コマンド：バッファメモリ一括読出し [CODE:0613]
                reqDataList.AddRange(new byte[] { 0x13, 0x06 });

                // サブコマンド
                reqDataList.AddRange(new byte[] { 0x00, 0x00 });

                // 先頭アドレスを示すバイト配列　※4byte(int) でいいらしい
                var byte_address = BitConverter.GetBytes(startAddress);

                // 反転させる
                byte_address.Reverse();

                // 先頭アドレスを示すバイト配列を追加
                reqDataList.AddRange(byte_address);


                // ワード長を示すバイト配列　※2byte
                var byte_devCount = BitConverter.GetBytes((short)wordCount);

                // 反転させる
                byte_devCount.Reverse();

                // ワード長を示すバイト配列を追加
                reqDataList.AddRange(byte_devCount);

                // 送信
                ret = this.sendRequestData(reqDataList.ToArray(), out recievedData);
            }


            // エラーの時は経路情報が返ってくるだけなので、正常終了した時のみ
            if (ret == 0 && recievedData != null)
            {
                readData = new ushort[wordCount];

                for (int b = 0; b < wordCount; b++)
                {
                    readData[b] = BitConverter.ToUInt16(recievedData.Skip(b * 2).Take(2).ToArray(), 0);
                }
                //readData
            }


            return ret;
        }
    }
}
