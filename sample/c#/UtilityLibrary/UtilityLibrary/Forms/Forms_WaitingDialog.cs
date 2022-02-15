using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UtilityLibrary.Forms
{
    public partial class WaitingDialogBase : Form
    {
        public MethodInvoker CancelButtonClick = () => { };

        public MethodInvoker ProccessCompletedCallback = () => { };

        public MethodInvoker DoWorkMethod = () => { };

        private BackgroundWorker _bgWorker = new BackgroundWorker();



        public WaitingDialogBase()
        {
            InitializeComponent();

            //BackgroundWorkerの設定：イベント登録
            _bgWorker.DoWork +=
                new DoWorkEventHandler(this.BackgroundWorker_DoWork);
            _bgWorker.ProgressChanged +=
                new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            _bgWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);

            //BackgroundWorkerの設定：キャンセルできるようにする
            _bgWorker.WorkerSupportsCancellation = true;

            //BackgroundWorkerのProgressChangedイベントが発生するようにする
            _bgWorker.WorkerReportsProgress = true;
        }


        //プログレスバーの値を増やすタイマー
        Timer _timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// 表示するメッセージ
        /// </summary>
        public string DisplayMessage
        {
            set
            {
                if (InvokeRequired)
                {
                    // 別スレッドから呼び出された場合
                    Invoke(new MethodInvoker(() =>
                    {
                        this.label_Message.Text = value;
                        this.Update();
                    }));
                }
                else
                {
                    this.label_Message.Text = value;
                    this.Update();
                }
            }
            private get { return this.btn_Cancel.Text; }
        }



        public override string Text
        {
            set
            {
                if (InvokeRequired)
                {
                    // 別スレッドから呼び出された場合
                    Invoke(new MethodInvoker(() =>
                    {
                        base.Text = value;
                        this.Update();
                    }));
                }
                else
                {
                    base.Text = value;
                    this.Update();
                }
            }
            get { return base.Text; }
        }

        public void Start()
        {
            // ダイアログの表示(表示する事で処理が始まる仕様 → というかそれ以外に手がない)
            this.ShowDialog();     
        }


        /// <summary>
        /// 時間のかかる処理を行う
        /// </summary>
        protected void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //パラメータを取得する ※必要ない
            //var parameter = e.Argument;

            //処理を開始
            DoWorkMethod.Invoke();

            //ProgressChangedで取得できる結果を設定する　（結果が必要なければ省略できる）
            //e.Result = arg;
        }
        
        /// <summary>
        /// 処理が終わったときに呼び出される
        /// </summary>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {   //エラーが発生したとき

                this.Text = "error:" + e.Error.Message;

                //エラーをスロー
                throw e.Error;
            }
            else
            {   //正常に終了したとき

                //結果を取得する ※必要ない
                // var result = e.Result;

                this.ProccessCompletedCallback.Invoke();
            }

            //タイマーを止める
            this._timer.Enabled = false;

            //閉じる
            this.Dispose();
        }


        /// <summary>
        /// コントロールの操作は必ずここで行う
        /// </summary>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressBar1の値を変更する
            progressBar.Value = e.ProgressPercentage;

            //Label1のテキストを変更する
            //this.label_Message.Text = this.DisplayMesssage;
        }


        private void WaitingDialogBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            //タイマーを止める
            this._timer.Enabled = false;

            //タイマーオブジェクトの破棄
            this._timer.Dispose();

            //処理をキャンセル
            this._bgWorker.CancelAsync();
        }

 
        private void WaitingDialogBase_Shown(object sender, EventArgs e)
        {
            var sw = new System.Diagnostics.Stopwatch();

            //処理が行われているときは、何もしない
            while (this._bgWorker.IsBusy)
            {
                if (!sw.IsRunning) { sw.Start(); }

                //１０秒以上経過
                if (sw.ElapsedMilliseconds > (10 * 1000))
                {
                    var dlgRet_yesNo = MessageBox.Show
                        (string.Format("ビジー状態です。中止しますか？"), "確認", MessageBoxButtons.YesNo);

                    if (dlgRet_yesNo == DialogResult.Yes) { return; }

                    else { sw.Stop(); sw.Reset(); sw.Start(); }
                }
            }

            //BackgroundWorkerのProgressChangedイベントが発生するようにする
            _bgWorker.WorkerReportsProgress = true;

            // 処理を開始する パラメータが必要なければ省略できる
            _bgWorker.RunWorkerAsync();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this._bgWorker.CancelAsync();

            CancelButtonClick.Invoke();
        }
    }

    public class WaitingDialog_Simple : WaitingDialogBase
    {
        public WaitingDialog_Simple()
        {
            base.progressBar.Visible = false;
            base.btn_Cancel.Visible = false;
            base.ClientSize = new Size(base.ClientSize.Width, base.ClientSize.Height - 40);
            // base.label_Message.Location = new Point(60,40);
        }
    }

    public class WaitingDialog_Cancel : WaitingDialogBase
    {
        public WaitingDialog_Cancel()
        {
            base.progressBar.Visible = false;
            base.btn_Cancel.Visible = true;
            base.btn_Cancel.Location = new Point(base.btn_Cancel.Location.X, base.btn_Cancel.Location.Y - 20);
            base.ClientSize = new Size(base.ClientSize.Width, base.ClientSize.Height -20);
            // base.label_Message.Location = new Point(60,40);
        }
    }
}
