using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public partial class MCComponent
    {
        /*
        public int someMethod(out byte[] recievedData)
        {
            // このメソッドの戻り値
            int ret = -1;


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

                ret = this.sendRequestData(reqDataList.ToArray(), out recievedData);
            }

            return ret;
        }*/
    }

}
