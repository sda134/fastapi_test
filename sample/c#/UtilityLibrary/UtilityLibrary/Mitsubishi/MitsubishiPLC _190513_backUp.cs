using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using Mtec.UtilityLibrary.Mitsubishi.FxDeviceExtensionMethod;

namespace Mtec.UtilityLibrary.Mitsubishi
{
    #region region - class


    [System.Serializable]
    public class DeviceFieldFormat : ICloneable, IEquatable<DeviceFieldFormat>, IComparable<DeviceFieldFormat>
    {
        public string DeviceName { get; set; }

        public Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType DeviceFormatType { get; set; }

        public string Detail { get; set; }

        public object CurrentValue { get; set; }

        #region region - object 標準メソッドの override

        // 19.04.19 [ver 0.3.3.0]デバッグ時に見やすくする為に追加
        public override string ToString() => string.Format("{0}:{1}", this.DeviceName, this.CurrentValue);


        // Equal メソッドを継承した場合は GetHashCode も継承する必要がある
        public override int GetHashCode()
        {
            if (FxDevice.TryParse(this.DeviceName, out FxDevice fxDev))
                return fxDev.GetHashCode();
            else
                // 無効なキャスト
                throw new InvalidCastException();            
        }

        #endregion

        //  19.04.19 [ver 0.3.3.0] 追加
        #region region - ICloneable 継承の為のメソッド

        public Object Clone() => this.MemberwiseClone();

        #endregion

        #region region - IEquatable 継承の為のメソッド

        bool IEquatable<DeviceFieldFormat>.Equals(DeviceFieldFormat arg)
        {
            if (arg == null)
                return false;

            return (arg.DeviceName == this.DeviceName);
        }



        #endregion

        #region region - IComparable 継承の為のメソッド

        public int CompareTo(DeviceFieldFormat arg)
        {
            /*
             * •自分より与えられた値のほうが小さいなら-1以下を
             * •自分より与えられた値のほうが大きいなら1以上を
             * •自分の値と与えられた値が等しいなら0を返す。
             */

            if (arg.GetHashCode() < this.GetHashCode())
                return -1;

            else if (this.GetHashCode() < arg.GetHashCode())
                return 1;
            else
                return 0;
        }

        #endregion

    }

    #endregion


    #region  region - enum

    public enum DeviceFormatType : int
    {
        Signed16,
        Signed32,
        Float,
        Bit,
        Unsigned16,
        Unsigned32,
        BCD16,
        BCD32
    }


    #endregion

}


// FxDevice
namespace Mtec.UtilityLibrary.Mitsubishi
{
    // 19.05.03 FxDevice という名前になっているからFxでOK ⇒ 16進数表記はしない
    // MC プロトコルからのコピー
    public class FxDevice : IEquatable<FxDevice>, IComparable<FxDevice>
    {
        public FxDeviceType DeviceType { get; set; }

        // Qシリーズだと Dが 18432 くらいまである
        public int DeviceNumber { get; set; }



        // 19.04.19 [ver 0.3.3.0]　以下の override メソッドを追加

        #region region - object 標準メソッドの override

        public override int GetHashCode()
        {
            if (this == null)
                return 0;

            return
                ((int)this.DeviceType * 1000000)
                + this.DeviceNumber;
        }

        public override string ToString()
        {
            // デバイスによって8進数文字にしたりする必要がある
            string.Format("{0}{1}", this.DeviceType.ToDeviceLetter(), this.DeviceNumber);

            return null;
        }


        #endregion


        #region region - IComparable 継承の為のメソッド

        public static bool operator ==(FxDevice arg1, FxDevice arg2)
        {
            // null 1対策
            
            return
                   arg1?.DeviceType == arg2?.DeviceType &&
                   arg1?.DeviceNumber == arg2?.DeviceNumber;
        }
        public static bool operator !=(FxDevice arg1, FxDevice arg2) => !(arg1 == arg2);


        public int CompareTo(FxDevice arg)
        {
            /*
             * •自分より与えられた値のほうが小さいなら-1以下を
             * •自分より与えられた値のほうが大きいなら1以上を
             * •自分の値と与えられた値が等しいなら0を返す。
             */

            if (arg.GetHashCode() < this.GetHashCode())
                return -1;

            else if (this.GetHashCode() < arg.GetHashCode())
                return 1;
            else
                return 0;
        }

        #endregion


        #region region - IEquatable 継承の為のメソッド


       

        bool IEquatable<FxDevice>.Equals(FxDevice arg)
        {
            if (arg == null)
                return false;

            return
                   this.DeviceType == arg.DeviceType &&
                   this.DeviceNumber == arg.DeviceNumber
                   ;
        }

        #endregion


        #region region - static methods

        public static bool TryParse(string arg, out FxDevice result)
        {
            result = null;

            // 小文字を大文字に変えておく
            arg = arg.ToUpper();

            // 文字列の長い順に検索する必要がある 17.12.25
            var devList = from FxDeviceType type in Enum.GetValues(typeof(FxDeviceType))
                          let devStr = type.ToDeviceLetter()
                          orderby devStr.Length
                          select new { devType = type, devString = devStr };

            foreach (var dev in devList)
            {
                if (arg.StartsWith(dev.devString))
                {
                    int devNum;
                    bool isNumeric;
                    string numString = arg.Substring(dev.devString.Length, arg.Length - dev.devString.Length);

                    // 19.04.26 この処理、問題あり temporary
                    // デバイス番号表記が 16進数のものと、10進数のものがあるので処理を分ける
                    switch (dev.devType)
                    {
                        #region region

                        case FxDeviceType.InputSignal:  // FX5, FX3 では8進数   それ以外は16進数
                        case FxDeviceType.OutputSignal: // FX5, FX3 では8進数   それ以外は16進数
                        case FxDeviceType.LinqRelay:
                        case FxDeviceType.LinqRegister:
                        case FxDeviceType.LinqSpecialRelay:
                        case FxDeviceType.LinqSpecialRegister:
                        case FxDeviceType.DirectAccessInput:
                        case FxDeviceType.DirectAccessOutput:
                        case FxDeviceType.FileRegister_ZR:
                            isNumeric = int.TryParse(numString, System.Globalization.NumberStyles.HexNumber, null, out devNum);
                            break;

                        default:
                            isNumeric = int.TryParse(numString, out devNum);
                            break;

                            #endregion
                    }


                    // if (int.TryParse(numString, out devNum))
                    // ↓ デバイス番号は16進数で表す事を忘れていたので修正 18.01.26
                    //if (int.TryParse(numString, System.Globalization.NumberStyles.HexNumber, null, out devNum))
                    // ↓ 10進数のものと16進数のものが混在しているらしいので更に修正 18.01.26
                    if (isNumeric)
                    {
                        result = new FxDevice();
                        result.DeviceType = dev.devType;
                        result.DeviceNumber = devNum;

                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

    }



    // MCプロトコルと同じ
    public enum FxDeviceType : byte
    {
        /// <summary>
        /// 特殊リレー [SM]
        /// </summary>
        SpecialRelay = 0x91,

        /// <summary>
        /// 特殊レジスタ [SD]
        /// </summary>
        SpecialRegister = 0xA9,

        /// <summary>
        /// 入力 [X]
        /// </summary>
        InputSignal = 0x9C,

        /// <summary>
        /// 出力 [Y]
        /// </summary>
        OutputSignal = 0x9D,

        /// <summary>
        /// 内部リレー [M]
        /// </summary>
        InnerRelay = 0x90,

        /// <summary>
        /// ラッチリレー [L]
        /// </summary>
        LatchRelay = 0x92,

        /// <summary>
        /// アナンシェータ [F]
        /// </summary>
        Annunciator = 0x93,

        /// <summary>
        /// エッジリレー [V]
        /// </summary>
        EdgeRelay = 0x94,

        /// <summary>
        /// リンクリレー [B]
        /// </summary>
        LinqRelay = 0xA0,

        /// <summary>
        /// データレジスタ [D]
        /// </summary>
        DataRegister = 0xA8,

        /// <summary>
        /// リンクレジスタ [W]
        /// </summary>
        LinqRegister = 0xB4,

        /// <summary>
        /// タイマー接点 [TS]
        /// </summary>
        Timer_Contact = 0xC1,

        /// <summary>
        /// タイマーコイル [TC]
        /// </summary>
        Timer_Coil = 0xC0,

        /// <summary>
        /// タイマー現在値 [TN]
        /// </summary>
        Timer_Value = 0xC2,

        /// <summary>
        /// ロングタイマー接点 [LTS]
        /// </summary>
        LongTimer_Contact = 0x51,

        /// <summary>
        /// ロングタイマーコイル [LTC]
        /// </summary>
        LongTimer_Coil = 0x50,

        /// <summary>
        /// ロングタイマー接点 [LTN]
        /// </summary>
        LongTimer_Value = 0x52,

        /// <summary>
        /// 積算タイマー接点 [STS]
        /// </summary>
        IntegratedTimerContact = 0xC7,

        /// <summary>
        /// 積算タイマーコイル [STC]
        /// </summary>
        IntegratedTimer_Coil = 0xC6,

        /// <summary>
        /// 積算タイマー現在値 [STN]
        /// </summary>
        IntegratedTimer_Value = 0xC8,

        /// <summary>
        /// ロング積算タイマー接点 [LSTS]
        /// </summary>
        LongIntegratedTimer_Contact = 0x59,

        /// <summary>
        /// ロング積算タイマーコイル [LSTC]
        /// </summary>
        LongIntegratedTimer_Coil = 0x58,

        /// <summary>
        /// ロング積算タイマー接点 [LSTN]
        /// </summary>
        LongIntegratedTimer_Value = 0x5A,

        /// <summary>
        /// カウンタ接点 [CS]
        /// </summary>
        Counter_Contact = 0xC4,

        /// <summary>
        /// カウンタコイル [CC]
        /// </summary>
        Counter_Coil = 0xC3,

        /// <summary>
        /// カウンタ現在値 [CN]
        /// </summary>
        Counter_Value = 0xC5,

        /// <summary>
        /// ロングカウンタ接点 [LCS]
        /// </summary>
        LongCounter_Contact = 0x55,

        /// <summary>
        /// ロングカウンタコイル [LCC]
        /// </summary>
        LongCounter_Coil = 0x54,

        /// <summary>
        /// ロングカウンタ現在値 [LCN]
        /// </summary>
        LongCounter_Value = 0x56,

        /// <summary>
        /// リンク特殊リレー [SB]
        /// </summary>
        LinqSpecialRelay = 0xA1,

        /// <summary>
        /// リンク特殊レジスタ [SW]
        /// </summary>
        LinqSpecialRegister = 0xB5,

        /// <summary>
        /// ダイレクトアクセス入力 [DX]
        /// </summary>
        DirectAccessInput = 0xA2,

        /// <summary>
        /// ダイレクトアクセス出力 [DY]
        /// </summary>
        DirectAccessOutput = 0xA3,

        /// <summary>
        /// インデックスレジスタ [Z]
        /// </summary>
        IndexRegister = 0xCC,

        /// <summary>
        /// ロングインデックスレジスタ [LZ]
        /// </summary>
        LongIndexRegister = 0x62,

        /// <summary>
        /// ファイルレジスタ：ブロック切り替え方式 [R]
        /// </summary>
        FileRegister = 0xAF,

        /// <summary>
        /// ファイルレジスタ：連番アクセス方式 [ZR]
        /// </summary>
        FileRegister_ZR = 0xB0,     //なぜ２つある？ 18.01.16

        /// <summary>
        /// リフレッシュデータレジスタ [RD]
        /// </summary>
        RefreshDataRegister = 0x2C,

        /// <summary>
        /// バッファ [Un\G]
        /// </summary>
        UnitBuffer = 0xAB,

        /// <summary>
        /// バッファ [Un\HG]
        /// </summary>
        UnitBufferHG = 0x2E,    // よくわからない 17.12.25

    }

    /// <summary>
    /// 拡張メソッド
    /// </summary>
}
namespace Mtec.UtilityLibrary.Mitsubishi.FxDeviceExtensionMethod
{
    public static partial class DeviceTypeExtensionMethod
    {
        public static string ToDeviceLetter(this FxDeviceType value)
        {
            switch (value)
            {
                case FxDeviceType.SpecialRelay: return "SM";
                case FxDeviceType.SpecialRegister: return "SD";
                case FxDeviceType.InputSignal: return "X";
                case FxDeviceType.OutputSignal: return "Y";
                case FxDeviceType.InnerRelay: return "M";
                case FxDeviceType.LatchRelay: return "L";
                case FxDeviceType.Annunciator: return "F";
                case FxDeviceType.EdgeRelay: return "V";
                case FxDeviceType.LinqRelay: return "B";
                case FxDeviceType.DataRegister: return "D";
                case FxDeviceType.LinqRegister: return "W";
                case FxDeviceType.Timer_Contact: return "TS";
                case FxDeviceType.Timer_Coil: return "TC";
                case FxDeviceType.Timer_Value: return "TN";
                case FxDeviceType.LongTimer_Contact: return "LTS";
                case FxDeviceType.LongTimer_Coil: return "LTC";
                case FxDeviceType.LongTimer_Value: return "LTN";
                case FxDeviceType.IntegratedTimerContact: return "STS";
                case FxDeviceType.IntegratedTimer_Coil: return "STC";
                case FxDeviceType.IntegratedTimer_Value: return "STN";
                case FxDeviceType.LongIntegratedTimer_Contact: return "LSTS";
                case FxDeviceType.LongIntegratedTimer_Coil: return "LSTC";
                case FxDeviceType.LongIntegratedTimer_Value: return "LSTN";
                case FxDeviceType.Counter_Contact: return "CS";
                case FxDeviceType.Counter_Coil: return "CC";
                case FxDeviceType.Counter_Value: return "CN";
                case FxDeviceType.LongCounter_Contact: return "LCS";
                case FxDeviceType.LongCounter_Coil: return "LCC";
                case FxDeviceType.LongCounter_Value: return "LCN";
                case FxDeviceType.LinqSpecialRelay: return "SB";
                case FxDeviceType.LinqSpecialRegister: return "SW";
                case FxDeviceType.DirectAccessInput: return "DX";
                case FxDeviceType.DirectAccessOutput: return "DY";
                case FxDeviceType.IndexRegister: return "Z";
                case FxDeviceType.LongIndexRegister: return "LZ";
                case FxDeviceType.FileRegister: return "R";  //???
                case FxDeviceType.FileRegister_ZR: return "ZR";  //???
                case FxDeviceType.RefreshDataRegister: return "RD";  //???
                case FxDeviceType.UnitBuffer: return "G";
                case FxDeviceType.UnitBufferHG: return "HG";  //???

                default:
                    return null;
            }
        }

        public static string ToStringFromEnum(this FxDeviceType value)
        {
            switch (value)
            {
                case FxDeviceType.SpecialRelay: return "特殊リレー";
                case FxDeviceType.SpecialRegister: return "特殊レジスタ";
                case FxDeviceType.InputSignal: return "入力";
                case FxDeviceType.OutputSignal: return "出力";
                case FxDeviceType.InnerRelay: return "内部リレー";
                case FxDeviceType.LatchRelay: return "ラッチリレー";
                case FxDeviceType.Annunciator: return "アナンシェータ";
                case FxDeviceType.EdgeRelay: return "エッジリレー";
                case FxDeviceType.DataRegister: return "データレジスタ";
                case FxDeviceType.LinqRegister: return "リンクレジスタ";
                case FxDeviceType.Timer_Contact: return "タイマ接点";
                case FxDeviceType.Timer_Coil: return "タイマコイル";
                case FxDeviceType.Timer_Value: return "タイマ現在値";
                case FxDeviceType.LongTimer_Contact: return "ロングタイマ接点";
                case FxDeviceType.LongTimer_Coil: return "ロングタイマコイル";
                case FxDeviceType.LongTimer_Value: return "ロングタイマ現在値";
                case FxDeviceType.IntegratedTimerContact: return "積算タイマー接点";
                case FxDeviceType.IntegratedTimer_Coil: return "積算タイマーコイル";
                case FxDeviceType.IntegratedTimer_Value: return "積算タイマー現在値";
                case FxDeviceType.LongIntegratedTimer_Contact: return "積算ロングタイマー接点";
                case FxDeviceType.LongIntegratedTimer_Coil: return "積算ロングタイマーコイル";
                case FxDeviceType.LongIntegratedTimer_Value: return "積算ロングタイマー現在値";
                case FxDeviceType.Counter_Contact: return "カウンタ接点";
                case FxDeviceType.Counter_Coil: return "カウンタコイル";
                case FxDeviceType.Counter_Value: return "カウンタ現在値";
                case FxDeviceType.LongCounter_Contact: return "ロングカウンタ接点";
                case FxDeviceType.LongCounter_Coil: return "ロングカウンタコイル";
                case FxDeviceType.LongCounter_Value: return "ロングカウンタ現在値";
                case FxDeviceType.LinqSpecialRelay: return "リンク特殊リレー";
                case FxDeviceType.LinqSpecialRegister: return "リンク特殊レジスタ";
                case FxDeviceType.IndexRegister: return "インデックスレジスタ";
                case FxDeviceType.LongIndexRegister: return "ロングインデックスレジスタ";
                case FxDeviceType.FileRegister: return "ファイルレジスタ：ブロック切り替え方式";
                case FxDeviceType.FileRegister_ZR: return "ファイルレジスタ：連番アクセス方式";
                case FxDeviceType.RefreshDataRegister: return "リフレッシュデータレジスタ";
                case FxDeviceType.UnitBuffer: return "ユニットアクセスバッファ";
                case FxDeviceType.UnitBufferHG: return "ユニットアクセスバッファ(HG)";    //???

                default:
                    return null;
            }
        }
    }
}


// BCD 未完成 18.12.28
namespace Mtec.UtilityLibrary.Mitsubishi
{

    //    [CLSCompliant(false)]
    [ComVisible(true)]
    public struct BCD16 // : IComparable, IFormattable, IConvertible, IComparable<BCD16>, IEquatable<BCD16>
    {
        public const Int32 MaxValue = 9999;
        public const Int32 MinValue = 0;

        /* // 中止 18.12.26
        #region region - IConvertible


        double IConvertible.ToDouble(IFormatProvider provider) => Convert.ToDouble((this as IConvertible).ToUInt16(provider));
        float IConvertible.ToSingle(IFormatProvider provider) => Convert.ToSingle((this as IConvertible).ToUInt16(provider));
        decimal IConvertible.ToDecimal(IFormatProvider provider) => Convert.ToDecimal((this as IConvertible).ToUInt16(provider));
        byte IConvertible.ToByte(IFormatProvider provider) => Convert.ToByte((this as IConvertible).ToUInt16(provider));
        short IConvertible.ToInt16(IFormatProvider provider) => Convert.ToInt16((this as IConvertible).ToUInt16(provider));
        int IConvertible.ToInt32(IFormatProvider provider) => Convert.ToInt32((this as IConvertible).ToUInt16(provider));
        long IConvertible.ToInt64(IFormatProvider provider) => Convert.ToInt32((this as IConvertible).ToUInt16(provider));
        sbyte IConvertible.ToSByte(IFormatProvider provider) => Convert.ToSByte((this as IConvertible).ToUInt16(provider));
        ushort IConvertible.ToUInt16(IFormatProvider provider) => Convert.ToUInt16((this as IConvertible).ToUInt16(provider));
        uint IConvertible.ToUInt32(IFormatProvider provider) => Convert.ToUInt32((this as IConvertible).ToUInt16(provider));
        ulong IConvertible.ToUInt64(IFormatProvider provider) => Convert.ToUInt64((this as IConvertible).ToUInt16(provider));
        bool IConvertible.ToBoolean(IFormatProvider provider) => Convert.ToBoolean((this as IConvertible).ToUInt16(provider));
        char IConvertible.ToChar(IFormatProvider provider) => Convert.ToChar((this as IConvertible).ToUInt16(provider));
        DateTime IConvertible.ToDateTime(IFormatProvider provider) => Convert.ToDateTime((this as IConvertible).ToUInt16(provider));
        string IConvertible.ToString(IFormatProvider provider) => Convert.ToString((this as IConvertible).ToUInt16(provider));
        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => Convert.ToChar((this as IConvertible).ToUInt16(provider));


        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }



        #endregion


        #region region - IEquatable<BCD16>

        public bool Equals(BCD16 other)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region region - IFormattable


        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region region - IComparable

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region region - IComparable<BCD16>

        public int CompareTo(BCD16 other)
        {
            throw new NotImplementedException();
        }

        #endregion

        */

    }

    [ComVisible(true)]
    public struct BCD32 //: IComparable, IFormattable, IConvertible, IComparable<BCD32>, IEquatable<BCD32>
    {
        public const Int32 MaxValue = 99999999;
        public const Int32 MinValue = 0;
    }



    public class BCD
    {
        public static int Int32ToBCD(int value)
        {
            return 0;
        }

        public static int BCDToInt32(int value)
        {
            return 0;
        }
    }
}


// DeviceFormat D20　の次のデバイス(D21) を計算で算出したりする為に汎用する
//namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol

    //namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol からのコピー 
    // 18.12.07 結構使うのでファイルの場所を変更　但し名前空間は MCProtocol のままとする
    // → やはり逐一参照追加する事にする。ただし独立ファイルとする 18.12.28
    //public class MCDevice
   


