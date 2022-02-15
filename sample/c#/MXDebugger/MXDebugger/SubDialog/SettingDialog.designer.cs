namespace Mtec.Internal.Mitsubishi.MXDebugger
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
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_mx = new System.Windows.Forms.TabPage();
            this.mxComponentConfigurationPannel1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel();
            this.tabControl1.SuspendLayout();
            this.tabPage_mx.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(444, 312);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 103;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(12, 312);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 102;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_mx);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(507, 294);
            this.tabControl1.TabIndex = 104;
            // 
            // tabPage_mx
            // 
            this.tabPage_mx.Controls.Add(this.mxComponentConfigurationPannel1);
            this.tabPage_mx.Location = new System.Drawing.Point(4, 22);
            this.tabPage_mx.Name = "tabPage_mx";
            this.tabPage_mx.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_mx.Size = new System.Drawing.Size(499, 268);
            this.tabPage_mx.TabIndex = 0;
            this.tabPage_mx.Text = "通信設定";
            this.tabPage_mx.UseVisualStyleBackColor = true;
            // 
            // mxComponentConfigurationPannel1
            // 
            this.mxComponentConfigurationPannel1.Location = new System.Drawing.Point(6, 6);
            this.mxComponentConfigurationPannel1.Name = "mxComponentConfigurationPannel1";
            this.mxComponentConfigurationPannel1.Size = new System.Drawing.Size(470, 200);
            this.mxComponentConfigurationPannel1.TabIndex = 0;
            this.mxComponentConfigurationPannel1.Text = "MX Component 通信設定";
            this.mxComponentConfigurationPannel1.Value.ActControl = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl.TRC_DTR_OR_RTS;
            this.mxComponentConfigurationPannel1.Value.ActCpuType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType.Default;
            this.mxComponentConfigurationPannel1.Value.ActDestinationIONumber = 0;
            this.mxComponentConfigurationPannel1.Value.ActDestinationPortNumber = 0;
            this.mxComponentConfigurationPannel1.Value.ActHostAddress = "192.168.1.1";
            this.mxComponentConfigurationPannel1.Value.ActIONumber = 1023;
            this.mxComponentConfigurationPannel1.Value.ActProtocolType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_SERIAL;
            this.mxComponentConfigurationPannel1.Value.ActTimeOut = 10000;
            this.mxComponentConfigurationPannel1.Value.ActUnitNumber = 0;
            this.mxComponentConfigurationPannel1.Value.ActUnitType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.Default;
            this.mxComponentConfigurationPannel1.Value.BaudRate = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate.BAUDRATE_9600;
            this.mxComponentConfigurationPannel1.Value.DataBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits.DATABIT_7;
            this.mxComponentConfigurationPannel1.Value.ParityBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity.EVEN_PARITY;
            this.mxComponentConfigurationPannel1.Value.PortNumber = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber.PORT_1;
            this.mxComponentConfigurationPannel1.Value.StopBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits.STOPBIT_ONE;
            // 
            // SettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 347);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "設定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDialog_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_mx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button button_ok;
        protected System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_mx;
        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MXComponentConfigurationPannel mxComponentConfigurationPannel1;
    }
}