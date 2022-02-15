using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.UtilityLibrary.Forms
{ 
    // いずれライブラリへ

    public partial class ListOrderEditDialog<T> : Form
    {
        protected ListOrderEditDialog()
        {
            InitializeComponent();
        }

        //protected List<T> _items;

        protected ColumnHeader[] ListViewColumns
        {
            set
            {
                this.listView.Columns.Clear();
                this.listView.Columns.AddRange(value);
            }
        }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<T> Value
        {
            get => from ListViewItem item in this.listView.Items select (T)item.Tag;
            set
            {
                Func<T, string, string> getText = (arg, propName) =>
                {
                    #region region

                    var prop = arg.GetType().GetProperty(propName);

                    return prop?.GetValue(arg, null)?.ToString() ?? "";

                    #endregion
                };

                this.listView.Items.AddRange(value.Select((x, i) =>
                {
                    #region region 

                    var ret = new ListViewItem
                    {
                        Text = this.listView.Columns.Count > 0 ? getText(x, this.listView.Columns[0].Name) : "",
                        Tag = x,
                    };

                    for (int c = 1; c < this.listView.Columns.Count; c++)
                    {
                        ColumnHeader clm = this.listView.Columns[c];
                        ret.SubItems.Add(getText(x, clm.Name));
                    }

                    return ret;

                    #endregion

                }).ToArray());
            }
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            if (this.listView.SelectedIndices.Count > 0)
            {
                int idx = this.listView.SelectedIndices[0];

                
                // 上下限チェック
                if (idx < 1 || this.listView.Items.Count < 2) return;

                // 要素のコピー
                //var current = UtilityLibrary.Tools.DeepClone.Utility.GetDeepClone<ListViewItem>(this.listView.Items[idx]);
                //var target = UtilityLibrary.Tools.DeepClone.Utility.GetDeepClone<ListViewItem>(this.listView.Items[idx - 1]);
                // ↓ 18.12.12 Tag はこのSerializeClone メソッドでコピーされない事が判明 temporary ライブラリに入れるのにSerializeClone はまずいかも
                var current = (ListViewItem)UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<ListViewItem>(this.listView.Items[idx]);
                current.Tag = (T)this.listView.Items[idx].Tag;
                var target = (ListViewItem)UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<ListViewItem>(this.listView.Items[idx - 1]);
                target.Tag = (T)this.listView.Items[idx - 1].Tag;

                // 入れ替え
                this.listView.Items[idx - 1] = current;
                this.listView.Items[idx] = target;
            }
        }

        private void button_down_Click(object sender, EventArgs e)
        {
            if (this.listView.SelectedIndices.Count > 0)
            {
                int idx = this.listView.SelectedIndices[0];

                // 上下限チェック
                if (idx == -1 ||
                    this.listView.Items.Count < 2 ||
                    idx >= (this.listView.Items.Count - 1)) return;

                // 要素のコピー
                //var current = UtilityLibrary.Tools.DeepClone.Utility.GetDeepClone<ListViewItem>(this.listView.Items[idx]);
                //var target = UtilityLibrary.Tools.DeepClone.Utility.GetDeepClone<ListViewItem>(this.listView.Items[idx + 1]);
                // ↓ 18.12.12 Tag はこのSerializeClone メソッドでコピーされない事が判明 
                var current = (ListViewItem)UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<ListViewItem>(this.listView.Items[idx]);
                current.Tag = (T)this.listView.Items[idx].Tag;
                var target = (ListViewItem)UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<ListViewItem>(this.listView.Items[idx + 1]);
                target.Tag = (T)this.listView.Items[idx+1].Tag;

                // 入れ替え
                this.listView.Items[idx] = target;
                this.listView.Items[idx + 1] = current;
            }
        }
    }
}
