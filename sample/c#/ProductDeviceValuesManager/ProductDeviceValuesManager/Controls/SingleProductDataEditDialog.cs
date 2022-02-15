using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductDeviceValuesManager
{
    public partial class SingleProductDataEditDialog : Form
    {
        protected BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat> _bindingDeviceFields;
        protected BindingList<TextFieldFormat> _bindingTextFields;


        protected List<PropertyGroupFormat> _propertyGroups;

        // 19.05.06 追加
        protected List<TextFieldGroupFormat> _textFieldGroups;

        protected int _currentGroupIndex_device = 0;
        protected int _currentGroupIndex_text = 0;




        public SingleProductDataEditDialog()
        {
            InitializeComponent();

            // デザイナで設定できない項目：DataGridView
            this.dataGridView_deviceFields.AutoGenerateColumns = false;
            this.dataGridView_textFields.AutoGenerateColumns = false;

            // デザイナで設定できない項目：DataGridViewColumns
            this.Column_DeviceFormatType.ValueType = typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType);
            this.Column_DeviceFormatType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType));

            // private member の生成
            this._propertyGroups = new List<PropertyGroupFormat>();
            this._propertyGroups.Add(new PropertyGroupFormat { Fields = new List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>() });

            this._bindingDeviceFields = new BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>();
            this.dataGridView_deviceFields.DataSource = this._bindingDeviceFields;

            this.apply_privateMemberData_to_GUI();


            // private member の生成: Text
            this._textFieldGroups = new List<TextFieldGroupFormat>();
            this._textFieldGroups.Add(new TextFieldGroupFormat { Fields = new List<TextFieldFormat>() });

            this._bindingTextFields = new BindingList<TextFieldFormat>();
            this.dataGridView_textFields.DataSource = this._bindingTextFields;

            this.apply_privateMemberData_to_GUI_text();
        }



        #region region - public properties


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual SingleProductFormat Value
        {
            set
            {
                this.textBox_recordName.Text = value.RecordName;
                this.textBox_serial.Text = value.SerialCode;

                //19.01.10_2 [ver 0.1.2.0] 追加
                this.textBox_reference.Text = value.Reference;


                //this._propertyGroups = value.PropertyGroups.Count < 1 ?
                // ↓ 19.01.08 [ver 0.1.0.1] 修正 
                this._propertyGroups = (value.PropertyGroups?.Count ?? -1) < 1 ?
                    new List<PropertyGroupFormat> { new PropertyGroupFormat { Fields = new List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>() } } :                   
                    new List<PropertyGroupFormat>(value.PropertyGroups);


                // 19.05.06 追加
                this._textFieldGroups = (value.TextFieldGroups?.Count ?? -1) < 1 ?
                    new List<TextFieldGroupFormat> { new TextFieldGroupFormat { Fields = new List<TextFieldFormat>() } } :
                    new List<TextFieldGroupFormat>(value.TextFieldGroups);




                this.mxComponentConfigurationPannel1.Value = value.ActControlSetting;

                this.apply_privateMemberData_to_GUI();
                this.apply_privateMemberData_to_GUI_text();
            }

            get => new SingleProductFormat
            {
                RecordName = this.textBox_recordName.Text,
                SerialCode = this.textBox_serial.Text,
                Reference = this.textBox_reference.Text,

                PropertyGroups = this._propertyGroups,
                ActControlSetting = this.mxComponentConfigurationPannel1.Value,

                // 19.05.06 追加
                TextFieldGroups = this._textFieldGroups,
            };
        }

        #endregion


        #region region - protected methods

        

        protected virtual void apply_guiData_to_privateMember()
        {
            this._propertyGroups[this._currentGroupIndex_device].GroupName = this.textBox_groupName.Text;

            this._propertyGroups[this._currentGroupIndex_device].Fields = this._bindingDeviceFields.ToList();
        }

        protected virtual void apply_privateMemberData_to_GUI()
        {
            this.textBox_groupName.Text = this._propertyGroups[this._currentGroupIndex_device].GroupName;
            this._bindingDeviceFields = new BindingList<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>(this._propertyGroups[this._currentGroupIndex_device].Fields);
            this.dataGridView_deviceFields.DataSource = this._bindingDeviceFields;

            this.label_groupIndex.Text = string.Format("{0} / {1}", _currentGroupIndex_device + 1, this._propertyGroups.Count);
        }

        #endregion


        #region region - event : buttons

        private void button_group_previous_Click(object sender, EventArgs e)
        {
            if (this._propertyGroups.Count > 0 && this._currentGroupIndex_device > 0)
            {
                // 現在のコントロールの値を private member に適用
                this.apply_guiData_to_privateMember();

                // index の変更
                this._currentGroupIndex_device -= 1;

                // 変更されたグループの値を 現在のコントロールの値 に格納
                this.apply_privateMemberData_to_GUI();
            }
        }

        private void button_group_next_Click(object sender, EventArgs e)
        {
            if (this._propertyGroups.Count > 0  && this._currentGroupIndex_device < this._propertyGroups.Count - 1)
            {
                // 現在のコントロールの値を private member に適用
                this.apply_guiData_to_privateMember();

                // index の変更
                this._currentGroupIndex_device += 1;

                // 変更されたグループの値を 現在のコントロールの値 に格納
                this.apply_privateMemberData_to_GUI();
            }
        }


        private void button_add_group_Click(object sender, EventArgs e)
        {
            // 現在のコントロールの値を private member に適用
            this.apply_guiData_to_privateMember();


            if(this._currentGroupIndex_device == this._propertyGroups.Count - 1)
                this._propertyGroups.Add(new PropertyGroupFormat { Fields = new List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>() });

            else// 新しい要素の挿入
                this._propertyGroups.Insert(this._currentGroupIndex_device, new PropertyGroupFormat { Fields = new List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat>()});

            // index の変更
            this._currentGroupIndex_device += 1;

            // 変更されたグループの値を 現在のコントロールの値 に格納
            this.apply_privateMemberData_to_GUI();
        }



        #endregion


        protected virtual void SingleProductDataEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(this.textBox_recordName.Text))
                {
                    MessageBox.Show("必要な項目が入力されていません。");
                    e.Cancel = true;
                }

                this.apply_guiData_to_privateMember();
            }
        }

        private void button_delete_group_Click(object sender, EventArgs e)
        {
            if ((this._propertyGroups?.Count ?? -1) < 1 ||
                _currentGroupIndex_device < 0) return;


            if ((this._propertyGroups.Count == 1))
            {
                MessageBox.Show("グループを０個にする事はできません。");
                return;
            }


            if (MessageBox.Show(
                text:"現在のグループを削除します。\r\nよろしいですか？",
                caption:"確認",
                buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 削除
                this._propertyGroups.RemoveAt(this._currentGroupIndex_device);

                // カウントダウン
                this._currentGroupIndex_device--;

                // 現在値の変更を GUI に適用する
                this.apply_privateMemberData_to_GUI();
            }
        }




        #region region - 19.05.06 追加　テキストメモ

        protected virtual void apply_guiData_to_privateMember_text()
        {
            this._textFieldGroups[this._currentGroupIndex_text].GroupName = this.textBox_groupName_text.Text;
            this._textFieldGroups[this._currentGroupIndex_text].Fields = this._bindingTextFields.ToList();
        }

        protected virtual void apply_privateMemberData_to_GUI_text()
        {
            //this._textFieldGroups.Add(new TextFieldGroupFormat { Fields = new List<TextFieldFormat>()});

            this.textBox_groupName_text.Text = this._textFieldGroups[this._currentGroupIndex_text].GroupName;

            this._bindingTextFields = new BindingList<TextFieldFormat>(this._textFieldGroups[this._currentGroupIndex_text].Fields);
            this.dataGridView_textFields.DataSource = this._bindingTextFields;

            this.label_groupIndex_text.Text = string.Format("{0} / {1}", _currentGroupIndex_text + 1, this._textFieldGroups.Count);
        }


        private void button_add_group_text_Click(object sender, EventArgs e)
        {
            // 現在のコントロールの値を private member に適用
            this.apply_guiData_to_privateMember_text();


            if (this._currentGroupIndex_text == this._textFieldGroups.Count - 1)
                this._textFieldGroups.Add(new TextFieldGroupFormat { Fields = new List<TextFieldFormat>() });

            else// 新しい要素の挿入
                this._textFieldGroups.Insert(this._currentGroupIndex_text, new TextFieldGroupFormat { Fields = new List<TextFieldFormat>() });

            // index の変更
            this._currentGroupIndex_text += 1;

            // 変更されたグループの値を 現在のコントロールの値 に格納
            this.apply_privateMemberData_to_GUI_text();
        }

        private void button_delete_textGroup_Click(object sender, EventArgs e)
        {
            if ((this._textFieldGroups?.Count ?? -1) < 1 ||
                this._currentGroupIndex_text < 0) return;


            if ((this._textFieldGroups.Count == 1))
            {
                MessageBox.Show("グループを０個にする事はできません。");
                return;
            }


            if (MessageBox.Show(
                text: "現在のグループを削除します。\r\nよろしいですか？",
                caption: "確認",
                buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 削除
                this._textFieldGroups.RemoveAt(this._currentGroupIndex_text);

                // カウントダウン
                this._currentGroupIndex_text--;

                // 現在値の変更を GUI に適用する
                this.apply_privateMemberData_to_GUI_text();
            }
        }

        private void button_textGroup_previous_Click(object sender, EventArgs e)
        {
            if (this._textFieldGroups.Count > 0 && this._currentGroupIndex_text > 0)
            {
                // 現在のコントロールの値を private member に適用
                this.apply_guiData_to_privateMember_text();

                // index の変更
                this._currentGroupIndex_text -= 1;

                // 変更されたグループの値を 現在のコントロールの値 に格納
                this.apply_privateMemberData_to_GUI_text();
            }
        }

        private void button_textGroup_next_Click(object sender, EventArgs e)
        {
            if (this._textFieldGroups.Count > 0 && this._currentGroupIndex_text < this._textFieldGroups.Count - 1)
            {
                // 現在のコントロールの値を private member に適用
                this.apply_guiData_to_privateMember_text();

                // index の変更
                this._currentGroupIndex_text += 1;

                // 変更されたグループの値を 現在のコントロールの値 に格納
                this.apply_privateMemberData_to_GUI_text();
            }
        }




        #endregion



        private void dataGridView_deviceFields_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = this.dataGridView_deviceFields[e.ColumnIndex, e.RowIndex];

            // "値" 列
            if (this.dataGridView_deviceFields.Columns[e.ColumnIndex] == this.Column_CurrentValue)
            {                
                if (cell.Value is string)
                {
                    #region 19.06.19 [ver 0.2.1.0] DataGridViewで編集で string 型になってしまった値の処理２

                    string strVal = (string)cell.Value;

                    switch (this._bindingDeviceFields[e.RowIndex].DeviceFormatType)
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
            //　"デバイス名" 列
            if (this.dataGridView_deviceFields.Columns[e.ColumnIndex] == this.Column_DeviceName)
            {
                string cellValStr = cell.Value?.ToString();

                // 全角文字が使われていない事の確認 [ver 0.3.0.0 : 2021-03-01] 
                if (cellValStr != null &&
                    !System.Text.RegularExpressions.Regex.IsMatch(cellValStr, @"^[a-zA-Z0-9]+$"))
                {
                    // 全角文字が使われた場合
                    // Step1. 変換可能な全角文字を全て半角文字に変換。 VB のdll を使わない限り，自作する羽目に合う
                    string halfStr = Microsoft.VisualBasic.Strings.StrConv(cellValStr, Microsoft.VisualBasic.VbStrConv.Narrow, 0);

                    // Step2. 残った全角文字（つまり半角に変換できなかったもの）は削除
                    string removedStr = System.Text.RegularExpressions.Regex.Replace(halfStr, @"[^a-zA-Z0-9\\]", string.Empty);

                    cell.Value = removedStr;
                }
            }
        }
    }
}
