namespace ProductDeviceValuesManager
{
    partial class SingleProductDataEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_groupName = new System.Windows.Forms.Label();
            this.textBox_groupName = new System.Windows.Forms.TextBox();
            this.button_group_next = new System.Windows.Forms.Button();
            this.button_group_previous = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_add_group = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.label_reference = new System.Windows.Forms.Label();
            this.label_recordName = new System.Windows.Forms.Label();
            this.label_serial = new System.Windows.Forms.Label();
            this.textBox_recordName = new System.Windows.Forms.TextBox();
            this.textBox_reference = new System.Windows.Forms.TextBox();
            this.textBox_serial = new System.Windows.Forms.TextBox();
            this.tabPage_device = new System.Windows.Forms.TabPage();
            this.dataGridView_deviceFields = new System.Windows.Forms.DataGridView();
            this.Column_DetailName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_DeviceFormatType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_CurrentValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_groupIndex = new System.Windows.Forms.Label();
            this.button_delete_group = new System.Windows.Forms.Button();
            this.tabPage_textFields = new System.Windows.Forms.TabPage();
            this.dataGridView_textFields = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_TextFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn_TextValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_TextRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_groupIndex_text = new System.Windows.Forms.Label();
            this.label_groupName_text = new System.Windows.Forms.Label();
            this.textBox_groupName_text = new System.Windows.Forms.TextBox();
            this.button_group_next_text = new System.Windows.Forms.Button();
            this.button_delete_group_text = new System.Windows.Forms.Button();
            this.button_add_group_text = new System.Windows.Forms.Button();
            this.button_group_previous_text = new System.Windows.Forms.Button();
            this.tabPage_connectionSetting = new System.Windows.Forms.TabPage();
            this.mxComponentConfigurationPannel1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel();
            this.tabControl.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.tabPage_device.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_deviceFields)).BeginInit();
            this.tabPage_textFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_textFields)).BeginInit();
            this.tabPage_connectionSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_groupName
            // 
            this.label_groupName.AutoSize = true;
            this.label_groupName.Location = new System.Drawing.Point(15, 13);
            this.label_groupName.Name = "label_groupName";
            this.label_groupName.Size = new System.Drawing.Size(55, 12);
            this.label_groupName.TabIndex = 8;
            this.label_groupName.Text = "グループ名";
            // 
            // textBox_groupName
            // 
            this.textBox_groupName.Location = new System.Drawing.Point(76, 10);
            this.textBox_groupName.Name = "textBox_groupName";
            this.textBox_groupName.Size = new System.Drawing.Size(229, 19);
            this.textBox_groupName.TabIndex = 21;
            // 
            // button_group_next
            // 
            this.button_group_next.Location = new System.Drawing.Point(359, 8);
            this.button_group_next.Name = "button_group_next";
            this.button_group_next.Size = new System.Drawing.Size(42, 23);
            this.button_group_next.TabIndex = 4;
            this.button_group_next.TabStop = false;
            this.button_group_next.Text = "＞";
            this.button_group_next.UseVisualStyleBackColor = true;
            this.button_group_next.Click += new System.EventHandler(this.button_group_next_Click);
            // 
            // button_group_previous
            // 
            this.button_group_previous.Location = new System.Drawing.Point(311, 8);
            this.button_group_previous.Name = "button_group_previous";
            this.button_group_previous.Size = new System.Drawing.Size(42, 23);
            this.button_group_previous.TabIndex = 5;
            this.button_group_previous.TabStop = false;
            this.button_group_previous.Text = "＜";
            this.button_group_previous.UseVisualStyleBackColor = true;
            this.button_group_previous.Click += new System.EventHandler(this.button_group_previous_Click);
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(423, 338);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 51;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(342, 338);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 50;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_add_group
            // 
            this.button_add_group.Location = new System.Drawing.Point(396, 184);
            this.button_add_group.Name = "button_add_group";
            this.button_add_group.Size = new System.Drawing.Size(75, 23);
            this.button_add_group.TabIndex = 26;
            this.button_add_group.Text = "グループ追加";
            this.button_add_group.UseVisualStyleBackColor = true;
            this.button_add_group.Click += new System.EventHandler(this.button_add_group_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage_device);
            this.tabControl.Controls.Add(this.tabPage_textFields);
            this.tabControl.Controls.Add(this.tabPage_connectionSetting);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(490, 320);
            this.tabControl.TabIndex = 18;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.label_reference);
            this.tabPage_general.Controls.Add(this.label_recordName);
            this.tabPage_general.Controls.Add(this.label_serial);
            this.tabPage_general.Controls.Add(this.textBox_recordName);
            this.tabPage_general.Controls.Add(this.textBox_reference);
            this.tabPage_general.Controls.Add(this.textBox_serial);
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(482, 294);
            this.tabPage_general.TabIndex = 2;
            this.tabPage_general.Text = "基本情報";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // label_reference
            // 
            this.label_reference.AutoSize = true;
            this.label_reference.Location = new System.Drawing.Point(19, 80);
            this.label_reference.Name = "label_reference";
            this.label_reference.Size = new System.Drawing.Size(29, 12);
            this.label_reference.TabIndex = 18;
            this.label_reference.Text = "備考";
            // 
            // label_recordName
            // 
            this.label_recordName.AutoSize = true;
            this.label_recordName.Location = new System.Drawing.Point(19, 25);
            this.label_recordName.Name = "label_recordName";
            this.label_recordName.Size = new System.Drawing.Size(29, 12);
            this.label_recordName.TabIndex = 17;
            this.label_recordName.Text = "名前";
            // 
            // label_serial
            // 
            this.label_serial.AutoSize = true;
            this.label_serial.Location = new System.Drawing.Point(19, 55);
            this.label_serial.Name = "label_serial";
            this.label_serial.Size = new System.Drawing.Size(41, 12);
            this.label_serial.TabIndex = 17;
            this.label_serial.Text = "シリアル";
            // 
            // textBox_recordName
            // 
            this.textBox_recordName.Location = new System.Drawing.Point(66, 22);
            this.textBox_recordName.Name = "textBox_recordName";
            this.textBox_recordName.Size = new System.Drawing.Size(197, 19);
            this.textBox_recordName.TabIndex = 11;
            // 
            // textBox_reference
            // 
            this.textBox_reference.Location = new System.Drawing.Point(66, 77);
            this.textBox_reference.Multiline = true;
            this.textBox_reference.Name = "textBox_reference";
            this.textBox_reference.Size = new System.Drawing.Size(197, 38);
            this.textBox_reference.TabIndex = 12;
            // 
            // textBox_serial
            // 
            this.textBox_serial.Location = new System.Drawing.Point(66, 52);
            this.textBox_serial.Name = "textBox_serial";
            this.textBox_serial.Size = new System.Drawing.Size(197, 19);
            this.textBox_serial.TabIndex = 12;
            // 
            // tabPage_device
            // 
            this.tabPage_device.Controls.Add(this.dataGridView_deviceFields);
            this.tabPage_device.Controls.Add(this.label_groupIndex);
            this.tabPage_device.Controls.Add(this.label_groupName);
            this.tabPage_device.Controls.Add(this.textBox_groupName);
            this.tabPage_device.Controls.Add(this.button_group_next);
            this.tabPage_device.Controls.Add(this.button_delete_group);
            this.tabPage_device.Controls.Add(this.button_add_group);
            this.tabPage_device.Controls.Add(this.button_group_previous);
            this.tabPage_device.Location = new System.Drawing.Point(4, 22);
            this.tabPage_device.Name = "tabPage_device";
            this.tabPage_device.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_device.Size = new System.Drawing.Size(482, 294);
            this.tabPage_device.TabIndex = 0;
            this.tabPage_device.Text = "デバイス";
            this.tabPage_device.UseVisualStyleBackColor = true;
            // 
            // dataGridView_deviceFields
            // 
            this.dataGridView_deviceFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_deviceFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_DetailName,
            this.Column_DeviceFormatType,
            this.Column_CurrentValue,
            this.Column_DeviceName});
            this.dataGridView_deviceFields.Location = new System.Drawing.Point(17, 39);
            this.dataGridView_deviceFields.Name = "dataGridView_deviceFields";
            this.dataGridView_deviceFields.RowHeadersWidth = 15;
            this.dataGridView_deviceFields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_deviceFields.RowTemplate.Height = 21;
            this.dataGridView_deviceFields.Size = new System.Drawing.Size(373, 239);
            this.dataGridView_deviceFields.TabIndex = 25;
            this.dataGridView_deviceFields.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_deviceFields_CellValidated);
            // 
            // Column_DetailName
            // 
            this.Column_DetailName.DataPropertyName = "Detail";
            this.Column_DetailName.HeaderText = "項目名";
            this.Column_DetailName.Name = "Column_DetailName";
            this.Column_DetailName.Width = 120;
            // 
            // Column_DeviceFormatType
            // 
            this.Column_DeviceFormatType.DataPropertyName = "DeviceFormatType";
            this.Column_DeviceFormatType.HeaderText = "データ型";
            this.Column_DeviceFormatType.Name = "Column_DeviceFormatType";
            this.Column_DeviceFormatType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_DeviceFormatType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_DeviceFormatType.Width = 90;
            // 
            // Column_CurrentValue
            // 
            this.Column_CurrentValue.DataPropertyName = "CurrentValue";
            this.Column_CurrentValue.FillWeight = 90F;
            this.Column_CurrentValue.HeaderText = "値";
            this.Column_CurrentValue.Name = "Column_CurrentValue";
            this.Column_CurrentValue.Width = 80;
            // 
            // Column_DeviceName
            // 
            this.Column_DeviceName.DataPropertyName = "DeviceName";
            this.Column_DeviceName.HeaderText = "デバイス名";
            this.Column_DeviceName.Name = "Column_DeviceName";
            this.Column_DeviceName.Width = 80;
            // 
            // label_groupIndex
            // 
            this.label_groupIndex.AutoSize = true;
            this.label_groupIndex.Location = new System.Drawing.Point(422, 13);
            this.label_groupIndex.Name = "label_groupIndex";
            this.label_groupIndex.Size = new System.Drawing.Size(31, 12);
            this.label_groupIndex.TabIndex = 19;
            this.label_groupIndex.Text = "0 / 0";
            // 
            // button_delete_group
            // 
            this.button_delete_group.Location = new System.Drawing.Point(396, 255);
            this.button_delete_group.Name = "button_delete_group";
            this.button_delete_group.Size = new System.Drawing.Size(75, 23);
            this.button_delete_group.TabIndex = 27;
            this.button_delete_group.Text = "グループ削除";
            this.button_delete_group.UseVisualStyleBackColor = true;
            this.button_delete_group.Click += new System.EventHandler(this.button_delete_group_Click);
            // 
            // tabPage_textFields
            // 
            this.tabPage_textFields.Controls.Add(this.dataGridView_textFields);
            this.tabPage_textFields.Controls.Add(this.label_groupIndex_text);
            this.tabPage_textFields.Controls.Add(this.label_groupName_text);
            this.tabPage_textFields.Controls.Add(this.textBox_groupName_text);
            this.tabPage_textFields.Controls.Add(this.button_group_next_text);
            this.tabPage_textFields.Controls.Add(this.button_delete_group_text);
            this.tabPage_textFields.Controls.Add(this.button_add_group_text);
            this.tabPage_textFields.Controls.Add(this.button_group_previous_text);
            this.tabPage_textFields.Location = new System.Drawing.Point(4, 22);
            this.tabPage_textFields.Name = "tabPage_textFields";
            this.tabPage_textFields.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_textFields.Size = new System.Drawing.Size(482, 294);
            this.tabPage_textFields.TabIndex = 3;
            this.tabPage_textFields.Text = "テキスト";
            this.tabPage_textFields.UseVisualStyleBackColor = true;
            // 
            // dataGridView_textFields
            // 
            this.dataGridView_textFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_textFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_TextFieldName,
            this.dataGridViewTextBoxColumn_TextValue,
            this.Column_TextRemark});
            this.dataGridView_textFields.Location = new System.Drawing.Point(15, 43);
            this.dataGridView_textFields.Name = "dataGridView_textFields";
            this.dataGridView_textFields.RowHeadersWidth = 15;
            this.dataGridView_textFields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_textFields.RowTemplate.Height = 21;
            this.dataGridView_textFields.Size = new System.Drawing.Size(373, 239);
            this.dataGridView_textFields.TabIndex = 33;
            // 
            // dataGridViewTextBoxColumn_TextFieldName
            // 
            this.dataGridViewTextBoxColumn_TextFieldName.DataPropertyName = "FieldName";
            this.dataGridViewTextBoxColumn_TextFieldName.HeaderText = "項目名";
            this.dataGridViewTextBoxColumn_TextFieldName.Name = "dataGridViewTextBoxColumn_TextFieldName";
            // 
            // dataGridViewTextBoxColumn_TextValue
            // 
            this.dataGridViewTextBoxColumn_TextValue.DataPropertyName = "FieldValue";
            this.dataGridViewTextBoxColumn_TextValue.FillWeight = 90F;
            this.dataGridViewTextBoxColumn_TextValue.HeaderText = "値";
            this.dataGridViewTextBoxColumn_TextValue.Name = "dataGridViewTextBoxColumn_TextValue";
            this.dataGridViewTextBoxColumn_TextValue.Width = 120;
            // 
            // Column_TextRemark
            // 
            this.Column_TextRemark.HeaderText = "備考";
            this.Column_TextRemark.Name = "Column_TextRemark";
            this.Column_TextRemark.Width = 140;
            // 
            // label_groupIndex_text
            // 
            this.label_groupIndex_text.AutoSize = true;
            this.label_groupIndex_text.Location = new System.Drawing.Point(420, 17);
            this.label_groupIndex_text.Name = "label_groupIndex_text";
            this.label_groupIndex_text.Size = new System.Drawing.Size(31, 12);
            this.label_groupIndex_text.TabIndex = 31;
            this.label_groupIndex_text.Text = "0 / 0";
            // 
            // label_groupName_text
            // 
            this.label_groupName_text.AutoSize = true;
            this.label_groupName_text.Location = new System.Drawing.Point(13, 17);
            this.label_groupName_text.Name = "label_groupName_text";
            this.label_groupName_text.Size = new System.Drawing.Size(55, 12);
            this.label_groupName_text.TabIndex = 30;
            this.label_groupName_text.Text = "グループ名";
            // 
            // textBox_groupName_text
            // 
            this.textBox_groupName_text.Location = new System.Drawing.Point(74, 14);
            this.textBox_groupName_text.Name = "textBox_groupName_text";
            this.textBox_groupName_text.Size = new System.Drawing.Size(229, 19);
            this.textBox_groupName_text.TabIndex = 32;
            // 
            // button_group_next_text
            // 
            this.button_group_next_text.Location = new System.Drawing.Point(357, 12);
            this.button_group_next_text.Name = "button_group_next_text";
            this.button_group_next_text.Size = new System.Drawing.Size(42, 23);
            this.button_group_next_text.TabIndex = 28;
            this.button_group_next_text.TabStop = false;
            this.button_group_next_text.Text = "＞";
            this.button_group_next_text.UseVisualStyleBackColor = true;
            this.button_group_next_text.Click += new System.EventHandler(this.button_textGroup_next_Click);
            // 
            // button_delete_group_text
            // 
            this.button_delete_group_text.Location = new System.Drawing.Point(394, 259);
            this.button_delete_group_text.Name = "button_delete_group_text";
            this.button_delete_group_text.Size = new System.Drawing.Size(75, 23);
            this.button_delete_group_text.TabIndex = 35;
            this.button_delete_group_text.Text = "グループ削除";
            this.button_delete_group_text.UseVisualStyleBackColor = true;
            this.button_delete_group_text.Click += new System.EventHandler(this.button_delete_textGroup_Click);
            // 
            // button_add_group_text
            // 
            this.button_add_group_text.Location = new System.Drawing.Point(394, 188);
            this.button_add_group_text.Name = "button_add_group_text";
            this.button_add_group_text.Size = new System.Drawing.Size(75, 23);
            this.button_add_group_text.TabIndex = 34;
            this.button_add_group_text.Text = "グループ追加";
            this.button_add_group_text.UseVisualStyleBackColor = true;
            this.button_add_group_text.Click += new System.EventHandler(this.button_add_group_text_Click);
            // 
            // button_group_previous_text
            // 
            this.button_group_previous_text.Location = new System.Drawing.Point(309, 12);
            this.button_group_previous_text.Name = "button_group_previous_text";
            this.button_group_previous_text.Size = new System.Drawing.Size(42, 23);
            this.button_group_previous_text.TabIndex = 29;
            this.button_group_previous_text.TabStop = false;
            this.button_group_previous_text.Text = "＜";
            this.button_group_previous_text.UseVisualStyleBackColor = true;
            this.button_group_previous_text.Click += new System.EventHandler(this.button_textGroup_previous_Click);
            // 
            // tabPage_connectionSetting
            // 
            this.tabPage_connectionSetting.Controls.Add(this.mxComponentConfigurationPannel1);
            this.tabPage_connectionSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPage_connectionSetting.Name = "tabPage_connectionSetting";
            this.tabPage_connectionSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_connectionSetting.Size = new System.Drawing.Size(482, 294);
            this.tabPage_connectionSetting.TabIndex = 1;
            this.tabPage_connectionSetting.Text = "通信設定";
            this.tabPage_connectionSetting.UseVisualStyleBackColor = true;
            // 
            // mxComponentConfigurationPannel1
            // 
            this.mxComponentConfigurationPannel1.Location = new System.Drawing.Point(6, 6);
            this.mxComponentConfigurationPannel1.Name = "mxComponentConfigurationPannel1";
            this.mxComponentConfigurationPannel1.Size = new System.Drawing.Size(470, 200);
            this.mxComponentConfigurationPannel1.TabIndex = 31;
            this.mxComponentConfigurationPannel1.Text = "MXComponent通信";
            this.mxComponentConfigurationPannel1.Value.ActControl = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl.TRC_DTR_OR_RTS;
            this.mxComponentConfigurationPannel1.Value.ActCpuType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType.Default;
            this.mxComponentConfigurationPannel1.Value.ActHostAddress = "192.168.1.1";
            this.mxComponentConfigurationPannel1.Value.ActProtocolType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_SERIAL;
            this.mxComponentConfigurationPannel1.Value.ActTimeOut = 10000;
            this.mxComponentConfigurationPannel1.Value.ActUnitType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.Default;
            this.mxComponentConfigurationPannel1.Value.BaudRate = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate.BAUDRATE_9600;
            this.mxComponentConfigurationPannel1.Value.DataBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits.DATABIT_7;
            this.mxComponentConfigurationPannel1.Value.ParityBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity.EVEN_PARITY;
            this.mxComponentConfigurationPannel1.Value.PortNumber = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber.PORT_1;
            this.mxComponentConfigurationPannel1.Value.StopBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits.STOPBIT_ONE;
            // 
            // SingleProductDataEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 369);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleProductDataEditDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "個別製品ごとの設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SingleProductDataEditDialog_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.tabPage_device.ResumeLayout(false);
            this.tabPage_device.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_deviceFields)).EndInit();
            this.tabPage_textFields.ResumeLayout(false);
            this.tabPage_textFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_textFields)).EndInit();
            this.tabPage_connectionSetting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label_groupName;
        protected System.Windows.Forms.Label label_recordName;
        protected System.Windows.Forms.Label label_groupIndex;
        protected System.Windows.Forms.Label label_serial;
        protected System.Windows.Forms.TextBox textBox_serial;
        protected System.Windows.Forms.TextBox textBox_recordName;
        protected System.Windows.Forms.TextBox textBox_groupName;
        protected System.Windows.Forms.Button button_ok;
        protected System.Windows.Forms.Button button_cancel;
        protected System.Windows.Forms.Button button_add_group;
        protected System.Windows.Forms.Button button_group_next;
        protected System.Windows.Forms.Button button_group_previous;
        protected System.Windows.Forms.TabPage tabPage_device;
        protected System.Windows.Forms.TabPage tabPage_connectionSetting;
        protected System.Windows.Forms.TabPage tabPage_general;
        protected System.Windows.Forms.TabControl tabControl;
        protected Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel mxComponentConfigurationPannel1;
        protected System.Windows.Forms.DataGridView dataGridView_deviceFields;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column_Detail;
        protected System.Windows.Forms.Button button_delete_group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_DetailName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_DeviceFormatType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_CurrentValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_DeviceName;
        protected System.Windows.Forms.TextBox textBox_reference;
        private System.Windows.Forms.Label label_reference;
        private System.Windows.Forms.TabPage tabPage_textFields;
        protected System.Windows.Forms.DataGridView dataGridView_textFields;
        protected System.Windows.Forms.Label label_groupIndex_text;
        protected System.Windows.Forms.Label label_groupName_text;
        protected System.Windows.Forms.TextBox textBox_groupName_text;
        protected System.Windows.Forms.Button button_group_next_text;
        protected System.Windows.Forms.Button button_delete_group_text;
        protected System.Windows.Forms.Button button_add_group_text;
        protected System.Windows.Forms.Button button_group_previous_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_TextFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_TextValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_TextRemark;
    }
}