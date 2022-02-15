using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.UtilityLibrary.Forms
{
    public partial class InputBox : Form
    {
        // Microsoft.VisualBasic を参照して Microsoft.VisualBasic.Interaction.InputBox を使用する方法もある

        public InputBox()
        {
            InitializeComponent();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {

        }

        public string Prompt
        {
            get => this.label_prompt.Text;
            set => this.label_prompt.Text = value;
        }

        public string Value
        {
            get => this.textBox_value.Text;
            set => this.textBox_value.Text = value;
        }
    }
}
