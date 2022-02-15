using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.UtilityLibrary.Forms;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public class GroupOrderEditDialog : ListOrderEditDialog<LogGroupFormat>
    {      
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GroupOrderEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(267, 248);
            this.Name = "GroupOrderEditDialog";
            this.ShowIcon = false;
            this.Text = "グループの並び替え";
            this.ResumeLayout(false);

        }

        public GroupOrderEditDialog()
        {
            this.InitializeComponent();

            base.ListViewColumns = new System.Windows.Forms.ColumnHeader[]
            {
                new System.Windows.Forms.ColumnHeader
                {
                    Text = "グループ名",
                    Name = "GroupName",     //※ T のProperty の名前と完全に一致している必要がある
                    Width = 120,
                },

                new System.Windows.Forms.ColumnHeader
                {
                    Text = "トリガ",
                    Name = "TriggerType",     //※ T のProperty の名前と完全に一致している必要がある
                    Width = 100,
                },
            };
        }
    }
}
