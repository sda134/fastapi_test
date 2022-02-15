using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public class ErrorMessage
    {
        public static string GetErrorMessage(int errorCode)
        {
            
            // 何故かまえ2byte が FFFF なので小細工をする
            var ushCode = (ushort)errorCode;

            switch (ushCode)
            {
                case 0x4031: return "自局のCPU ユニットが他局のCPU ユニットに対して指定したデバイスが存在しない，またはデバイスNo.が範囲外。";
                case 0x408B: return "指定のリモート要求が実行可能な状態ではない。";

                case 0xC05C: return "ワードデバイスに対するビット単位などを行った。";

                case 0xC059: return "CPUユニットでは使用不可能なコマンド，サブコマンド。";
                case 0xC060: return "要求データ長異常。";


                case 0xFFFF: return "本dll (MCProtocol) 内のエラー。主にタイムアウト。";

                default: return null;
            }
        }
    }
}
