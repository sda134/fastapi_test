using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public partial class SettingDialog : Form
    {
        public SettingDialog()
        {
            InitializeComponent();

            this.applyConfigData_to_GUI();
        }

        private void applyGUIData_to_Config()
        {
            Config.Instance.Interval_mSec = (int)this.numericUpDownInterval.Value;
            Config.Instance.ActSetting = this.mxComponentConfigurationPannel1.Value;
            Config.Instance.LogFileSaveDirectory = this.textBox_filePath.Text;

            // 19.04.29 [ver 0.3.3.1] 追加
            Config.Instance.NeedCSVFieldsSorted = this.checkBox_needCSVFieldsSorted.Checked;
        }

        private void applyConfigData_to_GUI()
        {
            this.numericUpDownInterval.Value = Config.Instance.Interval_mSec;
            this.mxComponentConfigurationPannel1.Value = Config.Instance.ActSetting;
            this.textBox_filePath.Text = Config.Instance.LogFileSaveDirectory;

            // 19.04.29 [ver 0.3.3.1] 追加
            this.checkBox_needCSVFieldsSorted.Checked = Config.Instance.NeedCSVFieldsSorted;
        }

        private void button_pathSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "保存ディレクトリを指定してください。";
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = this.textBox_filePath.Text;

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    this.textBox_filePath.Text = fbd.SelectedPath;
                }
            }
        }

        private void SettingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.applyGUIData_to_Config();
            Config.SaveToXmlFile();
        }
    }
}
