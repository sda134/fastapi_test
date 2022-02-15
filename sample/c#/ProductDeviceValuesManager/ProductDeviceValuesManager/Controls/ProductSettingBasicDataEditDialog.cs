using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductDeviceValuesManager
{
    public partial class ProductSettingBasicDataEditDialog : Form
    {
        public ProductSettingBasicDataEditDialog()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProductSettingFormat Value
        {
            set
            {
                this.textBox_productName.Text = value.ProductName;
            }

            get => new ProductSettingFormat
            {
                ProductName = this.textBox_productName.Text,
            };            
        }

        private void ProductSettingBasicDataEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK &&
                string.IsNullOrWhiteSpace(this.textBox_productName.Text))
            {
                MessageBox.Show("必要な情報が入力されていません。");

                e.Cancel = true;
            }
        }
    }
}
