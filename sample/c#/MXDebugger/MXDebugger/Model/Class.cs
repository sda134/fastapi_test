using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{

    [System.Serializable]
    public class ReverseDeviceFormat
    {
        public string DeviceName { get; set; }

        public ReverseType ReverseType { get; set; }

        public Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType DeviceFormatType { get; set; }


        public string Detail { get; set; }


        public bool UseInTable { get; set; } = false;

        // 19.01.29 追加
        // public bool IsUnderSurveillance { get; set; } = false;

        // 19.01.29 追加
        public List<PredicationFieldFormat> PredicationList { get; set; }

    }


    [System.Serializable]
    public class PredicationFormat
    {

    }


    [System.Serializable]
    public class PredicationFieldFormat
    {
        public string DeviceName { get; set; }

        public Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType DeviceFormatType { get; set; }


        // 比較の仕方
        public CompareType CompareType { get; set; }

        // 比較する値
        public object Value { get; set; }

        // 条件式の追加の仕方
        public ConditionType ConditionType { get; set; }
    }


    #region region - enum


    public enum ReverseType : int
    {
        //Table,    // 19.01.28 テーブルはチェックボックスなどで管理する
        Button,
        CheckBox,
        TextBox,    // 19.01.24 追加
        Correspond, // 19.01.29 追加
    }


    public enum CompareType
    {
        GreaterThan,        // >
        GreaterThanOrEqual, // >=
        LessThan,           // <
        LessThanOrEqual,    // <=
        Equal,              // ==
        NotEqual,           // !=
    }

    public enum ConditionType
    {
        And,
        Or,
    }


    #endregion


    [System.Serializable]
    public class LogGroupFormat
    {
        public string GroupName { get; set; }

        public List<ReverseDeviceFormat> FieldList { get; set; }
    }


    #region region - エンコーダのエミュレータ関連

        
    [System.Serializable]
    public class EncoderParameterFormat
    {        
        public string FirstDevice { get; set; } = "X0";

        public int SpeedOfRevolution { get; set; } = 60;

        public bool IsCounterClockwise { get; set; } = false;


        public EncoderFormat EncoderFormat { get; set; } = EncoderFormat.GrayCode;

        public EncoderBitsCount EncoderBitsCount { get; set; } = EncoderBitsCount._8Bits;

        public EncorderDevNumberNotation NumberNotation { get; set; } = EncorderDevNumberNotation.Octal;


        public List<PredicationFieldFormat> Trigger { get; set; }

    }


    public enum EncoderFormat
    {
        Binary,
        GrayCode,
        GrayCode_Excess76,
        GrayCode_Excess152,
    }

    public enum EncoderBitsCount
    {
        _7Bits,
        _8Bits,
        _9Bits,
    }
    public enum EncorderDevNumberNotation
    {
        Octal,
        Decimal,
        Hexadecimal,
    }

    #endregion



    static class ExtensionMethod_Mitsubishi_Enum
    {
        public static string ToStringFromEnum(this EncoderFormat arg)
        {
            switch (arg)
            {
                case EncoderFormat.Binary: return "バイナリ";
                case EncoderFormat.GrayCode: return "グレイコード";
                case EncoderFormat.GrayCode_Excess76: return "76余りグレイコード";
                case EncoderFormat.GrayCode_Excess152: return "152余りグレイコード";
                default: return null;
            }
        }
        public static string ToStringFromEnum(this EncoderBitsCount arg)
        {
            switch (arg)
            {
                case EncoderBitsCount._7Bits: return "7bit";
                case EncoderBitsCount._8Bits: return "8bit";
                case EncoderBitsCount._9Bits: return "9bit";
                default: return null;
            }
        }
        public static string ToStringFromEnum(this EncorderDevNumberNotation arg)
        {
            switch (arg)
            {
                case EncorderDevNumberNotation.Octal: return "8進数";
                case EncorderDevNumberNotation.Decimal: return "10進数";
                case EncorderDevNumberNotation.Hexadecimal: return "16進数";
                default: return null;
            }
        }
    }
}


