using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.Internal.Mitsubishi.MCProtocol;

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent
{
    public class MXComponentOperator
    {

        #region region - private member


        private ActProg _actProg;

        private System.Timers.Timer _timer;

        // Fieldリストへのアクセスを制限する Semaphore
        private System.Threading.SemaphoreSlim _semaphore;


        #endregion


        #region region - public properties

        public double MoniteringIntervalMiliSeconds
        {
            get => this._timer.Interval;
            set => this._timer.Interval = value;
        }


        public bool IsLogging
        {
            get => this._isLogging;
        }
        private bool _isLogging = false;


        public List<LogerFieldFormat> LoggingDeviceList { get; set; }


        public ActControlSettingFormat ActProgSetting
        {
            set
            {
                if (value == null) value = new ActControlSettingFormat();

                // 標準設定
                this._actProg.ActStationNumber = 255; //通信設定ユーティリティで設定した倫理局番の事 ※Progでは未使用なはずだが、初期値255を入れておかないとエラーになる
                this._actProg.ActPortNumber = 0;     // PC 側のポート番号。 =0 で自動選択
                this._actProg.ActThroughNetworkType = 0x01;

                // 外からの入力を適応
                this._actProg.ActProtocolType = (int)value.ActProtocolType;
                this._actProg.ActCpuType = (int)value.ActCpuType;
                this._actProg.ActUnitType = (int)value.ActUnitType;

                this._actProg.ActHostAddress = value.ActHostAddress;

                this._actProg.ActPortNumber = (int)value.PortNumber;
                this._actProg.ActBaudRate = (int)value.BaudRate;
                this._actProg.ActDataBits = (int)value.DataBits;
                this._actProg.ActParity = (int)value.ParityBits;
                this._actProg.ActControl = (int)value.ActControl;
            }
        }


        #endregion


        #region region - event

        public event MXLogOperatorEventHandler EventCallback = (obj, e) => { };

        #endregion



        public MXComponentOperator()
        {
            this._semaphore = new System.Threading.SemaphoreSlim(1, 1);
            this._actProg = new ActProg();
            this._timer = new System.Timers.Timer();
        }




        public bool StartLogging()
        {
            this._isLogging = true;
            return true;
        }
        public bool StopLogging()
        {
            this._isLogging = false;
            return true;
        }

        public bool Open()
        {
            int ret = this._actProg.Open();

            if (ret != 0)
            {
                EventCallback.Invoke(this, new MXLogOperatorEventArgs
                {
                    Message ="Open に失敗しました",
                    ActProgReturnCode = ret,
                });

                return false;
            }
            else
            {
                this._timer.Start();

                return true;
            }
        }


        public bool Close()
        {
            int ret = this._actProg.Close();
            if (ret != 0)
            {
                EventCallback.Invoke(this, new MXLogOperatorEventArgs
                {
                    Message = "Close に失敗しました",
                    ActProgReturnCode = ret,
                });

                return false;
            }

            this._timer.Stop();

            return true;
        }



        public bool ReadData(ref List<LogerFieldFormat> targetDeviceList)
        {
            int iRet = this._actProg.ReadDeviceRandom(
                targetDeviceList.Select(x => x.DeviceName ?? "D0"),
                targetDeviceList.Select(x =>
                {
                    switch (x.TypeOfDevice)
                    {
                        case Internal.Mitsubishi.TypeOfDevice.Signed32:
                            return typeof(Int32);

                        case Internal.Mitsubishi.TypeOfDevice.Float:
                            return typeof(Single);

                        case Internal.Mitsubishi.TypeOfDevice.Unsigned16:
                        case Internal.Mitsubishi.TypeOfDevice.BCD16:
                            return typeof(UInt16);

                        case Internal.Mitsubishi.TypeOfDevice.Unsigned32:
                        case Internal.Mitsubishi.TypeOfDevice.BCD32:
                            return typeof(UInt32);

                        default: return typeof(Int16);
                    }
                }),
                out object[] values);

            if (iRet == 0)
            {
                for (int i = 0; i < targetDeviceList.Count; i++)
                {
                    targetDeviceList[i].CurrentValue = i < values.Count() ? values[i] : null;

#if DEBUG
                    if (values[i] != null)
                    {
                        byte[] byteArray;

                        switch (values[i])
                        {
                            case Int32 iVal: byteArray = BitConverter.GetBytes((Int32)values[i]); break;
                            case UInt16 iVal: byteArray = BitConverter.GetBytes((UInt16)values[i]); break;
                            case UInt32 iVal: byteArray = BitConverter.GetBytes((UInt32)values[i]); break;
                            case Single iVal: byteArray = BitConverter.GetBytes((Single)values[i]); break;
                            default: byteArray = BitConverter.GetBytes((Int16)values[i]); break;
                        }

                        Console.WriteLine("Device Name: {0} - Value:{1}", targetDeviceList[i].DeviceName, string.Join(",", byteArray.Select(x => x.ToString("x"))));
                    }
#endif
                }
            }
            else
            {
                this.EventCallback.Invoke(this, new MXLogOperatorEventArgs
                {
                    ActProgReturnCode = iRet,
                });
            }

            return iRet == 0;
        }

        public bool WriteData(List<LogerFieldFormat> targetDeviceList)
        {
            int iRet = this._actProg.WriteDeviceRandom(
                targetDeviceList.Select(x => x.DeviceName),
                targetDeviceList.Select(x => x.CurrentValue)
                );

            return iRet == 0;
        }
    }


#region region - Model


    [System.Serializable]
    public class LogerFieldFormat
    {
        
        public Mtec.Internal.Mitsubishi.TypeOfDevice TypeOfDevice { get; set; }


        public string DeviceName { get; set; }


        public object CurrentValue { get; set; }

        public string Detail { get; set; }

    }


    public class LogValueFormat
    {
        public string DeviceName { get; set; }
        public object Value { get; set; }
    }


#endregion


#region region - delegate

    public class MXLogOperatorEventArgs : EventArgs
    {
        public Exception Ex { get; set; }

        public string Message { get; set; }

        public int ActProgReturnCode { get; set; }

    }

    [System.Runtime.InteropServices.ComVisible(true)]
    public delegate void MXLogOperatorEventHandler(object sender, MXLogOperatorEventArgs e);


    public class MXLogOperatorMonitoringDeviceValueChangedEventArgs : EventArgs
    {
        public LogerFieldFormat ChangedDevice { get; set; }
    }


    [System.Runtime.InteropServices.ComVisible(true)]
    public delegate void MXLogOperatorMonitoringDeviceValueChangedEventHandler(object sender, MXLogOperatorMonitoringDeviceValueChangedEventArgs e);




#endregion

}
