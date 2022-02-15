using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi.MXComponent;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public partial class SettingDialog : Form
    {        
        public SettingDialog()
        {
            InitializeComponent();

            this.mxComponentConfigurationPannel1.Value = Config.Instance.ActSetting;
        }


        private void SettingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                Config.Instance.ActSetting = this.mxComponentConfigurationPannel1.Value;
            }
        }


    }
}
