using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public partial class PredicateEditDialog : Form
    {
        private BindingList<PredicationFieldFormat> _bindingItems;


        public PredicateEditDialog()
        {
            InitializeComponent();

            // セル内の表示位置
            this.Column_DeviceName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.Column_CompareType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ComboBoxColumn の設定
            //this.Column_CompareType.DataSource = (from CompareType e in Enum.GetValues(typeof(CompareType)) select e.ToStringFromEnum()).ToList();
            // ↓ 19.01.29 変更
            this.Column_CompareType.DisplayMember = "Text";
            this.Column_CompareType.ValueMember = "Value";
            this.Column_CompareType.DataSource = (from CompareType com in typeof(CompareType).GetEnumValues()
                                         select new { Text = com.ToStringFromEnum(), Value = com }).ToList();

            this.Column_DeviceFormatType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.DeviceFormatType));


            this._bindingItems = new BindingList<PredicationFieldFormat>();
            this.dataGridView1.DataSource = this._bindingItems;
        }

        public IEnumerable<PredicationFieldFormat> Value
        {
            get => this._bindingItems.Select(x =>
            {
                if (x.Value is string)
                {
                    #region region - 型変換
                    
                    switch (x.DeviceFormatType)
                    {
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed32:
                            {
                                if (Int32.TryParse(x.Value.ToString(), out int iVal))
                                    x.Value = iVal;
                            }
                            break;

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Bit:
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Signed16:
                            {
                                if (Int16.TryParse(x.Value.ToString(), out short shVal))
                                    x.Value = shVal;
                            }
                            break;

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned16:
                            {
                                if (UInt16.TryParse(x.Value.ToString(), out ushort ushVal))
                                    x.Value = ushVal;
                            }
                            break;
                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Unsigned32:
                            {
                                if (UInt32.TryParse(x.Value.ToString(), out uint uiVal))
                                    x.Value = uiVal;
                            }
                            break;

                        case UtilityLibrary.Mitsubishi.DeviceFormatType.Float:
                            {
                                if (Single.TryParse(x.Value.ToString(), out float fVal))
                                    x.Value = fVal;
                            }
                            break;
                        
                        default:
                            break;
                    }

                    #endregion
                }

                return x;
            });

            set
            {
                this._bindingItems = new BindingList<PredicationFieldFormat>(value.ToArray());
                this.dataGridView1.DataSource = this._bindingItems;
            }
        }


        protected virtual void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // ≒行変更イベントと考えてよさそう

            var dg = (DataGridView)sender;

            // 変わる前の行番号（備忘録）
            int current = dg.CurrentRow != null ? dg.CurrentRow.Index : -1;

            // 今まさに突入した行の番号（備忘録）
            int index = e.RowIndex;

        }

        protected virtual void ExpressionListEditDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
            }
        }
    }

    #region  region - ExtensionMethod

    static partial class ExtensionMethod
    {


        public static string ToStringFromEnum(this CompareType value)
        {
            switch (value)
            {
                case CompareType.GreaterThan: return "＞";
                case CompareType.GreaterThanOrEqual: return "≧";
                case CompareType.LessThan: return "＜";
                case CompareType.LessThanOrEqual: return "≦";
                case CompareType.Equal: return "＝";
                case CompareType.NotEqual: return "≠";
                default:
                    return null;
            }
        }


        public static string ToStringFromEnum(this ConditionType value)
        {
            switch (value)
            {
                case ConditionType.And: return "And";
                case ConditionType.Or: return "Or";
                default:
                    return null;
            }
        }
    }
    #endregion

}
