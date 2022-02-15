namespace Mtec.Internal.Mitsubishi.MXLogger
{
    partial class GroupInfoEditDialog
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
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_groupName = new System.Windows.Forms.TextBox();
            this.label_groupName = new System.Windows.Forms.Label();
            this.label_triggerType = new System.Windows.Forms.Label();
            this.comboBox_triggerType = new System.Windows.Forms.ComboBox();
            this.comboBox_trigger_deviceFormatTypee = new System.Windows.Forms.ComboBox();
            this.groupBox_trigger = new System.Windows.Forms.GroupBox();
            this.numericUpDown_threshold = new System.Windows.Forms.NumericUpDown();
            this.label_compareType = new System.Windows.Forms.Label();
            this.label_dataType = new System.Windows.Forms.Label();
            this.label_threshold = new System.Windows.Forms.Label();
            this.label_deviceName = new System.Windows.Forms.Label();
            this.textBox_trigger_deviceName = new System.Windows.Forms.TextBox();
            this.comboBox_trigger_compareType = new System.Windows.Forms.ComboBox();
            this.groupBox_trigger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(234, 145);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 51;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(153, 145);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 50;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // textBox_groupName
            // 
            this.textBox_groupName.Location = new System.Drawing.Point(73, 16);
            this.textBox_groupName.Name = "textBox_groupName";
            this.textBox_groupName.Size = new System.Drawing.Size(202, 19);
            this.textBox_groupName.TabIndex = 10;
            // 
            // label_groupName
            // 
            this.label_groupName.AutoSize = true;
            this.label_groupName.Location = new System.Drawing.Point(12, 19);
            this.label_groupName.Name = "label_groupName";
            this.label_groupName.Size = new System.Drawing.Size(55, 12);
            this.label_groupName.TabIndex = 2;
            this.label_groupName.Text = "グループ名";
            // 
            // label_triggerType
            // 
            this.label_triggerType.AutoSize = true;
            this.label_triggerType.Location = new System.Drawing.Point(14, 48);
            this.label_triggerType.Name = "label_triggerType";
            this.label_triggerType.Size = new System.Drawing.Size(53, 12);
            this.label_triggerType.TabIndex = 7;
            this.label_triggerType.Text = "動作条件";
            // 
            // comboBox_triggerType
            // 
            this.comboBox_triggerType.FormattingEnabled = true;
            this.comboBox_triggerType.Location = new System.Drawing.Point(73, 45);
            this.comboBox_triggerType.Name = "comboBox_triggerType";
            this.comboBox_triggerType.Size = new System.Drawing.Size(202, 20);
            this.comboBox_triggerType.TabIndex = 11;
            this.comboBox_triggerType.SelectedIndexChanged += new System.EventHandler(this.comboBox_triggerType_SelectedIndexChanged);
            // 
            // comboBox_trigger_typeOfDevice
            // 
            this.comboBox_trigger_deviceFormatTypee.FormattingEnabled = true;
            this.comboBox_trigger_deviceFormatTypee.Location = new System.Drawing.Point(194, 14);
            this.comboBox_trigger_deviceFormatTypee.Name = "comboBox_trigger_typeOfDevice";
            this.comboBox_trigger_deviceFormatTypee.Size = new System.Drawing.Size(84, 20);
            this.comboBox_trigger_deviceFormatTypee.TabIndex = 22;
            this.comboBox_trigger_deviceFormatTypee.SelectedIndexChanged += new System.EventHandler(this.comboBox_trigger_typeOfDevice_SelectedIndexChanged);
            // 
            // groupBox_trigger
            // 
            this.groupBox_trigger.Controls.Add(this.numericUpDown_threshold);
            this.groupBox_trigger.Controls.Add(this.label_compareType);
            this.groupBox_trigger.Controls.Add(this.label_dataType);
            this.groupBox_trigger.Controls.Add(this.label_threshold);
            this.groupBox_trigger.Controls.Add(this.label_deviceName);
            this.groupBox_trigger.Controls.Add(this.textBox_trigger_deviceName);
            this.groupBox_trigger.Controls.Add(this.comboBox_trigger_deviceFormatTypee);
            this.groupBox_trigger.Controls.Add(this.comboBox_trigger_compareType);
            this.groupBox_trigger.Location = new System.Drawing.Point(16, 71);
            this.groupBox_trigger.Name = "groupBox_trigger";
            this.groupBox_trigger.Size = new System.Drawing.Size(293, 68);
            this.groupBox_trigger.TabIndex = 10;
            this.groupBox_trigger.TabStop = false;
            this.groupBox_trigger.Text = "トリガ";
            // 
            // numericUpDown_threshold
            // 
            this.numericUpDown_threshold.Location = new System.Drawing.Point(178, 40);
            this.numericUpDown_threshold.Name = "numericUpDown_threshold";
            this.numericUpDown_threshold.Size = new System.Drawing.Size(100, 19);
            this.numericUpDown_threshold.TabIndex = 23;
            // 
            // label_compareType
            // 
            this.label_compareType.AutoSize = true;
            this.label_compareType.Location = new System.Drawing.Point(8, 42);
            this.label_compareType.Name = "label_compareType";
            this.label_compareType.Size = new System.Drawing.Size(53, 12);
            this.label_compareType.TabIndex = 13;
            this.label_compareType.Text = "比較方式";
            // 
            // label_dataType
            // 
            this.label_dataType.AutoSize = true;
            this.label_dataType.Location = new System.Drawing.Point(143, 18);
            this.label_dataType.Name = "label_dataType";
            this.label_dataType.Size = new System.Drawing.Size(45, 12);
            this.label_dataType.TabIndex = 13;
            this.label_dataType.Text = "データ型";
            // 
            // label_threshold
            // 
            this.label_threshold.AutoSize = true;
            this.label_threshold.Location = new System.Drawing.Point(143, 42);
            this.label_threshold.Name = "label_threshold";
            this.label_threshold.Size = new System.Drawing.Size(29, 12);
            this.label_threshold.TabIndex = 12;
            this.label_threshold.Text = "閾値";
            // 
            // label_deviceName
            // 
            this.label_deviceName.AutoSize = true;
            this.label_deviceName.Location = new System.Drawing.Point(6, 18);
            this.label_deviceName.Name = "label_deviceName";
            this.label_deviceName.Size = new System.Drawing.Size(55, 12);
            this.label_deviceName.TabIndex = 12;
            this.label_deviceName.Text = "デバイス名";
            // 
            // textBox_trigger_deviceName
            // 
            this.textBox_trigger_deviceName.Location = new System.Drawing.Point(67, 15);
            this.textBox_trigger_deviceName.Name = "textBox_trigger_deviceName";
            this.textBox_trigger_deviceName.Size = new System.Drawing.Size(70, 19);
            this.textBox_trigger_deviceName.TabIndex = 21;
            // 
            // comboBox_trigger_compareType
            // 
            this.comboBox_trigger_compareType.FormattingEnabled = true;
            this.comboBox_trigger_compareType.Location = new System.Drawing.Point(67, 39);
            this.comboBox_trigger_compareType.Name = "comboBox_trigger_compareType";
            this.comboBox_trigger_compareType.Size = new System.Drawing.Size(70, 20);
            this.comboBox_trigger_compareType.TabIndex = 22;
            // 
            // GroupInfoEditDialog
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 178);
            this.Controls.Add(this.groupBox_trigger);
            this.Controls.Add(this.label_triggerType);
            this.Controls.Add(this.comboBox_triggerType);
            this.Controls.Add(this.label_groupName);
            this.Controls.Add(this.textBox_groupName);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupInfoEditDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "グループ情報の編集";
            this.groupBox_trigger.ResumeLayout(false);
            this.groupBox_trigger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_groupName;
        private System.Windows.Forms.Label label_groupName;
        private System.Windows.Forms.Label label_triggerType;
        private System.Windows.Forms.ComboBox comboBox_triggerType;
        private System.Windows.Forms.ComboBox comboBox_trigger_deviceFormatTypee;
        private System.Windows.Forms.GroupBox groupBox_trigger;
        private System.Windows.Forms.Label label_deviceName;
        private System.Windows.Forms.TextBox textBox_trigger_deviceName;
        private System.Windows.Forms.Label label_dataType;
        private System.Windows.Forms.NumericUpDown numericUpDown_threshold;
        private System.Windows.Forms.Label label_threshold;
        private System.Windows.Forms.ComboBox comboBox_trigger_compareType;
        private System.Windows.Forms.Label label_compareType;
    }
}