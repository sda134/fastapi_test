using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// MitsubishiPLC の参照が必要

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent
{
    public class MXComponentOperator
    {

        #region region - private member

        private ActProg _actProg;

        private System.Timers.Timer _timer;

        // Fieldリストへのアクセスを制限する Semaphore
        private System.Threading.SemaphoreSlim _semaphore;


        private List<LogRecordFormat> _logRecords;

        private List<LogValueFormat> _monitorDevicesPriviousValues { get; set; }


        #endregion


        #region region - public properties

        public double MoniteringIntervalMiliSeconds
        {
            get => this._timer.Interval;
            set => this._timer.Interval = value;
        }


        public bool IsOpen { get => this._timer.Enabled; }

        public bool IsLogging { get => this._isLogging; }
        private bool _isLogging = false;


        public string CsvFileFullPath { get => this._csvFileDirectoryPath; set => this._csvFileDirectoryPath = value; }
        public string _csvFileDirectoryPath;

        public IEnumerable<DeviceFieldFormat> MonitorDevices
        {
            get => this._monitorDevices;
            set
            {
                Task.Run(async() =>
                {
                    await this._semaphore.WaitAsync();

                    this._monitorDevices = value.ToList();
                    this._monitorDevicesPriviousValues = value.Select(x => new LogValueFormat { DeviceName = x.DeviceName }).ToList();

                    this._semaphore.Release();
                });
            }
        }
        private List<DeviceFieldFormat> _monitorDevices;


        public IEnumerable<DeviceFieldFormat> LoggingDeviceList
        {
            get => this._loggingDeviceList;
            set
            {
                Task.Run(async() =>
                {
                    await this._semaphore.WaitAsync();


                    value = value
                        .OrderBy(x => x.GetHashCode()); // 見やすくする為に追加。　処理的には必要ないので消してもOK

                    value = value                    
                        .Distinct();                    // 19.04.19 追加 重複リストを削除                    

                    this._loggingDeviceList = value.ToList();

                    this._semaphore.Release();
                });
            }
        }
        private List<DeviceFieldFormat> _loggingDeviceList;


        public ActControlSettingFormat ActProgSetting
        {
            set
            {
                // 標準設定
                this._actProg.ActStationNumber = 255; //通信設定ユーティリティで設定した倫理局番の事 ※Progでは未使用なはずだが、初期値255を入れておかないとエラーになる
                this._actProg.ActPortNumber = 0;     // PC 側のポート番号。 =0 で自動選択
                this._actProg.ActThroughNetworkType = 0x01;

                // 19.02.11 追加
                if (value == null) value = new ActControlSettingFormat();

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

                // 19.01.24 追加
                this._actProg.ActTimeOut = value.ActTimeOut;


                // 19.07.19 追加
                this._actProg.ActDestinationPortNumber = value.ActDestinationPortNumber;
                this._actProg.ActDestinationIONumber = value.ActDestinationIONumber;
                this._actProg.ActIONumber = value.ActIONumber;
                this._actProg.ActUnitNumber = value.ActUnitNumber;


                if(value.ActUnitType == ActUnitType.SIMULATOR3)
                {
                    // 20.11.10 実験部分
                    this._actProg.ActStationNumber = 1;
                    this._actProg.ActDestinationPortNumber = 0;
                    this._actProg.ActTargetSimulator = 1;

                    // 不要らしい
                    //this._actProg.ActHostAddress = "";
                    //this._actProg.ActIONumber = 0;
                    //this._actProg.ActPortNumber = 0;
                }
            }
        }


        #endregion


        #region region - delegate

        public event MXComponentOperatorEventHandler EventCallback = (obj, e) => { };

        public event MXComponentOperatorMonitoringDeviceValueChangedEventHandler MonitoringDeviceValueChanged = (obj, e) => { };

       #endregion


        public MXComponentOperator()
        {
            this._semaphore = new System.Threading.SemaphoreSlim(1, 1);
            this._logRecords = new List<LogRecordFormat>();
            this._actProg = new ActProg();
            this._timer = new System.Timers.Timer();
            this._timer.Elapsed += timer_Elapsed;
        }


        #region region - public methods

        public void ClearLogData()
        {
            Task.Run(async () =>
            {
                await this._semaphore.WaitAsync();

                this._logRecords = new List<LogRecordFormat>();

                this._semaphore.Release();
            });
        }

        public void ClearLogData(DateTime dtFrom, DateTime dtTo)
        {
            // 書き込み成功時かつユーザー任意時にログのレコードを消去する
            Task.Run(async () =>
            {
                await this._semaphore.WaitAsync();

                this._logRecords.RemoveAll(x => dtFrom <= x.DateTime && x.DateTime <= dtTo);

                this._semaphore.Release();
            });
        }

        public void ClearLogData(DateTime dtFrom, DateTime dtTo, IEnumerable<string> targetDeviceNames)
        {
            var indexListRec = this._logRecords
                .Select((x, i) => dtFrom <= x.DateTime && x.DateTime <= dtTo ? i : -1)
                .Where(x => x != -1);

            var targetDeviceNameList = targetDeviceNames.ToList();

            Task.Run(async () =>
            {
                await this._semaphore.WaitAsync();

                foreach (int idx_rec in indexListRec)
                {
                    var indexListFld = this._logRecords[idx_rec].LogValues
                    .Select((x, i) => targetDeviceNameList.FindIndex(y => y == x.DeviceName))    
                    .Where(x => x != -1);

                    foreach (int idx_fld in indexListFld)
                    {
                        this._logRecords[idx_rec].LogValues[idx_fld].Value = null;
                    }
                }
               
                this._semaphore.Release();

                throw new NotImplementedException();
            });
        }

        public void GetLogRecords(IEnumerable<string> targetDeviceNameList, out LogRecordFormat[] records, DateTime dtFrom, DateTime dtTo)
        {
            records = this._logRecords
                .Where(x => dtFrom <= x.DateTime && x.DateTime <= dtTo)
                .Select(x => new LogRecordFormat
                {
                    DateTime = x.DateTime,
                    LogValues = (from string dev in targetDeviceNameList
                                 select x.LogValues.Find(y => y.DeviceName == dev) ?? new LogValueFormat()).ToList(),
                }).ToArray();

            // フィールドデータがすべて null なレコードは削除
        }

        public int ExportToCSVFile(IEnumerable<DeviceFieldFormat> targetDeviceNameList, DateTime dtFrom, DateTime dtTo)
        {
            // 入力値確認            
            if (dtTo == DateTime.MinValue)
                dtTo = DateTime.Now;

            // 指定時間内の対象ログを抽出
            var targetLogs = this._logRecords?.FindAll(x => dtFrom <= x.DateTime && x.DateTime <= dtTo) ?? new List<LogRecordFormat>();

            // 対象ログが無ければ処理を中止
            if (targetLogs.Count < 1) return 0;
            
            // 重複を省いたデバイス名リストを作成する
            var distinctDeviceList = 
                targetDeviceNameList.Distinct()
            //    .OrderBy(x => x.GetHashCode())  // 19.04.19 追加 → 19.04.29 ここで 並び替えるのは微妙
                .Where(x => this._logRecords.FindIndex(y => y.LogValues.FindIndex(z => z.DeviceName == x.DeviceName) != -1) !=-1);


            #region region - ファイル名、ディレクトリ処理
            
            // .csv の拡張子をつける
            if (System.IO.Path.GetExtension(this._csvFileDirectoryPath).ToUpper() != ".CSV")
            {
                // 今の拡張子無しのファイル名
                string fn = System.IO.Path.GetFileNameWithoutExtension(this._csvFileDirectoryPath);
                string dir = System.IO.Path.GetDirectoryName(this._csvFileDirectoryPath);

                this._csvFileDirectoryPath = dir + "\\" + fn + ".csv";
            }

            // フォルダが無ければ作成
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(this._csvFileDirectoryPath)))
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(this._csvFileDirectoryPath));

            #endregion


            try
            {
                // csvファイルへの書き込み
                var enc = System.Text.Encoding.GetEncoding("utf-8");

                using (var sr = new System.IO.StreamWriter(this._csvFileDirectoryPath, false, enc))
                {
                    // 列ヘッダ１行目を記載
                    string clmHeader_1 = string.Format("{0},{1}", ""/*日時ヘッダ*/ , string.Join(",", distinctDeviceList.Select(x => x.DeviceName)));
                    sr.Write(clmHeader_1 + "\r\n");
                    
                    // 列ヘッダ２行目を記載
                    string clmHeader_2 = string.Format("{0},{1}", "time" , string.Join(",", distinctDeviceList.Select(x => x.Detail)));
                    sr.Write(clmHeader_2 + "\r\n");


                    // 対象データをCSVに保存
                    foreach (var rec in targetLogs)
                    {
                        // 対象デバイスの値のリスト
                        var valueArray = from dev in distinctDeviceList
                                         select rec.LogValues.Find(x => x.DeviceName == dev.DeviceName)?.Value;

                        int count = valueArray.Where(x => x != null).Count();

                        // 対象データの無いレコードは消去
                        if (valueArray.Where(x => x != null).Count() < 1) continue;

                        string strRecord = string.Format("{0:HH:mm:ss.ff},{1}",
                            rec.DateTime,
                            string.Join(",", valueArray)
                            );

                        sr.Write(strRecord + "\r\n");
                    }
                }

                
            }
            catch (Exception)
            {
                return -1;
            }

            return targetLogs.Count;
        }

        public int ExportToCSVFile(IEnumerable<DeviceFieldFormat> targetDeviceNameList)
        {
            return ExportToCSVFile(targetDeviceNameList, DateTime.MinValue, DateTime.Now);
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
                string errorDetail = Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret) ?? "0x" +ret.ToString("x8");

                EventCallback.Invoke(this, new MXLogOperatorEventArgs
                {
                    Message = string.Format("Open に失敗しました：{0}", errorDetail),
                });

                return false;
            }
            else
            {
                this._monitorDevicesPriviousValues = new List<LogValueFormat>();
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

        public bool ReadData(ref List<DeviceFieldFormat> targetDeviceList)
        {

            int iRet = this._actProg.ReadDeviceRandom(
                //targetDeviceList.Select(x => x.DeviceName ?? "D0"),
                // ↓変更
                targetDeviceList.Where(x => !string.IsNullOrWhiteSpace( x.DeviceName)).Select(x => x.DeviceName),
                targetDeviceList.Select(x =>
                {
                    switch (x.DeviceFormatType)
                    {
                        case DeviceFormatType.Signed32: return typeof(Int32);
                        case DeviceFormatType.Unsigned16: return typeof(UInt16);    // ushort
                        case DeviceFormatType.Unsigned32: return typeof(UInt32);    // uint
                        case DeviceFormatType.Float: return typeof(Single);
                        default: return typeof(Int16);
                    }
                }),
                out object[] values);

            if (iRet == 0)
            {
                for (int i = 0; i < targetDeviceList.Count; i++)
                {
                    targetDeviceList[i].CurrentValue = i < values.Count() ? values[i] : null;
                }
            }
            else
            {
#if DEBUG
                Console.WriteLine("code:{0}", iRet);
#endif
            }

            return iRet == 0;
        }

        public bool WriteData(List<DeviceFieldFormat> targetDeviceList)
        {
            int iRet = this._actProg.WriteDeviceRandom(
                targetDeviceList.Select(x => x.DeviceName),
                targetDeviceList.Select(x => x.CurrentValue)
                );


            if (iRet != 0) this.EventCallback(this, new MXLogOperatorEventArgs
            {
                ActProgReturnCode = iRet,
                Message = "デバイス書き込み失敗",
            });            

            return iRet == 0;
        }



        #endregion


        #region region - public methods : ActProg

        // もともとの三菱のライブラリで処理
        public int SetDevice(string DeviceName, short shData) => this._actProg.SetDevice2(DeviceName, shData);

        // int32 のオーバーライド版（ActProg）で処理
        public int SetDevice(string DeviceName, int iData) => this._actProg.SetDevice(DeviceName, iData);

        // int16 に変換して、三菱のライブラリで処理
        public int SetDevice(string DeviceName, bool bData) => this._actProg.SetDevice2(DeviceName, bData ? (short)1 : (short)0);

        // ActProg の拡張メソッドで処理
        public int SetDevice(string DeviceName, float fData) => this._actProg.SetDevice(DeviceName, fData);


        // バイト配列のオーバーロード版（ActProg）で処理
        public int SetDevice(string DeviceName, byte[] arg) => this._actProg.SetDevice(DeviceName, arg);



        public int GetDevice(string DeviceName, out float fData) => this._actProg.GetDevice(DeviceName, out fData);

        public int GetDevice(string DeviceName, out int iData) => this._actProg.GetDevice(DeviceName, out iData);
        public int GetDevice(string DeviceName, out uint uiData) => this._actProg.GetDevice(DeviceName, out uiData);

        public int GetDevice(string DeviceName, out short shData) => this._actProg.GetDevice(DeviceName, out shData);
        public int GetDevice(string DeviceName, out ushort ushData) => this._actProg.GetDevice(DeviceName, out ushData);
        

        #endregion


        #region reiong - event

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this._semaphore.Wait();

            var targetDevices = new List<DeviceFieldFormat>();

            if ((this._monitorDevices?.Count ?? -1) > 0)
                targetDevices.AddRange(this._monitorDevices);

            if ((this._loggingDeviceList?.Count ?? -1) > 0)
                targetDevices.AddRange(this._loggingDeviceList);

            /*
            var comparer = new DeviceFieldFormatComparer();
            targetDevices = targetDevices.Distinct(comparer).ToList();
            */


            // if (this.ReadData(ref targetDevices))
            // ↓ 19.01.21 変更
            if (targetDevices.Count > 0 &&
                this.ReadData(ref targetDevices))
            {
                #region region - モニタ対象のデバイスの処理

                this._monitorDevices?.ForEach(x =>
            {
                var readDevice = targetDevices.Find(y => x.DeviceName == y.DeviceName);

                int idx = this._monitorDevicesPriviousValues?.FindIndex(y => x.DeviceName == y.DeviceName) ?? -1;

                if (idx == -1)
                {
                    if (this._monitorDevicesPriviousValues == null)
                        this._monitorDevicesPriviousValues = new List<LogValueFormat>();

                    this._monitorDevicesPriviousValues.Add(new LogValueFormat
                    {
                        DeviceName = x.DeviceName,
                        Value = x.CurrentValue
                    });
                }
                else
                {
                    if (this._monitorDevicesPriviousValues[idx].Value != null &&
                        !this._monitorDevicesPriviousValues[idx].Value.Equals(x.CurrentValue))
                    {
                            // イベント発生
                            this.MonitoringDeviceValueChanged.Invoke(this, new MXComponentOperatorMonitoringDeviceValueChangedEventArgs
                        {
                            ChangedDevice = x,
                        });
                    }

                        // 現在の値を保持
                        this._monitorDevicesPriviousValues[idx].Value = x.CurrentValue;
                }
            });

                #endregion

                #region region - ログの処理

                this._logRecords.Add(new LogRecordFormat
                {
                    DateTime = DateTime.Now,
                    LogValues = targetDevices.Select(x => new LogValueFormat { DeviceName = x.DeviceName, Value = x.CurrentValue }).ToList(),
                });

                #endregion
            }

            this._semaphore.Release();
        }

        #endregion

    }


    #region region - Model

    // 19.04.19 Mtec.UtilityLibrary.Mitsubishi に同名のクラスを重複して作っていたらしい、こちらは廃止
    /*
    [System.Serializable]
    public class DeviceFieldFormat
    {        
        public Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType  DeviceFormatType { get; set; }


        public string DeviceName { get; set; }


        public string Detail { get; set; }


        [System.Xml.Serialization.XmlIgnore]
        public object CurrentValue { get; set; }
    }
    

    public class DeviceFieldFormatComparer : IEqualityComparer<DeviceFieldFormat>
    {
        // Distinctメソッド で用いる比較クラス

        public bool Equals(DeviceFieldFormat arg1, DeviceFieldFormat arg2) => arg1.DeviceName == arg2.DeviceName;        

        public int GetHashCode(DeviceFieldFormat arg)
        {
            return arg.DeviceName.GetHashCode();
        }
    }
    */


    public class LogValueFormat
    {
        public string DeviceName { get; set; }
        public object Value { get; set; }
    }

    public class LogRecordFormat
    {
        public DateTime DateTime { get; set; }

        public List<LogValueFormat> LogValues { get; set; }
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
    public delegate void MXComponentOperatorEventHandler(object sender, MXLogOperatorEventArgs e);


    public class MXComponentOperatorMonitoringDeviceValueChangedEventArgs : EventArgs
    {
        public DeviceFieldFormat ChangedDevice { get; set; }
    }


    [System.Runtime.InteropServices.ComVisible(true)]
    public delegate void MXComponentOperatorMonitoringDeviceValueChangedEventHandler(object sender, MXComponentOperatorMonitoringDeviceValueChangedEventArgs e);




    #endregion

}
