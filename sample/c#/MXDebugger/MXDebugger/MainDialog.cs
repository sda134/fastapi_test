using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi.FxDeviceExtensionMethod;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public partial class MainDialog : Form
    {

        #region region - private member
        
        private int _currentGroupIndex = -1;
        private bool _isBeingInitialised = false;

        private System.Threading.SemaphoreSlim _semaphore;
        System.Timers.Timer _timer_encorderRun;

        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.MXComponentOperator _mx;
        private BindingList<ReverseDeviceFormat> _bindingLoggerFields;
        private List<PLCDeviceControlPannel> _controls;

        #endregion


        public MainDialog()
        {
            Config.LoadFromXmlFile();
            if (Config.Instance.ActSetting == null) Config.Instance.ActSetting = new UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat();


            // private member のインスタンス生成と設定
            this._mx = new UtilityLibrary.Mitsubishi.MXComponent.MXComponentOperator();
            this._mx.CsvFileFullPath = Config.DirectoryPath;
            this._mx.EventCallback += mx_EventCallback;
            this._mx.MonitoringDeviceValueChanged += mx_MonitoringDeviceValueChanged;

            this._semaphore = new System.Threading.SemaphoreSlim(1, 1);


            // PLCDeviceControl のコレクションの生成
            this._controls = new List<PLCDeviceControlPannel>();


            InitializeComponent();

            #region region - デザイナで編集できない項目

            // デザイナで設定できない項目：Combobox
            this.comboBox_encoderFormat.DisplayMember = "Text";
            this.comboBox_encoderFormat.ValueMember = "Value";
            this.comboBox_encoderFormat.DataSource = (from EncoderFormat enco in typeof(EncoderFormat).GetEnumValues()
                                                      select new { Text = enco.ToStringFromEnum(), Value = enco }).ToList();

            this.comboBox_encoder_bits.DisplayMember = "Text";
            this.comboBox_encoder_bits.ValueMember = "Value";
            this.comboBox_encoder_bits.DataSource = (from EncoderBitsCount bits in typeof(EncoderBitsCount).GetEnumValues()
                                                     select new { Text = bits.ToStringFromEnum(), Value = bits }).ToList();

            this.comboBox_encorder_numNotation.DisplayMember = "Text";
            this.comboBox_encorder_numNotation.ValueMember = "Value";
            this.comboBox_encorder_numNotation.DataSource = (from EncorderDevNumberNotation note in typeof(EncorderDevNumberNotation).GetEnumValues()
                                                     select new { Text = note.ToStringFromEnum(), Value = note }).ToList();




            // デザイナで設定できない項目：DataGridView
            this.dataGridView_fields.AutoGenerateColumns = false;
            this.dataGridView_table.AutoGenerateColumns = false;

            // デザイナで設定できない項目：DataGridViewColumns
            this.Column_DeviceFormatType.ValueType = typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType);
            this.Column_DeviceFormatType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType));

            this.Column_ReverseType.ValueType = typeof(ReverseType);
            this.Column_ReverseType.DataSource = Enum.GetValues(typeof(ReverseType));

            #endregion


            // Config 内のデータの内、最小値を現在グループとする
            if ((Config.Instance?.FieldTables?.Count ?? -1) > 0)
                this._currentGroupIndex = 0;


            // Config 値の復元
            this.textBox_encorder_firstDevice.Text = Config.Instance.Encorder.FirstDevice;
            this.comboBox_encoderFormat.SelectedValue = Config.Instance.Encorder.EncoderFormat;
            this.comboBox_encorder_numNotation.SelectedValue = Config.Instance.Encorder.NumberNotation;
            this.comboBox_encoder_bits.SelectedValue = Config.Instance.Encorder.EncoderBitsCount;
            this.numericUpDown_encorder_rpm.Value = Config.Instance.Encorder.SpeedOfRevolution;

            this.radioButton_cw.Checked = !Config.Instance.Encorder.IsCounterClockwise;
            this.radioButton_ccw.Checked = Config.Instance.Encorder.IsCounterClockwise;


            // 画面の更新
            this.applyChangedGroupConfigToGUI();

            // Control のタブを再構築
            this.rebuidControlTab();

            // エンコーダー用のタイマー
            this._timer_encorderRun = new System.Timers.Timer();
            this._timer_encorderRun.Interval = 500;
            this._timer_encorderRun.Elapsed += timer_encorderRun_Elapsed;

            // バージョン表示
            this.Text += " ver:" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;
        }



        #region region - private methods

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

        private void showMessage(string format, params object[] args) => this.showMessage(string.Format(format, args));
        
        private void setDegree(int degree)
        {
            //  余り値処理
            switch (Config.Instance.Encorder.EncoderFormat)
            {
                #region

                case EncoderFormat.GrayCode_Excess76:
                    degree += 76;
                    break;
                case EncoderFormat.GrayCode_Excess152:
                    degree += 152;
                    break;
                default:
                    break;

                    #endregion
            }

            // グレイ値変換
            switch (Config.Instance.Encorder.EncoderFormat)
            {
                #region region

                case EncoderFormat.GrayCode:
                case EncoderFormat.GrayCode_Excess76:
                case EncoderFormat.GrayCode_Excess152:
                    degree = (degree >> 1) ^ degree;
                    break;

                case EncoderFormat.Binary:
                default:
                    break;

                    #endregion
            }


            // 19.02.12 7bit未対応メッセージ
            if (((EncoderBitsCount)this.comboBox_encoder_bits.SelectedValue) == EncoderBitsCount._7Bits)
                showMessage("7bitは未対応です。");


            if (this._mx.IsOpen && this._semaphore.CurrentCount > 0)
            {
                string firstDev = (string.IsNullOrWhiteSpace(Config.Instance.Encorder?.FirstDevice) ?
                    "X0" : Config.Instance.Encorder?.FirstDevice);


                var bits = (EncoderBitsCount)this.comboBox_encoder_bits.SelectedValue;
                var note = (EncorderDevNumberNotation)this.comboBox_encorder_numNotation.SelectedValue;


                Task task = Task.Run(() =>
                {
                    this._semaphore.Wait();

                    #region - region 過去の処理

                    var bitArray = new System.Collections.BitArray(BitConverter.GetBytes(degree));

                    var devices = new List<UtilityLibrary.Mitsubishi.DeviceFieldFormat>();

                    for (int x = 0; x < 9; x++)
                    {
                        devices.Add(new UtilityLibrary.Mitsubishi.DeviceFieldFormat
                        {
                             DeviceName = string.Format("X{0}", Convert.ToString(x, 8)),
                            CurrentValue = bitArray.Length > x ? (bitArray[x] ? (short)1 : (short)0) : (short)0,
                            DeviceFormatType = UtilityLibrary.Mitsubishi.DeviceFormatType.Bit,
                        });
                    }

                    bool ret = this._mx.WriteData(devices);
                    /*
                    #endregion
                    // ↓ 19.02.12 Kが使える事が判明。変換する必要なし

                    int iRet1 = this._mx.SetDevice("K2" + firstDev, degree);

                    if (bits == EncoderBitsCount._9Bits &&
                        Mtec.UtilityLibrary.Mitsubishi.FxDevice.TryParse(firstDev, out UtilityLibrary.Mitsubishi.FxDevice fxDev))
                    {
                        string ninethDev =
                            fxDev.DeviceType.ToDeviceLetter() +
                            (note == EncorderDevNumberNotation.Octal ? Convert.ToString((fxDev.DeviceNumber + 8), 8) :
                            note == EncorderDevNumberNotation.Hexadecimal ? Convert.ToString((fxDev.DeviceNumber + 8), 16) :
                            (fxDev.DeviceNumber + 8).ToString());


                        // 9bit目の書き込み
                        this._mx.SetDevice(ninethDev, (degree / 256));
                    }


                    #region region - 過去の処理２

                    // 19.02.12 複数デバイスを使うと処理が非常に遅くなる 上の単発処理２つに戻す
                    // 原因は自作の中間クラス MXComponentOperator かもしれない。MXComponent の仕様なのかもしれない
                    /*
                    var devices = new List<UtilityLibrary.Mitsubishi.MXComponent.DeviceFieldFormat>
                    {
                        // １～８ビット目まで
                        new UtilityLibrary.Mitsubishi.MXComponent.DeviceFieldFormat
                        {
                            DeviceName = "K2X0",
                            CurrentValue = degree,
                            DeviceFormatType = UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32,
                        },
                        
                        // ９ビット目
                        new UtilityLibrary.Mitsubishi.MXComponent.DeviceFieldFormat
                        {
                            DeviceName ="X10",
                            CurrentValue = degree / 256,
                            DeviceFormatType = UtilityLibrary.Mitsubishi.DeviceFormatType.Bit,
                        },
                    };
                    bool bRet = this._mx.WriteData(devices);
                    */

                    // Console.WriteLine("Done! - ID:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

                    #endregion

                    this._semaphore.Release();
                });
            }
        }

        private void _openMx()
        {
            // 再度読み込み → 何故？20.11.10
            //Config.LoadFromXmlFile();


            //var actSet = Config.Instance.ActSetting;
            // ↓19.04.10 [ver0.2.1.1] ここが参照型なので、元の設定まで変わってしまう。修正
            var actSet = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat)Config.Instance.ActSetting.Clone();

            if (Config.Instance.SimulatorType != UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.Default)
            {
                actSet.ActUnitType = Config.Instance.SimulatorType;
            }


            this._mx.ActProgSetting = actSet;

            if (this._mx.Open())
            {
                this.showMessage("オープンに成功しました");
                this.refreshCotrolTab();
                this.applySurveillance_to_MX();
            }
        }

        private void _closeMx()
        {
            if (this._mx.Close())
                this.showMessage("クローズに成功しました");
        }


        #endregion


        #region region - privete method : apply, rebuild, refresh

        private void applyChangedGroupConfigToGUI()
        {
            // グループに関連する画面更新
            if ((Config.Instance?.FieldTables?.Count ?? -1) > 0 &&
                this._currentGroupIndex != -1)
            {
                #region region - 現在値修正

                if (this._currentGroupIndex > Config.Instance.FieldTables.Count - 1)
                    this._currentGroupIndex = Config.Instance.FieldTables.Count - 1;
                if (this._currentGroupIndex < 0)
                    this._currentGroupIndex = 0;

                #endregion

                var gp = Config.Instance.FieldTables[this._currentGroupIndex];

                this.textBox_groupName.Text = gp.GroupName;

                this.button_gropup_privious.Enabled = this._currentGroupIndex > 0;
                this.button_group_next.Enabled = this._currentGroupIndex < (Config.Instance.FieldTables.Count - 1);


                // DataGridView
                this._bindingLoggerFields = new BindingList<ReverseDeviceFormat>(Config.Instance.FieldTables[this._currentGroupIndex].FieldList);
                this.dataGridView_fields.DataSource = this._bindingLoggerFields;
            }
            else
            {
                this.textBox_groupName.Text = "";

                this.button_gropup_privious.Enabled = false;
                this.button_group_next.Enabled = false;

                // DataGridView
                //this._bindingLoggerFields = new BindingList<ReverseDeviceFormat>(Config.Instance.FieldTables[this._currentGroupIndex].FieldList);
                // ↓ 変更 19.03.04 [ver0.2.0.1] 
                this._bindingLoggerFields = new BindingList<ReverseDeviceFormat>();
                this.dataGridView_fields.DataSource = this._bindingLoggerFields;
            }
        }

        private void applyGUIData_to_Config()
        {
            Config.Instance.Encorder = new EncoderParameterFormat
            {
                EncoderFormat = (EncoderFormat)this.comboBox_encoderFormat.SelectedValue,
                IsCounterClockwise = this.radioButton_ccw.Checked,
                SpeedOfRevolution = (int)this.numericUpDown_encorder_rpm.Value,

                FirstDevice = this.textBox_encorder_firstDevice.Text,
                EncoderBitsCount = (EncoderBitsCount)this.comboBox_encoder_bits.SelectedValue,
            };
        }


        private void applySurveillance_to_MX()
        {
            if (this._mx.IsOpen)
            {
                var target =
                    (from table in Config.Instance.FieldTables
                     from field in table.FieldList
                     where field.PredicationList != null
                     from predic in field.PredicationList
                     select new Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat
                     {
                         DeviceName = predic.DeviceName,
                         DeviceFormatType = predic.DeviceFormatType,
                     }).ToList();

                // 19.02.12追加
                if (this.radioButton_encorder_trigger.Checked && Config.Instance.Encorder != null)
                    target.AddRange(Config.Instance.Encorder.Trigger.Select(x => new Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat
                    {
                        DeviceName = x.DeviceName,
                        DeviceFormatType = x.DeviceFormatType,
                    }));

                // Distinct を使うのがめんどくさすぎるので手動で追加　Distinct の仕様、あほすぎる
                var deviceList = new List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>();
                target.ForEach(x =>
                {
                    if (!deviceList.Any(y => y.DeviceName == x.DeviceName))
                        deviceList.Add(x);
                });

                this._mx.MonitorDevices = deviceList;
            }
        }

        private void rebuidControlTab()
        {
            // 今のコントロールを削除
            this._controls?.ForEach(x =>
            {
                this.panel_controlTab_inner.Controls.Remove(x);
            });

            // メソッド内変数の生成
            int panelHeight = 0;
            int y_location = 0;

            // グループ内の全コントロールを追加
            this._controls = this._bindingLoggerFields.Select(x =>
            {
                var panel = new PLCDeviceControlPannel();

                #region region  - ダウンキャスト＋イベント登録

                switch (x.ReverseType)
                {
                    case ReverseType.Button:
                        panel = new PLCDeviceControlButton();
                        panel.PlcDeviceControlPannelEvent += this.plcDeviceControlPannelEvent; ;
                        break;

                    case ReverseType.CheckBox:
                        panel = new PLCDeviceControlCheckBox();
                        panel.PlcDeviceControlPannelEvent += this.plcDeviceControlPannelEvent;
                        break;

                    case ReverseType.TextBox:
                        panel = new PLCDeviceContriolTextBox();
                        panel.PlcDeviceControlPannelEvent += this.plcDeviceControlPannelEvent;
                        break;

                    //19.01.29 追加
                    case ReverseType.Correspond:
                        panel = new PLCDeviceControlCorrespond();
                        ((PLCDeviceControlCorrespond)panel).PredicateEditButtonClicked += DebuggerMainDialog_PredicateEditButtonClicked;
                        break;

                    default:
                        break;
                }

                #endregion

                // PLCDeviceControlPannel　共通処理
                panel.DeviceName = x.DeviceName;
                panel.Detail = x.Detail;
                panel.DeviceFormatType = x.DeviceFormatType;
                panel.Location = new Point(0, y_location);

                // メソッド内変数の処理
                panelHeight += panel.Height;
                y_location += panel.Height;

                // コントロール追加
                this.panel_controlTab_inner.Controls.Add(panel);

                return panel;

            }).ToList();


            // 現在値の取得            
            // → 19.01.28 個別にメソッドを作成。このメソッド内の最後でそのメソッドを呼び出すように変更。

            // パネルの Size 変更
            this.panel_controlTab_inner.Size = new Size
            {
                Width = this.tabControl_field.Width - this.tabControl_field.Margin.Horizontal - 30,
                Height = panelHeight,    // this._controls.Sum(x => ((PLCDeviceControlPannel)x).Height),
            };
            
            this.refreshCotrolTab();
        }

        private void refreshCotrolTab()
        {
            // 現在値の取得
            if (this._mx.IsOpen && _bindingLoggerFields.Count > 0)
            {
                this._isBeingInitialised = true;

                #region MyRegion

                var deviceList = _bindingLoggerFields.Select(x => new Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat
                {
                    DeviceName = x.DeviceName,
                    DeviceFormatType = x.DeviceFormatType,
                }).ToList();

                bool readRet = this._mx.ReadData(ref deviceList);

                deviceList.ForEach(x =>
                {
                    PLCDeviceControlPannel pannel = this._controls.Find(y => y.DeviceName == x.DeviceName);

                    if (pannel != null)
                    {
                        switch (pannel)
                        {
                            case PLCDeviceControlCheckBox chk:
                                {
                                    var val = ((short)(x.CurrentValue ?? (short)0));
                                    chk.Checked = val == 1;
                                }
                                break;

                            case PLCDeviceContriolTextBox text:
                                {
                                    text.Text = x.CurrentValue?.ToString() ?? "";
                                }
                                break;

                            default:
                                break;
                        }
                    }
                });


                #endregion

                this._isBeingInitialised = false;
            }
        }


        #endregion


        #region region - event : Menu

        private void 設定の保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // DataGridView はセル移動などしないと編集を確定しないので、EndEdit メソッドで無理やり終わらせる
            this.dataGridView_fields.EndEdit();

            // 保存
            showMessage("設定ファイルの保存に{0}しました。", Config.SaveToXmlFile() ? "成功" : "失敗");
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();


        private void グループGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new GroupListEditDialog())
            {
                // 今までの編集を適用
                //this.dataGridView_fields.EndEdit();

                dlg.Value = Config.Instance.FieldTables ?? new List<LogGroupFormat>();


                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Config.Instance.FieldTables = dlg.Value;

                    if (dlg.SelectedIndecies.Count() < 1)
                    {
                        // this._currentGroupIndex = -1;
                        // ↓ 19.03.04 [ver0.2.0.1]変更
                        if (dlg.Value.Count > 0)
                            this._currentGroupIndex = 0;
                        else
                            this._currentGroupIndex = -1;
                    }
                    else
                        this._currentGroupIndex = dlg.SelectedIndecies.ElementAt(0);


                    // 画面の更新
                    this.applyChangedGroupConfigToGUI();

                    // Control のタブを再構築
                    this.rebuidControlTab();
                }
            }
        }

        private void オープンToolStripMenuItem_Click(object sender, EventArgs e) => this._openMx();

        private void クローズToolStripMenuItem_Click(object sender, EventArgs e) => this._closeMx();

        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 実機ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Instance.SimulatorType = UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.Default;
            
            if (this._mx.IsOpen)
            {
                this._closeMx();
                                
                /*
                // 19.03.22_1 [ver0.2.1.0]  実機 ⇔ シミュレータ切り替えの為追加
                var actSet = Config.Instance.ActSetting;
                this._mx.ActProgSetting = actSet;
                */

                this._openMx();
            }
        }

        private void simulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Instance.SimulatorType = UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR;

            if (this._mx.IsOpen)
            {
                this._closeMx();
                
                this._openMx();
            }
        }

        private void simulator2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Instance.SimulatorType = UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR2;

            if (this._mx.IsOpen)
            {
                this._closeMx();

                this._openMx();
            }
        }

        private void simulator3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Instance.SimulatorType = UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR3;

            // 実験中
            Config.Instance.ActSetting.ActProtocolType = UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_TCPIP;

            if (this._mx.IsOpen)
            {
                this._closeMx();


                /*
                // 19.03.22_1 [ver0.2.1.0]  実機 ⇔ シミュレータ切り替えの為追加
                var actSet = Config.Instance.ActSetting;
                actSet.ActUnitType = Config.Instance.SimulatorType;
                this._mx.ActProgSetting = actSet;
                */

                this._openMx();
            }
        }


        private void 設定SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new SettingDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // SettingDialog で保存は行う
                }
            }
        }


        private void 通信先の選択ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.simulatorToolStripMenuItem.Text = "　Simulator";
            this.simulator2ToolStripMenuItem.Text = "　Simulator2";
            this.simulator3ToolStripMenuItem.Text = "　Simulator3";
            this.実機ToolStripMenuItem.Text = "　実機";


            switch (Config.Instance.SimulatorType)
            {
                case UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR:
                    this.simulatorToolStripMenuItem.Text = "●Simulator";
                    break;
                case UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR2:
                    this.simulator2ToolStripMenuItem.Text = "●Simulator2";
                    break;
                case UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR3:
                    this.simulator3ToolStripMenuItem.Text = "●Simulator3";
                    break;
                default:
                    this.実機ToolStripMenuItem.Text = "●実機";
                    break;
            }

            this.Update();
        }
        #endregion


        #region region - event : button

        private void button_gropup_privious_Click(object sender, EventArgs e)
        {
            if (Config.Instance.FieldTables.Count > 0 &&
                this._currentGroupIndex > 0)
            {
                this._currentGroupIndex -= 1;

                // グループの変更をGUI に適用
                this.applyChangedGroupConfigToGUI();

                // Control のタブを再構築
                this.rebuidControlTab();
            }
        }

        private void button_group_next_Click(object sender, EventArgs e)
        {
            if (Config.Instance.FieldTables.Count > 0 &&
                this._currentGroupIndex < Config.Instance.FieldTables.Count - 1)
            {
                this._currentGroupIndex += 1;

                // グループの変更をGUI に適用
                this.applyChangedGroupConfigToGUI();

                // Control のタブを再構築
                this.rebuidControlTab();
            }
        }


        private void button_field_up_Click(object sender, EventArgs e)
        {
            int idx = this.dataGridView_fields.SelectedCells.Count < 0 ? -1 : this.dataGridView_fields.SelectedCells[0].RowIndex;

            // 上下限チェック
            if (idx < 1 ||
                this.dataGridView_fields.Rows[idx].IsNewRow ||
                this._bindingLoggerFields.Count < 2) return;
            
            // 要素のコピー
            var current = this._bindingLoggerFields[idx];

            // 挿入
            this._bindingLoggerFields.Insert(idx - 1, current);

            // 削除 
            this._bindingLoggerFields.RemoveAt(idx + 1);

            this.dataGridView_fields.CurrentCell = this.dataGridView_fields[this.dataGridView_fields.CurrentCell.ColumnIndex, idx -1];
        }

        private void button_field_down_Click(object sender, EventArgs e)
        {
            int idx = this.dataGridView_fields.CurrentRow?.Index ?? -1;

            // 上下限チェック
            if (idx == -1 ||
                this._bindingLoggerFields.Count < 2 ||
                idx >= (this._bindingLoggerFields.Count - 1)) return;

            // 要素のコピー
            var target = this._bindingLoggerFields[idx + 1];

            // 挿入
            this._bindingLoggerFields.Insert(idx, target);

            // 削除 
            this._bindingLoggerFields.RemoveAt(idx + 2);

            this.dataGridView_fields.CurrentCell = this.dataGridView_fields[this.dataGridView_fields.CurrentCell.ColumnIndex, idx +1];
        }


        private void button_encorder_start_Click(object sender, EventArgs e) => this._timer_encorderRun.Enabled = true;

        private void button_encorder_stop_Click(object sender, EventArgs e) => this._timer_encorderRun.Enabled = false;

        private void button_encorder_trigger_Click(object sender, EventArgs e)
        {
            using (var dlg = new PredicateEditDialog())
            {
                if ((Config.Instance?.Encorder?.Trigger?.Count ?? -1) > 0)
                {
                    dlg.Value = Config.Instance.Encorder.Trigger;
                }

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Config.Instance.Encorder.Trigger = dlg.Value.ToList();
                }
            }
        }

        #endregion


        #region region - event : MXComponent


        private void mx_MonitoringDeviceValueChanged(object sender, UtilityLibrary.Mitsubishi.MXComponent.MXComponentOperatorMonitoringDeviceValueChangedEventArgs e)
        {
            if (e.ChangedDevice.CurrentValue == null) return;

            var surveyTarget =
                (from table in Config.Instance.FieldTables
                 from field in table.FieldList
                 where field.PredicationList != null
                 let idx = field.PredicationList.FindIndex(x => x.DeviceName == e.ChangedDevice.DeviceName)
                 where idx != -1
                 select field).ToList();


            // 動的に条件判断するFunc    ※デバッグしやすいようにラムダ式にしておく
            Func<List<PredicationFieldFormat>, bool> func_predicate = (arg) =>
             {
                 #region region

                 var left_type = e.ChangedDevice.CurrentValue?.GetType();
                 var left = System.Linq.Expressions.Expression.Constant(e.ChangedDevice.CurrentValue, left_type);

                 // 全体のbody
                 var body_whole = default(System.Linq.Expressions.Expression);

                 // Object との比較ができないので、Expression に工夫をする
                 for (int i = 0; i < arg.Count; i++)
                 {
                     var right_type = arg[i].Value.GetType();
                     var right = System.Linq.Expressions.Expression.Constant(arg[i].Value, right_type);

                     // 今回のループの body
                     var body = default(System.Linq.Expressions.BinaryExpression);

                     // 条件によって今回の body を生成
                     switch (arg[i].CompareType)
                     {
                         #region reiong - CompareType の処理                        

                         case CompareType.GreaterThan:
                             body = System.Linq.Expressions.Expression.GreaterThan(left, right);
                             break;
                         case CompareType.GreaterThanOrEqual:
                             body = System.Linq.Expressions.Expression.GreaterThanOrEqual(left, right);
                             break;
                         case CompareType.LessThan:
                             body = System.Linq.Expressions.Expression.LessThan(left, right);
                             break;
                         case CompareType.LessThanOrEqual:
                             body = System.Linq.Expressions.Expression.LessThanOrEqual(left, right);
                             break;
                         case CompareType.Equal:
                             body = System.Linq.Expressions.Expression.Equal(left, right);
                             break;
                         case CompareType.NotEqual:
                             System.Linq.Expressions.Expression.NotEqual(left, right);
                             break;
                         default:
                             body = System.Linq.Expressions.Expression.Equal(left, right);
                             break;

                             #endregion
                     }

                     switch (i)
                     {
                         #region region - ConditionType の処理

                         case 0:
                             body_whole = body;
                             break;

                         default:
                             var lastConditionEnum = arg.ElementAt(i - 1).ConditionType;

                             switch (lastConditionEnum)
                             {
                                 case ConditionType.And:
                                     body_whole = System.Linq.Expressions.Expression.And(body_whole, body);
                                     break;

                                 case ConditionType.Or:
                                 default:
                                     body_whole = System.Linq.Expressions.Expression.Or(body_whole, body);
                                     break;
                             }
                             break;

                             #endregion
                     }
                 }

                 var express = System.Linq.Expressions.Expression.Lambda<Func<bool>>(body_whole);
                 var lamda = express.Compile();

                 #endregion

                 return lamda.Invoke();
             };


            if (Config.Instance.Encorder.Trigger.Any(x => x.DeviceName == e.ChangedDevice.DeviceName))
            {
                // 19.02.12 追加
                if (this.radioButton_encorder_trigger.Checked && Config.Instance.Encorder.Trigger != null)
                    this._timer_encorderRun.Enabled = func_predicate(Config.Instance.Encorder.Trigger);
            }


            foreach (var field in surveyTarget)
            {
                //this._mx.SetDevice(field.DeviceName, func_predicate(field.PredicationList) ? 1 : 0);
                // ↓
                bool bVal = func_predicate(field.PredicationList);
                this._mx.SetDevice(field.DeviceName, bVal);
            }
        }


        private void mx_EventCallback(object sender, UtilityLibrary.Mitsubishi.MXComponent.MXLogOperatorEventArgs e)
        {
            if (e.Ex != null)
            {
                this.showMessage(string.Format("例外エラー：{0}", e.Ex.Message));
            }
            else
            {
                this.showMessage(e.Message +
                    (e.ActProgReturnCode != 0 ? string.Format(" return code:{0:x8}", e.ActProgReturnCode) : ""));
            }
        }


        #endregion


        #region region - event : MXControlPannel

        private void plcDeviceControlPannelEvent(object sender, PCLDeviceControlPannelEventArgs e)
        {

            // 19.01.29 追加
            if (this._isBeingInitialised) return;

            if (!this._mx.IsOpen)
                this.showMessage("オープンしていません");
            else
                switch (e.Value)
                {
                    case int iVal:
                        this._mx.SetDevice(e.DeviceName, iVal);
                        // ↓ 19.04.26_1 [ver0.2.2.0] 修正
                        this._mx.SetDevice(e.DeviceName, BitConverter.GetBytes(iVal));
                        break;

                    case short shVal:
                        //this._mx.SetDevice(e.DeviceName, shVal);
                        this._mx.SetDevice(e.DeviceName, shVal);
                        break;

                    case uint uiVal:
                        //this._mx.SetDevice(e.DeviceName, uiVal);
                        this._mx.SetDevice(e.DeviceName, BitConverter.GetBytes(uiVal));
                        break;

                    case ushort ushVal:
                        //this._mx.SetDevice(e.DeviceName, ushVal);
                        this._mx.SetDevice(e.DeviceName, BitConverter.GetBytes(ushVal));
                        break;

                    case float fVal:
                        this._mx.SetDevice(e.DeviceName, fVal);
                        break;

                    default:
                        throw new ArgumentException("対象外の型です。");
                }
        }

        private void DebuggerMainDialog_PredicateEditButtonClicked(object sender, PCLDeviceControlPannelPredicateEditButtonClickedEventArgs e)
        {
            using (var dlg = new PredicateEditDialog())
            {
                int idx = this._bindingLoggerFields.ToList().FindIndex(x => x.DeviceName == e.DeviceName);

                dlg.Value = idx == -1 || this._bindingLoggerFields[idx].PredicationList == null ?
                    new List<PredicationFieldFormat>() : this._bindingLoggerFields[idx].PredicationList;

                if (idx != -1 && dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this._bindingLoggerFields[idx].PredicationList = dlg.Value.ToList();
                    this.applySurveillance_to_MX();
                }
            }
        }

        #endregion


        #region region - event others


        private void numericUpDown_degree_ValueChanged(object sender, EventArgs e)
        {
            if (this._isBeingInitialised) return;

            /*
            if (!Int32.TryParse(this.textBox_degree.Text, out int iVal))
                iVal = 0;
                */

            int iVal = (int)this.numericUpDown_degree.Value;

            Action act_set_degree_to_numericUpDown = () =>
            {
                #region


                this._isBeingInitialised = true;

                this.numericUpDown_degree.Value = iVal;

                this._isBeingInitialised = false;

                #endregion
            };

            if (360 < iVal)
            {
                iVal -= 360;
                act_set_degree_to_numericUpDown();
            }
            if (iVal < 0)
            {
                iVal += 360;
                act_set_degree_to_numericUpDown();
            }



            this.setDegree(iVal);
        }

        private void radioButton_encorder_CheckedChanged(object sender, EventArgs e)
        {
            if (this._timer_encorderRun.Enabled) this._timer_encorderRun.Stop();

            // GUI の有効性変更
            this.numericUpDown_degree.Enabled = this.radioButton_encorder_direct.Checked;
            this.button_encorder_start.Enabled = this.radioButton_encorder_button.Checked;
            this.button_encorder_stop.Enabled = this.radioButton_encorder_button.Checked;
            this.button_encorder_trigger.Enabled = this.radioButton_encorder_trigger.Checked;

            // トリガのチェック状況変更で監視デバイスを変更する必要がある
            this.applySurveillance_to_MX();
        }


        private void tabControl_fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl_field.SelectedIndex == -1) return;

            if (this.tabControl_field.TabPages[this.tabControl_field.SelectedIndex] == this.tabPage_fields_control)
            {
                this.rebuidControlTab();
            }
            /*
            else if (this.tabControl.TabPages[this.tabControl.SelectedIndex] == this.tabPage_table)
            {
                this.rebuidTable();
            }*/

        }


        private void timer_encorderRun_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            double elapsedTime = this._timer_encorderRun.Interval / 1000.0f;

            double rotateRate = (float)this.numericUpDown_encorder_rpm.Value / 60.0f;

            int addingDegree = (int)(elapsedTime * rotateRate * 360.0f);


            this.Invoke(new System.Windows.Forms.MethodInvoker(() =>
            {
                //this._isBeingInitialised = true;

                // 角度の値を変更
                if (this.radioButton_cw.Checked)
                    this.numericUpDown_degree.Value += addingDegree;
                else
                    this.numericUpDown_degree.Value -= addingDegree;

                //this._isBeingInitialised = false;

               // this.setDegree((int)this.numericUpDown_degree.Value);

            }));
        }


        private void MainDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._mx.IsOpen) this._mx.Close();

            this.applyGUIData_to_Config();

            //Config.SaveToXmlFile();
        }




        #endregion

    }
}
