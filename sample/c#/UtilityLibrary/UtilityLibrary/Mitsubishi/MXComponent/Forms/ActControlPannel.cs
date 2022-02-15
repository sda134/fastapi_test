using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Mtec.UtilityLibrary.Mitsubishi.MXComponent;

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms
{
    
    public partial class ActControlPannel : UserControl
    {
        protected ActProgTypeLib.ActProgTypeClass actProg;


        private bool isOpen = false;

        private System.Timers.Timer timer;
        private List<int> onErrorList;

        public event EventHandler MessageSent = (obj,e) => { };

        //public static System.Threading.Semaphore Semaphore = new System.Threading.Semaphore(1, 1);


        public ActControlPannel()
        {
            InitializeComponent();

            this.actProg = new ActProgTypeLib.ActProgTypeClass();

            this.comboBox_CpuType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType));
            this.comboBox_UnitType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType));

            //this.actProg.ActProtocolType = (int)Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_UDPIP;
            this.actProg.ActProtocolType = (int)Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_TCPIP;
            this.actProg.ActStationNumber = 255; //通信設定ユーティリティで設定した倫理局番の事 ※Progでは未使用なはずだが、初期値255を入れておかないとエラーになる
            this.actProg.ActPortNumber = 0; // PC 側のポート番号。 =0 で自動選択


            this.actProg.ActThroughNetworkType = 0x01;

            

            // タイマー設定
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;

            // エラー時のBMF# リスト
            this.onErrorList = new List<int>();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var bufferNumList = default(List<int>);

            switch (this.actProg.ActUnitType)
            {
                case (int)Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType.FXENET:  // ActDefine.UNIT_FXENET:
                    bufferNumList = new List<int> { 105, 124, 125, 126, 229 };
                    break;

                default:
                    break;
            }

            if (bufferNumList != null)
            {
                foreach (int bfm in bufferNumList)
                {
                    // バッファ読み出しテスト
                    short shValue;

                    int ret_buff = this.actProg.ReadBuffer(0, bfm, 1, out shValue);

                    if (ret_buff == 0)
                    {
                        if (shValue != 0)
                        {
                            if (onErrorList.FindIndex(x => x == bfm) == -1)
                            {
                                onErrorList.Add(bfm);

                                this.InvokeMessage(string.Format("エラー：BFM# {0} - 0x{1:x8} [HEX]", bfm, shValue));
                            }
                        }
                        else
                        {
                            int idx = onErrorList.FindIndex(x => x == bfm);

                            if (idx != -1)
                            {
                                // リセット
                                onErrorList.RemoveAt(idx);
                            }
                        }
                    }
                }
            }
        }

        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat Value
        {
            get
            {
                var ret = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat
                {
                    ActHostAddress = this.textBox_target.Text,
                    ActCpuType = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)this.comboBox_CpuType.SelectedItem,
                    ActUnitType = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)this.comboBox_UnitType.SelectedItem,
                };

                return ret;
            }
            set
            {
                if (value == null) return;

                this.textBox_target.Text = value.ActHostAddress;
                this.comboBox_CpuType.SelectedItem = value.ActCpuType;
                this.comboBox_UnitType.SelectedItem = value.ActUnitType;
            }
        }

        #region region - デザイナ用プロパティ


        [System.ComponentModel.Category("表示")]
        [System.ComponentModel.Description("コントロールに関連付けられたテキストです。")]
        [System.ComponentModel.DefaultValue("")]
        public override string Text
        {
            set => this.groupBox1.Text = value; get => this.groupBox1.Text;
        }


        [System.ComponentModel.Description("交信するPLC のＩＰアドレスです。")]
        [System.ComponentModel.DefaultValue("192.168.0.0")]
        public string IPAddress
        {
            get => this.actProg.ActHostAddress; set => this.actProg.ActHostAddress = value;
        }
        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType ActCpuType
        {
            get => (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)this.actProg.ActCpuType; set => this.actProg.ActCpuType = (int)value;
        }
        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType ActUnitType
        {
            get => (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)this.actProg.ActUnitType; set => this.actProg.ActUnitType = (int)value;
        }

        #endregion


        private void button_prog_open_Click(object sender, EventArgs e)
        {
            this.actProg.ActHostAddress = this.textBox_target.Text; // 接続先IP/COM           
            this.actProg.ActCpuType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)this.comboBox_CpuType.SelectedItem;
            this.actProg.ActUnitType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)this.comboBox_UnitType.SelectedItem;

            /*
            this.actProg.ActIONumber = 0x03ff;
            this.actProg.ActThroughNetworkType = 0x0001;
            this.actProg.ActDataBits = 0;
            this.actProg.ActControl = 0;
            this.actProg.ActParity = 0;
            */


            int iRet = this.actProg.Open();

            if (iRet == 0)
            {
                this.isOpen = true;
                this.refreshDisplay();

                this.InvokeMessage(string.Format("オープン成功"));

                timer.Enabled = true;
            }
            else
            {
                this.InvokeMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet));
            }
        }


        private void button_prog_close_Click(object sender, EventArgs e)
        {            
            int iRet = this.actProg.Close();

            if (iRet == 0)
            {
                this.isOpen = false;
                this.refreshDisplay();
                this.InvokeMessage(string.Format("クローズ成功"));

                timer.Enabled = false;
            }
            else
            {
                this.InvokeMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet));
            }
        }


        private void button_deviceRead_Click(object sender, EventArgs e)
        {
            int val;

            // 18.01.30 通信時間測定
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();


            int iRet = this.actProg.GetDevice(this.textBox_device.Text, out val);

            // 18.01.30 通信時間測定
            sw.Stop();
            Console.WriteLine("MX GetDevice: {0} mSec",sw.ElapsedMilliseconds);


            if (iRet != 0)
                this.InvokeMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet));
            else
            {
                this.InvokeMessage("読み込み正常終了");
                this.textBox_deviceVal.Text = val.ToString();
            }
        }


        private void button_deviceWrite_Click(object sender, EventArgs e)
        {
            int iVal;

            if (int.TryParse(this.textBox_deviceVal.Text, out iVal))
            {
                int iRet = this.actProg.SetDevice(this.textBox_device.Text, iVal);

                if (iRet != 0)
                    this.InvokeMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet));
                else
                    this.InvokeMessage(string.Format("書き込み正常終了"));
            }
            else
                InvokeMessage("数値に変換できません");
        }


        private void refreshDisplay()
        {
            this.textBox_target.Enabled = !this.isOpen;
            this.comboBox_UnitType.Enabled = !this.isOpen;
            this.comboBox_CpuType.Enabled = !this.isOpen;

            this.textBox_Message.Enabled = !this.isOpen;
            this.button_confirmConnection.Enabled = !this.isOpen;
        }

        private void button_confirmConnection_Click(object sender, EventArgs e)
        {
            Confirm_ServerConnection();
        }

        private void InvokeMessage(string msg)
        {
            this.MessageSent.Invoke(this, new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.ActControlPannelEventArg { Message = msg });
        }

        public bool Confirm_ServerConnection()
        {
            // 接続失敗した場合は途中で false を返して処理を中止する事！！

            if (string.IsNullOrWhiteSpace(this.textBox_target.Text))
            {
                this.showMessage(string.Format("IPアドレスが指定されていません"));
                return false;
            }

            //Pingオブジェクトの作成
            using (var p = new System.Net.NetworkInformation.Ping())
            {
                try
                {
                    //指定アドレスにPingを送信する 17.10.31 ここでも例外が発生する事が判明
                    System.Net.NetworkInformation.PingReply reply = p.Send(this.textBox_target.Text);

                    //結果を取得
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        this.actProg.ActHostAddress = this.textBox_target.Text; // 接続ホスト名（IPアドレス）            
                        this.actProg.ActCpuType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)this.comboBox_CpuType.SelectedItem;
                        this.actProg.ActUnitType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)this.comboBox_UnitType.SelectedItem;

                        int iRet_open = this.actProg.Open();

                        if (iRet_open == 0)
                            this.showMessage(string.Format("成功"));

                        else
                        {
                            // ServerSetting の値を変更　参照型になっているので、これで ServerSetting のインスタンスは変更されている
                            this.showMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet_open));

                            return false;
                        }


                    } // ping 結果

                    else
                    {
                        this.showMessage(string.Format("Ping 送信失敗"));
                        return false;
                    }

                } // try


                catch (Exception ex)
                {
                    this.showMessage(ex.Message);
                }
                finally
                {
                    int iRet_close = this.actProg.Close();

                    if (iRet_close == 0)
                        this.showMessage(string.Format("成功"));

                    else
                    {
                        // ServerSetting の値を変更　参照型になっているので、これで ServerSetting のインスタンスは変更されている
                        this.showMessage(string.Format("エラー：0x{0:x8} [HEX]", iRet_close));
                    }
                }

            } // using ping

            return true;
        }


        private void showMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                // 別スレッドから呼び出された場合
                this.Invoke(new MethodInvoker(() => { showMessage(msg); }));
            }
            else
            {
                this.textBox_Message.ForeColor = System.Drawing.SystemColors.WindowText;
                this.textBox_Message.Text = msg;
            }
        }
    }
}
