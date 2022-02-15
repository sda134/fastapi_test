namespace Mtec.Internal.Mitsubishi.MXLogger
{
    partial class SettingDialog
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
            this.components = new System.ComponentModel.Container();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_pathSelect = new System.Windows.Forms.Button();
            this.label_savePath = new System.Windows.Forms.Label();
            this.textBox_filePath = new System.Windows.Forms.TextBox();
            this.label_logInterval = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.toolTip_msg = new System.Windows.Forms.ToolTip(this.components);
            this.mxComponentConfigurationPannel1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel();
            this.checkBox_needCSVFieldsSorted = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(318, 283);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 100;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(399, 283);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 101;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_pathSelect
            // 
            this.button_pathSelect.Location = new System.Drawing.Point(102, 218);
            this.button_pathSelect.Name = "button_pathSelect";
            this.button_pathSelect.Size = new System.Drawing.Size(45, 23);
            this.button_pathSelect.TabIndex = 30;
            this.button_pathSelect.Text = "選択";
            this.button_pathSelect.UseVisualStyleBackColor = true;
            this.button_pathSelect.Click += new System.EventHandler(this.button_pathSelect_Click);
            // 
            // label_savePath
            // 
            this.label_savePath.AutoSize = true;
            this.label_savePath.Location = new System.Drawing.Point(20, 223);
            this.label_savePath.Name = "label_savePath";
            this.label_savePath.Size = new System.Drawing.Size(76, 12);
            this.label_savePath.TabIndex = 9;
            this.label_savePath.Text = "ログの保存パス";
            // 
            // textBox_filePath
            // 
            this.textBox_filePath.Location = new System.Drawing.Point(153, 220);
            this.textBox_filePath.Name = "textBox_filePath";
            this.textBox_filePath.Size = new System.Drawing.Size(321, 19);
            this.textBox_filePath.TabIndex = 31;
            // 
            // label_logInterval
            // 
            this.label_logInterval.AutoSize = true;
            this.label_logInterval.Location = new System.Drawing.Point(20, 250);
            this.label_logInterval.Name = "label_logInterval";
            this.label_logInterval.Size = new System.Drawing.Size(107, 12);
            this.label_logInterval.TabIndex = 13;
            this.label_logInterval.Text = "ログ稼働周期(mSec)";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(153, 248);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(84, 19);
            this.numericUpDownInterval.TabIndex = 41;
            this.toolTip_msg.SetToolTip(this.numericUpDownInterval, "70mSec 以上推奨");
            this.numericUpDownInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // mxComponentConfigurationPannel1
            // 
            this.mxComponentConfigurationPannel1.Location = new System.Drawing.Point(12, 12);
            this.mxComponentConfigurationPannel1.Name = "mxComponentConfigurationPannel1";
            this.mxComponentConfigurationPannel1.Size = new System.Drawing.Size(470, 200);
            this.mxComponentConfigurationPannel1.TabIndex = 1;
            this.mxComponentConfigurationPannel1.Text = "MXComponentControlPannel";
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
            // NeedCSVFieldSorted
            // 
            this.checkBox_needCSVFieldsSorted.AutoSize = true;
            this.checkBox_needCSVFieldsSorted.Location = new System.Drawing.Point(22, 283);
            this.checkBox_needCSVFieldsSorted.Name = "NeedCSVFieldSorted";
            this.checkBox_needCSVFieldsSorted.Size = new System.Drawing.Size(170, 16);
            this.checkBox_needCSVFieldsSorted.TabIndex = 102;
            this.checkBox_needCSVFieldsSorted.Text = "CSVログ　フィールドの並び替え";
            this.checkBox_needCSVFieldsSorted.UseVisualStyleBackColor = true;
            // 
            // SettingDialog
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 318);
            this.Controls.Add(this.checkBox_needCSVFieldsSorted);
            this.Controls.Add(this.label_logInterval);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.button_pathSelect);
            this.Controls.Add(this.label_savePath);
            this.Controls.Add(this.textBox_filePath);
            this.Controls.Add(this.mxComponentConfigurationPannel1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel mxComponentConfigurationPannel1;
        private System.Windows.Forms.Button button_pathSelect;
        private System.Windows.Forms.Label label_savePath;
        private System.Windows.Forms.TextBox textBox_filePath;
        private System.Windows.Forms.Label label_logInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.ToolTip toolTip_msg;
        private System.Windows.Forms.CheckBox checkBox_needCSVFieldsSorted;
    }
}