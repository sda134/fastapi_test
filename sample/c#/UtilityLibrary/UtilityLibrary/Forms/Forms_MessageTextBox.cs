using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibrary.Forms
{
    // 修正履歴
    // 18.12.05 元のメッセージが"メッセージを表示します" 固定だったので、変更できるようにした

    public class MessageTextBox : System.Windows.Forms.TextBox
    {
        private System.Diagnostics.Stopwatch _stopWatch;

        public MessageTextBox()
        {
            //InitializeComponent();


            this.Disposed += (sender, args) =>
            {
                this._stopWatch?.Reset();
                
                // 18.12.27 止め　意味なかったかも
                //this._stopWatch = null;
            };
        }

        [System.ComponentModel.Category("動作")]
        [System.ComponentModel.Description("表示テキストが自動的に消えるまでの時間を指定します。")]
        [System.ComponentModel.DefaultValue(5000)]
        public int TextDismissMilliseconds { get; set; } = 5000;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Category("表示")]
        [System.ComponentModel.Browsable(true)]
        public string OriginalText
        {
            get => this._originalText;
            set
            {
                this._originalText = value;
                base.Text = value;
            }
        }
        private string _originalText = "メッセージを表示します";


        public override string Text
        {
            get => base.Text;
            set
            {
                if (InvokeRequired)
                {
                    this.Invoke(new System.Windows.Forms.MethodInvoker(() => { this.Text = value; }));
                }
                else
                {
                    if (value == this._originalText)
                    {
                        // InitialiseComponent などの呼び出しの時の処理 18.12.12 追加
                        base.Text = value;
                        return;
                    }
                    
                    if (base.ForeColor == System.Drawing.SystemColors.InactiveCaption)
                    {
                        // 非アクティブの色になっているので、WindowText に戻す
                        base.ForeColor = System.Drawing.SystemColors.WindowText;

                        // 「メッセージを表示します」を消す
                        base.Text = "";
                    }


                    if (base.Multiline)
                    {
                        // メッセージを表示
                        base.AppendText(value + "\r\n");
                        base.ScrollToCaret();
                    }
                    else
                    {
                        base.Text = value;
                    }


                    // 消去時間を計測するストップウォッチ
                    if (this._stopWatch == null || this._stopWatch.ElapsedMilliseconds == 0)
                    {
                        this._stopWatch = new System.Diagnostics.Stopwatch();
                        this._stopWatch.Start();


                        // 5秒後にメッセージを消す処理
                        //Task.Run(() =>
                        // ↓ 19.02.07 WinXP 対応で実験的に変更
                        Task.Factory.StartNew(() => 
                        {
                            // 5秒まつ                        
                            // while (this._stopWatch.ElapsedMilliseconds < this.TextDismissMilliseconds)
                            // ↓ 18.12.27 修正
                            while ((this._stopWatch?.ElapsedMilliseconds ?? -1) < this.TextDismissMilliseconds)
                            {

                            }

                            try
                            {
                                // スレッドセーフでラベルの表示を変更
                                this.Invoke(new System.Windows.Forms.MethodInvoker(() =>
                                {
                                    base.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                                    base.Text = this._originalText;
                                }));

                                this._stopWatch.Reset();
                            }
                            catch (Exception)
                            {
                            }
                        });
                    }
                    else
                    {
                        // 重ねて次のメッセージが来たとき

                        // Reset, Start だと while (this.stopWatch.ElapsedMilliseconds < 10000) のループが抜けてしまう
                        this._stopWatch.Restart();
                    }
                }
            }
        }
    }

}
