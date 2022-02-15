using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class PLCDeviceControlCheckBox : PLCDeviceControlPannel
    {
        private System.Windows.Forms.CheckBox _checkBox;

        public override event PCLDeviceControlPannelEventHandler PlcDeviceControlPannelEvent = (snd, arg) => { };

        public PLCDeviceControlCheckBox()
        {
            this._checkBox = new System.Windows.Forms.CheckBox();
            this._checkBox.CheckedChanged += _checkBox_CheckedChanged; ;

            this.Controls.Add(this._checkBox);
        }

        public bool Checked
        {
            get => this._checkBox.Checked;
            set => this._checkBox.Checked = value;
        }

        public override string DeviceName
        {
            get => this._checkBox.Text;
            set => this._checkBox.Text = value;
        }



        #region region - event


        private void _checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PlcDeviceControlPannelEvent.Invoke(this, new PCLDeviceControlPannelEventArgs
            {
                DeviceName = this.DeviceName,
                Value = this._checkBox.Checked ? 1 : 0,
            });
        }

        #endregion


    }
}
