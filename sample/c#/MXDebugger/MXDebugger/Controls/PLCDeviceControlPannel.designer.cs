namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    partial class PLCDeviceControlPannel
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_detail = new System.Windows.Forms.Label();
            this.label_dataType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_detail
            // 
            this.label_detail.Location = new System.Drawing.Point(100, 6);
            this.label_detail.Name = "label_detail";
            this.label_detail.Size = new System.Drawing.Size(140, 12);
            this.label_detail.TabIndex = 0;
            this.label_detail.Text = "Detail";
            // 
            // label_dataType
            // 
            this.label_dataType.Location = new System.Drawing.Point(260, 6);
            this.label_dataType.Name = "label_dataType";
            this.label_dataType.Size = new System.Drawing.Size(90, 12);
            this.label_dataType.TabIndex = 0;
            this.label_dataType.Text = "DataType";
            // 
            // PLCDeviceControlPannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_dataType);
            this.Controls.Add(this.label_detail);
            this.Name = "PLCDeviceControlPannel";
            this.Size = new System.Drawing.Size(360, 24);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Label label_detail;
        protected System.Windows.Forms.Label label_dataType;
    }
}
