using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel;

namespace ProductDeviceValuesManager
{
    public class SingleProductsSelectDialog :
        UtilityLibrary.Forms.ListEditDialog_ListView<SingleProductFormat>
    {

        public SingleProductsSelectDialog()
        {
            this.InitializeComponent();

            base.ListViewColumns = new System.Windows.Forms.ColumnHeader[]
            {
                new System.Windows.Forms.ColumnHeader
                {
                    Text = "名前",
                    Name = "RecordName",    // ※ T のProperty の名前と完全に一致している必要がある
                    Width = 140,
                },
                new System.Windows.Forms.ColumnHeader
                {
                    Text = "シリアル",
                    Name = "SerialCode",    // ※ T のProperty の名前と完全に一致している必要がある
                    Width = 100,
                }
            };
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SingleProductsSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(268, 246);
            this.Name = "SingleProductsSelectDialog";
            this.Text = "製品リスト";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SingleProductsSelectDialog_FormClosing);
            this.ResumeLayout(false);
        }


        #region region - override メソッド（button_click)


        protected override void button_add_Click(object sender, EventArgs e)
        {
            using (var dlg = new SingleProductDataEditDialog())
            {
                // dlg.Value = this._defaultData;
                // ↓ 19.01.08 [ver 0.1.0.1] 修正 
                // dlg.Value = this._defaultData ?? new SingleProductFormat();
                // ↓ 19.01.10 [ver 0.1.1.0] 修正 
                //var newInstance = Mtec.UtilityLibrary.Tools.SerializeClone.StaticMethods.GetSerializedClone<SingleProductFormat>(this._defaultData) ?? new SingleProductFormat();
                /*
                 for (int gp = 0; gp < newInstance.PropertyGroups.Count; gp++ )
                {
                    for (int fld = 0; fld < newInstance.PropertyGroups[gp].Fields.Count; fld ++)
                    {
                        newInstance.PropertyGroups[gp].Fields[fld].CurrentValue = null;
                    }
                }
                 */
                // ↓ 19.01.11 参照型で、defaultまで影響を受けたので、さらに修正

                var newInstance = new SingleProductFormat
                {
                    ActControlSetting = this._defaultData?.ActControlSetting,
                    PropertyGroups = this._defaultData ?.PropertyGroups.Select(x => new PropertyGroupFormat
                    {
                        GroupName = x.GroupName,

                        Fields = x.Fields.Select(y => new Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat
                        {
                            Detail = y.Detail,
                            DeviceFormatType = y.DeviceFormatType,
                            DeviceName = y.DeviceName,
                            CurrentValue = null,

                        }).ToList(),

                    }).ToList(),
                };


                
                dlg.Value = newInstance;


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

            using (var dlg = new SingleProductDataEditDialog())
            {
                dlg.Value = base._items[base.SelectedIndex];

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    base._items[base.SelectedIndex] = dlg.Value;
                    base.rebuildDisplay();
                }
            }
        }

        protected override void button_delete_Click(object sender, EventArgs e)
        {
            if (this.listView.SelectedIndices.Count < 1) return;

            if (System.Windows.Forms.MessageBox.Show(
            "削除してもよろしいですか？",
            "確認",
            System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                base.button_delete_Click(sender, e);
            }
        }


        #endregion



        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public IEnumerable<SingleProductFormat> Value
        {
            set
            {
                base._items = value?.ToList() ?? new List<SingleProductFormat>();
                base.rebuildDisplay();
            }
            get => base._items;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public SingleProductFormat DefaultData
        {
            get => this._defaultData ?? new SingleProductFormat();
            set => this._defaultData = value;
        }
        private SingleProductFormat _defaultData;

        private void SingleProductsSelectDialog_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (base.listView.SelectedIndices.Count < 1)
                {
                    System.Windows.Forms.MessageBox.Show("項目が選択されていません。");
                    e.Cancel = true;
                }
            }
        }
    }
}
