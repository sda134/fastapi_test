using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class PLCDeviceControlButton : PLCDeviceControlPannel
    {
        private System.Windows.Forms.Button _button;

        public override event PCLDeviceControlPannelEventHandler PlcDeviceControlPannelEvent = (snd, arg) => { };

        public PLCDeviceControlButton()
        {
            this._button = new System.Windows.Forms.Button();
            this._button.MouseUp += _button_MouseUp;
            this._button.MouseDown += _button_MouseDown;

            this.Controls.Add(this._button);
        }

        public override string DeviceName
        {
            get => this._button.Text;
            set => this._button.Text = value;
        }


        #region region - events
        
        private void _button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
            => this.PlcDeviceControlPannelEvent.Invoke(this, new PCLDeviceControlPannelEventArgs
            {
                DeviceName = this.DeviceName,
                Value = 1,
            });

        private void _button_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
            => this.PlcDeviceControlPannelEvent.Invoke(this, new PCLDeviceControlPannelEventArgs
            { 
                DeviceName = this.DeviceName,
                Value = 0,
            });


        #endregion

    }
}
