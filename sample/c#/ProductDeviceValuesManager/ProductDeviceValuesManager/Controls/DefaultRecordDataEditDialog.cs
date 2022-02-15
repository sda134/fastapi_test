using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
これで１ページ目を削除
    this.tabControl.TabPages.RemoveAt(0);
*/

namespace ProductDeviceValuesManager
{
    public class DefaultRecordDataEditDialog : SingleProductDataEditDialog
    {        
        public DefaultRecordDataEditDialog()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.tabPage_device.SuspendLayout();
            this.tabPage_connectionSetting.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_serial
            // 
            this.label_serial.Visible = false;
            // 
            // textBox_serial
            // 
            this.textBox_serial.Visible = false;
            // tabControl
            //
            this.tabControl.TabPages.RemoveAt(0);
            // 
            // mxComponentConfigurationPannel1
            // 
            this.mxComponentConfigurationPannel1.Value.ActControl = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl.TRC_DTR_OR_RTS;
            this.mxComponentConfigurationPannel1.Value.ActCpuType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType.Default;
            this.mxComponentConfigurationPannel1.Value.ActHostAddress = "192.168.1.1";
            this.mxComponentConfigurationPannel1.Value.ActProtocolType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_SERIAL;
            this.mxComponentConfigurationPannel1.Value.ActUnitType = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.Default;
            this.mxComponentConfigurationPannel1.Value.BaudRate = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate.BAUDRATE_9600;
            this.mxComponentConfigurationPannel1.Value.DataBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits.DATABIT_7;
            this.mxComponentConfigurationPannel1.Value.ParityBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity.EVEN_PARITY;
            this.mxComponentConfigurationPannel1.Value.PortNumber = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber.PORT_1;
            this.mxComponentConfigurationPannel1.Value.StopBits = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits.STOPBIT_ONE;
            // 
            // DefaultRecordDataEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(516, 369);
            this.Name = "DefaultRecordDataEditDialog";
            this.Text = "デフォルト値の設定";
            this.tabPage_device.ResumeLayout(false);
            this.tabPage_device.PerformLayout();
            this.tabPage_connectionSetting.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void SingleProductDataEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                this.apply_guiData_to_privateMember();
            }
        }

        public override SingleProductFormat Value
        {
            set
            {
                base._propertyGroups = value.PropertyGroups;
                base._textFieldGroups = value.TextFieldGroups;

                base.mxComponentConfigurationPannel1.Value = value.ActControlSetting;

                base.apply_privateMemberData_to_GUI();
                base.apply_privateMemberData_to_GUI_text();
            }

            get => new SingleProductFormat
            {
                RecordName = null,
                SerialCode = null,
                Reference = null,

                PropertyGroups = base._propertyGroups,
                ActControlSetting = base.mxComponentConfigurationPannel1.Value,

                TextFieldGroups = base._textFieldGroups,
            };
        }
    }
}
