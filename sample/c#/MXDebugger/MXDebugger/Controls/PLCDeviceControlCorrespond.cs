using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class PLCDeviceControlCorrespond : PLCDeviceControlPannel
    {
        private System.Windows.Forms.Button _button;

        public event PCLDeviceControlPannelPredicateEditButtonClickedEventHandler PredicateEditButtonClicked = (obj, arg) => { };

        public PLCDeviceControlCorrespond()
        {
            this._button = new System.Windows.Forms.Button
            {
                Text = "編集",
            };

            this._button.Click += button_Click;
            this.Controls.Add(this._button);
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.PredicateEditButtonClicked.Invoke(this, new PCLDeviceControlPannelPredicateEditButtonClickedEventArgs
            {
                DeviceName = this.DeviceName,
            });
        }
    }
    public delegate void PCLDeviceControlPannelPredicateEditButtonClickedEventHandler(object sender, PCLDeviceControlPannelPredicateEditButtonClickedEventArgs e);

    public class PCLDeviceControlPannelPredicateEditButtonClickedEventArgs : EventArgs
    {
        public string DeviceName { get; set; }

        public object Value { get; set; }
    }
}
