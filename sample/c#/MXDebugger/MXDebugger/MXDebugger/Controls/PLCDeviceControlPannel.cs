using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    // 即興で作ったので、BCD に非対応、書き込み時の型対応してない 19.01.24 temporary

    public partial class PLCDeviceControlPannel : UserControl
    {

        private Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType _deviceFormatType { get; set; }
        public virtual Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType DeviceFormatType
        {
            get => this._deviceFormatType;
            set
            {
                this._deviceFormatType = value;
                this.label_dataType.Text = value.ToString();
            }
        }

        public virtual string Detail
        {
            get => this.label_detail.Text;
            set => this.label_detail.Text = value;
        }

        public virtual string DeviceName { get; set; }


        public virtual event PCLDeviceControlPannelEventHandler PlcDeviceControlPannelEvent = (snd, arg) => { };


        public PLCDeviceControlPannel()
        {
            InitializeComponent();
        }
    }

    public delegate void PCLDeviceControlPannelEventHandler(object sender, PCLDeviceControlPannelEventArgs e);

    public class PCLDeviceControlPannelEventArgs : EventArgs
    {
        public string DeviceName { get; set; }

        public object Value { get; set; }
    }
}
