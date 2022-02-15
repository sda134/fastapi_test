using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    // 1Eフレームの処理は独特なので、このファイルにまとめて書く

    public partial class MCComponent
    {
        private byte[] get_ByteData_BasicFormat_1E(SubHeader_1E subHeader)
        {
            var ret = new List<byte>();

            // サブヘッダ
            ret.Add((byte)subHeader);

            // PC 番号
            ret.Add((byte)this._pCNumber);

            // ACPU 監視タイマ
            ret.AddRange(new byte[] { (byte)this._watchTimer, 0x00 });

            return ret.ToArray();
        }


        public static byte[] Get_DeviceCodeDataByte_1E(FxDeviceType deviceType)
        {
            // バイト数は２バイト固定
            switch (deviceType)
            {
                case FxDeviceType.InnerRelay: return new byte[] { 0x20, 0x4D };
                case FxDeviceType.DataRegister: return new byte[] { 0x20, 0x44 };

                // 以下、未確認 18.01.26
                case FxDeviceType.InputSignal: return new byte[] { 0x20, 0x58 };
                case FxDeviceType.OutputSignal: return new byte[] { 0x20, 0x59 };
                case FxDeviceType.Annunciator: return new byte[] { 0x20, 0x46 };
                case FxDeviceType.LinqRelay: return new byte[] { 0x20, 0x42 };
                case FxDeviceType.Timer_Value: return new byte[] { 0x4E, 0x54 };
                case FxDeviceType.Timer_Contact: return new byte[] { 0x53, 0x54 };
                case FxDeviceType.Timer_Coil: return new byte[] { 0x43, 0x54 };
                case FxDeviceType.Counter_Value: return new byte[] { 0x4E, 0x43 };
                case FxDeviceType.Counter_Contact: return new byte[] { 0x53, 0x43 };
                case FxDeviceType.Counter_Coil: return new byte[] { 0x43, 0x43 };
                case FxDeviceType.LinqRegister: return new byte[] { 0x20, 0x57 };
                case FxDeviceType.FileRegister: return new byte[] { 0x20, 0x52 };

                default:
                    return null;
            }
        }


        public static string Get_CpuName_1E(int cpuCode)
        {
            switch (cpuCode)
            {
                case 0xF3: return "FX3U/FX3UC";
                default: return null;
            }
        }


        /// <summary>
        /// 1E フレーム専用の電文送信メソッド
        /// </summary>
        /// <remarks>
        /// 正常終了の場合：    戻り値が 0, receivedData が応答データ    
        /// タイムアウトの場合：戻り値が -1, receivedData が null
        /// </remarks>
        private int sendData_1E(byte[] sendData, out byte[] receivedData, out int errorCode)
        {
            // 各変数を生成、または初期化
            byte[] rcvBytes;
            int retCode = -1;
            receivedData = null;


            // まだよくわからないので 0 とする 18.01.05
            errorCode = 0;

            // データ送信
            this.sendData(sendData, out rcvBytes);


            // エラー対策を取りつつ応答データを解析する
            if (rcvBytes != null && rcvBytes.Length > 0)
            {
                if (rcvBytes.Length >= 2)
                {
                    // 終了コード 1byte    ※サブヘッダ分の 1byte をスキップしたあとの2バイトを取得
                    retCode = rcvBytes.Skip(1).Take(1).ToArray()[0];
                }


                // 終了コードが 0 でなく、応答電文が 2byte な事があった為
                if (retCode != 0 && rcvBytes.Length >= 4)
                {
                    // 異常コードがある場合： 2byte らしい
                    byte[] errCdByte = rcvBytes.Skip(2).Take(2).ToArray();

                    // 2byte 数値、Int16 に変換
                    errorCode = (int)BitConverter.ToInt16(errCdByte, 0);
                }
                else
                {
                    if (rcvBytes.Length > 2)
                    {
                        receivedData = rcvBytes.Skip(2).ToArray();
                    }
                }
            }


            return retCode;
        }
    }
}
