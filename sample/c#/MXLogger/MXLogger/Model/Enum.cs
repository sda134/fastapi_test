using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public enum CompareType
    {
        GreaterThan,        // >
        GreaterThanOrEqual, // >=
        LessThan,           // <
        LessThanOrEqual,    // <=
        Equal,              // ==
        NotEqual,           // !=
    }


    public enum TriggerType : int
    {
        Standby,
        UserRecording,
        Trigger
    }


    public static partial class ExtensionMethod
    {
        public static string ToStringFromEnum(this TriggerType arg)
        {
            switch (arg)
            {
                case TriggerType.Standby: return "スタンバイ";
                case TriggerType.UserRecording: return "記録ボタン";
                case TriggerType.Trigger: return "トリガ";
                default: return "";
            }
        }

        public static string ToStringFromEnum(this CompareType value)
        {
            switch (value)
            {
                case CompareType.GreaterThan: return "＞";
               case CompareType.GreaterThanOrEqual: return "≧";
                case CompareType.LessThan: return "＜";
                case CompareType.LessThanOrEqual: return "≦";
                case CompareType.Equal: return "＝";
                case CompareType.NotEqual: return "≠";
                default:
                    return null;
            }
        }
    }
}
