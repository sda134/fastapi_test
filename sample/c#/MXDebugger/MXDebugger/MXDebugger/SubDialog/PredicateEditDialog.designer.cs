namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    partial class PredicateEditDialog
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_CompareType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_DeviceFormatType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_DeviceName,
            this.Column_CompareType,
            this.Column_DeviceFormatType,
            this.Column_Value});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(490, 231);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // Column_DeviceName
            // 
            this.Column_DeviceName.DataPropertyName = "DeviceName";
            this.Column_DeviceName.HeaderText = "デバイス";
            this.Column_DeviceName.Name = "Column_DeviceName";
            this.Column_DeviceName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_DeviceName.Width = 70;
            // 
            // Column_CompareType
            // 
            this.Column_CompareType.DataPropertyName = "CompareType";
            this.Column_CompareType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column_CompareType.HeaderText = "比較";
            this.Column_CompareType.Name = "Column_CompareType";
            this.Column_CompareType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_CompareType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_CompareType.Width = 80;
            // 
            // Column_DeviceFormatType
            // 
            this.Column_DeviceFormatType.DataPropertyName = "DeviceFormatType";
            this.Column_DeviceFormatType.HeaderText = "データ型";
            this.Column_DeviceFormatType.Name = "Column_DeviceFormatType";
            this.Column_DeviceFormatType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_DeviceFormatType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_Value
            // 
            this.Column_Value.DataPropertyName = "Value";
            this.Column_Value.HeaderText = "値";
            this.Column_Value.Name = "Column_Value";
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(371, 249);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 101;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(12, 249);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 100;
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // PredicateEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 279);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PredicateEditDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "デバイス動作条件の編集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExpressionListEditDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.DataGridView dataGridView1;
        protected System.Windows.Forms.Button button_ok;
        protected System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_DeviceName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_CompareType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_DeviceFormatType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Value;
    }
}