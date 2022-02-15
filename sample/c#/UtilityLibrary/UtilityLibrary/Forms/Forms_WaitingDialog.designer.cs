namespace UtilityLibrary.Forms
{
    partial class WaitingDialogBase
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label_Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(71, 73);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(211, 23);
            this.progressBar.TabIndex = 1;
            // 
            // label_Message
            // 
            this.label_Message.Location = new System.Drawing.Point(10, 18);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(209, 20);
            this.label_Message.TabIndex = 2;
            this.label_Message.Text = "label_Message";
            this.label_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WaitingDialogBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 102);
            this.ControlBox = false;
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btn_Cancel);
            this.Name = "WaitingDialogBase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WaitingDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitingDialogBase_FormClosing);
            this.Shown += new System.EventHandler(this.WaitingDialogBase_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btn_Cancel;
        protected System.Windows.Forms.ProgressBar progressBar;
        protected System.Windows.Forms.Label label_Message;

    }
}