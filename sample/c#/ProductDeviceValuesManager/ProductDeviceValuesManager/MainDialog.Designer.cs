namespace ProductDeviceValuesManager
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.現在のグループの値をデフォルトにするToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.グループToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.接続先ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.実機ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.シミュレータToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulator2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulator3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.groupBox_singleProduct = new System.Windows.Forms.GroupBox();
            this.label_reference_display = new System.Windows.Forms.Label();
            this.label_sirial_display = new System.Windows.Forms.Label();
            this.label_reference = new System.Windows.Forms.Label();
            this.label_name_display = new System.Windows.Forms.Label();
            this.label_serial_label = new System.Windows.Forms.Label();
            this.label_name_label = new System.Windows.Forms.Label();
            this.groupBox_buttons = new System.Windows.Forms.GroupBox();
            this.button_product_select = new System.Windows.Forms.Button();
            this.button_defaultValue_edit = new System.Windows.Forms.Button();
            this.label_fileName_display = new System.Windows.Forms.Label();
            this.label_fileName_label = new System.Windows.Forms.Label();
            this.tabPage_device = new System.Windows.Forms.TabPage();
            this.groupBox_button_device = new System.Windows.Forms.GroupBox();
            this.button_read_default = new System.Windows.Forms.Button();
            this.button_write_group = new System.Windows.Forms.Button();
            this.button_read_group = new System.Windows.Forms.Button();
            this.button_read_all = new System.Windows.Forms.Button();
            this.dataGridView_device = new System.Windows.Forms.DataGridView();
            this.Column_detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_groupIndex_dev = new System.Windows.Forms.Label();
            this.label_groupName_display_dev = new System.Windows.Forms.Label();
            this.label_groupName_dev = new System.Windows.Forms.Label();
            this.button_group_next_dev = new System.Windows.Forms.Button();
            this.button_group_previous_dev = new System.Windows.Forms.Button();
            this.tabPage_textMemo = new System.Windows.Forms.TabPage();
            this.dataGridView_text = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_groupIndex_text = new System.Windows.Forms.Label();
            this.label_groupName_display_text = new System.Windows.Forms.Label();
            this.label_groupName_text = new System.Windows.Forms.Label();
            this.button_group_next_text = new System.Windows.Forms.Button();
            this.button_group_previous_text = new System.Windows.Forms.Button();
            this.tabPage_record = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.messageTextBox1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MessageTextBox();
            this.名前を付けて保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.groupBox_singleProduct.SuspendLayout();
            this.groupBox_buttons.SuspendLayout();
            this.tabPage_device.SuspendLayout();
            this.groupBox_button_device.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_device)).BeginInit();
            this.tabPage_textMemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_text)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.設定SToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(581, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開くOToolStripMenuItem,
            this.新規作成NToolStripMenuItem,
            this.保存SToolStripMenuItem,
            this.名前を付けて保存ToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 開くOToolStripMenuItem
            // 
            this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            this.開くOToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.開くOToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.開くOToolStripMenuItem.Text = "開く(&O)";
            this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
            // 
            // 新規作成NToolStripMenuItem
            // 
            this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
            this.新規作成NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
            this.新規作成NToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripMenuItem_Click);
            // 
            // 保存SToolStripMenuItem
            // 
            this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
            this.保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存SToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.保存SToolStripMenuItem.Text = "保存(&S)";
            this.保存SToolStripMenuItem.Click += new System.EventHandler(this.保存SToolStripMenuItem_Click);
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.現在のグループの値をデフォルトにするToolStripMenuItem});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // 現在のグループの値をデフォルトにするToolStripMenuItem
            // 
            this.現在のグループの値をデフォルトにするToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.グループToolStripMenuItem,
            this.全体ToolStripMenuItem});
            this.現在のグループの値をデフォルトにするToolStripMenuItem.Name = "現在のグループの値をデフォルトにするToolStripMenuItem";
            this.現在のグループの値をデフォルトにするToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.現在のグループの値をデフォルトにするToolStripMenuItem.Text = "現在の値をデフォルトにする";
            // 
            // グループToolStripMenuItem
            // 
            this.グループToolStripMenuItem.Name = "グループToolStripMenuItem";
            this.グループToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.グループToolStripMenuItem.Text = "グループ";
            this.グループToolStripMenuItem.Click += new System.EventHandler(this.グループToolStripMenuItem_Click);
            // 
            // 全体ToolStripMenuItem
            // 
            this.全体ToolStripMenuItem.Name = "全体ToolStripMenuItem";
            this.全体ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.全体ToolStripMenuItem.Text = "全体";
            this.全体ToolStripMenuItem.Click += new System.EventHandler(this.全体ToolStripMenuItem_Click);
            // 
            // 設定SToolStripMenuItem
            // 
            this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.接続先ToolStripMenuItem});
            this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
            this.設定SToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.設定SToolStripMenuItem.Text = "ツール(&T)";
            // 
            // 接続先ToolStripMenuItem
            // 
            this.接続先ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.実機ToolStripMenuItem,
            this.シミュレータToolStripMenuItem});
            this.接続先ToolStripMenuItem.Name = "接続先ToolStripMenuItem";
            this.接続先ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.接続先ToolStripMenuItem.Text = "接続先";
            this.接続先ToolStripMenuItem.DropDownOpening += new System.EventHandler(this.接続先ToolStripMenuItem_DropDownOpening);
            // 
            // 実機ToolStripMenuItem
            // 
            this.実機ToolStripMenuItem.Name = "実機ToolStripMenuItem";
            this.実機ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.実機ToolStripMenuItem.Text = "実機";
            this.実機ToolStripMenuItem.Click += new System.EventHandler(this.実機ToolStripMenuItem_Click);
            // 
            // シミュレータToolStripMenuItem
            // 
            this.シミュレータToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulatorToolStripMenuItem,
            this.simulator2ToolStripMenuItem,
            this.simulator3ToolStripMenuItem});
            this.シミュレータToolStripMenuItem.Name = "シミュレータToolStripMenuItem";
            this.シミュレータToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.シミュレータToolStripMenuItem.Text = "シミュレータ";
            // 
            // simulatorToolStripMenuItem
            // 
            this.simulatorToolStripMenuItem.Name = "simulatorToolStripMenuItem";
            this.simulatorToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.simulatorToolStripMenuItem.Text = "Simulator";
            this.simulatorToolStripMenuItem.Click += new System.EventHandler(this.simulatorToolStripMenuItem_Click);
            // 
            // simulator2ToolStripMenuItem
            // 
            this.simulator2ToolStripMenuItem.Name = "simulator2ToolStripMenuItem";
            this.simulator2ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.simulator2ToolStripMenuItem.Text = "Simulator2";
            this.simulator2ToolStripMenuItem.Click += new System.EventHandler(this.simulator2ToolStripMenuItem_Click);
            // 
            // simulator3ToolStripMenuItem
            // 
            this.simulator3ToolStripMenuItem.Name = "simulator3ToolStripMenuItem";
            this.simulator3ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.simulator3ToolStripMenuItem.Text = "Simulator3";
            this.simulator3ToolStripMenuItem.Click += new System.EventHandler(this.simulator3ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_general);
            this.tabControl1.Controls.Add(this.tabPage_device);
            this.tabControl1.Controls.Add(this.tabPage_textMemo);
            this.tabControl1.Controls.Add(this.tabPage_record);
            this.tabControl1.Location = new System.Drawing.Point(12, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 307);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.groupBox_singleProduct);
            this.tabPage_general.Controls.Add(this.groupBox_buttons);
            this.tabPage_general.Controls.Add(this.label_fileName_display);
            this.tabPage_general.Controls.Add(this.label_fileName_label);
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(548, 281);
            this.tabPage_general.TabIndex = 0;
            this.tabPage_general.Text = "基本";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // groupBox_singleProduct
            // 
            this.groupBox_singleProduct.Controls.Add(this.label_reference_display);
            this.groupBox_singleProduct.Controls.Add(this.label_sirial_display);
            this.groupBox_singleProduct.Controls.Add(this.label_reference);
            this.groupBox_singleProduct.Controls.Add(this.label_name_display);
            this.groupBox_singleProduct.Controls.Add(this.label_serial_label);
            this.groupBox_singleProduct.Controls.Add(this.label_name_label);
            this.groupBox_singleProduct.Location = new System.Drawing.Point(13, 93);
            this.groupBox_singleProduct.Name = "groupBox_singleProduct";
            this.groupBox_singleProduct.Size = new System.Drawing.Size(348, 166);
            this.groupBox_singleProduct.TabIndex = 2;
            this.groupBox_singleProduct.TabStop = false;
            this.groupBox_singleProduct.Text = "個別製品";
            // 
            // label_reference_display
            // 
            this.label_reference_display.BackColor = System.Drawing.SystemColors.Window;
            this.label_reference_display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_reference_display.Location = new System.Drawing.Point(76, 91);
            this.label_reference_display.Name = "label_reference_display";
            this.label_reference_display.Size = new System.Drawing.Size(266, 32);
            this.label_reference_display.TabIndex = 8;
            // 
            // label_sirial_display
            // 
            this.label_sirial_display.BackColor = System.Drawing.SystemColors.Window;
            this.label_sirial_display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_sirial_display.Location = new System.Drawing.Point(76, 59);
            this.label_sirial_display.Name = "label_sirial_display";
            this.label_sirial_display.Size = new System.Drawing.Size(266, 16);
            this.label_sirial_display.TabIndex = 8;
            this.label_sirial_display.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_reference
            // 
            this.label_reference.AutoSize = true;
            this.label_reference.Location = new System.Drawing.Point(19, 93);
            this.label_reference.Name = "label_reference";
            this.label_reference.Size = new System.Drawing.Size(29, 12);
            this.label_reference.TabIndex = 7;
            this.label_reference.Text = "備考";
            // 
            // label_name_display
            // 
            this.label_name_display.BackColor = System.Drawing.SystemColors.Window;
            this.label_name_display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_name_display.Location = new System.Drawing.Point(76, 30);
            this.label_name_display.Name = "label_name_display";
            this.label_name_display.Size = new System.Drawing.Size(266, 16);
            this.label_name_display.TabIndex = 8;
            this.label_name_display.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_serial_label
            // 
            this.label_serial_label.AutoSize = true;
            this.label_serial_label.Location = new System.Drawing.Point(19, 61);
            this.label_serial_label.Name = "label_serial_label";
            this.label_serial_label.Size = new System.Drawing.Size(41, 12);
            this.label_serial_label.TabIndex = 7;
            this.label_serial_label.Text = "シリアル";
            // 
            // label_name_label
            // 
            this.label_name_label.AutoSize = true;
            this.label_name_label.Location = new System.Drawing.Point(19, 32);
            this.label_name_label.Name = "label_name_label";
            this.label_name_label.Size = new System.Drawing.Size(29, 12);
            this.label_name_label.TabIndex = 7;
            this.label_name_label.Text = "名前";
            // 
            // groupBox_buttons
            // 
            this.groupBox_buttons.Controls.Add(this.button_product_select);
            this.groupBox_buttons.Controls.Add(this.button_defaultValue_edit);
            this.groupBox_buttons.Location = new System.Drawing.Point(367, 17);
            this.groupBox_buttons.Name = "groupBox_buttons";
            this.groupBox_buttons.Size = new System.Drawing.Size(162, 242);
            this.groupBox_buttons.TabIndex = 2;
            this.groupBox_buttons.TabStop = false;
            this.groupBox_buttons.Text = "ボタン";
            // 
            // button_product_select
            // 
            this.button_product_select.Location = new System.Drawing.Point(19, 27);
            this.button_product_select.Name = "button_product_select";
            this.button_product_select.Size = new System.Drawing.Size(120, 23);
            this.button_product_select.TabIndex = 0;
            this.button_product_select.Text = "個別製品の選択";
            this.button_product_select.UseVisualStyleBackColor = true;
            this.button_product_select.Click += new System.EventHandler(this.button_products_edit_Click);
            // 
            // button_defaultValue_edit
            // 
            this.button_defaultValue_edit.Location = new System.Drawing.Point(19, 76);
            this.button_defaultValue_edit.Name = "button_defaultValue_edit";
            this.button_defaultValue_edit.Size = new System.Drawing.Size(120, 23);
            this.button_defaultValue_edit.TabIndex = 0;
            this.button_defaultValue_edit.Text = "デフォルト値の設定";
            this.button_defaultValue_edit.UseVisualStyleBackColor = true;
            this.button_defaultValue_edit.Click += new System.EventHandler(this.button_defaultValue_edit_Click);
            // 
            // label_fileName_display
            // 
            this.label_fileName_display.BackColor = System.Drawing.SystemColors.Window;
            this.label_fileName_display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_fileName_display.Location = new System.Drawing.Point(68, 24);
            this.label_fileName_display.Name = "label_fileName_display";
            this.label_fileName_display.Size = new System.Drawing.Size(293, 16);
            this.label_fileName_display.TabIndex = 1;
            this.label_fileName_display.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_fileName_label
            // 
            this.label_fileName_label.AutoSize = true;
            this.label_fileName_label.Location = new System.Drawing.Point(11, 25);
            this.label_fileName_label.Name = "label_fileName_label";
            this.label_fileName_label.Size = new System.Drawing.Size(51, 12);
            this.label_fileName_label.TabIndex = 1;
            this.label_fileName_label.Text = "ファイル名";
            // 
            // tabPage_device
            // 
            this.tabPage_device.Controls.Add(this.groupBox_button_device);
            this.tabPage_device.Controls.Add(this.dataGridView_device);
            this.tabPage_device.Controls.Add(this.label_groupIndex_dev);
            this.tabPage_device.Controls.Add(this.label_groupName_display_dev);
            this.tabPage_device.Controls.Add(this.label_groupName_dev);
            this.tabPage_device.Controls.Add(this.button_group_next_dev);
            this.tabPage_device.Controls.Add(this.button_group_previous_dev);
            this.tabPage_device.Location = new System.Drawing.Point(4, 22);
            this.tabPage_device.Name = "tabPage_device";
            this.tabPage_device.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_device.Size = new System.Drawing.Size(548, 281);
            this.tabPage_device.TabIndex = 3;
            this.tabPage_device.Text = "デバイス";
            this.tabPage_device.UseVisualStyleBackColor = true;
            // 
            // groupBox_button_device
            // 
            this.groupBox_button_device.Controls.Add(this.button_read_default);
            this.groupBox_button_device.Controls.Add(this.button_write_group);
            this.groupBox_button_device.Controls.Add(this.button_read_group);
            this.groupBox_button_device.Controls.Add(this.button_read_all);
            this.groupBox_button_device.Location = new System.Drawing.Point(321, 69);
            this.groupBox_button_device.Name = "groupBox_button_device";
            this.groupBox_button_device.Size = new System.Drawing.Size(176, 177);
            this.groupBox_button_device.TabIndex = 21;
            this.groupBox_button_device.TabStop = false;
            this.groupBox_button_device.Text = "ボタン";
            // 
            // button_read_default
            // 
            this.button_read_default.Location = new System.Drawing.Point(17, 97);
            this.button_read_default.Name = "button_read_default";
            this.button_read_default.Size = new System.Drawing.Size(138, 23);
            this.button_read_default.TabIndex = 0;
            this.button_read_default.Text = "デフォルト値の読み込み";
            this.button_read_default.UseVisualStyleBackColor = true;
            this.button_read_default.Click += new System.EventHandler(this.button_read_default_Click);
            // 
            // button_write_group
            // 
            this.button_write_group.Location = new System.Drawing.Point(17, 140);
            this.button_write_group.Name = "button_write_group";
            this.button_write_group.Size = new System.Drawing.Size(138, 23);
            this.button_write_group.TabIndex = 0;
            this.button_write_group.Text = "このグループのみ書き込み";
            this.button_write_group.UseVisualStyleBackColor = true;
            this.button_write_group.Click += new System.EventHandler(this.button_write_group_Click);
            // 
            // button_read_group
            // 
            this.button_read_group.Location = new System.Drawing.Point(17, 18);
            this.button_read_group.Name = "button_read_group";
            this.button_read_group.Size = new System.Drawing.Size(138, 23);
            this.button_read_group.TabIndex = 0;
            this.button_read_group.Text = "このグループのみ読み込み";
            this.button_read_group.UseVisualStyleBackColor = true;
            this.button_read_group.Click += new System.EventHandler(this.button_read_group_Click);
            // 
            // button_read_all
            // 
            this.button_read_all.Location = new System.Drawing.Point(17, 68);
            this.button_read_all.Name = "button_read_all";
            this.button_read_all.Size = new System.Drawing.Size(138, 23);
            this.button_read_all.TabIndex = 0;
            this.button_read_all.Text = "すべて読み込み";
            this.button_read_all.UseVisualStyleBackColor = true;
            this.button_read_all.Click += new System.EventHandler(this.button_read_all_Click);
            // 
            // dataGridView_device
            // 
            this.dataGridView_device.AllowUserToAddRows = false;
            this.dataGridView_device.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_device.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_detail,
            this.Column_Device});
            this.dataGridView_device.Location = new System.Drawing.Point(8, 28);
            this.dataGridView_device.Name = "dataGridView_device";
            this.dataGridView_device.RowHeadersWidth = 15;
            this.dataGridView_device.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_device.RowTemplate.Height = 21;
            this.dataGridView_device.Size = new System.Drawing.Size(288, 239);
            this.dataGridView_device.TabIndex = 20;
            this.dataGridView_device.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dataGridView_device_CellToolTipTextNeeded);
            this.dataGridView_device.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_device_CellValidated);
            // 
            // Column_detail
            // 
            this.Column_detail.DataPropertyName = "Detail";
            this.Column_detail.HeaderText = "項目名";
            this.Column_detail.Name = "Column_detail";
            this.Column_detail.ReadOnly = true;
            this.Column_detail.Width = 160;
            // 
            // Column_Device
            // 
            this.Column_Device.DataPropertyName = "CurrentValue";
            this.Column_Device.FillWeight = 90F;
            this.Column_Device.HeaderText = "値";
            this.Column_Device.Name = "Column_Device";
            this.Column_Device.Width = 80;
            // 
            // label_groupIndex_dev
            // 
            this.label_groupIndex_dev.AutoSize = true;
            this.label_groupIndex_dev.Location = new System.Drawing.Point(409, 11);
            this.label_groupIndex_dev.Name = "label_groupIndex_dev";
            this.label_groupIndex_dev.Size = new System.Drawing.Size(31, 12);
            this.label_groupIndex_dev.TabIndex = 19;
            this.label_groupIndex_dev.Text = "0 / 0";
            // 
            // label_groupName_display_dev
            // 
            this.label_groupName_display_dev.BackColor = System.Drawing.SystemColors.Window;
            this.label_groupName_display_dev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_groupName_display_dev.Location = new System.Drawing.Point(67, 9);
            this.label_groupName_display_dev.Name = "label_groupName_display_dev";
            this.label_groupName_display_dev.Size = new System.Drawing.Size(229, 16);
            this.label_groupName_display_dev.TabIndex = 9;
            this.label_groupName_display_dev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_groupName_dev
            // 
            this.label_groupName_dev.AutoSize = true;
            this.label_groupName_dev.Location = new System.Drawing.Point(6, 13);
            this.label_groupName_dev.Name = "label_groupName_dev";
            this.label_groupName_dev.Size = new System.Drawing.Size(55, 12);
            this.label_groupName_dev.TabIndex = 3;
            this.label_groupName_dev.Text = "グループ名";
            // 
            // button_group_next_dev
            // 
            this.button_group_next_dev.Location = new System.Drawing.Point(350, 6);
            this.button_group_next_dev.Name = "button_group_next_dev";
            this.button_group_next_dev.Size = new System.Drawing.Size(42, 23);
            this.button_group_next_dev.TabIndex = 0;
            this.button_group_next_dev.TabStop = false;
            this.button_group_next_dev.Text = "＞";
            this.button_group_next_dev.UseVisualStyleBackColor = true;
            this.button_group_next_dev.Click += new System.EventHandler(this.button_group_next_Click);
            // 
            // button_group_previous_dev
            // 
            this.button_group_previous_dev.Location = new System.Drawing.Point(302, 6);
            this.button_group_previous_dev.Name = "button_group_previous_dev";
            this.button_group_previous_dev.Size = new System.Drawing.Size(42, 23);
            this.button_group_previous_dev.TabIndex = 0;
            this.button_group_previous_dev.TabStop = false;
            this.button_group_previous_dev.Text = "＜";
            this.button_group_previous_dev.UseVisualStyleBackColor = true;
            this.button_group_previous_dev.Click += new System.EventHandler(this.button_group_previous_Click);
            // 
            // tabPage_textMemo
            // 
            this.tabPage_textMemo.Controls.Add(this.dataGridView_text);
            this.tabPage_textMemo.Controls.Add(this.label_groupIndex_text);
            this.tabPage_textMemo.Controls.Add(this.label_groupName_display_text);
            this.tabPage_textMemo.Controls.Add(this.label_groupName_text);
            this.tabPage_textMemo.Controls.Add(this.button_group_next_text);
            this.tabPage_textMemo.Controls.Add(this.button_group_previous_text);
            this.tabPage_textMemo.Location = new System.Drawing.Point(4, 22);
            this.tabPage_textMemo.Name = "tabPage_textMemo";
            this.tabPage_textMemo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_textMemo.Size = new System.Drawing.Size(548, 281);
            this.tabPage_textMemo.TabIndex = 4;
            this.tabPage_textMemo.Text = "テキスト情報";
            this.tabPage_textMemo.UseVisualStyleBackColor = true;
            // 
            // dataGridView_text
            // 
            this.dataGridView_text.AllowUserToAddRows = false;
            this.dataGridView_text.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_text.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dataGridView_text.Location = new System.Drawing.Point(8, 36);
            this.dataGridView_text.Name = "dataGridView_text";
            this.dataGridView_text.RowHeadersWidth = 15;
            this.dataGridView_text.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView_text.RowTemplate.Height = 21;
            this.dataGridView_text.Size = new System.Drawing.Size(288, 239);
            this.dataGridView_text.TabIndex = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FieldName";
            this.dataGridViewTextBoxColumn1.HeaderText = "項目名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FieldValue";
            this.dataGridViewTextBoxColumn2.FillWeight = 90F;
            this.dataGridViewTextBoxColumn2.HeaderText = "値";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // label_groupIndex_text
            // 
            this.label_groupIndex_text.AutoSize = true;
            this.label_groupIndex_text.Location = new System.Drawing.Point(409, 10);
            this.label_groupIndex_text.Name = "label_groupIndex_text";
            this.label_groupIndex_text.Size = new System.Drawing.Size(31, 12);
            this.label_groupIndex_text.TabIndex = 24;
            this.label_groupIndex_text.Text = "0 / 0";
            // 
            // label_groupName_display_text
            // 
            this.label_groupName_display_text.BackColor = System.Drawing.SystemColors.Window;
            this.label_groupName_display_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_groupName_display_text.Location = new System.Drawing.Point(67, 8);
            this.label_groupName_display_text.Name = "label_groupName_display_text";
            this.label_groupName_display_text.Size = new System.Drawing.Size(229, 16);
            this.label_groupName_display_text.TabIndex = 23;
            this.label_groupName_display_text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_groupName_text
            // 
            this.label_groupName_text.AutoSize = true;
            this.label_groupName_text.Location = new System.Drawing.Point(6, 12);
            this.label_groupName_text.Name = "label_groupName_text";
            this.label_groupName_text.Size = new System.Drawing.Size(55, 12);
            this.label_groupName_text.TabIndex = 22;
            this.label_groupName_text.Text = "グループ名";
            // 
            // button_group_next_text
            // 
            this.button_group_next_text.Location = new System.Drawing.Point(350, 5);
            this.button_group_next_text.Name = "button_group_next_text";
            this.button_group_next_text.Size = new System.Drawing.Size(42, 23);
            this.button_group_next_text.TabIndex = 20;
            this.button_group_next_text.TabStop = false;
            this.button_group_next_text.Text = "＞";
            this.button_group_next_text.UseVisualStyleBackColor = true;
            this.button_group_next_text.Click += new System.EventHandler(this.button_group_next_text_Click);
            // 
            // button_group_previous_text
            // 
            this.button_group_previous_text.Location = new System.Drawing.Point(302, 5);
            this.button_group_previous_text.Name = "button_group_previous_text";
            this.button_group_previous_text.Size = new System.Drawing.Size(42, 23);
            this.button_group_previous_text.TabIndex = 21;
            this.button_group_previous_text.TabStop = false;
            this.button_group_previous_text.Text = "＜";
            this.button_group_previous_text.UseVisualStyleBackColor = true;
            this.button_group_previous_text.Click += new System.EventHandler(this.button_group_previous_text_Click);
            // 
            // tabPage_record
            // 
            this.tabPage_record.Location = new System.Drawing.Point(4, 22);
            this.tabPage_record.Name = "tabPage_record";
            this.tabPage_record.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_record.Size = new System.Drawing.Size(548, 281);
            this.tabPage_record.TabIndex = 1;
            this.tabPage_record.Text = "レコード";
            this.tabPage_record.UseVisualStyleBackColor = true;
            // 
            // messageTextBox1
            // 
            this.messageTextBox1.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.messageTextBox1.Location = new System.Drawing.Point(16, 346);
            this.messageTextBox1.Multiline = true;
            this.messageTextBox1.Name = "messageTextBox1";
            this.messageTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextBox1.Size = new System.Drawing.Size(548, 51);
            this.messageTextBox1.TabIndex = 2;
            this.messageTextBox1.Text = "メッセージを表示します";
            // 
            // 名前を付けて保存ToolStripMenuItem
            // 
            this.名前を付けて保存ToolStripMenuItem.Name = "名前を付けて保存ToolStripMenuItem";
            this.名前を付けて保存ToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.名前を付けて保存ToolStripMenuItem.Text = "名前を付けて保存(&A)";
            this.名前を付けて保存ToolStripMenuItem.Click += new System.EventHandler(this.名前を付けて保存ToolStripMenuItem_Click);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 408);
            this.Controls.Add(this.messageTextBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainDialog";
            this.Text = "製品データ管理アプリ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.groupBox_singleProduct.ResumeLayout(false);
            this.groupBox_singleProduct.PerformLayout();
            this.groupBox_buttons.ResumeLayout(false);
            this.tabPage_device.ResumeLayout(false);
            this.tabPage_device.PerformLayout();
            this.groupBox_button_device.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_device)).EndInit();
            this.tabPage_textMemo.ResumeLayout(false);
            this.tabPage_textMemo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_text)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新規作成NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 現在のグループの値をデフォルトにするToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem グループToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全体ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_record;
        private System.Windows.Forms.TabPage tabPage_device;
        private System.Windows.Forms.Button button_group_next_dev;
        private System.Windows.Forms.Button button_group_previous_dev;
        private System.Windows.Forms.Button button_defaultValue_edit;
        private System.Windows.Forms.Button button_read_default;
        private System.Windows.Forms.Button button_write_group;
        private System.Windows.Forms.Button button_read_group;
        private System.Windows.Forms.Button button_read_all;
        private System.Windows.Forms.Button button_product_select;
        private System.Windows.Forms.GroupBox groupBox_buttons;
        private System.Windows.Forms.GroupBox groupBox_singleProduct;
        private System.Windows.Forms.GroupBox groupBox_button_device;
        private System.Windows.Forms.Label label_groupName_dev;
        private System.Windows.Forms.Label label_fileName_display;
        private System.Windows.Forms.Label label_fileName_label;
        private System.Windows.Forms.Label label_sirial_display;
        private System.Windows.Forms.Label label_name_display;
        private System.Windows.Forms.Label label_serial_label;
        private System.Windows.Forms.Label label_name_label;
        private System.Windows.Forms.Label label_groupIndex_dev;
        private System.Windows.Forms.Label label_groupName_display_dev;
        private System.Windows.Forms.DataGridView dataGridView_device;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Device;
        private System.Windows.Forms.ToolTip toolTip1;
        private Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MessageTextBox messageTextBox1;
        private System.Windows.Forms.Label label_reference_display;
        private System.Windows.Forms.Label label_reference;
        private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 接続先ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 実機ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem シミュレータToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulator2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulator3ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage_textMemo;
        private System.Windows.Forms.Label label_groupIndex_text;
        private System.Windows.Forms.Label label_groupName_display_text;
        private System.Windows.Forms.Label label_groupName_text;
        private System.Windows.Forms.Button button_group_next_text;
        private System.Windows.Forms.Button button_group_previous_text;
        private System.Windows.Forms.DataGridView dataGridView_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStripMenuItem 名前を付けて保存ToolStripMenuItem;
    }
}

