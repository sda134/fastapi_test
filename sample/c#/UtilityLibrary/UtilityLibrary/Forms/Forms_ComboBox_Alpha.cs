using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mtec.UtilityLibrary.Forms
{
    public class ComboBox_Alpha: System.Windows.Forms.ComboBox
    {
        //http://jeanne.wankuma.com/library/readonlycombobox/source.html 参考元
        

        #region　region - private member

        private System.ComponentModel.IContainer components;

        #endregion


        [System.ComponentModel.Category("動作")]
        [System.ComponentModel.Description("エディット コントロールの中の文字列を変更できるかどうかを設定します。")]
        [System.ComponentModel.DefaultValue(false)]
        public bool ReadOnly
        {
            get => this._readOnly;
            
            set
            {
                this._readOnly = value;

                if (value)
                {
                    this.BackColor = System.Drawing.SystemColors.Control;
                    this.ContextMenu = new System.Windows.Forms.ContextMenu();
                    this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);
                    this.SetStyle(System.Windows.Forms.ControlStyles.UserMouse, true);
                    this.UpdateStyles();
                    this.RecreateHandle();
                }
                else
                {
                    this.BackColor = System.Drawing.SystemColors.Window;
                    this.ContextMenu = null;
                    this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
                    this.SetStyle(System.Windows.Forms.ControlStyles.UserMouse, false);
                    this.UpdateStyles();
                    this.RecreateHandle();
                }
            }
        }
        private bool _readOnly = false;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ComboBox_Alpha()
        {
            this.components = new System.ComponentModel.Container();
        }


        #region region - override methods
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (this.ReadOnly) e.Handled = true;
            else
                base.OnKeyUp(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.ReadOnly) e.Handled = true;
            else
                base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.ReadOnly) e.Handled = true;
            else
                base.OnKeyPress(e);
        }

        #endregion

    }
}
