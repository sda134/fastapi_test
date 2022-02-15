using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.UtilityLibrary.Forms;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class GroupListEditDialog : Mtec.UtilityLibrary.Forms.ListEditDialog_ListView<LogGroupFormat>
    {

        public GroupListEditDialog()
        {
            this.InitializeComponent();

            base.ListViewColumns = new System.Windows.Forms.ColumnHeader[]
            {

                new System.Windows.Forms.ColumnHeader
            {
                Text = "グループ名",
                Name = "GroupName",
                Width = 120,
            }
        };
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GroupListEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(268, 246);
            this.Name = "GroupListEditDialog";
            this.Controls.SetChildIndex(this.button_cancel, 0);
            this.Controls.SetChildIndex(this.button_OK, 0);
            this.Controls.SetChildIndex(this.button_delete, 0);
            this.Controls.SetChildIndex(this.button_add, 0);
            this.Controls.SetChildIndex(this.listView, 0);
            this.Controls.SetChildIndex(this.button_edit, 0);
            this.ResumeLayout(false);

        }

        public List<LogGroupFormat> Value
        {
            get => base._items;
            set
            {
                base._items = value;
                base.rebuildDisplay();
            }
        }

        protected override void button_add_Click(object sender, EventArgs e)
        {
            using (var dlg = new GroupInfoEditDialog())
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    base._items.Add(dlg.Value);
                    base.rebuildDisplay();
                }
            }
        }
        protected override void button_edit_Click(object sender, EventArgs e)
        {
            if (base.SelectedIndecies.Count() < 1) return;

            using (var dlg = new GroupInfoEditDialog())
            {
                dlg.Value = base._items[base.SelectedIndecies.ElementAt(0)];

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    base._items[base.SelectedIndecies.ElementAt(0)] = dlg.Value;
                }
            }
        }
    }
}
