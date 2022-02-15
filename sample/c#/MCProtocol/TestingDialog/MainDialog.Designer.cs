namespace TestingDialog
{
    partial class MainDialog
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_RatchClear = new System.Windows.Forms.Button();
            this.button_test = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_RemoteRun = new System.Windows.Forms.Button();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_cpuName = new System.Windows.Forms.Button();
            this.button_response = new System.Windows.Forms.Button();
            this.button_Pause = new System.Windows.Forms.Button();
            this.button_bind = new System.Windows.Forms.Button();
            this.textBox_RemoteIP = new System.Windows.Forms.TextBox();
            this.label_RemotePort = new System.Windows.Forms.Label();
            this.label_RemoteIP = new System.Windows.Forms.Label();
            this.numericUpDown_RemotePort = new System.Windows.Forms.NumericUpDown();
            this.button_errorReset = new System.Windows.Forms.Button();
            this.button_FileInfo = new System.Windows.Forms.Button();
            this.comboBox_frame = new System.Windows.Forms.ComboBox();
            this.label_Frame = new System.Windows.Forms.Label();
            this.button_write = new System.Windows.Forms.Button();
            this.groupBox_result = new System.Windows.Forms.GroupBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.textBox_outValue = new System.Windows.Forms.TextBox();
            this.textBox_retValue = new System.Windows.Forms.TextBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.label_outValue = new System.Windows.Forms.Label();
            this.label_retValue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_bool = new System.Windows.Forms.RadioButton();
            this.radioButton_float = new System.Windows.Forms.RadioButton();
            this.radioButton_32bit = new System.Windows.Forms.RadioButton();
            this.radioButton_16bit = new System.Windows.Forms.RadioButton();
            this.textBox_deviceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_deviceCount = new System.Windows.Forms.NumericUpDown();
            this.button_read = new System.Windows.Forms.Button();
            this.label_DeviceCount = new System.Windows.Forms.Label();
            this.groupBox_buffer = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_bfmNumber = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_bfm_readCount = new System.Windows.Forms.NumericUpDown();
            this.button_bfm_read = new System.Windows.Forms.Button();
            this.button_bfm_write = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_protocol = new System.Windows.Forms.Label();
            this.comboBox_protocol = new System.Windows.Forms.ComboBox();
            this.groupBox_connection = new System.Windows.Forms.GroupBox();
            this.label_localIP = new System.Windows.Forms.Label();
            this.textBox_LocalIP = new System.Windows.Forms.TextBox();
            this.label_localPort = new System.Windows.Forms.Label();
            this.numericUpDown_LocalPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox_commands = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RemotePort)).BeginInit();
            this.groupBox_result.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_deviceCount)).BeginInit();
            this.groupBox_buffer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bfmNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bfm_readCount)).BeginInit();
            this.groupBox_connection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LocalPort)).BeginInit();
            this.groupBox_commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_RatchClear
            // 
            this.button_RatchClear.Location = new System.Drawing.Point(154, 21);
            this.button_RatchClear.Name = "button_RatchClear";
            this.button_RatchClear.Size = new System.Drawing.Size(108, 23);
            this.button_RatchClear.TabIndex = 55;
            this.button_RatchClear.Text = "Ratch Clear";
            this.button_RatchClear.UseVisualStyleBackColor = true;
            this.button_RatchClear.Click += new System.EventHandler(this.button_ratchClear_Click);
            // 
            // button_test
            // 
            this.button_test.Location = new System.Drawing.Point(503, 334);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(75, 23);
            this.button_test.TabIndex = 72;
            this.button_test.Text = "test";
            this.button_test.UseVisualStyleBackColor = true;
            this.button_test.Click += new System.EventHandler(this.button_test_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(24, 50);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(108, 23);
            this.button_stop.TabIndex = 52;
            this.button_stop.Text = "Remote Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_RemoteRun
            // 
            this.button_RemoteRun.Location = new System.Drawing.Point(24, 21);
            this.button_RemoteRun.Name = "button_RemoteRun";
            this.button_RemoteRun.Size = new System.Drawing.Size(108, 23);
            this.button_RemoteRun.TabIndex = 51;
            this.button_RemoteRun.Text = "Remote Run";
            this.button_RemoteRun.UseVisualStyleBackColor = true;
            this.button_RemoteRun.Click += new System.EventHandler(this.button_RemoteRun_Click);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(24, 79);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(108, 23);
            this.button_reset.TabIndex = 53;
            this.button_reset.Text = "Remote Reset";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_cpuName
            // 
            this.button_cpuName.Location = new System.Drawing.Point(154, 50);
            this.button_cpuName.Name = "button_cpuName";
            this.button_cpuName.Size = new System.Drawing.Size(108, 23);
            this.button_cpuName.TabIndex = 56;
            this.button_cpuName.Text = "Get CPU Name";
            this.button_cpuName.UseVisualStyleBackColor = true;
            this.button_cpuName.Click += new System.EventHandler(this.button_cpuName_Click);
            // 
            // button_response
            // 
            this.button_response.Location = new System.Drawing.Point(154, 79);
            this.button_response.Name = "button_response";
            this.button_response.Size = new System.Drawing.Size(108, 23);
            this.button_response.TabIndex = 57;
            this.button_response.Text = "Response";
            this.button_response.UseVisualStyleBackColor = true;
            this.button_response.Click += new System.EventHandler(this.button_response_Click);
            // 
            // button_Pause
            // 
            this.button_Pause.Location = new System.Drawing.Point(24, 108);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(108, 23);
            this.button_Pause.TabIndex = 54;
            this.button_Pause.Text = "Remote Pause";
            this.button_Pause.UseVisualStyleBackColor = true;
            this.button_Pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_bind
            // 
            this.button_bind.Location = new System.Drawing.Point(21, 76);
            this.button_bind.Name = "button_bind";
            this.button_bind.Size = new System.Drawing.Size(108, 23);
            this.button_bind.TabIndex = 10;
            this.button_bind.Text = "Bind";
            this.button_bind.UseVisualStyleBackColor = true;
            this.button_bind.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // textBox_RemoteIP
            // 
            this.textBox_RemoteIP.Location = new System.Drawing.Point(80, 19);
            this.textBox_RemoteIP.Name = "textBox_RemoteIP";
            this.textBox_RemoteIP.Size = new System.Drawing.Size(100, 19);
            this.textBox_RemoteIP.TabIndex = 4;
            // 
            // label_RemotePort
            // 
            this.label_RemotePort.AutoSize = true;
            this.label_RemotePort.Location = new System.Drawing.Point(6, 49);
            this.label_RemotePort.Name = "label_RemotePort";
            this.label_RemotePort.Size = new System.Drawing.Size(69, 12);
            this.label_RemotePort.TabIndex = 9;
            this.label_RemotePort.Text = "Remote Port";
            // 
            // label_RemoteIP
            // 
            this.label_RemoteIP.AutoSize = true;
            this.label_RemoteIP.Location = new System.Drawing.Point(17, 22);
            this.label_RemoteIP.Name = "label_RemoteIP";
            this.label_RemoteIP.Size = new System.Drawing.Size(58, 12);
            this.label_RemoteIP.TabIndex = 8;
            this.label_RemoteIP.Text = "Remote IP";
            // 
            // numericUpDown_RemotePort
            // 
            this.numericUpDown_RemotePort.Location = new System.Drawing.Point(80, 47);
            this.numericUpDown_RemotePort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_RemotePort.Name = "numericUpDown_RemotePort";
            this.numericUpDown_RemotePort.Size = new System.Drawing.Size(100, 19);
            this.numericUpDown_RemotePort.TabIndex = 5;
            // 
            // button_errorReset
            // 
            this.button_errorReset.Location = new System.Drawing.Point(154, 108);
            this.button_errorReset.Name = "button_errorReset";
            this.button_errorReset.Size = new System.Drawing.Size(108, 23);
            this.button_errorReset.TabIndex = 58;
            this.button_errorReset.Text = "Error Reset";
            this.button_errorReset.UseVisualStyleBackColor = true;
            this.button_errorReset.Click += new System.EventHandler(this.button_errorReset_Click);
            // 
            // button_FileInfo
            // 
            this.button_FileInfo.Location = new System.Drawing.Point(376, 334);
            this.button_FileInfo.Name = "button_FileInfo";
            this.button_FileInfo.Size = new System.Drawing.Size(108, 23);
            this.button_FileInfo.TabIndex = 71;
            this.button_FileInfo.Text = "File Info";
            this.button_FileInfo.UseVisualStyleBackColor = true;
            this.button_FileInfo.Click += new System.EventHandler(this.button_FileInfo_Click);
            // 
            // comboBox_frame
            // 
            this.comboBox_frame.FormattingEnabled = true;
            this.comboBox_frame.Location = new System.Drawing.Point(244, 46);
            this.comboBox_frame.Name = "comboBox_frame";
            this.comboBox_frame.Size = new System.Drawing.Size(114, 20);
            this.comboBox_frame.TabIndex = 8;
            // 
            // label_Frame
            // 
            this.label_Frame.AutoSize = true;
            this.label_Frame.Location = new System.Drawing.Point(201, 49);
            this.label_Frame.Name = "label_Frame";
            this.label_Frame.Size = new System.Drawing.Size(37, 12);
            this.label_Frame.TabIndex = 9;
            this.label_Frame.Text = "Frame";
            // 
            // button_write
            // 
            this.button_write.Location = new System.Drawing.Point(21, 46);
            this.button_write.Name = "button_write";
            this.button_write.Size = new System.Drawing.Size(75, 23);
            this.button_write.TabIndex = 6;
            this.button_write.TabStop = false;
            this.button_write.Text = "Write";
            this.button_write.UseVisualStyleBackColor = true;
            this.button_write.Click += new System.EventHandler(this.button_write_Click);
            // 
            // groupBox_result
            // 
            this.groupBox_result.Controls.Add(this.textBox_message);
            this.groupBox_result.Controls.Add(this.textBox_outValue);
            this.groupBox_result.Controls.Add(this.textBox_retValue);
            this.groupBox_result.Controls.Add(this.label_Message);
            this.groupBox_result.Controls.Add(this.label_outValue);
            this.groupBox_result.Controls.Add(this.label_retValue);
            this.groupBox_result.Location = new System.Drawing.Point(14, 363);
            this.groupBox_result.Name = "groupBox_result";
            this.groupBox_result.Size = new System.Drawing.Size(564, 70);
            this.groupBox_result.TabIndex = 14;
            this.groupBox_result.TabStop = false;
            this.groupBox_result.Text = "Result";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(100, 43);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(392, 19);
            this.textBox_message.TabIndex = 83;
            // 
            // textBox_outValue
            // 
            this.textBox_outValue.Location = new System.Drawing.Point(271, 14);
            this.textBox_outValue.Name = "textBox_outValue";
            this.textBox_outValue.Size = new System.Drawing.Size(221, 19);
            this.textBox_outValue.TabIndex = 82;
            // 
            // textBox_retValue
            // 
            this.textBox_retValue.Location = new System.Drawing.Point(103, 18);
            this.textBox_retValue.Name = "textBox_retValue";
            this.textBox_retValue.Size = new System.Drawing.Size(67, 19);
            this.textBox_retValue.TabIndex = 81;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(25, 46);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(50, 12);
            this.label_Message.TabIndex = 9;
            this.label_Message.Text = "Message";
            // 
            // label_outValue
            // 
            this.label_outValue.AutoSize = true;
            this.label_outValue.Location = new System.Drawing.Point(216, 18);
            this.label_outValue.Name = "label_outValue";
            this.label_outValue.Size = new System.Drawing.Size(56, 12);
            this.label_outValue.TabIndex = 9;
            this.label_outValue.Text = "Out Value";
            // 
            // label_retValue
            // 
            this.label_retValue.AutoSize = true;
            this.label_retValue.Location = new System.Drawing.Point(25, 21);
            this.label_retValue.Name = "label_retValue";
            this.label_retValue.Size = new System.Drawing.Size(72, 12);
            this.label_retValue.TabIndex = 9;
            this.label_retValue.Text = "Return Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_bool);
            this.groupBox1.Controls.Add(this.radioButton_float);
            this.groupBox1.Controls.Add(this.radioButton_32bit);
            this.groupBox1.Controls.Add(this.radioButton_16bit);
            this.groupBox1.Controls.Add(this.textBox_deviceName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown_deviceCount);
            this.groupBox1.Controls.Add(this.button_read);
            this.groupBox1.Controls.Add(this.button_write);
            this.groupBox1.Controls.Add(this.label_DeviceCount);
            this.groupBox1.Location = new System.Drawing.Point(12, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 108);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // radioButton_bool
            // 
            this.radioButton_bool.AutoSize = true;
            this.radioButton_bool.Location = new System.Drawing.Point(201, 81);
            this.radioButton_bool.Name = "radioButton_bool";
            this.radioButton_bool.Size = new System.Drawing.Size(44, 16);
            this.radioButton_bool.TabIndex = 15;
            this.radioButton_bool.Text = "bool";
            this.radioButton_bool.UseVisualStyleBackColor = true;
            // 
            // radioButton_float
            // 
            this.radioButton_float.AutoSize = true;
            this.radioButton_float.Location = new System.Drawing.Point(201, 61);
            this.radioButton_float.Name = "radioButton_float";
            this.radioButton_float.Size = new System.Drawing.Size(46, 16);
            this.radioButton_float.TabIndex = 15;
            this.radioButton_float.Text = "float";
            this.radioButton_float.UseVisualStyleBackColor = true;
            // 
            // radioButton_32bit
            // 
            this.radioButton_32bit.AutoSize = true;
            this.radioButton_32bit.Location = new System.Drawing.Point(201, 39);
            this.radioButton_32bit.Name = "radioButton_32bit";
            this.radioButton_32bit.Size = new System.Drawing.Size(48, 16);
            this.radioButton_32bit.TabIndex = 15;
            this.radioButton_32bit.Text = "32bit";
            this.radioButton_32bit.UseVisualStyleBackColor = true;
            // 
            // radioButton_16bit
            // 
            this.radioButton_16bit.AutoSize = true;
            this.radioButton_16bit.Checked = true;
            this.radioButton_16bit.Location = new System.Drawing.Point(201, 19);
            this.radioButton_16bit.Name = "radioButton_16bit";
            this.radioButton_16bit.Size = new System.Drawing.Size(48, 16);
            this.radioButton_16bit.TabIndex = 15;
            this.radioButton_16bit.TabStop = true;
            this.radioButton_16bit.Text = "16bit";
            this.radioButton_16bit.UseVisualStyleBackColor = true;
            // 
            // textBox_deviceName
            // 
            this.textBox_deviceName.Location = new System.Drawing.Point(97, 18);
            this.textBox_deviceName.Name = "textBox_deviceName";
            this.textBox_deviceName.Size = new System.Drawing.Size(80, 19);
            this.textBox_deviceName.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Device Name";
            // 
            // numericUpDown_deviceCount
            // 
            this.numericUpDown_deviceCount.Location = new System.Drawing.Point(133, 77);
            this.numericUpDown_deviceCount.Name = "numericUpDown_deviceCount";
            this.numericUpDown_deviceCount.Size = new System.Drawing.Size(45, 19);
            this.numericUpDown_deviceCount.TabIndex = 22;
            this.numericUpDown_deviceCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_read
            // 
            this.button_read.Location = new System.Drawing.Point(102, 46);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(75, 23);
            this.button_read.TabIndex = 6;
            this.button_read.TabStop = false;
            this.button_read.Text = "Read";
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // label_DeviceCount
            // 
            this.label_DeviceCount.AutoSize = true;
            this.label_DeviceCount.Location = new System.Drawing.Point(22, 81);
            this.label_DeviceCount.Name = "label_DeviceCount";
            this.label_DeviceCount.Size = new System.Drawing.Size(104, 12);
            this.label_DeviceCount.TabIndex = 9;
            this.label_DeviceCount.Text = "Read Device Count";
            // 
            // groupBox_buffer
            // 
            this.groupBox_buffer.Controls.Add(this.label1);
            this.groupBox_buffer.Controls.Add(this.numericUpDown_bfmNumber);
            this.groupBox_buffer.Controls.Add(this.numericUpDown_bfm_readCount);
            this.groupBox_buffer.Controls.Add(this.button_bfm_read);
            this.groupBox_buffer.Controls.Add(this.button_bfm_write);
            this.groupBox_buffer.Controls.Add(this.label2);
            this.groupBox_buffer.Location = new System.Drawing.Point(12, 245);
            this.groupBox_buffer.Name = "groupBox_buffer";
            this.groupBox_buffer.Size = new System.Drawing.Size(269, 80);
            this.groupBox_buffer.TabIndex = 14;
            this.groupBox_buffer.TabStop = false;
            this.groupBox_buffer.Text = "Buffer Member";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "BFM#";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_bfmNumber
            // 
            this.numericUpDown_bfmNumber.Location = new System.Drawing.Point(60, 19);
            this.numericUpDown_bfmNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_bfmNumber.Name = "numericUpDown_bfmNumber";
            this.numericUpDown_bfmNumber.Size = new System.Drawing.Size(66, 19);
            this.numericUpDown_bfmNumber.TabIndex = 31;
            // 
            // numericUpDown_bfm_readCount
            // 
            this.numericUpDown_bfm_readCount.Location = new System.Drawing.Point(214, 19);
            this.numericUpDown_bfm_readCount.Name = "numericUpDown_bfm_readCount";
            this.numericUpDown_bfm_readCount.Size = new System.Drawing.Size(45, 19);
            this.numericUpDown_bfm_readCount.TabIndex = 32;
            this.numericUpDown_bfm_readCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_bfm_read
            // 
            this.button_bfm_read.Location = new System.Drawing.Point(102, 46);
            this.button_bfm_read.Name = "button_bfm_read";
            this.button_bfm_read.Size = new System.Drawing.Size(75, 23);
            this.button_bfm_read.TabIndex = 6;
            this.button_bfm_read.TabStop = false;
            this.button_bfm_read.Text = "Read";
            this.button_bfm_read.UseVisualStyleBackColor = true;
            this.button_bfm_read.Click += new System.EventHandler(this.button_bfm_read_Click);
            // 
            // button_bfm_write
            // 
            this.button_bfm_write.Location = new System.Drawing.Point(21, 46);
            this.button_bfm_write.Name = "button_bfm_write";
            this.button_bfm_write.Size = new System.Drawing.Size(75, 23);
            this.button_bfm_write.TabIndex = 6;
            this.button_bfm_write.TabStop = false;
            this.button_bfm_write.Text = "Write";
            this.button_bfm_write.UseVisualStyleBackColor = true;
            this.button_bfm_write.Click += new System.EventHandler(this.button_bfm_write_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Read Count";
            // 
            // label_protocol
            // 
            this.label_protocol.AutoSize = true;
            this.label_protocol.Location = new System.Drawing.Point(194, 22);
            this.label_protocol.Name = "label_protocol";
            this.label_protocol.Size = new System.Drawing.Size(47, 12);
            this.label_protocol.TabIndex = 9;
            this.label_protocol.Text = "Protocol";
            // 
            // comboBox_protocol
            // 
            this.comboBox_protocol.FormattingEnabled = true;
            this.comboBox_protocol.Location = new System.Drawing.Point(244, 18);
            this.comboBox_protocol.Name = "comboBox_protocol";
            this.comboBox_protocol.Size = new System.Drawing.Size(114, 20);
            this.comboBox_protocol.TabIndex = 7;
            // 
            // groupBox_connection
            // 
            this.groupBox_connection.Controls.Add(this.label_localIP);
            this.groupBox_connection.Controls.Add(this.textBox_LocalIP);
            this.groupBox_connection.Controls.Add(this.label_localPort);
            this.groupBox_connection.Controls.Add(this.numericUpDown_LocalPort);
            this.groupBox_connection.Controls.Add(this.label_Frame);
            this.groupBox_connection.Controls.Add(this.label_protocol);
            this.groupBox_connection.Controls.Add(this.comboBox_protocol);
            this.groupBox_connection.Controls.Add(this.textBox_RemoteIP);
            this.groupBox_connection.Controls.Add(this.comboBox_frame);
            this.groupBox_connection.Controls.Add(this.label_RemotePort);
            this.groupBox_connection.Controls.Add(this.button_bind);
            this.groupBox_connection.Controls.Add(this.label_RemoteIP);
            this.groupBox_connection.Controls.Add(this.numericUpDown_RemotePort);
            this.groupBox_connection.Location = new System.Drawing.Point(12, 10);
            this.groupBox_connection.Name = "groupBox_connection";
            this.groupBox_connection.Size = new System.Drawing.Size(566, 115);
            this.groupBox_connection.TabIndex = 73;
            this.groupBox_connection.TabStop = false;
            this.groupBox_connection.Text = "Connection Infomation";
            // 
            // label_localIP
            // 
            this.label_localIP.AutoSize = true;
            this.label_localIP.Location = new System.Drawing.Point(409, 18);
            this.label_localIP.Name = "label_localIP";
            this.label_localIP.Size = new System.Drawing.Size(49, 12);
            this.label_localIP.TabIndex = 13;
            this.label_localIP.Text = "Client IP";
            // 
            // textBox_LocalIP
            // 
            this.textBox_LocalIP.Location = new System.Drawing.Point(460, 15);
            this.textBox_LocalIP.Name = "textBox_LocalIP";
            this.textBox_LocalIP.Size = new System.Drawing.Size(100, 19);
            this.textBox_LocalIP.TabIndex = 11;
            // 
            // label_localPort
            // 
            this.label_localPort.AutoSize = true;
            this.label_localPort.Location = new System.Drawing.Point(398, 45);
            this.label_localPort.Name = "label_localPort";
            this.label_localPort.Size = new System.Drawing.Size(60, 12);
            this.label_localPort.TabIndex = 14;
            this.label_localPort.Text = "Client Port";
            // 
            // numericUpDown_LocalPort
            // 
            this.numericUpDown_LocalPort.Location = new System.Drawing.Point(460, 43);
            this.numericUpDown_LocalPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_LocalPort.Name = "numericUpDown_LocalPort";
            this.numericUpDown_LocalPort.Size = new System.Drawing.Size(100, 19);
            this.numericUpDown_LocalPort.TabIndex = 12;
            // 
            // groupBox_commands
            // 
            this.groupBox_commands.Controls.Add(this.button_RemoteRun);
            this.groupBox_commands.Controls.Add(this.button_Pause);
            this.groupBox_commands.Controls.Add(this.button_stop);
            this.groupBox_commands.Controls.Add(this.button_reset);
            this.groupBox_commands.Controls.Add(this.button_cpuName);
            this.groupBox_commands.Controls.Add(this.button_response);
            this.groupBox_commands.Controls.Add(this.button_RatchClear);
            this.groupBox_commands.Controls.Add(this.button_errorReset);
            this.groupBox_commands.Location = new System.Drawing.Point(297, 131);
            this.groupBox_commands.Name = "groupBox_commands";
            this.groupBox_commands.Size = new System.Drawing.Size(281, 194);
            this.groupBox_commands.TabIndex = 73;
            this.groupBox_commands.TabStop = false;
            this.groupBox_commands.Text = "Command";
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 444);
            this.Controls.Add(this.groupBox_commands);
            this.Controls.Add(this.groupBox_connection);
            this.Controls.Add(this.groupBox_buffer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_result);
            this.Controls.Add(this.button_test);
            this.Controls.Add(this.button_FileInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainDialog";
            this.Text = "MC Protocol Test Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RemotePort)).EndInit();
            this.groupBox_result.ResumeLayout(false);
            this.groupBox_result.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_deviceCount)).EndInit();
            this.groupBox_buffer.ResumeLayout(false);
            this.groupBox_buffer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bfmNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bfm_readCount)).EndInit();
            this.groupBox_connection.ResumeLayout(false);
            this.groupBox_connection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LocalPort)).EndInit();
            this.groupBox_commands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_RatchClear;
        private System.Windows.Forms.Button button_test;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_RemoteRun;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Button button_cpuName;
        private System.Windows.Forms.Button button_response;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.Button button_bind;
        private System.Windows.Forms.TextBox textBox_RemoteIP;
        private System.Windows.Forms.Label label_RemotePort;
        private System.Windows.Forms.Label label_RemoteIP;
        private System.Windows.Forms.NumericUpDown numericUpDown_RemotePort;
        private System.Windows.Forms.Button button_errorReset;
        private System.Windows.Forms.Button button_FileInfo;
        private System.Windows.Forms.ComboBox comboBox_frame;
        private System.Windows.Forms.Label label_Frame;
        private System.Windows.Forms.Button button_write;
        private System.Windows.Forms.GroupBox groupBox_result;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.TextBox textBox_outValue;
        private System.Windows.Forms.TextBox textBox_retValue;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Label label_outValue;
        private System.Windows.Forms.Label label_retValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_deviceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.RadioButton radioButton_float;
        private System.Windows.Forms.RadioButton radioButton_32bit;
        private System.Windows.Forms.RadioButton radioButton_16bit;
        private System.Windows.Forms.RadioButton radioButton_bool;
        private System.Windows.Forms.NumericUpDown numericUpDown_deviceCount;
        private System.Windows.Forms.Label label_DeviceCount;
        private System.Windows.Forms.GroupBox groupBox_buffer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_bfmNumber;
        private System.Windows.Forms.NumericUpDown numericUpDown_bfm_readCount;
        private System.Windows.Forms.Button button_bfm_read;
        private System.Windows.Forms.Button button_bfm_write;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_protocol;
        private System.Windows.Forms.ComboBox comboBox_protocol;
        private System.Windows.Forms.GroupBox groupBox_connection;
        private System.Windows.Forms.GroupBox groupBox_commands;
        private System.Windows.Forms.Label label_localIP;
        private System.Windows.Forms.TextBox textBox_LocalIP;
        private System.Windows.Forms.Label label_localPort;
        private System.Windows.Forms.NumericUpDown numericUpDown_LocalPort;
    }
}

