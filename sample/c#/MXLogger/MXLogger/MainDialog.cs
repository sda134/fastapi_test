using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi;
using Mtec.UtilityLibrary.Mitsubishi.MXComponent;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public partial class MainDialog : Form
    {
        private int _currentGroupIndex = -1;
        private BindingList<DeviceFieldFormat> _bindingLoggerFields;
        private MXComponentOperator _mx;
        
        public MainDialog()
        {

            Config.LoadFromXmlFile();
            LogConfig.LoadFromXmlFile();

            // private member のインスタンス生成
            this._mx = new MXComponentOperator();
            this._mx.CsvFileFullPath = Config.DirectoryPath;
            this._mx.EventCallback += mx_EventCallback;
            this._mx.MonitoringDeviceValueChanged += mx_MonitoringDeviceValueChanged;

            // 19.12.06 追加　　これがなかった為に、Configの記録Intervalが適用されていなかった。
            this._mx.MoniteringIntervalMiliSeconds = Config.Instance.Interval_mSec;


            InitializeComponent();

            // バージョン表示
            this.Text = "MX Logger ver:" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;

            // デザイナで設定できない項目：DataGridView
            this.dataGridView_fields.AutoGenerateColumns = false;
            this.dataGridView_log.AutoGenerateColumns = false;

            // デザイナで設定できない項目：DataGridViewColumns
            this.Column_deviceFormatType.ValueType = typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType);
            this.Column_deviceFormatType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType));

            // 表示を更新
            #region region - configデータの読み込み

            // 起動初期の未選択状態の処理
            if ((LogConfig.Instance?.LogGroups?.Count ?? -1) > 0)
            {
                this._currentGroupIndex = 0;
                this._bindingLoggerFields = new BindingList<DeviceFieldFormat>(LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList);
                this.apply_GroupChanged_toGUI();
            }

            #endregion

            this.dataGridView_fields.DataSource = this._bindingLoggerFields;

        }


        #region region - private methods


        private void add_group()
        {
            using (var dlg = new GroupInfoEditDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // 実際に追加
                    LogConfig.AddGroup(dlg.Value);

                    this._currentGroupIndex = LogConfig.Instance.LogGroups.Count - 1;

                    // Configファイルを GUI に適用
                    this.apply_GroupChanged_toGUI();
                }
            }
        }


        private void delete_group()
        {
            // currentGroupIndex が有効な範囲内にいるか確認
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                //メッセージボックスを表示する
                if (MessageBox.Show("選択中のグループを削除しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // 削除
                    LogConfig.Instance.LogGroups.RemoveAt(this._currentGroupIndex);

                    // ひとつ前のグループを選択する
                    this._currentGroupIndex =
                        LogConfig.Instance.LogGroups.Count < 1 ? -1 :
                        this._currentGroupIndex == 0 ? 0 : this._currentGroupIndex - 1;

                    this.apply_GroupChanged_toGUI();
                }
            }
        }


        private void refreshLog()
        {
            // currentGroupIndex が有効な範囲内にあり、かつ記録中であるか
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                // 初期化
                this.dataGridView_log.Columns.Clear();
                this.dataGridView_log.Rows.Clear();

                // 18.12.12 追加 両方 0 = 一度も開始していない
                if ((LogConfig.Instance.LogGroups[this._currentGroupIndex].StartDateTime.Ticks < 1 &&
                    LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime.Ticks < 1))
                {

                }
                else
                {
                    // methods member
                    var targetFieldList = LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList;
                    DateTime dtEnd = LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime.Ticks < 1 ? DateTime.Now :
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime;

                    // データ取得
                    this._mx.GetLogRecords(
                        targetFieldList.Select(x => x.DeviceName),
                        out LogRecordFormat[] records,
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].StartDateTime,
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime.Ticks < 1 ? DateTime.Now : LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime
                        );

                    if (records != null && records.Length > 0)
                    {
                        #region 

                        
                        // 表示を更新：列の追加
                        this.dataGridView_log.Columns.AddRange(
                            targetFieldList.Select(x => new DataGridViewTextBoxColumn
                            {
                                Width = 50,
                                Name = x.DeviceName,
                                HeaderText = x.DeviceName,                                
                                SortMode = DataGridViewColumnSortMode.NotSortable,  // 18.12.13 暫定処理

                                // 18.12.13 苦肉の策　DataSet を手動で設定しない限り、この手がベター
                                ToolTipText = string.Format("デバイス\t{0}\r\nデータ型\t{1}\r\n詳細\t{2}", x.DeviceName, x.DeviceFormatType, x.Detail),

                            }).ToArray());


                        #region - 削除予定

                        // こっちの方が問題が多いので止め
                        /*
                        var dataSet = new DataSet();
                        var dataTable = new DataTable();
                        dataSet.Tables.Add(dataTable);
                        dataTable.Columns.AddRange(
                            targetFieldList.Select(x => new DataColumn
                            {
                                ColumnName = x.DeviceName,

                            }).ToArray());

                        this.dataGridView_log.DataSource = dataSet;
                        */

                        #endregion


                        // 表示を更新：行の追加
                        this.dataGridView_log.Rows.AddRange(records.Select(rec =>
                        {
                            var row = new DataGridViewRow
                            {
                                HeaderCell = new DataGridViewRowHeaderCell { Value = rec.DateTime.ToString("HH:mm:ss.fff") },
                            };

                            row.CreateCells(this.dataGridView_log);

                            targetFieldList.ForEach(x =>
                            {
                                int clmIdx = this.dataGridView_log.Columns[x.DeviceName].Index;
                                row.Cells[clmIdx].Value = rec.LogValues.Find(y => y.DeviceName == x.DeviceName)?.Value ?? "";
                            });

                            return row;

                        }).ToArray());
                        

                        #endregion
                    }
                }
            }

        }


        private void export_groupLog_toCSV()
        {
            // currentGroupIndex が有効な範囲内にいるか確認
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                // method member
                int iRet = 0;
                var currentGroup = LogConfig.Instance.LogGroups[this._currentGroupIndex];


                // 18.12.12 追加 両方 0 = 一度も開始していない
                if (currentGroup.StartDateTime.Ticks < 1 && currentGroup.EndDateTime.Ticks < 1)
                {

                }
                else
                {
                    // 保存ファイルパス
                    this._mx.CsvFileFullPath =
                        Config.Instance.LogFileSaveDirectory + "\\" +
                        DateTime.Now.ToString("MMdd_HHmmss_fff") +
                        string.Format("_{0}_log.csv", currentGroup.GroupName);

                    // 19.04.29 [ver 0.3.3.1] 追加
                    var targetFieldList = new List<DeviceFieldFormat>(currentGroup.FieldList);
                    if (Config.Instance.NeedCSVFieldsSorted) targetFieldList = targetFieldList.OrderBy(x => x.GetHashCode()).ToList();


                    // CSV ファイル作成
                    //iRet = this._mx.ExportToCSVFile(allDevices, currentGroup.StartDateTime, currentGroup.EndDateTime);
                    // ↓ CSV にデバイスの詳細情報がなかったので処理を変更
                    //iRet = this._mx.ExportToCSVFile(currentGroup.FieldList, currentGroup.StartDateTime, currentGroup.EndDateTime);
                    // ↓19.04.29 [ver 0.3.3.1] 並び替えを選択制にするための変更
                    iRet = this._mx.ExportToCSVFile(targetFieldList, currentGroup.StartDateTime, currentGroup.EndDateTime);
                }


                // 結果の表示
                this.showMessage(iRet == 0 ?
                    string.Format("レコードは 0 件でした。") : iRet > 0 ?
                    string.Format("CSV ファイルの書き込みに成功しました：{0} レコード", iRet) :
                    string.Format("CSV ファイルの書き込みに失敗しました。"));
            }
        }

        private void export_allLog_toCSV()
        {            
            // 保存ファイルパス
            this._mx.CsvFileFullPath =
                Config.Instance.LogFileSaveDirectory + "\\" +
                DateTime.Now.ToString("MMdd_HHmmss_fff") + "_log.csv";

            // デバイス名の抽出
            var allDeviceName = from gp in LogConfig.Instance.LogGroups
                                from fld in gp.FieldList
                                    //select fld.DeviceName;
                                    // ↓ CSV にデバイスの詳細情報がなかったので処理を変更 18.12.13
                                select fld;

            // 19.04.29 [ver 0.3.3.1] 追加
            if (Config.Instance.NeedCSVFieldsSorted) allDeviceName = allDeviceName.OrderBy(x => x.GetHashCode()).ToList();

            // CSV ファイル作成
            int iRet = this._mx.ExportToCSVFile(allDeviceName);

            // 結果の表示
            this.showMessage(iRet == 0 ?
                string.Format("レコードは 0 件でした。") : iRet > 0 ?
                string.Format("CSV ファイルの書き込みに成功しました：{0} レコード", iRet) :
                string.Format("CSV ファイルの書き込みに失敗しました。"));
        }


        private void showMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                // 別スレッドから呼び出された場合
                this.Invoke(new MethodInvoker(() => { this.showMessage(msg); }));
            }
            else
            {
                this.textBox_message.AppendText(string.Format("{0:HH:mm:ss.fff}\t{1}\r\n", DateTime.Now, msg));
                this.textBox_message.ScrollToCaret();
            }
        }

        // 19.01.21 追加
        private void showMessage(string format, params object[] args)
        {
            this.showMessage(string.Format(format, args));
        }


        #endregion


        #region region - private methods : apply


        // ページ変更イベントに起因するGUI(画像)の更新処理
        private void apply_GroupChanged_toGUI()
        {
            // ボタンの有効性
            this.button_group_next.Enabled = this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count - 1 ?? -1);
            this.button_gropup_privious.Enabled = this._currentGroupIndex > 0;
            this.button_record.Enabled = this._currentGroupIndex != -1 && this._mx.IsOpen &&
                !LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording;
            this.button_stop.Enabled = this._currentGroupIndex != -1 && this._mx.IsOpen &&
                LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording;


            // ボタンの可視性
            bool recordButtonsVisible = this._currentGroupIndex != -1 &&
                LogConfig.Instance.LogGroups[this._currentGroupIndex]?.TriggerType == TriggerType.UserRecording;
            this.button_record.Visible = recordButtonsVisible;
            this.button_stop.Visible = recordButtonsVisible;

            // ラベルの可視性
            this.label_recording.Visible = this._currentGroupIndex != -1 &&
                LogConfig.Instance.LogGroups[this._currentGroupIndex]?.TriggerType != TriggerType.UserRecording &&
                (LogConfig.Instance.LogGroups[this._currentGroupIndex]?.Recording ?? false);


            // テキストボックス
            this.textBox_groupName.Text = this._currentGroupIndex != -1 ?
                LogConfig.Instance.LogGroups[this._currentGroupIndex].GroupName : "";
            this.textBox_trigger.Text = this._currentGroupIndex != -1 ?
                LogConfig.Instance.LogGroups[this._currentGroupIndex].TriggerType.ToStringFromEnum() : "";


            // DataGridView：fields
            /*
            this._bindingLoggerFields = new BindingList<DeviceFieldFormat>(
                this._currentGroupIndex != -1 ? LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList ?? new List<DeviceFieldFormat>() : new List<DeviceFieldFormat>()
                );
            this.dataGridView_fields.DataSource = this._bindingLoggerFields;
            */
            // ↓ 18.12.13 LogConfig.Instance.LogGroups[index] に直接参照している事が判明したので、処理を変更
            if (this._currentGroupIndex != -1)
            {
                if (LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList == null)
                    LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList = new List<DeviceFieldFormat>();

                this._bindingLoggerFields = new BindingList<DeviceFieldFormat>(LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList);
                this.dataGridView_fields.DataSource = this._bindingLoggerFields;
            }

            // DataGridView：log
            this.refreshLog();

            // 19.03.22 追加
            if (this._mx.IsOpen) this.apply_labelRecording_VisibleChange();
        }


        private void applyMonitorFields()
        {
            // 監視デバイスの設定
            this._mx.MonitorDevices = (from gp in LogConfig.Instance.LogGroups
                                       where gp.TriggerType == TriggerType.Trigger && gp.Trigger != null
                                       select new DeviceFieldFormat
                                       {
                                           DeviceName = gp.Trigger.DeviceName,
                                       }).ToList();
        }

        private void applyTargetFields()
        {
            #region - 削除予定

            // trigger 未対応 18.12.03 トリガは常に監視するように変更する必要がある
            /*
            var targetFields = (from gp in LogConfig.Instance.LogGroups
                                where gp.TriggerType == TriggerType.Standby ||
                                ((gp.TriggerType == TriggerType.UserRecording || gp.TriggerType == TriggerType.UserRecording) && gp.Recording)
                                from field in gp.FieldList
                                select field).ToList();
            */

            #endregion

            var targetFields = new List<DeviceFieldFormat>();

            // 各グループの対象フィールドを追加する
            foreach (var gp in LogConfig.Instance.LogGroups)
            {
                if (gp.Recording) targetFields.AddRange(gp.FieldList);
            }



            this._mx.LoggingDeviceList = targetFields;
            
            if (targetFields.Count > 0)
                this._mx.StartLogging();
            else
                this._mx.StopLogging();
        }


        public void apply_OpenCloseStateChange_toGUI()
        {
            this.スタンバイToolStripMenuItem.Text = string.Format("{0}スタンバイ", this._mx.IsOpen ? "●" : "　");
            this.休止ToolStripMenuItem.Text = string.Format("{0}休止", !this._mx.IsOpen ? "●" : "　");

            // ボタンの表示可否を変更
            this.button_record.Enabled = this._currentGroupIndex != -1 && this._mx.IsOpen &&
                !LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording;

            this.button_stop.Enabled = this._currentGroupIndex != -1 && this._mx.IsOpen &&
                LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording;

            // 「記録中」の表示を更新
            this.apply_labelRecording_VisibleChange();
        }

        public void apply_labelRecording_VisibleChange()
        {
            // このメソッドの実行前に LogGroups[this._currentGroupIndex].Recording の値を変更しておきます。

            
            // 別スレッドから呼び出された場合
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { this.apply_labelRecording_VisibleChange(); }));
            }
            else
            {
                this.label_recording.Visible = this._currentGroupIndex != -1 && this._mx.IsOpen &&
                    LogConfig.Instance.LogGroups[this._currentGroupIndex].TriggerType != TriggerType.UserRecording &&
                    LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording;
            }
        }


        #endregion


        #region region - menu : ファイル

        private void toolStripMenuI_CSV_allLog_Click(object sender, EventArgs e) => this.export_allLog_toCSV();
        
        private void toolStripMenuI_CSV_groupLog_Click(object sender, EventArgs e) => this.export_groupLog_toCSV();


        private void ログ設定の保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LogConfig.SaveToXmlFile())
            {
                this.showMessage("ログ項目の設定データを保存しました");
            }
        }
        

        private void スタンバイToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ActProg の設定
            this._mx.ActProgSetting = Config.Instance.ActSetting;
            
            // MXOperator で監視するデバイスの更新。　このアプリでは各グループのトリガとなるデバイスのみ監視する
            this.applyMonitorFields();
            
            // ActProg の Open
            if (this._mx.Open())
            {
                #region region - recording 状態の変更

                // Standby になってるグループは Recording を　true に
                /*
                var indexArray = LogConfig.Instance.LogGroups.Select((x, i) => x.TriggerType == TriggerType.Standby ? i : -1);
                foreach (int i in indexArray.Where(x => x != -1))
                {
                    LogConfig.Instance.LogGroups[i].Recording = true;
                    LogConfig.Instance.LogGroups[i].StartDateTime = DateTime.Now;
                    LogConfig.Instance.LogGroups[i].EndDateTime = new DateTime();
                }
                */
                // ↓修正 19.03.22 [ver 0.3.2.0] 
                foreach (int i in LogConfig.Instance.LogGroups.Select((x, i) => i))
                {
                    var currentGp = LogConfig.Instance.LogGroups[i];
                    bool shouldTriggerOn = false;

                    switch (currentGp.TriggerType)
                    {
                        case TriggerType.Standby:
                            shouldTriggerOn = true;
                            break;
                        case TriggerType.Trigger:
                            {
                                object currentValue = null;

                                #region region - trigger 処理


                                switch (currentGp.Trigger.DeviceFormatType)
                                {
                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Bit:
                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed16:
                                        this._mx.GetDevice(currentGp.Trigger.DeviceName, out short sData);
                                        currentValue = sData;
                                        break;

                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                                        this._mx.GetDevice(currentGp.Trigger.DeviceName, out int iData);
                                        currentValue = iData;
                                        break;

                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                                        this._mx.GetDevice(currentGp.Trigger.DeviceName, out float fData);
                                        currentValue = fData;
                                        break;

                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                                        this._mx.GetDevice(currentGp.Trigger.DeviceName, out ushort ushData);
                                        currentValue = ushData;
                                        break;

                                    case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                                        this._mx.GetDevice(currentGp.Trigger.DeviceName, out uint uiData);
                                        currentValue = uiData;
                                        break;

                                    default:
                                        break;
                                }

                                #endregion

                                shouldTriggerOn =
                                    currentValue != null &&
                                    currentGp.Trigger.IsTriggerOn(currentValue);
                            }
                            break;
                        default:
                            break;

                    }

                    if (shouldTriggerOn)
                    {
                        LogConfig.Instance.LogGroups[i].Recording = true;
                        LogConfig.Instance.LogGroups[i].StartDateTime = DateTime.Now;
                        LogConfig.Instance.LogGroups[i].EndDateTime = new DateTime();

                        // ターゲットデバイスの変更
                        this.applyTargetFields();
                    }
                }

                

                #endregion

                this.showMessage("オープン成功");

                this.apply_OpenCloseStateChange_toGUI();
            }
        }

        private void 休止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close の場合は成功しようがしまいが後処理が必要

            // Standby になってるグループは Recording を　false に
            //var indexArray = LogConfig.Instance.LogGroups.Select((x, i) => x.TriggerType != TriggerType.UserRecording ? i : -1);
            // ↓ 19.03.22 [ver 0.3.2.0]  変更
            var indexArray = LogConfig.Instance.LogGroups.Select((x, i) => i);

            foreach (int i in indexArray.Where(x => x != -1))
            {
                //LogConfig.Instance.LogGroups[i].Recording = true;
                // ↓ 19.03.22 [ver 0.3.2.0] 何故か trueになっていた。修正。
                LogConfig.Instance.LogGroups[i].Recording = false;

                LogConfig.Instance.LogGroups[i].EndDateTime = DateTime.Now;
            }

            if (this._mx.Close())
            {
                this.showMessage("クローズ成功");
            }

            this.apply_OpenCloseStateChange_toGUI();
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion


        #region region - menu : 編集

        private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.add_group();
        }

        private void 削除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.delete_group();
        }

        private void 並び替えToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new GroupOrderEditDialog())
            {
                dlg.Value = LogConfig.Instance.LogGroups;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    

                    // 現在値の保存処理が要る　18.12.13 temporary

                    LogConfig.Instance.LogGroups = dlg.Value.ToList();
                    this.apply_GroupChanged_toGUI();
                }
            }
        }


        #endregion


        #region region - menu : データ

        private void ログデータの初期化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ログデータを初期化します。\n", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this._mx.ClearLogData();

                this.refreshLog();
            }
        }

        #endregion


        #region region - menu : ツール

        private void 通信設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._mx.MoniteringIntervalMiliSeconds = Config.Instance.Interval_mSec;
                }
            }
        }

        #endregion


        #region region - event : button

        private void button_gropup_privious_Click(object sender, EventArgs e)
        {
            if (LogConfig.Instance.LogGroups.Count > 0 && this._currentGroupIndex > 0)
            {

                this._currentGroupIndex -= 1;

                // 設定ファイルの値を GUI に適用
                this.apply_GroupChanged_toGUI();
            }
        }

        private void button_group_next_Click(object sender, EventArgs e)
        {
            if (LogConfig.Instance.LogGroups.Count > 0 && this._currentGroupIndex < LogConfig.Instance.LogGroups.Count - 1)
            {
                this._currentGroupIndex += 1;

                // 設定ファイルの値を GUI に適用
                this.apply_GroupChanged_toGUI();
            }
        }

        private void button_groupInfoEdit_Click(object sender, EventArgs e)
        {
            // currentGroupIndex が有効な範囲内にいるか確認
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {

                using (var dlg = new GroupInfoEditDialog())
                {
                    dlg.Value = LogConfig.Instance.LogGroups[this._currentGroupIndex];

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        // 変更したプロパティの適用 ※バグ対策の為、１つ１つ値を入れる
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].GroupName = dlg.Value.GroupName;
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].Trigger = dlg.Value.Trigger;
                        LogConfig.Instance.LogGroups[this._currentGroupIndex].TriggerType = dlg.Value.TriggerType;


                        // ボタンの可視性
                        bool recordButtonsVisible = this._currentGroupIndex != -1 &&
                            LogConfig.Instance.LogGroups[this._currentGroupIndex]?.TriggerType == TriggerType.UserRecording;
                        this.button_record.Visible = recordButtonsVisible;
                        this.button_stop.Visible = recordButtonsVisible;

                        // ボタンの有効性
                        this.button_group_next.Enabled = this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count - 1 ?? -1);
                        this.button_gropup_privious.Enabled = this._currentGroupIndex > 0;

                        // ラベルの可視性                   
                        this.label_recording.Visible = this._currentGroupIndex != -1 &&                
                            LogConfig.Instance.LogGroups[this._currentGroupIndex]?.TriggerType != TriggerType.UserRecording &&                
                            (LogConfig.Instance.LogGroups[this._currentGroupIndex]?.Recording ?? false);

                        // GUI の変更
                        this.textBox_groupName.Text = LogConfig.Instance.LogGroups[this._currentGroupIndex].GroupName;
                        this.textBox_trigger.Text = LogConfig.Instance.LogGroups[this._currentGroupIndex].TriggerType.ToStringFromEnum();
                    }
                }
            }
        }

        private void button_record_Click(object sender, EventArgs e)
        {
            // currentGroupIndex が有効な範囲内にいるか確認
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                // Config データの変更
                LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording = true;
                LogConfig.Instance.LogGroups[this._currentGroupIndex].StartDateTime = DateTime.Now;
                LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime = new DateTime();

                // ボタンの有効性変更
                this.button_record.Enabled = false;
                this.button_stop.Enabled = true;

                // 対象デバイスの変更
                this.applyTargetFields();
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            // currentGroupIndex が有効な範囲内にいるか確認
            if (this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                // Config データの変更
                LogConfig.Instance.LogGroups[this._currentGroupIndex].Recording = false;
                LogConfig.Instance.LogGroups[this._currentGroupIndex].EndDateTime = DateTime.Now;

                // ボタンの有効性変更
                this.button_record.Enabled = true;
                this.button_stop.Enabled = false;

                // 対象デバイスの変更
                this.applyTargetFields();

                // ログの更新
                this.refreshLog();
            }
        }

        private void button_field_up_Click(object sender, EventArgs e)
        {
            //int idx = this.dataGridView_fields.CurrentRow?.Index ?? -1;
            // ↓変更 フォーカスを失った時はCurrentRow を正しく取得できない 18.12.13_1
            //int idx = (from DataGridViewCell cell in this.dataGridView_fields.SelectedCells select cell.RowIndex).Min();
            // ↓最初に選択した場所　の方が自然と思い変更 18.12.13_2
            int idx = this.dataGridView_fields.SelectedCells.Count < 0 ? -1 : this.dataGridView_fields.SelectedCells[0].RowIndex;

            // 上下限チェック
            if (idx < 1 ||
                this.dataGridView_fields.Rows[idx].IsNewRow ||
                this._bindingLoggerFields.Count < 2 ) return;

          
            // 要素のコピー
            var current = UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<DeviceFieldFormat>(this._bindingLoggerFields[idx]);
            var target = UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<DeviceFieldFormat>(this._bindingLoggerFields[idx - 1]);

            // 入れ替え
            this._bindingLoggerFields[idx - 1] = current;
            this._bindingLoggerFields[idx] = target;

            // セルの移動
            this.dataGridView_fields.CurrentCell =
                this.dataGridView_fields[this.dataGridView_fields.CurrentCell.ColumnIndex, idx - 1];
        }

        private void button_field_down_Click(object sender, EventArgs e)
        {
            int idx = this.dataGridView_fields.CurrentRow?.Index ?? -1;
            
            // 上下限チェック
            if (idx == -1 ||
                this._bindingLoggerFields.Count < 2 ||
                idx >= (this._bindingLoggerFields.Count - 1)) return;

            // 要素のコピー
            var current = UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<DeviceFieldFormat>(this._bindingLoggerFields[idx]);
            var target = UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<DeviceFieldFormat>(this._bindingLoggerFields[idx + 1]);

            // 入れ替え
            this._bindingLoggerFields[idx] = target;
            this._bindingLoggerFields[idx + 1] = current;

            // セルの移動
            this.dataGridView_fields.CurrentCell =
                this.dataGridView_fields[this.dataGridView_fields.CurrentCell.ColumnIndex, idx + 1];
        }
        
        private void button_refreshLog_Click(object sender, EventArgs e)
        {
            this.refreshLog();            
        }

        private void button_csv_group_Click(object sender, EventArgs e)
        {
            this.export_groupLog_toCSV();
        }

        #endregion


        #region region - event : MXComponentOperator

        private void mx_EventCallback(object sender, MXLogOperatorEventArgs e)
        {
            if (e.Ex != null)
            {
                this.showMessage(string.Format("例外エラー：{0}", e.Ex.Message));
            }
            else
            {
                this.showMessage(e.Message);
            }
        }

        private void mx_MonitoringDeviceValueChanged(object sender, MXComponentOperatorMonitoringDeviceValueChangedEventArgs e)
        {
#if DEBUG
            this.showMessage(string.Format("モニターデバイス　値変更 [ name:{0} value:{1} ]", e.ChangedDevice.DeviceName, e.ChangedDevice.CurrentValue));
#endif

            int gpIndex = LogConfig.Instance.LogGroups.FindIndex
                (x => x.TriggerType == TriggerType.Trigger &&
                x.Trigger != null && x.Trigger.DeviceName == e.ChangedDevice.DeviceName);

            if (gpIndex != -1 &&
                LogConfig.Instance.LogGroups[gpIndex].TriggerType == TriggerType.Trigger &&
                LogConfig.Instance.LogGroups[gpIndex].Trigger != null)
            {
                #region region - トリガ処理

                bool shouldTriggerOn = false;

                // 値の大きさ比較の為、一度 decimal に変換する
                var currentVal = Convert.ToDecimal(e.ChangedDevice.CurrentValue);
                var threshold = Convert.ToDecimal(LogConfig.Instance.LogGroups[gpIndex].Trigger.ThresholdValue);

                switch (LogConfig.Instance.LogGroups[gpIndex].Trigger.CompareType)
                {
                    case CompareType.Equal:
                        shouldTriggerOn = threshold.Equals(currentVal);
                        break;
                    case CompareType.NotEqual:
                        shouldTriggerOn = !threshold.Equals(currentVal);
                        break;

                    case CompareType.GreaterThan:
                        shouldTriggerOn = currentVal > threshold;
                        break;
                    case CompareType.LessThan:
                        shouldTriggerOn = currentVal < threshold;
                        break;
                    default:
                        break;
                }



                // Recording 状態が変わったら
                if (LogConfig.Instance.LogGroups[gpIndex].Recording != shouldTriggerOn)
                {
                    LogConfig.Instance.LogGroups[gpIndex].Recording = shouldTriggerOn;

                    if (shouldTriggerOn)
                    {
                        LogConfig.Instance.LogGroups[gpIndex].StartDateTime = DateTime.Now;
                        LogConfig.Instance.LogGroups[gpIndex].EndDateTime = new DateTime();
                    }
                    else
                        LogConfig.Instance.LogGroups[gpIndex].EndDateTime = DateTime.Now;

                    this.applyTargetFields();

                    this.showMessage(string.Format("トリガ{0} [ グループ名:{1} ]",
                        LogConfig.Instance.LogGroups[gpIndex].Recording ? "ON" : "OFF",
                        LogConfig.Instance.LogGroups[gpIndex].GroupName));


                    // 19.03.22 [ver 0.3.2.0] 追加。表示の更新が必要
                    this.apply_labelRecording_VisibleChange();
                }
                else
                    // Recording 状態が変わらなくても、現在の状態を入れる必要がある
                    LogConfig.Instance.LogGroups[gpIndex].Recording = shouldTriggerOn;




                #endregion
            }
        }

        #endregion


        #region region - event : other

        private void MainDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._mx?.IsLogging ?? false)
            {
                // temporary
                this._mx.Close();
            }

            if (this._currentGroupIndex != -1)
            {
            }

            // 止めにする 19.01.08
            //LogConfig.SaveToXmlFile();
        }

        

        // 過去の産物 18.12.12
        /*
        private void comboBox_triggerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ボタンの可視性
            bool recordButtonsVisible = this._currentGroupIndex != -1 &&
                ((TriggerType)((ComboBox)sender).SelectedValue) == TriggerType.UserRecording;
            this.button_record.Visible = recordButtonsVisible;
            this.button_stop.Visible = recordButtonsVisible;

            // ボタンの有効性
            this.button_group_next.Enabled = this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count - 1 ?? -1);
            this.button_gropup_privious.Enabled = this._currentGroupIndex > 0;

            // ターゲットのフィールドの変更
            this.applyTargetFields();
        }*/
        

        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = this.tabControl.TabPages[this.tabControl.SelectedIndex];

            // 参照型なので == が見れるはず
            if (tab == this.tabPage_log)
            {
                this.refreshLog();
            }
        }

        #endregion


        #region region - event : datagGridView

        
        private void dataGridView_fields_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.ColumnIndex < 0 && e.RowIndex != -1 && !this.dataGridView_fields.Rows[e.RowIndex].IsNewRow)
            {
                #region region - フィールド行の削除
                
                var menuItem_delete = new ToolStripMenuItem
                {
                    Text = "行の削除",                    
                    Size = new Size(120,22),
                };

                menuItem_delete.Click += (obj, arg) =>
                {
                    this._bindingLoggerFields.RemoveAt(e.RowIndex);

                    // 表示がおかしな事になるので Refresh　する事に 18.12.13
                    this.dataGridView_fields.Refresh();
                };

                var contexMenu = new ContextMenuStrip ();
                contexMenu.Items.Add(menuItem_delete);
                e.ContextMenuStrip = contexMenu;

                #endregion
            }
        }


        private void dataGridView_log_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 &&
                this._currentGroupIndex > -1 &&
                this._currentGroupIndex < (LogConfig.Instance?.LogGroups?.Count ?? -1))
            {
                var clm = this.dataGridView_log.Columns[e.ColumnIndex];
                int idx = LogConfig.Instance.LogGroups[this._currentGroupIndex].FieldList.FindIndex(x => x.DeviceName == clm.Name);

                if (idx != -1)
                {
                    var field = LogConfig.Instance?.LogGroups[this._currentGroupIndex].FieldList[idx];
                    e.ToolTipText = string.Format("デバイス\t{0}\r\nデータ型\t{1}\r\n詳細\t{2}",
                        field.DeviceName, field.DeviceFormatType, field.Detail);
                }
            }
        }

        #endregion

    }
}
