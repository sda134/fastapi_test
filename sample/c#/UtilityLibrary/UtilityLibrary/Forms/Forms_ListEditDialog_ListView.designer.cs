namespace UtilityLibrary.Forms
{
    partial class ListEditDialog_ListView<T>
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
            this.button_add = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader_Group = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_edit = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.上に移動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下に移動ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(93, 184);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 101;
            this.button_add.Text = "追加";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(12, 184);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(75, 23);
            this.button_delete.TabIndex = 100;
            this.button_delete.Text = "削除";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(174, 211);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 201;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(12, 211);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 200;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Group,
            this.columnHeader_Column});
            this.listView.ContextMenuStrip = this.contextMenuStrip1;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(12, 12);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(237, 166);
            this.listView.TabIndex = 20;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_Group
            // 
            this.columnHeader_Group.Text = "項目1";
            this.columnHeader_Group.Width = 76;
            // 
            // columnHeader_Column
            // 
            this.columnHeader_Column.Text = "項目2";
            this.columnHeader_Column.Width = 73;
            // 
            // button_edit
            // 
            this.button_edit.Location = new System.Drawing.Point(174, 184);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(75, 23);
            this.button_edit.TabIndex = 102;
            this.button_edit.Text = "編集";
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上に移動ToolStripMenuItem,
            this.下に移動ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 48);
            // 
            // 上に移動ToolStripMenuItem
            // 
            this.上に移動ToolStripMenuItem.Name = "上に移動ToolStripMenuItem";
            this.上に移動ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.上に移動ToolStripMenuItem.Text = "上に移動";
            this.上に移動ToolStripMenuItem.Click += new System.EventHandler(this.上に移動ToolStripMenuItem_Click);
            // 
            // 下に移動ToolStripMenuItem
            // 
            this.下に移動ToolStripMenuItem.Name = "下に移動ToolStripMenuItem";
            this.下に移動ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下に移動ToolStripMenuItem.Text = "下に移動";
            this.下に移動ToolStripMenuItem.Click += new System.EventHandler(this.下に移動ToolStripMenuItem_Click);
            // 
            // ListEditDialog_ListView
            // 
            this.AcceptButton = this.button_cancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 246);
            this.Controls.Add(this.button_edit);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListEditDialog_ListView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ListEditDialog";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader_Group;
        private System.Windows.Forms.ColumnHeader columnHeader_Column;
        protected System.Windows.Forms.ListView listView;
        protected System.Windows.Forms.Button button_add;
        protected System.Windows.Forms.Button button_delete;
        protected System.Windows.Forms.Button button_OK;
        protected System.Windows.Forms.Button button_cancel;
        protected System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.ToolStripMenuItem 上に移動ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下に移動ToolStripMenuItem;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        //private System.Windows.Forms.ColumnHeader columnHeader_Group;
        //private System.Windows.Forms.ColumnHeader columnHeader_Column;
    }
}