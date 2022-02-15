using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent
{
    // ※MitsubishiPLC.cs を（リンクとして）追加する必要がある


    public static class ActProgExtensionMethods
    {
        /// <summary>
        /// BCD にも対応した拡張メソッドです。
        /// </summary>
        public static int ReadDeviceRandom(this ActProg actProg, ref Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat[] fields)
        {
            int iRet = actProg.ReadDeviceRandom(
                fields.Select(x => x.DeviceName),
                fields.Select(x =>
                {
                    #region

                    switch (x.DeviceFormatType)
                    {
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                            return typeof(Int32);

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                            return typeof(Single);

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.BCD16:
                            return typeof(UInt16);

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.BCD32:
                            return typeof(UInt32);

                        default: return typeof(Int16);
                    }
                    
                    #endregion
                }),
                out object[] values);


            if (iRet == 0)
            {
                #region region - BCD変換

                for (int i = 0; i < fields.Count(); i++)
                {
                    object currentValue = i < values.Count() ? values[i] : null;

                    if (currentValue == null) continue;

                    switch (fields.ElementAt(i).DeviceFormatType)
                    {
                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD16:
                            {
                                ushort curVal = (ushort)currentValue;
                                ushort shVal = 0;

                                // target & 0b11111111(0xF) とする事で、1になっているbit だけ取り出せる
                                for (int digit = 0; digit < 4; digit++)
                                    shVal += (ushort)(((curVal >> (4 * digit)) & 0xF) * (Math.Pow(10, digit)));

                                fields.ElementAt(i).CurrentValue = shVal;
                            }
                            break;

                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD32:
                            {
                                uint curVal = (uint)currentValue;
                                uint uiVal = 0;

                                for (int digit = 0; digit < 8; digit++)
                                    uiVal += (uint)(((curVal >> (4 * digit)) & 0xF) * (Math.Pow(10, digit)));

                                fields.ElementAt(i).CurrentValue = uiVal;
                            }
                            break;

                        default:
                            fields.ElementAt(i).CurrentValue = currentValue;
                            break;
                    }                    
                }

                #endregion
            }

            return iRet;
        }


        /// <summary>
        /// BCD にも対応した拡張メソッドです。
        /// </summary>
        public static int WriteDeviceRandom(this ActProg actProg, Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat[] fields)
        {
            for (int f = 0; f < fields.Count(); f++)
            {
                switch (fields.ElementAt(f).DeviceFormatType)
                {
                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD16:
                        {
                            ushort curVal = (ushort)fields.ElementAt(f).CurrentValue;
                            ushort ushVal = 0;

                            for (int digit = 0; digit < 4; digit++)
                            {                                
                                // 今回の桁数
                                ushort digitVal = 
                                    (ushort)((curVal % (ushort)Math.Pow(10, digit + 1)) / (ushort)Math.Pow(10, digit));

                                // 4 バイト間隔で桁の数値を入れていく
                                ushVal += (ushort)( digitVal << (4 * digit));

                                // これ以上計算する必要が無ければループを抜ける
                                if (curVal < (ushort)Math.Pow(10, digit + 1)) break;
                            }

                            fields.ElementAt(f).CurrentValue = ushVal;
                        }
                        break;

                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD32:
                        {
                            uint curVal = (uint)fields.ElementAt(f).CurrentValue;
                            uint uiVal = 0;

                            for (int digit = 0; digit < 8; digit++)
                            {
                                // 今回の桁数
                                uint digitVal =
                                    (uint)( (curVal % Math.Pow(10, digit + 1)) / Math.Pow(10, digit));

                                // 4 バイト間隔で桁の数値を入れていく
                                uiVal += (uint)(digitVal << (4 * digit));

                                // これ以上計算する必要が無ければループを抜ける
                                if (curVal < (uint)Math.Pow(10, digit + 1)) break;
                            }

                            fields.ElementAt(f).CurrentValue = uiVal;                           
                        }
                        break;
                    default:
                        break;
                }
            }

            int iRet = actProg.WriteDeviceRandom(
                fields.Select(x => x.DeviceName),
                fields.Select(x => x.CurrentValue)
                );

            return iRet;
        }
    }
}
