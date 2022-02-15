using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.UtilityLibrary.Forms
{ 
    public partial class SerialPortSettingPannel : UserControl
    {
        public SerialPortSettingPannel()
        {
            InitializeComponent();

            this.comboBox_StopBits.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));
            this.comboBox_ParityBits.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //        [System.ComponentModel.Browsable(false)]
        public SerialPortSettingFormat Value
        {
            get
            {
                var ret = new SerialPortSettingFormat();

                // enum
                ret.StopBits = (System.IO.Ports.StopBits)this.comboBox_StopBits.SelectedItem;
                ret.ParityBits = (System.IO.Ports.Parity)this.comboBox_ParityBits.SelectedItem;


                // int
                int baudRate; int.TryParse(this.textBox_BaudRate.Text, out baudRate);
                int dataBits; int.TryParse(this.textBox_DataBits.Text, out dataBits);

                ret.BaudRate = baudRate;
                ret.DataBits = dataBits;

                ret.WriteTimeout = (int)this.numericUpDown_WriteTimeout.Value;
                ret.ReadTimeout = (int)this.numericUpDown_ReadTimeout.Value;

                // String
                ret.PortName = this.textBox_portName.Text;

                return ret;
            }
            set
            {
                if (value == null) value = new SerialPortSettingFormat();

                this.comboBox_StopBits.SelectedItem = value.StopBits;
                this.comboBox_ParityBits.SelectedItem = value.ParityBits;

                // int
                this.textBox_BaudRate.Text = value.BaudRate.ToString();
                this.textBox_DataBits.Text = value.DataBits.ToString();

                this.numericUpDown_WriteTimeout.Value = value.WriteTimeout;
                this.numericUpDown_ReadTimeout.Value = value.ReadTimeout;

                // String
                this.textBox_portName.Text = value.PortName;
            }
        }
    }

}

namespace Mtec.UtilityLibrary
{
    // 名前空間を考えておく 18.07.04

    [Serializable]
    public class SerialPortSettingFormat
    {
        public System.IO.Ports.StopBits StopBits { get => _stopBits; set => _stopBits = value; }
        private System.IO.Ports.StopBits _stopBits = System.IO.Ports.StopBits.One;

        public System.IO.Ports.Parity ParityBits { get => _parityBits; set => _parityBits = value; }
        private System.IO.Ports.Parity _parityBits = System.IO.Ports.Parity.None;

        // int
        public int BaudRate { get => _baudRate; set => _baudRate = value; }
        private int _baudRate = 9600;

        public int DataBits { get => _dataBits; set => _dataBits = value; }
        private int _dataBits = 8;


        public int ReadTimeout { get; set; } = -1;
        public int WriteTimeout { get; set; } = -1;


        // String
        public string PortName { get; set; } = "COM1";



        public static explicit operator System.IO.Ports.SerialPort(SerialPortSettingFormat arg)
        {
            return new System.IO.Ports.SerialPort
            {
                StopBits = arg._stopBits,
                Parity = arg._parityBits,
                BaudRate = arg._baudRate,
                DataBits = arg._dataBits,
                PortName = arg.PortName, 
                
                ReadTimeout = arg.ReadTimeout,
                WriteTimeout = arg.WriteTimeout,
            };
        }
    }
}
