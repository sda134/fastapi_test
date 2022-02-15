using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    public class Tools
    {
        public static byte[] GetDeviceByteData(FxDevice device, MCProtocolFrame frame)
        {
            var byteDataList = new List<byte>();

            if (frame == MCProtocolFrame._1EFrame)
            {
                // 先頭デバイス番号 1Eでは 4byte なのでそのまま int(Int32)を使用
                byteDataList.AddRange(BitConverter.GetBytes(device.DeviceNumber));

                // デバイスコード 2byte
                var deviceBytes = MCComponent.Get_DeviceCodeDataByte_1E(device.DeviceType);

                // 1E フレームでは非対応なデバイスが多数あるので、有効な場合のみデータ配列を返す  
                if (deviceBytes != null)
                    byteDataList.AddRange(deviceBytes);
                else
                    return null;
            }
            else
            {
                // ひとまず 4byte整数(int)を取得して、そこから 3byte だけ取り出す
                var byte_num = BitConverter.GetBytes(device.DeviceNumber).Take(3);

                // 反転させる
                byte_num.Reverse();

                // デバイス番号分を追加
                byteDataList.AddRange(byte_num);

                // デバイスタイプを追加
                byteDataList.Add((byte)device.DeviceType);
            }

            // 配列にして返す
            return byteDataList.ToArray();
        }
    }
}
