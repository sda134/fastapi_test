namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms
{
    partial class ActControlPannel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_IPAddress = new System.Windows.Forms.Label();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.button_confirmConnection = new System.Windows.Forms.Button();
            this.label_UnitType = new System.Windows.Forms.Label();
            this.comboBox_UnitType = new System.Windows.Forms.ComboBox();
            this.label_CpuType = new System.Windows.Forms.Label();
            this.comboBox_CpuType = new System.Windows.Forms.ComboBox();
            this.label_value = new System.Windows.Forms.Label();
            this.label_device = new System.Windows.Forms.Label();
            this.textBox_deviceVal = new System.Windows.Forms.TextBox();
            this.textBox_device = new System.Windows.Forms.TextBox();
            this.button_deviceWrite = new System.Windows.Forms.Button();
            this.button_prog_close = new System.Windows.Forms.Button();
            this.button_prog_open = new System.Windows.Forms.Button();
            this.button_deviceRead = new System.Windows.Forms.Button();
            this.textBox_target = new System.Windows.Forms.TextBox();
            this.comboBox_protocolType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_protocolType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_IPAddress);
            this.groupBox1.Controls.Add(this.textBox_target);
            this.groupBox1.Controls.Add(this.textBox_Message);
            this.groupBox1.Controls.Add(this.button_confirmConnection);
            this.groupBox1.Controls.Add(this.label_UnitType);
            this.groupBox1.Controls.Add(this.comboBox_UnitType);
            this.groupBox1.Controls.Add(this.label_CpuType);
            this.groupBox1.Controls.Add(this.comboBox_CpuType);
            this.groupBox1.Controls.Add(this.label_value);
            this.groupBox1.Controls.Add(this.label_device);
            this.groupBox1.Controls.Add(this.textBox_deviceVal);
            this.groupBox1.Controls.Add(this.textBox_device);
            this.groupBox1.Controls.Add(this.button_deviceWrite);
            this.groupBox1.Controls.Add(this.button_prog_close);
            this.groupBox1.Controls.Add(this.button_prog_open);
            this.groupBox1.Controls.Add(this.button_deviceRead);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 184);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label_IPAddress
            // 
            this.label_IPAddress.Location = new System.Drawing.Point(2, 48);
            this.label_IPAddress.Name = "label_IPAddress";
            this.label_IPAddress.Size = new System.Drawing.Size(77, 19);
            this.label_IPAddress.TabIndex = 22;
            this.label_IPAddress.Text = "IP / Port";
            this.label_IPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Message
            // 
            this.textBox_Message.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Message.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox_Message.Location = new System.Drawing.Point(85, 126);
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.ReadOnly = true;
            this.textBox_Message.Size = new System.Drawing.Size(146, 19);
            this.textBox_Message.TabIndex = 21;
            this.textBox_Message.Text = "確認結果を表示します";
            // 
            // button_confirmConnection
            // 
            this.button_confirmConnection.Location = new System.Drawing.Point(6, 124);
            this.button_confirmConnection.Name = "button_confirmConnection";
            this.button_confirmConnection.Size = new System.Drawing.Size(75, 23);
            this.button_confirmConnection.TabIndex = 20;
            this.button_confirmConnection.Text = "接続確認";
            this.button_confirmConnection.UseVisualStyleBackColor = true;
            this.button_confirmConnection.Click += new System.EventHandler(this.button_confirmConnection_Click);
            // 
            // label_UnitType
            // 
            this.label_UnitType.Location = new System.Drawing.Point(9, 102);
            this.label_UnitType.Name = "label_UnitType";
            this.label_UnitType.Size = new System.Drawing.Size(72, 15);
            this.label_UnitType.TabIndex = 19;
            this.label_UnitType.Text = "ユニットタイプ";
            this.label_UnitType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_UnitType
            // 
            this.comboBox_UnitType.FormattingEnabled = true;
            this.comboBox_UnitType.Location = new System.Drawing.Point(85, 100);
            this.comboBox_UnitType.Name = "comboBox_UnitType";
            this.comboBox_UnitType.Size = new System.Drawing.Size(146, 20);
            this.comboBox_UnitType.TabIndex = 18;
            // 
            // label_CpuType
            // 
            this.label_CpuType.Location = new System.Drawing.Point(9, 76);
            this.label_CpuType.Name = "label_CpuType";
            this.label_CpuType.Size = new System.Drawing.Size(72, 15);
            this.label_CpuType.TabIndex = 17;
            this.label_CpuType.Text = "CPU タイプ";
            this.label_CpuType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_CpuType
            // 
            this.comboBox_CpuType.FormattingEnabled = true;
            this.comboBox_CpuType.Location = new System.Drawing.Point(85, 74);
            this.comboBox_CpuType.Name = "comboBox_CpuType";
            this.comboBox_CpuType.Size = new System.Drawing.Size(146, 20);
            this.comboBox_CpuType.TabIndex = 16;
            // 
            // label_value
            // 
            this.label_value.AutoSize = true;
            this.label_value.Location = new System.Drawing.Point(193, 159);
            this.label_value.Name = "label_value";
            this.label_value.Size = new System.Drawing.Size(34, 12);
            this.label_value.TabIndex = 13;
            this.label_value.Text = "Value";
            // 
            // label_device
            // 
            this.label_device.AutoSize = true;
            this.label_device.Location = new System.Drawing.Point(39, 160);
            this.label_device.Name = "label_device";
            this.label_device.Size = new System.Drawing.Size(40, 12);
            this.label_device.TabIndex = 14;
            this.label_device.Text = "Device";
            // 
            // textBox_deviceVal
            // 
            this.textBox_deviceVal.Location = new System.Drawing.Point(239, 154);
            this.textBox_deviceVal.Name = "textBox_deviceVal";
            this.textBox_deviceVal.Size = new System.Drawing.Size(68, 19);
            this.textBox_deviceVal.TabIndex = 11;
            // 
            // textBox_device
            // 
            this.textBox_device.Location = new System.Drawing.Point(85, 156);
            this.textBox_device.Name = "textBox_device";
            this.textBox_device.Size = new System.Drawing.Size(68, 19);
            this.textBox_device.TabIndex = 12;
            // 
            // button_deviceWrite
            // 
            this.button_deviceWrite.Location = new System.Drawing.Point(249, 98);
            this.button_deviceWrite.Name = "button_deviceWrite";
            this.button_deviceWrite.Size = new System.Drawing.Size(75, 23);
            this.button_deviceWrite.TabIndex = 9;
            this.button_deviceWrite.Text = "device write";
            this.button_deviceWrite.UseVisualStyleBackColor = true;
            this.button_deviceWrite.Click += new System.EventHandler(this.button_deviceWrite_Click);
            // 
            // button_prog_close
            // 
            this.button_prog_close.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_prog_close.Location = new System.Drawing.Point(249, 124);
            this.button_prog_close.Name = "button_prog_close";
            this.button_prog_close.Size = new System.Drawing.Size(75, 23);
            this.button_prog_close.TabIndex = 10;
            this.button_prog_close.Text = "close";
            this.button_prog_close.UseVisualStyleBackColor = true;
            this.button_prog_close.Click += new System.EventHandler(this.button_prog_close_Click);
            // 
            // button_prog_open
            // 
            this.button_prog_open.Location = new System.Drawing.Point(249, 46);
            this.button_prog_open.Name = "button_prog_open";
            this.button_prog_open.Size = new System.Drawing.Size(75, 23);
            this.button_prog_open.TabIndex = 1;
            this.button_prog_open.Text = "open";
            this.button_prog_open.UseVisualStyleBackColor = true;
            this.button_prog_open.Click += new System.EventHandler(this.button_prog_open_Click);
            // 
            // button_deviceRead
            // 
            this.button_deviceRead.Location = new System.Drawing.Point(249, 72);
            this.button_deviceRead.Name = "button_deviceRead";
            this.button_deviceRead.Size = new System.Drawing.Size(75, 23);
            this.button_deviceRead.TabIndex = 2;
            this.button_deviceRead.Text = "device read";
            this.button_deviceRead.UseVisualStyleBackColor = true;
            this.button_deviceRead.Click += new System.EventHandler(this.button_deviceRead_Click);
            // 
            // textBox_IPAddress
            // 
            this.textBox_target.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_target.Location = new System.Drawing.Point(85, 48);
            this.textBox_target.Name = "textBox_IPAddress";
            this.textBox_target.Size = new System.Drawing.Size(152, 19);
            this.textBox_target.TabIndex = 23;
            // 
            // comboBox_protocolType
            // 
            this.comboBox_protocolType.FormattingEnabled = true;
            this.comboBox_protocolType.Location = new System.Drawing.Point(85, 22);
            this.comboBox_protocolType.Name = "comboBox_protocolType";
            this.comboBox_protocolType.Size = new System.Drawing.Size(152, 20);
            this.comboBox_protocolType.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "接続経路";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ActControlPannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ActControlPannel";
            this.Size = new System.Drawing.Size(337, 188);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_deviceWrite;
        private System.Windows.Forms.Button button_prog_close;
        private System.Windows.Forms.Button button_prog_open;
        private System.Windows.Forms.Button button_deviceRead;
        private System.Windows.Forms.Label label_value;
        private System.Windows.Forms.Label label_device;
        private System.Windows.Forms.TextBox textBox_deviceVal;
        private System.Windows.Forms.TextBox textBox_device;
        private System.Windows.Forms.Label label_IPAddress;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.Button button_confirmConnection;
        private System.Windows.Forms.Label label_UnitType;
        private System.Windows.Forms.ComboBox comboBox_UnitType;
        private System.Windows.Forms.Label label_CpuType;
        private System.Windows.Forms.ComboBox comboBox_CpuType;
        private System.Windows.Forms.ComboBox comboBox_protocolType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_target;
    }
}
