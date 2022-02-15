using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.UtilityLibrary.Mitsubishi;
using Mtec.UtilityLibrary.Mitsubishi.MXComponent;

namespace Mtec.Internal.Mitsubishi.MXLogger
{

    [System.Serializable]
    public class LogGroupFormat
    {
        [System.Xml.Serialization.XmlIgnore]
        public bool Recording { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public DateTime StartDateTime { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public DateTime EndDateTime { get; set; }


        public string GroupName { get; set; }


        public TriggerFormat Trigger { get; set; }


        public TriggerType TriggerType { get; set; }


        public List<UtilityLibrary.Mitsubishi.DeviceFieldFormat> FieldList { get; set; }

    }



    public class TriggerFormat
    {
        public Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType DeviceFormatType { get; set; }

        public string DeviceName { get; set; }

        public object ThresholdValue { get; set; }

        public CompareType CompareType { get; set; }

        /*
        public bool IsTriggered(object currentValue)
        {
            if (this.DeviceFormatType == DeviceFormatType.Bit)
                return ((Int16)currentValue) == 1;
            else
            {
            }
            return true;
        }*/




        public bool IsTriggerOn(object currentValue)
        {
            var currentDec = Convert.ToDecimal(currentValue);
            var thresholdDec = Convert.ToDecimal(this.ThresholdValue);

            switch (this.CompareType)
            {
                case CompareType.Equal: return thresholdDec.Equals(currentDec);
                case CompareType.NotEqual: return !thresholdDec.Equals(currentDec);

                case CompareType.GreaterThan: return currentDec > thresholdDec;
                case CompareType.GreaterThanOrEqual: return currentDec >= thresholdDec;

                case CompareType.LessThan:return currentDec < thresholdDec;
                case CompareType.LessThanOrEqual: return currentDec <= thresholdDec;
                default: return false;
            }
        }
    }
}
