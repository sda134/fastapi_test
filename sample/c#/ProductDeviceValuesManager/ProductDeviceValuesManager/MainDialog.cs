using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi.MXComponent;

namespace ProductDeviceValuesManager
{
    public partial class MainDialog : Form
    {

        #region region - private member

        
        private int _currentProductIndex = -1;
        private int _currentGroupIndex = -1;
        private int _currentGroupIndex_text = -1;

        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProg _actProg;
        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType _simulatorType;

        private ProductSettingFormat _currentData;
        private BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat> _groupFieldBindingList;

        private BindingList<TextFieldFormat> _textFieldBindingList;

        #endregion


        public MainDialog()
        {
            InitializeComponent();

            this._actProg = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProg();

            this.dataGridView_device.AutoGenerateColumns = false;
            this.dataGridView_text.AutoGenerateColumns = false;

            // バージョン表示
            this.Text += " ver:" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;
        }



        #region region - private methods

        private void showMessage(string msg)
        {
            string dtStr = DateTime.Now.ToString("HH:mm:ss");
            this.messageTextBox1.Text = string.Format("{0} - {1}", dtStr, msg);
        }

        private void showMessage(string format, params object[] arg0)
        {
            this.showMessage(string.Format(format, arg0));
        }

        private void apply_GUIdata_to_privateInstance()
        {
            if (this._currentData == null) return;

            this._currentData.ProductName = this.label_fileName_display.Text;
        }

        private void apply_currentGroupData_to_GUI()
        {
            // null 対策
            bool groupIsNotSelectedYet =           
                this._currentData == null ||
                this._currentProductIndex == -1 ||
                this._currentGroupIndex == -1;

            // 現在のグループのレコードリスト
            this._groupFieldBindingList = groupIsNotSelectedYet ?
                new BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>() :        
                new BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>
                    (this._currentData
                    .SingleProductsData[this._currentProductIndex]
                    .PropertyGroups[this._currentGroupIndex].Fields);






            // dataGridView 表示の更新
            this.dataGridView_device.DataSource = this._groupFieldBindingList;

            // グループ名
            this.label_groupName_display_dev.Text = groupIsNotSelectedYet ?
                "" : this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex].GroupName;

            // 現在選択中のグループ番号の表示
            this.label_groupIndex_dev.Text = string.Format("{0} / {1}",
                this._currentGroupIndex + 1,
                groupIsNotSelectedYet ? 0 : this._currentData.SingleProductsData?[this._currentProductIndex].PropertyGroups?.Count ?? 0
                );
        }

        private void apply_privateInstanceData_to_GUI()
        {
            this.label_fileName_display.Text = this._currentData.ProductName;


            #region region - product

            //if (this._currentProductIndex == -1) return;
            // ↓ 19.01.07 変更

            // Product適用

            //this.label_name_display.Text = this._currentData.SingleProductsData[this._currentProductIndex].RecordName;
            //this.label_sirial_display.Text = this._currentData.SingleProductsData[this._currentProductIndex].SerialCode;
            // ↓ 19.01.07 変更
            this.label_name_display.Text = this._currentProductIndex == -1 ?
                "" : this._currentData.SingleProductsData[this._currentProductIndex].RecordName;

            this.label_sirial_display.Text = this._currentProductIndex == -1 ?
                "" : this._currentData.SingleProductsData[this._currentProductIndex].SerialCode;

            // 19.01.10 追加
            this.label_reference_display.Text = this._currentProductIndex == -1 ?
                "" : this._currentData.SingleProductsData[this._currentProductIndex].Reference;


            #endregion


            #region region - group


            // Group適用
            this.apply_currentGroupData_to_GUI();

            #endregion

        }

        private void set_currentData_as_default()
        {
            // null 対策
            if (this._currentData == null ||
                this._currentProductIndex == -1) return;

            // 確認メッセージ
            if (System.Windows.Forms.MessageBox.Show(
                "現在の値をデフォルトとして設定します。\r\nよろしいですか？",
                "確認",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._currentData.DefaultData = 
                    (SingleProductFormat)this._currentData.SingleProductsData[this._currentProductIndex].Clone();
            }
        }

        private void set_currentGroup_as_default()
        {
            // null 対策
            if (this._currentData == null ||
                this._currentProductIndex == -1 ||
                this._currentGroupIndex == -1) return;


            // 確認メッセージ
            if (System.Windows.Forms.MessageBox.Show(
                "現在のグループの値をデフォルトとして設定します。\r\nよろしいですか？",
                "確認",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {


                int gpIdx = this._currentData.DefaultData.PropertyGroups.FindIndex
                    (x => x.GroupName == this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex].GroupName);

                if (gpIdx != -1)
                {
                    /*
                    this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex].Fields
                    .ForEach(x =>
                    {
                        int idx = this._currentData.DefaultData.PropertyGroups[gpIdx].Fields.FindIndex(y => y.DeviceName == x.DeviceName);

                        if (idx == -1) return;

                        this._currentData.DefaultData.PropertyGroups[gpIdx].Fields[idx] = (Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat)x.Clone();
                    });
                    */ // ↓ 19.01.08 変更　要素数が異なる可能性もあるので、シンプルにコピーした方がいい
                    this._currentData.DefaultData.PropertyGroups[gpIdx].Fields =
                        this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex].Fields;

                    this.showMessage("現在のグループの値をデフォルト値として設定しました。");
                }
            }
        }

        private void set_act_conneTarget_as_simulator(ref ActProg actProg)
        {
            switch (this._simulatorType)
            {
                case ActUnitType.SIMULATOR:
                case ActUnitType.SIMULATOR2:
                case ActUnitType.SIMULATOR3:
                    actProg.ActProtocolType = (int)ActProtocolType.PROTOCOL_SHAREDMEMORY;
                    actProg.ActUnitType = (int)this._simulatorType;
                    break;

                default:
                    break;
            }
        }


        #endregion



        #region region - event : buttons

        /* 19.05.06 ???
        private void button_product_edit_Click(object sender, EventArgs e)
        {
            if (this._currentData == null) return;

            using (var dlg = new SingleProductDataEditDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }
        }
*/

        private void button_defaultValue_edit_Click(object sender, EventArgs e)
        {
            if (this._currentData == null) return;

            using (var dlg = new DefaultRecordDataEditDialog())
            {
                if (this._currentData.DefaultData != null)
                    dlg.Value = this._currentData.DefaultData;

               //resume // 値の代入まではできているらしい

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._currentData.DefaultData = dlg.Value;
                }
            }
        }


        private void button_products_edit_Click(object sender, EventArgs e)
        {
            if (this._currentData == null) return;

            using (var dlg = new SingleProductsSelectDialog())
            {
                dlg.DefaultData = this._currentData.DefaultData;
                dlg.Value = this._currentData.SingleProductsData;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._currentData.SingleProductsData = dlg.Value.ToList();

                    this._currentProductIndex = dlg.SelectedIndecies.Count() < 1 ?                    
                        -1 : dlg.SelectedIndex;

                    this._currentGroupIndex =
                        this._currentProductIndex == -1 || this._currentData.SingleProductsData?[this._currentProductIndex].PropertyGroups?.Count < 1 ?
                        -1 : 0;

                    // シーケンサからデータを読み直した方がいい？ 18.12.28
                    this.apply_privateInstanceData_to_GUI();
                    this.apply_currentGroupData_to_GUI_text();
                }
            }
        }


        private void button_group_previous_Click(object sender, EventArgs e)
        {
            if (this._currentData == null ||    
                this._currentProductIndex == -1) return;

            if (this._currentData
                .SingleProductsData[this._currentProductIndex]
                .PropertyGroups.Count > 0 && this._currentGroupIndex > 0)
            {
                this._currentGroupIndex--;

                this.apply_currentGroupData_to_GUI();
            }
        }


        private void button_group_next_Click(object sender, EventArgs e)
        {
            if (this._currentData == null ||
                this._currentProductIndex == -1) return;

            if (this._currentData
                .SingleProductsData[this._currentProductIndex]
                .PropertyGroups.Count > 0 &&
                this._currentGroupIndex < this._currentData
                .SingleProductsData[this._currentProductIndex]
                .PropertyGroups.Count - 1)
            {
                this._currentGroupIndex++;

                this.apply_currentGroupData_to_GUI();
            }
        }


        private void button_read_all_Click(object sender, EventArgs e)
        {
            // null 対策
            if (this._currentData == null ||
                this._currentProductIndex == -1)  return;



            // 対象デバイスの検出
            var targetFields = (from gp in this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups
                               from fld in gp.Fields
                               select fld).ToArray();

            // actProg の Open
            this._actProg.Setting = this._currentData.SingleProductsData[this._currentProductIndex].ActControlSetting;

            // 19.01.31 追加　シミュレータとの通信設定
            if (this._simulatorType != ActUnitType.Default)
                this.set_act_conneTarget_as_simulator(ref this._actProg);

            int ret_open = this._actProg.Open();

            // 読み込み
            if (ret_open == 0)
            {
                try
                {
                    int ret_read = this._actProg.ReadDeviceRandom(ref targetFields);

                    if (ret_read != 0)
                        this.showMessage("読み込み失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_read) ??
                        string.Format("{0:x8}", ret_read)));
                    else
                    {
                        this.showMessage("読み込みに成功しました。");
                        this.apply_currentGroupData_to_GUI();
                    }
                }
                catch (Exception ex)
                {
                    this.showMessage("読み込み失敗: {0}", ex.Message);
                }

                int ret_close = this._actProg.Close();
                if (ret_close != 0)
                    this.showMessage("Close失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_close) ??
                        string.Format("{0:x8}", ret_close)));
            }
            else
                this.showMessage("Open失敗:" +
                    (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_open) ??
                    string.Format("{0:x8}", ret_open)));
        }


        private void button_read_group_Click(object sender, EventArgs e)
        {
            // null 対策
            if (this._currentData == null ||
                this._currentProductIndex == -1 ||
                this._currentGroupIndex == -1) return;

            // 対象デバイスの検出
            var targetFields = this._currentData
                .SingleProductsData[this._currentProductIndex]
                .PropertyGroups[this._currentGroupIndex].Fields.ToArray();

            // actProg の Open
            this._actProg.Setting = this._currentData.SingleProductsData[this._currentProductIndex].ActControlSetting;

            // 19.01.31 追加　シミュレータとの通信設定
            if (this._simulatorType != ActUnitType.Default)
                this.set_act_conneTarget_as_simulator(ref this._actProg);

            int ret_open = this._actProg.Open();

            // 読み込み
            if (ret_open == 0)
            {
                try
                {
                    int ret_read = this._actProg.ReadDeviceRandom(ref targetFields);

                    if (ret_read != 0)
                        this.showMessage("読み込み失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_read) ??
                        string.Format("{0:x8}", ret_read)));
                    else
                    {
                        this.showMessage("読み込みに成功しました。");
                        this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex].Fields = targetFields.ToList();
                        this.apply_currentGroupData_to_GUI();
                    }
                }
                catch (Exception ex)
                {
                    this.showMessage("読み込み失敗: {0}", ex.Message);
                }

                int ret_close = this._actProg.Close();
                if (ret_close != 0)
                    this.showMessage("Close失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_close) ??
                        string.Format("{0:x8}", ret_close)));                    
            }
            else
                this.showMessage("Open失敗:" +
                    (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_open) ??
                    string.Format("{0:x8}", ret_open)));
        }


        private void button_write_group_Click(object sender, EventArgs e)
        {
            if (this._currentData == null ||
                this._currentProductIndex == -1 ||
                this._currentGroupIndex == -1) return;

            //
            this.apply_GUIdata_to_privateInstance();


            /*var targetFields = this._currentData
                .SingleProductsData?[this._currentProductIndex]
                .PropertyGroups?[this._currentGroupIndex].Fields.ToArray(); */
            // ↓19.01.07 書き込み後にBCDの値が変わるバグ発生。どうやら、Listは別インスタンスでも要素は同じ参照オブジェクトらしい
            var targetFields = this._currentData.SingleProductsData?[this._currentProductIndex]
                .PropertyGroups?[this._currentGroupIndex]
                .Fields.Select(x => (Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat)x.Clone());


            // actProg の Open
            this._actProg.Setting = this._currentData.SingleProductsData[this._currentProductIndex].ActControlSetting;

            // 19.01.31 追加　シミュレータとの通信設定
            if (this._simulatorType != ActUnitType.Default)
                this.set_act_conneTarget_as_simulator(ref this._actProg);

            int ret_open = this._actProg.Open();

            // 書き込み
            if (ret_open == 0)
            {
                try
                {
                    int ret_write = this._actProg.WriteDeviceRandom(targetFields.ToArray());

                    if (ret_write != 0)
                        this.showMessage("書き込み失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_write) ??
                        string.Format("{0:x8}", ret_write)));
                    else
                    {
                        this.showMessage("書き込みに成功しました。");
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debugger.Break();
#endif
                    this.showMessage("書き込み失敗: {0}", ex.Message);
                }


                int ret_close = this._actProg.Close();
                if (ret_close != 0)
                    this.showMessage("Close失敗:" +
                        (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_close) ??
                        string.Format("{0:x8}", ret_close)));
            }
            else
                this.showMessage("Open失敗:" +
                    (Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(ret_open) ??
                    string.Format("{0:x8}", ret_open)));
        }


        private void button_read_default_Click(object sender, EventArgs e)
        {
            // デフォルト値の読み込み
            // 1. 通常は現在のグループ内の値のみ適用
            // 2. 通常は現在のグループ内の値のみ適用


            if (this._currentData == null)
                return;
            else if (this._currentData.DefaultData == null)
            {
                MessageBox.Show("デフォルト値が設定されていません。");
                return;
            }


            if (this._currentProductIndex == -1) return;


            if (MessageBox.Show(
                text: "デフォルト値を読み込んで適用しますか？",
                caption: "確認",
                buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int currentGpIdx = 0;

                // デフォルトにあって現在編集中にないグループが存在したら追加する
                foreach (var defGp in this._currentData.DefaultData.PropertyGroups)
                {
                    int idx = this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups.FindIndex(x => x.GroupName == defGp.GroupName);

                    if (idx == -1)
                    {
                        this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups
                            .Insert(currentGpIdx, (PropertyGroupFormat)defGp.Clone());

                        this.showMessage("デフォルトからグループ [{0}] をコピーして追加しました。", defGp.GroupName);
                        currentGpIdx++;
                    }
                    else
                    {
                        currentGpIdx = idx + 1;
                    }
                }


                int currentGpIdx_text = 0;

                // デフォルトにあって現在編集中にないグループが存在したら追加する
                foreach (var defTxGp in this._currentData.DefaultData.TextFieldGroups)
                {
                    int idx = this._currentData.SingleProductsData[this._currentProductIndex].TextFieldGroups.FindIndex(x => x.GroupName == defTxGp.GroupName);

                    if (idx == -1)
                    {
                        this._currentData.SingleProductsData[this._currentProductIndex].TextFieldGroups
                            .Insert(currentGpIdx_text, (TextFieldGroupFormat)defTxGp.Clone());

                        this.showMessage("デフォルトからグループ [{0}] をコピーして追加しました。", defTxGp.GroupName);
                        currentGpIdx++;
                    }
                    else
                    {
                        currentGpIdx_text = idx + 1;
                    }
                }

                if (this._currentGroupIndex == -1)
                {
                    //this._currentData.SingleProductsData[this._currentProductIndex] = this._currentData.DefaultData;
                    //
                    this._currentData.SingleProductsData[this._currentProductIndex] = (SingleProductFormat)this._currentData.DefaultData.Clone();


                    // グループが０個とかあり得るのか？
                    System.Diagnostics.Debugger.Break();
                }
                else
                {
                    this._currentData.SingleProductsData[this._currentProductIndex].PropertyGroups[this._currentGroupIndex] =
                    //    this._currentData.DefaultData.PropertyGroups[this._currentGroupIndex];
                    // ↓ 19.01.07_1 シャローコピーで対応
                    // (PropertyGroupFormat)this._currentData.DefaultData.PropertyGroups[this._currentGroupIndex].Clone();
                    // ↓ 19.01.07_2 ディープコピーでないとダメな事が判明
                    UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<PropertyGroupFormat>(this._currentData.DefaultData.PropertyGroups[this._currentGroupIndex]);
                }

                this.apply_currentGroupData_to_GUI();
            }
        }


        #endregion


        #region region - event : menu

        private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new ProductSettingBasicDataEditDialog())
            {
                // 基本情報の入力
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // デフォルトフォルダが無ければ作成
                    if (!System.IO.Directory.Exists(ProductSetting.DirectoryPath))
                        System.IO.Directory.CreateDirectory(ProductSetting.DirectoryPath);

                    // dlg.Value が new したインスタンスになっている
                    this._currentData = dlg.Value;

                    // ProductIndex
                    this._currentProductIndex = (this._currentData.SingleProductsData?.Count ?? -1) < 1 ? -1 : 0;

                    // GroupIndex
                    this._currentGroupIndex =
                        this._currentProductIndex == -1 || (this._currentData.SingleProductsData?[this._currentProductIndex].PropertyGroups?.Count ?? -1) < 1 ?

                        -1 : 0;

                    // 現在の private インスタンスの値を各 GUI に代入
                    this.apply_privateInstanceData_to_GUI();
                }
            }           
        }


        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // デフォルトフォルダが無ければ作成
            if (!System.IO.Directory.Exists(ProductSetting.DirectoryPath))
                System.IO.Directory.CreateDirectory(ProductSetting.DirectoryPath);

            using (var dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = ProductSetting.DirectoryPath;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
#if NET45
            Task<bool> task = Task.Run(async () =>
            {
                _instance = null;
                _instance = await Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.DeserializeFromXmlFileAsync(Config.FileNameFullPath);

                return _instance != null;
            });
            return task.Result;
#else
                        bool bRet = UtilityLibrary.Data.XMLSerializeOperator<ProductSettingFormat>.DeserializeFromXmlFile
                            (dlg.FileName, out this._currentData);
#endif
                        // 読み込み結果
                        this.showMessage(string.Format("ファイルの読込みに{0}しました", bRet ? "成功" : "失敗"));

                        // ProductIndex
                        this._currentProductIndex = (this._currentData.SingleProductsData?.Count ?? -1) < 1 ? -1 : 0;

                        // GroupIndex
                        this._currentGroupIndex =
                            this._currentProductIndex == -1 || (this._currentData.SingleProductsData?[this._currentProductIndex].PropertyGroups?.Count ?? -1) < 1 ?
                            -1 : 0;

                        // 現在の private インスタンスの値を各 GUI に代入
                        this.apply_privateInstanceData_to_GUI();

                        // 19.05.06 [ver 0.2.0.0] 追加
                        this.apply_currentGroupData_to_GUI_text();
                    }
                    catch (Exception ex)
                    {
                        this.showMessage("ファイル読込み中にエラーが発生しました: {0}", ex.Message);
                    }
                }
            }
        }

        
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._currentData == null) return;


            // 編集中のデータを現在の private インスタンスに適用
            this.apply_GUIdata_to_privateInstance();

#if NET45
            Task<bool> task = Task.Run(async () =>
            {
                return await
                    Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.SerializeToXmlFileAsync(Config.FileNameFullPath, _instance);
            });

            return task.Result;
#else
            bool bRet = UtilityLibrary.Data.XMLSerializeOperator<ProductSettingFormat>.SerializeToXmlFile
                (ProductSetting.DirectoryPath + string.Format(@"\{0}.xml", this._currentData.ProductName), this._currentData);
#endif

            this.showMessage(string.Format("ファイルの保存に{0}しました。", bRet ? "成功" : "失敗"));                          
        }

        // 2021-02-26 新規追加
        private void 名前を付けて保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {

                dlg.FileName = string.Format(@"{0}.xml", this._currentData.ProductName);
                dlg.InitialDirectory = ProductSetting.DirectoryPath;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool bRet = UtilityLibrary.Data.XMLSerializeOperator<ProductSettingFormat>.SerializeToXmlFile
                        (dlg.FileName, this._currentData);

                    this.showMessage(string.Format("ファイルの保存に{0}しました。", bRet ? "成功" : "失敗"));
                }
            }
        }




        private void グループToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.set_currentGroup_as_default();
        }


        private void 全体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.set_currentData_as_default();
        }


        private void simulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._simulatorType = ActUnitType.SIMULATOR;
        }

        private void simulator2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._simulatorType = ActUnitType.SIMULATOR2;
        }

        private void simulator3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._simulatorType = ActUnitType.SIMULATOR3;
        }

        private void 実機ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._simulatorType = ActUnitType.Default;
        }


        #endregion


        #region region - event : dataGridView

        /*
        private void dataGridView_device_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var dev = this._groupFieldBindingList[e.RowIndex];

            if (dev.CurrentValue is string)
            {
                // デバッグ用
                this.showMessage("string型です。");

                string valStr = (string)dev.CurrentValue;
                object val = null;

                switch (dev.DeviceFormatType)
                {
                    #region region - 型によって値変換

                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Signed16:
                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Bit:
                        {
                            if (Int16.TryParse(valStr, out short sVal))
                                val = sVal;
                            else
                                val = (short)0;
                        }
                        break;

                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                        {
                            if (Int32.TryParse(valStr, out int iVal))
                                val = iVal;
                            else
                                val = (int)0;
                        }
                        break;

                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                        {
                            if (Single.TryParse(valStr, out float sVal))
                                val = sVal;
                            else
                                val = 0f;
                        }
                        break;

                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                        {
                            if (UInt16.TryParse(valStr, out ushort ushVal))
                                val = ushVal;
                            else
                                val = (ushort)0;
                        }
                        break;
                    case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                        {
                            if (UInt32.TryParse(valStr, out uint uiVal))
                                val = uiVal;
                            else
                                val = (uint)0;
                        }
                        break;

                    default:
                        break;

                        #endregion
                }

                dev.CurrentValue = val;
            }
        }*/



        private void dataGridView_device_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView_device.Columns[e.ColumnIndex] == this.Column_detail &&
                e.RowIndex >= 0)
            {
                // どちらの方法でも良い
                var deviceInfo = (Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat)this.dataGridView_device.Rows[e.RowIndex].DataBoundItem;
                //var deviceInfo = this._groupFieldBindingList[e.RowIndex];

                e.ToolTipText = string.Format("名前：{0}\r\nデバイス：{1}\r\nデータ型：{2}",
                    deviceInfo.Detail,
                    deviceInfo.DeviceName,
                    deviceInfo.DeviceFormatType
                    );

                var tip = new ToolTip();
            }
        }

        private void dataGridView_device_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // ”値”列
            if (this.dataGridView_device.Columns[e.ColumnIndex] == this.Column_Device)
            {
                var cell = this.dataGridView_device[e.ColumnIndex, e.RowIndex];

                if (cell.Value is string)
                {
                    #region 19.02.22 [ver 0.1.2.2] DataGridViewで編集で string 型になってしった値の処理

                    string strVal = (string)cell.Value;

                    switch (this._groupFieldBindingList[e.RowIndex].DeviceFormatType)
                    {
                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Signed16:
                            {
                                cell.Value = Int16.TryParse(strVal, out short shVal) ? shVal : 0;
                            }
                            break;

                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                            {
                                cell.Value = Single.TryParse(strVal, out float fVal) ? fVal : 0.0f;
                            }
                            break;

                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD16:
                            {
                                cell.Value = UInt16.TryParse(strVal, out ushort ushVal) ? ushVal : ushort.MinValue;
                            }
                            break;

                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                        case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.BCD32:
                            {
                                cell.Value = UInt32.TryParse(strVal, out uint uiVal) ? uiVal : uint.MinValue;
                            }
                            break;

                        default:
                            {
                                cell.Value = Int32.TryParse(strVal, out int iVal) ? iVal : 0;
                            }
                            break;
                    }

                    #endregion
                }
            }
        }



        #endregion



        private void 接続先ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.simulatorToolStripMenuItem.Text = "　Simulator";
            this.simulator2ToolStripMenuItem.Text = "　Simulator2";
            this.simulator3ToolStripMenuItem.Text = "　Simulator3";
            this.実機ToolStripMenuItem.Text = "　実機";


            switch (this._simulatorType)
            {
                case Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR:
                    this.simulatorToolStripMenuItem.Text = "●Simulator";
                    break;
                case Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR2:
                    this.simulator2ToolStripMenuItem.Text = "●Simulator2";
                    break;
                case Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.SIMULATOR3:
                    this.simulator3ToolStripMenuItem.Text = "●Simulator3";
                    break;
                default:
                    this.実機ToolStripMenuItem.Text = "●実機";
                    break;
            }

            this.Update();
        }

        private void デフォルトと比較するToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw (new NotImplementedException());
        }

        #region region - 19.05.06 追加部分

        
        
        private void button_group_previous_text_Click(object sender, EventArgs e)
        {
            if (this._currentData == null ||
                this._currentProductIndex == -1) return;

            if (this._currentData
                .SingleProductsData[this._currentProductIndex]
                .TextFieldGroups.Count > 0 && this._currentGroupIndex_text > 0)
            {
                this._currentGroupIndex_text--;

                this.apply_currentGroupData_to_GUI_text();
            }
        }


        private void button_group_next_text_Click(object sender, EventArgs e)
        {
            if (this._currentData == null ||
                this._currentProductIndex == -1) return;

            if (this._currentData
                .SingleProductsData[this._currentProductIndex]
                .TextFieldGroups.Count > 0 &&
                this._currentGroupIndex < this._currentData
                .SingleProductsData[this._currentProductIndex]
                .TextFieldGroups.Count - 1)
            {
                this._currentGroupIndex_text++;

                this.apply_currentGroupData_to_GUI_text();
            }
        }


        private void apply_currentGroupData_to_GUI_text()
        {


            // null 対策
            bool groupIsNotSelectedYet =
                this._currentData == null ||
                this._currentProductIndex == -1 ||
                this._currentGroupIndex_text == -1;

            // 現在のグループのレコードリスト
            this._textFieldBindingList = groupIsNotSelectedYet ?
                new BindingList<TextFieldFormat>() :
                new BindingList<TextFieldFormat>
                    (this._currentData
                    .SingleProductsData[this._currentProductIndex]
                    .TextFieldGroups[this._currentGroupIndex_text].Fields);

            // dataGridView 表示の更新
            this.dataGridView_text.DataSource = this._textFieldBindingList;

            // グループ名
            this.label_groupName_display_text.Text = groupIsNotSelectedYet ?
                "" : this._currentData.SingleProductsData[this._currentProductIndex].TextFieldGroups[this._currentGroupIndex_text].GroupName;

            // 現在選択中のグループ番号の表示
            this.label_groupIndex_text.Text = string.Format("{0} / {1}",
                this._currentGroupIndex_text + 1,
                groupIsNotSelectedYet ? 0 : this._currentData.SingleProductsData?[this._currentProductIndex].TextFieldGroups?.Count ?? 0
                );
        }



        #endregion

    }
}
