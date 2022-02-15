namespace Mtec.UtilityLibrary.Forms
{ 
    partial class SerialPortSettingPannel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_StopBits = new System.Windows.Forms.ComboBox();
            this.comboBox_ParityBits = new System.Windows.Forms.ComboBox();
            this.label_StopBits = new System.Windows.Forms.Label();
            this.label_DataBits = new System.Windows.Forms.Label();
            this.label_ParityBits = new System.Windows.Forms.Label();
            this.label_PortName = new System.Windows.Forms.Label();
            this.label_BaudRate = new System.Windows.Forms.Label();
            this.textBox_DataBits = new System.Windows.Forms.TextBox();
            this.textBox_portName = new System.Windows.Forms.TextBox();
            this.textBox_BaudRate = new System.Windows.Forms.TextBox();
            this.numericUpDown_WriteTimeout = new System.Windows.Forms.NumericUpDown();
            this.label_ReadTimeout = new System.Windows.Forms.Label();
            this.numericUpDown_ReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.label_WriteTimeout = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_WriteTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_StopBits
            // 
            this.comboBox_StopBits.FormattingEnabled = true;
            this.comboBox_StopBits.Location = new System.Drawing.Point(72, 103);
            this.comboBox_StopBits.Name = "comboBox_StopBits";
            this.comboBox_StopBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_StopBits.TabIndex = 15;
            // 
            // comboBox_ParityBits
            // 
            this.comboBox_ParityBits.FormattingEnabled = true;
            this.comboBox_ParityBits.Location = new System.Drawing.Point(72, 52);
            this.comboBox_ParityBits.Name = "comboBox_ParityBits";
            this.comboBox_ParityBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ParityBits.TabIndex = 13;
            // 
            // label_StopBits
            // 
            this.label_StopBits.Location = new System.Drawing.Point(5, 106);
            this.label_StopBits.Name = "label_StopBits";
            this.label_StopBits.Size = new System.Drawing.Size(61, 12);
            this.label_StopBits.TabIndex = 7;
            this.label_StopBits.Text = "Stop Bits";
            this.label_StopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_DataBits
            // 
            this.label_DataBits.Location = new System.Drawing.Point(2, 81);
            this.label_DataBits.Name = "label_DataBits";
            this.label_DataBits.Size = new System.Drawing.Size(64, 12);
            this.label_DataBits.TabIndex = 8;
            this.label_DataBits.Text = "Data Bits";
            this.label_DataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_ParityBits
            // 
            this.label_ParityBits.Location = new System.Drawing.Point(6, 55);
            this.label_ParityBits.Name = "label_ParityBits";
            this.label_ParityBits.Size = new System.Drawing.Size(61, 12);
            this.label_ParityBits.TabIndex = 9;
            this.label_ParityBits.Text = "Parity Bits";
            this.label_ParityBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_PortName
            // 
            this.label_PortName.Location = new System.Drawing.Point(33, 4);
            this.label_PortName.Name = "label_PortName";
            this.label_PortName.Size = new System.Drawing.Size(33, 15);
            this.label_PortName.TabIndex = 10;
            this.label_PortName.Text = "Port";
            this.label_PortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_BaudRate
            // 
            this.label_BaudRate.Location = new System.Drawing.Point(14, 31);
            this.label_BaudRate.Name = "label_BaudRate";
            this.label_BaudRate.Size = new System.Drawing.Size(52, 12);
            this.label_BaudRate.TabIndex = 11;
            this.label_BaudRate.Text = "Baudrate";
            this.label_BaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_DataBits
            // 
            this.textBox_DataBits.Location = new System.Drawing.Point(72, 78);
            this.textBox_DataBits.Name = "textBox_DataBits";
            this.textBox_DataBits.Size = new System.Drawing.Size(121, 19);
            this.textBox_DataBits.TabIndex = 14;
            // 
            // textBox_portName
            // 
            this.textBox_portName.Location = new System.Drawing.Point(72, 4);
            this.textBox_portName.Name = "textBox_portName";
            this.textBox_portName.Size = new System.Drawing.Size(121, 19);
            this.textBox_portName.TabIndex = 12;
            // 
            // textBox_BaudRate
            // 
            this.textBox_BaudRate.Location = new System.Drawing.Point(72, 28);
            this.textBox_BaudRate.Name = "textBox_BaudRate";
            this.textBox_BaudRate.Size = new System.Drawing.Size(121, 19);
            this.textBox_BaudRate.TabIndex = 6;
            // 
            // numericUpDown_WriteTimeout
            // 
            this.numericUpDown_WriteTimeout.Location = new System.Drawing.Point(72, 129);
            this.numericUpDown_WriteTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown_WriteTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_WriteTimeout.Name = "numericUpDown_WriteTimeout";
            this.numericUpDown_WriteTimeout.Size = new System.Drawing.Size(120, 19);
            this.numericUpDown_WriteTimeout.TabIndex = 16;
            this.numericUpDown_WriteTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label_ReadTimeout
            // 
            this.label_ReadTimeout.Location = new System.Drawing.Point(2, 153);
            this.label_ReadTimeout.Name = "label_ReadTimeout";
            this.label_ReadTimeout.Size = new System.Drawing.Size(64, 12);
            this.label_ReadTimeout.TabIndex = 8;
            this.label_ReadTimeout.Text = "Timeout(R)";
            this.label_ReadTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown_ReadTimeout
            // 
            this.numericUpDown_ReadTimeout.Location = new System.Drawing.Point(72, 151);
            this.numericUpDown_ReadTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown_ReadTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_ReadTimeout.Name = "numericUpDown_ReadTimeout";
            this.numericUpDown_ReadTimeout.Size = new System.Drawing.Size(120, 19);
            this.numericUpDown_ReadTimeout.TabIndex = 16;
            this.numericUpDown_ReadTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label_WriteTimeout
            // 
            this.label_WriteTimeout.Location = new System.Drawing.Point(3, 131);
            this.label_WriteTimeout.Name = "label_WriteTimeout";
            this.label_WriteTimeout.Size = new System.Drawing.Size(64, 12);
            this.label_WriteTimeout.TabIndex = 8;
            this.label_WriteTimeout.Text = "Timeout(W)";
            this.label_WriteTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SerialPortSettingPannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown_ReadTimeout);
            this.Controls.Add(this.numericUpDown_WriteTimeout);
            this.Controls.Add(this.comboBox_StopBits);
            this.Controls.Add(this.comboBox_ParityBits);
            this.Controls.Add(this.label_StopBits);
            this.Controls.Add(this.label_WriteTimeout);
            this.Controls.Add(this.label_ReadTimeout);
            this.Controls.Add(this.label_DataBits);
            this.Controls.Add(this.label_ParityBits);
            this.Controls.Add(this.label_PortName);
            this.Controls.Add(this.label_BaudRate);
            this.Controls.Add(this.textBox_DataBits);
            this.Controls.Add(this.textBox_portName);
            this.Controls.Add(this.textBox_BaudRate);
            this.Name = "SerialPortSettingPannel";
            this.Size = new System.Drawing.Size(201, 176);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_WriteTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ReadTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_ParityBits;
        private System.Windows.Forms.Label label_StopBits;
        private System.Windows.Forms.Label label_DataBits;
        private System.Windows.Forms.Label label_ParityBits;
        private System.Windows.Forms.Label label_PortName;
        private System.Windows.Forms.Label label_BaudRate;
        protected System.Windows.Forms.TextBox textBox_DataBits;
        protected System.Windows.Forms.TextBox textBox_portName;
        protected System.Windows.Forms.TextBox textBox_BaudRate;
        protected System.Windows.Forms.ComboBox comboBox_StopBits;
        private System.Windows.Forms.NumericUpDown numericUpDown_WriteTimeout;
        private System.Windows.Forms.Label label_ReadTimeout;
        private System.Windows.Forms.NumericUpDown numericUpDown_ReadTimeout;
        private System.Windows.Forms.Label label_WriteTimeout;
    }
}
