using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class PLCDeviceContriolTextBox : PLCDeviceControlPannel
    {
        private System.Windows.Forms.TextBox _textBox;

        public override event PCLDeviceControlPannelEventHandler PlcDeviceControlPannelEvent = (snd, arg) => { };

        public PLCDeviceContriolTextBox()
        {
            this._textBox = new System.Windows.Forms.TextBox();
            this._textBox.Size = new System.Drawing.Size(90, 19);
            this._textBox.TextChanged += this.textBox_TextChanged;
            this._textBox.Name = "_textBox";

            /*
            this.textBox1.Location = new System.Drawing.Point(182, 79);
            this.textBox1.TabIndex = 0;
             */

            this.Controls.Add(this._textBox);
        }

        #region region - public properties

        
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => this._textBox.Text;
            set => this._textBox.Text = value;
        }

        #endregion


        #region region - event


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            object val = null;

            switch (base.DeviceFormatType)
            {
                #region region - 型によって値変換

                case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed16:
                case UtilityLibrary.Mitsubishi.DeviceFormatType.Bit:
                    {
                        if (Int16.TryParse(this._textBox.Text, out short sVal))
                            val = sVal;
                        else
                            val = (short)0;
                    }
                    break;

                case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                    {
                        if (Int32.TryParse(this._textBox.Text, out int iVal))
                            val = iVal;
                        else
                            val = (int)0;
                    }
                    break;

                case UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                    {
                        if (Single.TryParse(this._textBox.Text, out float sVal))
                            val = sVal;
                        else
                            val = 0f;
                    }
                    break;

                case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                    {
                        if (UInt16.TryParse(this._textBox.Text, out ushort ushVal))
                            val = ushVal;
                        else
                            val = (ushort)0;
                    }
                    break;
                case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                    {
                        if (UInt32.TryParse(this._textBox.Text, out uint uiVal))
                            val = uiVal;
                        else
                            val = (uint)0;
                    }
                    break;

                default:
                    break;

                    #endregion
            }

            this.PlcDeviceControlPannelEvent.Invoke(this, new PCLDeviceControlPannelEventArgs
            {
                DeviceName = this.DeviceName,
                Value = val,
            });
        }

        #endregion

    }
}
