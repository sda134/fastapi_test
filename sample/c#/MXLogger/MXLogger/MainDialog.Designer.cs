namespace Mtec.Internal.Mitsubishi.MXLogger
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
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.menuStrip_mainMenu = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CSV_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuI_CSV_allLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuI_CSV_groupLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ログ設定の保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_file_border1 = new System.Windows.Forms.ToolStripSeparator();
            this.状態ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.スタンバイToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.休止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemfile_border2 = new System.Windows.Forms.ToolStripSeparator();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.グループToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.削除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.並び替えToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.データDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ログデータの初期化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通信設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.groupBox_groupInfo = new System.Windows.Forms.GroupBox();
            this.label_recording = new System.Windows.Forms.Label();
            this.textBox_trigger = new System.Windows.Forms.TextBox();
            this.textBox_groupName = new System.Windows.Forms.TextBox();
            this.label_groupName = new System.Windows.Forms.Label();
            this.button_gropup_privious = new System.Windows.Forms.Button();
            this.label_triggerType = new System.Windows.Forms.Label();
            this.button_group_next = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_groupInfoEdit = new System.Windows.Forms.Button();
            this.button_record = new System.Windows.Forms.Button();
            this.dataGridView_fields = new System.Windows.Forms.DataGridView();
            this.Column_Device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_deviceFormatType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_field_up = new System.Windows.Forms.Button();
            this.button_field_down = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_field = new System.Windows.Forms.TabPage();
            this.textBox_deviceDescription = new System.Windows.Forms.TextBox();
            this.tabPage_log = new System.Windows.Forms.TabPage();
            this.button_csv_group = new System.Windows.Forms.Button();
            this.button_refreshLog = new System.Windows.Forms.Button();
            this.dataGridView_log = new System.Windows.Forms.DataGridView();
            this.menuStrip_mainMenu.SuspendLayout();
            this.groupBox_groupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_fields)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage_field.SuspendLayout();
            this.tabPage_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_log)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip_mainMenu
            // 
            this.menuStrip_mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.データDToolStripMenuItem,
            this.ツールToolStripMenuItem});
            this.menuStrip_mainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_mainMenu.Name = "menuStrip_mainMenu";
            this.menuStrip_mainMenu.Size = new System.Drawing.Size(684, 24);
            this.menuStrip_mainMenu.TabIndex = 14;
            this.menuStrip_mainMenu.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CSV_ToolStripMenuItem,
            this.ログ設定の保存SToolStripMenuItem,
            this.toolStripMenuItem_file_border1,
            this.状態ToolStripMenuItem,
            this.toolStripMenuItemfile_border2,
            this.終了XToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // CSV_ToolStripMenuItem
            // 
            this.CSV_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuI_CSV_allLog,
            this.toolStripMenuI_CSV_groupLog});
            this.CSV_ToolStripMenuItem.Name = "CSV_ToolStripMenuItem";
            this.CSV_ToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.CSV_ToolStripMenuItem.Text = "CSV";
            // 
            // toolStripMenuI_CSV_allLog
            // 
            this.toolStripMenuI_CSV_allLog.Name = "toolStripMenuI_CSV_allLog";
            this.toolStripMenuI_CSV_allLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.toolStripMenuI_CSV_allLog.Size = new System.Drawing.Size(360, 22);
            this.toolStripMenuI_CSV_allLog.Text = "すべてのログをCSVファイルに保存(&L)";
            this.toolStripMenuI_CSV_allLog.Click += new System.EventHandler(this.toolStripMenuI_CSV_allLog_Click);
            // 
            // toolStripMenuI_CSV_groupLog_Click
            // 
            this.toolStripMenuI_CSV_groupLog.Name = "toolStripMenuI_CSV_groupLog_Click";
            this.toolStripMenuI_CSV_groupLog.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.toolStripMenuI_CSV_groupLog.Size = new System.Drawing.Size(360, 22);
            this.toolStripMenuI_CSV_groupLog.Text = "現在のグループのログのみをCSVファイルに保存";
            this.toolStripMenuI_CSV_groupLog.Click += new System.EventHandler(this.toolStripMenuI_CSV_groupLog_Click);
            // 
            // ログ設定の保存SToolStripMenuItem
            // 
            this.ログ設定の保存SToolStripMenuItem.Name = "ログ設定の保存SToolStripMenuItem";
            this.ログ設定の保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ログ設定の保存SToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.ログ設定の保存SToolStripMenuItem.Text = "ログ設定の保存(&S)";
            this.ログ設定の保存SToolStripMenuItem.Click += new System.EventHandler(this.ログ設定の保存SToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_file_border1
            // 
            this.toolStripMenuItem_file_border1.Name = "toolStripMenuItem_file_border1";
            this.toolStripMenuItem_file_border1.Size = new System.Drawing.Size(200, 6);
            // 
            // 状態ToolStripMenuItem
            // 
            this.状態ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スタンバイToolStripMenuItem,
            this.休止ToolStripMenuItem});
            this.状態ToolStripMenuItem.Name = "状態ToolStripMenuItem";
            this.状態ToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.状態ToolStripMenuItem.Text = "状態";
            // 
            // スタンバイToolStripMenuItem
            // 
            this.スタンバイToolStripMenuItem.Name = "スタンバイToolStripMenuItem";
            this.スタンバイToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.スタンバイToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.スタンバイToolStripMenuItem.Text = "　スタンバイ";
            this.スタンバイToolStripMenuItem.Click += new System.EventHandler(this.スタンバイToolStripMenuItem_Click);
            // 
            // 休止ToolStripMenuItem
            // 
            this.休止ToolStripMenuItem.Name = "休止ToolStripMenuItem";
            this.休止ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.休止ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.休止ToolStripMenuItem.Text = "●休止";
            this.休止ToolStripMenuItem.Click += new System.EventHandler(this.休止ToolStripMenuItem_Click);
            // 
            // toolStripMenuItemfile_border2
            // 
            this.toolStripMenuItemfile_border2.Name = "toolStripMenuItemfile_border2";
            this.toolStripMenuItemfile_border2.Size = new System.Drawing.Size(200, 6);
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.グループToolStripMenuItem});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // グループToolStripMenuItem
            // 
            this.グループToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加ToolStripMenuItem,
            this.削除ToolStripMenuItem1,
            this.並び替えToolStripMenuItem});
            this.グループToolStripMenuItem.Name = "グループToolStripMenuItem";
            this.グループToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.グループToolStripMenuItem.Text = "グループ";
            // 
            // 追加ToolStripMenuItem
            // 
            this.追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
            this.追加ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.追加ToolStripMenuItem.Text = "追加";
            this.追加ToolStripMenuItem.Click += new System.EventHandler(this.追加ToolStripMenuItem_Click);
            // 
            // 削除ToolStripMenuItem1
            // 
            this.削除ToolStripMenuItem1.Name = "削除ToolStripMenuItem1";
            this.削除ToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.削除ToolStripMenuItem1.Text = "削除";
            this.削除ToolStripMenuItem1.Click += new System.EventHandler(this.削除ToolStripMenuItem1_Click);
            // 
            // 並び替えToolStripMenuItem
            // 
            this.並び替えToolStripMenuItem.Name = "並び替えToolStripMenuItem";
            this.並び替えToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.並び替えToolStripMenuItem.Text = "並び替え";
            this.並び替えToolStripMenuItem.Click += new System.EventHandler(this.並び替えToolStripMenuItem_Click);
            // 
            // データDToolStripMenuItem
            // 
            this.データDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ログデータの初期化ToolStripMenuItem});
            this.データDToolStripMenuItem.Name = "データDToolStripMenuItem";
            this.データDToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.データDToolStripMenuItem.Text = "データ(&D)";
            // 
            // ログデータの初期化ToolStripMenuItem
            // 
            this.ログデータの初期化ToolStripMenuItem.Name = "ログデータの初期化ToolStripMenuItem";
            this.ログデータの初期化ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.ログデータの初期化ToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.ログデータの初期化ToolStripMenuItem.Text = "ログデータの初期化";
            this.ログデータの初期化ToolStripMenuItem.Click += new System.EventHandler(this.ログデータの初期化ToolStripMenuItem_Click);
            // 
            // ツールToolStripMenuItem
            // 
            this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通信設定ToolStripMenuItem});
            this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
            this.ツールToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ツールToolStripMenuItem.Text = "ツール(&T)";
            // 
            // 通信設定ToolStripMenuItem
            // 
            this.通信設定ToolStripMenuItem.Name = "通信設定ToolStripMenuItem";
            this.通信設定ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.通信設定ToolStripMenuItem.Text = "設定(&S)";
            this.通信設定ToolStripMenuItem.Click += new System.EventHandler(this.通信設定ToolStripMenuItem_Click);
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(8, 417);
            this.textBox_message.Multiline = true;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_message.Size = new System.Drawing.Size(660, 57);
            this.textBox_message.TabIndex = 16;
            // 
            // groupBox_groupInfo
            // 
            this.groupBox_groupInfo.Controls.Add(this.label_recording);
            this.groupBox_groupInfo.Controls.Add(this.textBox_trigger);
            this.groupBox_groupInfo.Controls.Add(this.textBox_groupName);
            this.groupBox_groupInfo.Controls.Add(this.label_groupName);
            this.groupBox_groupInfo.Controls.Add(this.button_gropup_privious);
            this.groupBox_groupInfo.Controls.Add(this.label_triggerType);
            this.groupBox_groupInfo.Controls.Add(this.button_group_next);
            this.groupBox_groupInfo.Controls.Add(this.button_stop);
            this.groupBox_groupInfo.Controls.Add(this.button_groupInfoEdit);
            this.groupBox_groupInfo.Controls.Add(this.button_record);
            this.groupBox_groupInfo.Location = new System.Drawing.Point(12, 28);
            this.groupBox_groupInfo.Name = "groupBox_groupInfo";
            this.groupBox_groupInfo.Size = new System.Drawing.Size(654, 78);
            this.groupBox_groupInfo.TabIndex = 17;
            this.groupBox_groupInfo.TabStop = false;
            this.groupBox_groupInfo.Text = "設定グループ情報";
            // 
            // label_recording
            // 
            this.label_recording.AutoSize = true;
            this.label_recording.Location = new System.Drawing.Point(588, 21);
            this.label_recording.Name = "label_recording";
            this.label_recording.Size = new System.Drawing.Size(41, 12);
            this.label_recording.TabIndex = 13;
            this.label_recording.Text = "記録中";
            // 
            // textBox_trigger
            // 
            this.textBox_trigger.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_trigger.Location = new System.Drawing.Point(71, 49);
            this.textBox_trigger.Name = "textBox_trigger";
            this.textBox_trigger.ReadOnly = true;
            this.textBox_trigger.Size = new System.Drawing.Size(202, 19);
            this.textBox_trigger.TabIndex = 9;
            // 
            // textBox_groupName
            // 
            this.textBox_groupName.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_groupName.Location = new System.Drawing.Point(71, 23);
            this.textBox_groupName.Name = "textBox_groupName";
            this.textBox_groupName.ReadOnly = true;
            this.textBox_groupName.Size = new System.Drawing.Size(202, 19);
            this.textBox_groupName.TabIndex = 9;
            // 
            // label_groupName
            // 
            this.label_groupName.AutoSize = true;
            this.label_groupName.Location = new System.Drawing.Point(36, 26);
            this.label_groupName.Name = "label_groupName";
            this.label_groupName.Size = new System.Drawing.Size(29, 12);
            this.label_groupName.TabIndex = 11;
            this.label_groupName.Text = "名前";
            // 
            // button_gropup_privious
            // 
            this.button_gropup_privious.Enabled = false;
            this.button_gropup_privious.Location = new System.Drawing.Point(280, 21);
            this.button_gropup_privious.Name = "button_gropup_privious";
            this.button_gropup_privious.Size = new System.Drawing.Size(32, 23);
            this.button_gropup_privious.TabIndex = 6;
            this.button_gropup_privious.Text = "＜";
            this.button_gropup_privious.UseVisualStyleBackColor = true;
            this.button_gropup_privious.Click += new System.EventHandler(this.button_gropup_privious_Click);
            // 
            // label_triggerType
            // 
            this.label_triggerType.AutoSize = true;
            this.label_triggerType.Location = new System.Drawing.Point(12, 51);
            this.label_triggerType.Name = "label_triggerType";
            this.label_triggerType.Size = new System.Drawing.Size(53, 12);
            this.label_triggerType.TabIndex = 12;
            this.label_triggerType.Text = "動作条件";
            // 
            // button_group_next
            // 
            this.button_group_next.Enabled = false;
            this.button_group_next.Location = new System.Drawing.Point(318, 21);
            this.button_group_next.Name = "button_group_next";
            this.button_group_next.Size = new System.Drawing.Size(32, 23);
            this.button_group_next.TabIndex = 7;
            this.button_group_next.Text = "＞";
            this.button_group_next.UseVisualStyleBackColor = true;
            this.button_group_next.Click += new System.EventHandler(this.button_group_next_Click);
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(573, 45);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 10;
            this.button_stop.Text = "停止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_groupInfoEdit
            // 
            this.button_groupInfoEdit.Location = new System.Drawing.Point(280, 46);
            this.button_groupInfoEdit.Name = "button_groupInfoEdit";
            this.button_groupInfoEdit.Size = new System.Drawing.Size(75, 23);
            this.button_groupInfoEdit.TabIndex = 10;
            this.button_groupInfoEdit.Text = "編集";
            this.button_groupInfoEdit.UseVisualStyleBackColor = true;
            this.button_groupInfoEdit.Click += new System.EventHandler(this.button_groupInfoEdit_Click);
            // 
            // button_record
            // 
            this.button_record.Enabled = false;
            this.button_record.Location = new System.Drawing.Point(573, 16);
            this.button_record.Name = "button_record";
            this.button_record.Size = new System.Drawing.Size(75, 23);
            this.button_record.TabIndex = 10;
            this.button_record.Text = "記録";
            this.button_record.UseVisualStyleBackColor = true;
            this.button_record.Visible = false;
            this.button_record.Click += new System.EventHandler(this.button_record_Click);
            // 
            // dataGridView_fields
            // 
            this.dataGridView_fields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_fields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Device,
            this.Column_deviceFormatType,
            this.Column_detail});
            this.dataGridView_fields.Location = new System.Drawing.Point(11, 12);
            this.dataGridView_fields.Name = "dataGridView_fields";
            this.dataGridView_fields.RowHeadersWidth = 15;
            this.dataGridView_fields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_fields.RowTemplate.Height = 21;
            this.dataGridView_fields.Size = new System.Drawing.Size(468, 255);
            this.dataGridView_fields.TabIndex = 16;
            this.dataGridView_fields.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridView_fields_CellContextMenuStripNeeded);
            // 
            // Column_Device
            // 
            this.Column_Device.DataPropertyName = "DeviceName";
            this.Column_Device.FillWeight = 90F;
            this.Column_Device.HeaderText = "デバイス名";
            this.Column_Device.Name = "Column_Device";
            this.Column_Device.Width = 80;
            // 
            // Column_deviceFormatType
            // 
            this.Column_deviceFormatType.DataPropertyName = "DeviceFormatType";
            this.Column_deviceFormatType.HeaderText = "データ型";
            this.Column_deviceFormatType.Name = "Column_deviceFormatType";
            this.Column_deviceFormatType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_deviceFormatType.Width = 85;
            // 
            // Column_detail
            // 
            this.Column_detail.DataPropertyName = "Detail";
            this.Column_detail.HeaderText = "詳細";
            this.Column_detail.Name = "Column_detail";
            this.Column_detail.Width = 260;
            // 
            // button_field_up
            // 
            this.button_field_up.Location = new System.Drawing.Point(513, 12);
            this.button_field_up.Name = "button_field_up";
            this.button_field_up.Size = new System.Drawing.Size(80, 23);
            this.button_field_up.TabIndex = 18;
            this.button_field_up.Text = "↑";
            this.button_field_up.UseVisualStyleBackColor = true;
            this.button_field_up.Click += new System.EventHandler(this.button_field_up_Click);
            // 
            // button_field_down
            // 
            this.button_field_down.Location = new System.Drawing.Point(513, 41);
            this.button_field_down.Name = "button_field_down";
            this.button_field_down.Size = new System.Drawing.Size(80, 23);
            this.button_field_down.TabIndex = 18;
            this.button_field_down.Text = "↓";
            this.button_field_down.UseVisualStyleBackColor = true;
            this.button_field_down.Click += new System.EventHandler(this.button_field_down_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_field);
            this.tabControl.Controls.Add(this.tabPage_log);
            this.tabControl.Location = new System.Drawing.Point(11, 112);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(661, 299);
            this.tabControl.TabIndex = 19;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage_field
            // 
            this.tabPage_field.Controls.Add(this.textBox_deviceDescription);
            this.tabPage_field.Controls.Add(this.dataGridView_fields);
            this.tabPage_field.Controls.Add(this.button_field_up);
            this.tabPage_field.Controls.Add(this.button_field_down);
            this.tabPage_field.Location = new System.Drawing.Point(4, 22);
            this.tabPage_field.Name = "tabPage_field";
            this.tabPage_field.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_field.Size = new System.Drawing.Size(653, 273);
            this.tabPage_field.TabIndex = 0;
            this.tabPage_field.Text = "フィールド";
            this.tabPage_field.UseVisualStyleBackColor = true;
            // 
            // textBox_deviceDescription
            // 
            this.textBox_deviceDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_deviceDescription.Location = new System.Drawing.Point(499, 74);
            this.textBox_deviceDescription.Multiline = true;
            this.textBox_deviceDescription.Name = "textBox_deviceDescription";
            this.textBox_deviceDescription.ReadOnly = true;
            this.textBox_deviceDescription.Size = new System.Drawing.Size(111, 193);
            this.textBox_deviceDescription.TabIndex = 19;
            this.textBox_deviceDescription.TabStop = false;
            this.textBox_deviceDescription.Text = "デバイスメモ\r\n\r\nTN:タイマー値\r\nTC:コイル\r\nTS:接点";
            // 
            // tabPage_log
            // 
            this.tabPage_log.Controls.Add(this.button_csv_group);
            this.tabPage_log.Controls.Add(this.button_refreshLog);
            this.tabPage_log.Controls.Add(this.dataGridView_log);
            this.tabPage_log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_log.Name = "tabPage_log";
            this.tabPage_log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_log.Size = new System.Drawing.Size(653, 273);
            this.tabPage_log.TabIndex = 1;
            this.tabPage_log.Text = "ログ";
            this.tabPage_log.UseVisualStyleBackColor = true;
            // 
            // button_csv_group
            // 
            this.button_csv_group.Location = new System.Drawing.Point(6, 7);
            this.button_csv_group.Name = "button_csv_group";
            this.button_csv_group.Size = new System.Drawing.Size(75, 23);
            this.button_csv_group.TabIndex = 1;
            this.button_csv_group.Text = "CSV";
            this.button_csv_group.UseVisualStyleBackColor = true;
            this.button_csv_group.Click += new System.EventHandler(this.button_csv_group_Click);
            // 
            // button_refreshLog
            // 
            this.button_refreshLog.Location = new System.Drawing.Point(568, 7);
            this.button_refreshLog.Name = "button_refreshLog";
            this.button_refreshLog.Size = new System.Drawing.Size(75, 23);
            this.button_refreshLog.TabIndex = 1;
            this.button_refreshLog.Text = "更新";
            this.button_refreshLog.UseVisualStyleBackColor = true;
            this.button_refreshLog.Click += new System.EventHandler(this.button_refreshLog_Click);
            // 
            // dataGridView_log
            // 
            this.dataGridView_log.AllowUserToAddRows = false;
            this.dataGridView_log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_log.Location = new System.Drawing.Point(5, 36);
            this.dataGridView_log.Name = "dataGridView_log";
            this.dataGridView_log.ReadOnly = true;
            this.dataGridView_log.RowHeadersWidth = 95;
            this.dataGridView_log.RowTemplate.Height = 21;
            this.dataGridView_log.Size = new System.Drawing.Size(642, 231);
            this.dataGridView_log.TabIndex = 0;
            this.dataGridView_log.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridView_log_CellToolTipTextNeeded);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 486);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox_groupInfo);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.menuStrip_mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_mainMenu;
            this.Name = "MainDialog";
            this.Text = "MX Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainDialog_FormClosing);
            this.menuStrip_mainMenu.ResumeLayout(false);
            this.menuStrip_mainMenu.PerformLayout();
            this.groupBox_groupInfo.ResumeLayout(false);
            this.groupBox_groupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_fields)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage_field.ResumeLayout(false);
            this.tabPage_field.PerformLayout();
            this.tabPage_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_log)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_mainMenu;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
//        private System.Windows.Forms.ToolStripMenuItem CSV_all_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_file_border1;
        private System.Windows.Forms.ToolStripMenuItem 状態ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemfile_border2;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通信設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem グループToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 削除ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem スタンバイToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 休止ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.GroupBox groupBox_groupInfo;
        private System.Windows.Forms.TextBox textBox_groupName;
        private System.Windows.Forms.Label label_groupName;
        private System.Windows.Forms.Label label_triggerType;
        private System.Windows.Forms.Button button_gropup_privious;
        private System.Windows.Forms.Button button_group_next;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_groupInfoEdit;
        private System.Windows.Forms.Button button_record;
        private System.Windows.Forms.DataGridView dataGridView_fields;
//        private System.Windows.Forms.DataGridViewTextBoxColumn Column_deviceName;
        private System.Windows.Forms.Button button_field_up;
        private System.Windows.Forms.Button button_field_down;
        private System.Windows.Forms.TextBox textBox_trigger;
        private System.Windows.Forms.ToolStripMenuItem データDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ログデータの初期化ToolStripMenuItem;
        private System.Windows.Forms.Label label_recording;
        private System.Windows.Forms.ToolStripMenuItem 並び替えToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_field;
        private System.Windows.Forms.TextBox textBox_deviceDescription;
        private System.Windows.Forms.TabPage tabPage_log;
        private System.Windows.Forms.Button button_refreshLog;
        private System.Windows.Forms.DataGridView dataGridView_log;
        private System.Windows.Forms.Button button_csv_group;
        private System.Windows.Forms.ToolStripMenuItem ログ設定の保存SToolStripMenuItem;
//        private System.Windows.Forms.ToolStripMenuItem CSV_group_ToolStripMenuItem;
//        private System.Windows.Forms.ToolStripMenuItem CSV_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CSV_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuI_CSV_allLog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuI_CSV_groupLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Device;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_deviceFormatType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_detail;
    }
}

