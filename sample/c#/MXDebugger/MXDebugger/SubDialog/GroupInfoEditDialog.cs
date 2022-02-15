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

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public partial class GroupInfoEditDialog : Form
    {
        private List<ReverseDeviceFormat> _items;

        public GroupInfoEditDialog()
        {
            InitializeComponent();

            _items = new List<ReverseDeviceFormat>();
        }


        public LogGroupFormat Value
        {
            get
            {
                return new LogGroupFormat
                {
                    GroupName = this.textBox_groupName.Text,
                    FieldList = this._items,
                };
            }
            set
            {
                this.textBox_groupName.Text = value.GroupName;
                this._items = value.FieldList;
            }
        }
    }
}
