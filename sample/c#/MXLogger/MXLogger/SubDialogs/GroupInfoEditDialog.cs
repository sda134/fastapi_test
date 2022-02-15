using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public partial class GroupInfoEditDialog : Form
    {

        public GroupInfoEditDialog()
        {
            InitializeComponent();

            
            // デザイナで設定できない項目
            this.comboBox_triggerType.DisplayMember = "Text";
            this.comboBox_triggerType.ValueMember = "Value";
            this.comboBox_triggerType.DataSource = (from TriggerType trigger in typeof(TriggerType).GetEnumValues()
                                                    select new { Text = trigger.ToStringFromEnum(), Value = trigger }).ToList();

            // トリガのデータ型
            this.comboBox_trigger_deviceFormatTypee.DataSource = Enum.GetValues(typeof(DeviceFormatType));


            // トリガの比較方法
            this.comboBox_trigger_compareType.DisplayMember = "Text";
            this.comboBox_trigger_compareType.ValueMember = "Value";
            this.comboBox_trigger_compareType.DataSource = (from CompareType trigger in typeof(CompareType).GetEnumValues()
                                                    select new { Text = trigger.ToStringFromEnum(), Value = trigger }).ToList();
        }


        public LogGroupFormat Value
        {
            get
            {
                Func<object> threshold_func = () =>
                {
                    #region region

                    switch ((DeviceFormatType)this.comboBox_trigger_deviceFormatTypee.SelectedItem)
                    {
                        case DeviceFormatType.Signed16: return (Int16)this.numericUpDown_threshold.Value;
                        case DeviceFormatType.Float:return (float)this.numericUpDown_threshold.Value;
                        default: return (Int32)this.numericUpDown_threshold.Value;
                    }

                    #endregion
                };

                return new LogGroupFormat
                {
                    GroupName = this.textBox_groupName.Text,

                    TriggerType = (TriggerType)this.comboBox_triggerType.SelectedValue,

                    Trigger = ((TriggerType)this.comboBox_triggerType.SelectedValue) != TriggerType.Trigger ? null : new TriggerFormat
                    {
                        DeviceName = this.textBox_trigger_deviceName.Text,
                        DeviceFormatType = (DeviceFormatType)this.comboBox_trigger_deviceFormatTypee.SelectedItem,
                        CompareType = (CompareType)this.comboBox_trigger_compareType.SelectedValue,
                        ThresholdValue = threshold_func(),
                    },
                };
            }
            set
            {
                this.textBox_groupName.Text = value.GroupName;
                this.comboBox_triggerType.SelectedValue = value.TriggerType;

                if (value.TriggerType == TriggerType.Trigger && value.Trigger != null)
                {
                    this.textBox_trigger_deviceName.Text = value.Trigger.DeviceName;
                    this.comboBox_trigger_deviceFormatTypee.SelectedItem = value.Trigger.DeviceFormatType;
                    this.comboBox_trigger_compareType.SelectedValue = value.Trigger.CompareType;

                    if (value.Trigger.ThresholdValue != null)
                        this.numericUpDown_threshold.Value = Convert.ToDecimal(value.Trigger.ThresholdValue);
                }
            }
        }


        private void comboBox_triggerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBox_trigger.Enabled =
                ((TriggerType)this.comboBox_triggerType.SelectedValue) == TriggerType.Trigger;
        }


        private void comboBox_trigger_typeOfDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (((DeviceFormatType)this.comboBox_trigger_deviceFormatTypee.SelectedItem))
            {
                case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                    {
                        this.numericUpDown_threshold.Maximum = Int32.MaxValue;
                        this.numericUpDown_threshold.Minimum = Int32.MinValue;
                        this.numericUpDown_threshold.Increment = 1.0m;
                        this.numericUpDown_threshold.DecimalPlaces = 0;
                    }
                    break;

                case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                    {
                        // ?? これでいいのか？
                        this.numericUpDown_threshold.Maximum = Int32.MaxValue;
                        this.numericUpDown_threshold.Minimum = -1 * Int32.MaxValue;
                        this.numericUpDown_threshold.Increment = 0.1m;
                        this.numericUpDown_threshold.DecimalPlaces = 3;
                    }
                    break;

                case Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType.Bit:
                    {
                        // ?? これでいいのか？
                        this.numericUpDown_threshold.Maximum = 1;
                        this.numericUpDown_threshold.Minimum = 0;
                        this.numericUpDown_threshold.Increment = 1.0m;
                        this.numericUpDown_threshold.DecimalPlaces = 0;
                    }
                    break;

                default:
                    {
                        // Single やその他
                        this.numericUpDown_threshold.Maximum = Int16.MaxValue;
                        this.numericUpDown_threshold.Minimum = Int16.MinValue;
                        this.numericUpDown_threshold.Increment = 1.0m;
                        this.numericUpDown_threshold.DecimalPlaces = 0;
                    }
                    break;
            }

        }
    }
}
