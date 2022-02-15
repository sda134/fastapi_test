using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi.MXComponent;


// 18.12.12 ようやく一つのファイルにまとめた。しばらく使ってみて、問題が無ければライブラリの方へ移す。
//namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms

// 19.03.06 
// ・接続確認時のエラーメッセージ対応
// ・showMessage のformat バージョンを追加

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms
{
    // CCLink の時は ActIONumber, ActUnitNumber の設定が必要だとか 19.07.19


    public class MXComponentConfigurationPannel : UserControl
    {
        protected System.ComponentModel.IContainer components = null;


        protected int _actIONumber  = 1023;
        protected int _actUnitNumber  = 0;
        protected int _actDestinationPortNumber  = 0;
        protected int _actDestinationIONumber  = 0;


        #region region - constructor, destructor


        public MXComponentConfigurationPannel()
        {
            this.InitializeComponent();

            // コンボボックスの項目設定
            this.comboBox_ProtocolType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType));
            this.comboBox_CpuType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType));
            this.comboBox_UnitType.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType));
        }

        protected void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBox_message = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.MessageTextBox();
            this.ethernetCommunicationSetupPannel1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.EthernetCommunicationSetupPannel();
            this.serialCommunicationSetupPannel_forMXComponent1 = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.Forms.SerialCommunicationSetupPannel_forMXComponent();
            this.comboBox_ProtocolType = new System.Windows.Forms.ComboBox();
            this.label_protocolType = new System.Windows.Forms.Label();
            this.label_UnitType = new System.Windows.Forms.Label();
            this.comboBox_UnitType = new System.Windows.Forms.ComboBox();
            this.label_CpuType = new System.Windows.Forms.Label();
            this.comboBox_CpuType = new System.Windows.Forms.ComboBox();
            this.label_message = new System.Windows.Forms.Label();
            this.button_confirm_connection = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBox_message);
            this.groupBox1.Controls.Add(this.ethernetCommunicationSetupPannel1);
            this.groupBox1.Controls.Add(this.serialCommunicationSetupPannel_forMXComponent1);
            this.groupBox1.Controls.Add(this.comboBox_ProtocolType);
            this.groupBox1.Controls.Add(this.label_protocolType);
            this.groupBox1.Controls.Add(this.label_UnitType);
            this.groupBox1.Controls.Add(this.comboBox_UnitType);
            this.groupBox1.Controls.Add(this.label_CpuType);
            this.groupBox1.Controls.Add(this.comboBox_CpuType);
            this.groupBox1.Controls.Add(this.label_message);
            this.groupBox1.Controls.Add(this.button_confirm_connection);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 194);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MXComponentControlPannel";
            // 
            // TextBox_message
            // 
            this.TextBox_message.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.TextBox_message.Location = new System.Drawing.Point(89, 157);
            this.TextBox_message.Name = "TextBox_message";
            this.TextBox_message.Size = new System.Drawing.Size(152, 19);
            this.TextBox_message.TabIndex = 45;
            this.TextBox_message.Text = "メッセージを表示します";
            // 
            // ethernetCommunicationSetupPannel1
            // 
            this.ethernetCommunicationSetupPannel1.Location = new System.Drawing.Point(251, 18);
            this.ethernetCommunicationSetupPannel1.Name = "ethernetCommunicationSetupPannel1";
            this.ethernetCommunicationSetupPannel1.Size = new System.Drawing.Size(211, 143);
            this.ethernetCommunicationSetupPannel1.TabIndex = 44;
            // 
            // serialCommunicationSetupPannel_forMXComponent1
            // 
            this.serialCommunicationSetupPannel_forMXComponent1.Location = new System.Drawing.Point(251, 18);
            this.serialCommunicationSetupPannel_forMXComponent1.Name = "serialCommunicationSetupPannel_forMXComponent1";
            this.serialCommunicationSetupPannel_forMXComponent1.Size = new System.Drawing.Size(204, 158);
            this.serialCommunicationSetupPannel_forMXComponent1.TabIndex = 43;
            // 
            // comboBox_ProtocolType
            // 
            this.comboBox_ProtocolType.FormattingEnabled = true;
            this.comboBox_ProtocolType.Location = new System.Drawing.Point(89, 24);
            this.comboBox_ProtocolType.Name = "comboBox_ProtocolType";
            this.comboBox_ProtocolType.Size = new System.Drawing.Size(152, 20);
            this.comboBox_ProtocolType.TabIndex = 42;
            this.comboBox_ProtocolType.SelectedIndexChanged += new System.EventHandler(this.comboBox_ProtocolType_SelectedIndexChanged);
            // 
            // label_protocolType
            // 
            this.label_protocolType.Location = new System.Drawing.Point(6, 25);
            this.label_protocolType.Name = "label_protocolType";
            this.label_protocolType.Size = new System.Drawing.Size(77, 19);
            this.label_protocolType.TabIndex = 40;
            this.label_protocolType.Text = "接続経路";
            this.label_protocolType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_UnitType
            // 
            this.label_UnitType.Location = new System.Drawing.Point(13, 80);
            this.label_UnitType.Name = "label_UnitType";
            this.label_UnitType.Size = new System.Drawing.Size(72, 15);
            this.label_UnitType.TabIndex = 36;
            this.label_UnitType.Text = "ユニットタイプ";
            this.label_UnitType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_UnitType
            // 
            this.comboBox_UnitType.FormattingEnabled = true;
            this.comboBox_UnitType.Location = new System.Drawing.Point(89, 78);
            this.comboBox_UnitType.Name = "comboBox_UnitType";
            this.comboBox_UnitType.Size = new System.Drawing.Size(152, 20);
            this.comboBox_UnitType.TabIndex = 35;
            // 
            // label_CpuType
            // 
            this.label_CpuType.Location = new System.Drawing.Point(13, 52);
            this.label_CpuType.Name = "label_CpuType";
            this.label_CpuType.Size = new System.Drawing.Size(72, 15);
            this.label_CpuType.TabIndex = 34;
            this.label_CpuType.Text = "CPU タイプ";
            this.label_CpuType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_CpuType
            // 
            this.comboBox_CpuType.FormattingEnabled = true;
            this.comboBox_CpuType.Location = new System.Drawing.Point(89, 50);
            this.comboBox_CpuType.Name = "comboBox_CpuType";
            this.comboBox_CpuType.Size = new System.Drawing.Size(152, 20);
            this.comboBox_CpuType.TabIndex = 33;
            // 
            // label_message
            // 
            this.label_message.AutoSize = true;
            this.label_message.Location = new System.Drawing.Point(17, 160);
            this.label_message.Name = "label_message";
            this.label_message.Size = new System.Drawing.Size(50, 12);
            this.label_message.TabIndex = 32;
            this.label_message.Text = "メッセージ";
            // 
            // button_confirm_connection
            // 
            this.button_confirm_connection.Location = new System.Drawing.Point(89, 131);
            this.button_confirm_connection.Name = "button_confirm_connection";
            this.button_confirm_connection.Size = new System.Drawing.Size(152, 23);
            this.button_confirm_connection.TabIndex = 25;
            this.button_confirm_connection.Text = "接続確認";
            this.button_confirm_connection.UseVisualStyleBackColor = true;
            this.button_confirm_connection.Click += new System.EventHandler(this.button_confirm_connection_Click);
            // 
            // MXComponentConfigurationPannel
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "MXComponentConfigurationPannel";
            this.Size = new System.Drawing.Size(470, 200);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #endregion


        #region region - public properties



        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("表示")]
        [System.ComponentModel.Description("コントロールに関連付けられたテキストです。")]
        public override string Text
        {
            get => this.groupBox1.Text;

            set
            {
                base.Text = value;
                this.groupBox1.Text = value;
            }
        }


        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        // これを書かないと resx ファイルが作成され、少々めんどくさい
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat Value
        {
            get
            {
                var protocolType = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType)this.comboBox_ProtocolType.SelectedItem;
                var cpuType = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)this.comboBox_CpuType.SelectedItem;
                
                switch (cpuType)
                {
                    case ActCpuType.FX5UCPU:
                        #region - FX5U

                        int pattern = 1;

                        // パターン判断 を入れる必要がある

                        // ActDestinationPortNumber
                        if (protocolType == Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_TCPIP)
                            this._actDestinationPortNumber = 5562;
                        else if (protocolType == Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType.PROTOCOL_UDPIP)
                            this._actDestinationPortNumber = 5560;

                        // ActDestinationIONumber
                        if (pattern == 3)
                        {
                            this._actDestinationIONumber = 1023;
                        }

                        #endregion
                        break;
                    default:
                        break;
                }


                var ret = new Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat
                {
                    ActProtocolType = protocolType,
                    ActCpuType = cpuType,
                    ActUnitType = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)this.comboBox_UnitType.SelectedItem,

                    // Serial
                    DataBits = this.serialCommunicationSetupPannel_forMXComponent1.Value.DataBits,
                    BaudRate = this.serialCommunicationSetupPannel_forMXComponent1.Value.BaudRate,
                    ParityBits = this.serialCommunicationSetupPannel_forMXComponent1.Value.ParityBits,
                    PortNumber = this.serialCommunicationSetupPannel_forMXComponent1.Value.PortNumber,
                    StopBits = this.serialCommunicationSetupPannel_forMXComponent1.Value.StopBits,
                    ActControl = this.serialCommunicationSetupPannel_forMXComponent1.Value.ActControl,

                    // Ether
                    ActHostAddress = this.ethernetCommunicationSetupPannel1.Value.IPAddress,

                    // 19.07.19 追加
                    ActDestinationPortNumber = this._actDestinationPortNumber,
                    ActDestinationIONumber = this._actDestinationIONumber,
                    ActIONumber = this._actIONumber,
                    ActUnitNumber = this._actUnitNumber,
                };

                return ret;
            }
            set
            {
                if (value == null) value = new ActControlSettingFormat();


                // Ether
                this.ethernetCommunicationSetupPannel1.Value = new EthernetSettingFormat()
                {
                    IPAddress = value.ActHostAddress,
                };

                // serial
                this.serialCommunicationSetupPannel_forMXComponent1.Value = new SerialPortSettingFormatMX
                {
                    DataBits = value.DataBits,
                    ActControl = value.ActControl,
                    BaudRate = value.BaudRate,
                    ParityBits = value.ParityBits,
                    PortNumber = value.PortNumber,
                    StopBits = value.StopBits,
                };

                this.comboBox_ProtocolType.SelectedItem = value.ActProtocolType;
                this.comboBox_CpuType.SelectedItem = value.ActCpuType;
                this.comboBox_UnitType.SelectedItem = value.ActUnitType;


                // 19.07.19 追加
                this._actDestinationPortNumber = value.ActDestinationPortNumber;
                this._actDestinationIONumber = value.ActDestinationIONumber;
                this._actIONumber = value.ActIONumber;
                this._actUnitNumber = value.ActUnitNumber;
            }    
        }




        #endregion


        #region region - event

        
        private void comboBox_ProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ↓ 0.1.1.1 修正
            // IP
            this.ethernetCommunicationSetupPannel1.Visible =
                (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType)this.comboBox_ProtocolType.SelectedItem == ActProtocolType.PROTOCOL_UDPIP ||
                (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType)this.comboBox_ProtocolType.SelectedItem == ActProtocolType.PROTOCOL_TCPIP;

            // Serial
            this.serialCommunicationSetupPannel_forMXComponent1.Visible =
                (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActProtocolType)this.comboBox_ProtocolType.SelectedItem == ActProtocolType.PROTOCOL_SERIAL;
        }



        #endregion


        protected void showMessage(string msg)
        {
            this.TextBox_message.Text = msg;
        }
        protected void showMessage(string format, params object[] args)　=> this.showMessage(string.Format(format, args));
        





        protected void button_confirm_connection_Click(object sender, EventArgs e)
        {

            // 接続タイプごとの事前確認
            switch ((ActProtocolType)this.comboBox_ProtocolType.SelectedItem)
            {
                #region

                case ActProtocolType.PROTOCOL_TCPIP:
                case ActProtocolType.PROTOCOL_UDPIP:
                    if (string.IsNullOrWhiteSpace(this.ethernetCommunicationSetupPannel1.Value.IPAddress))
                    {
                        this.showMessage(string.Format("IPアドレスが指定されていません"));

                        return;
                    }

                    //Pingオブジェクトの作成
                    using (var p = new System.Net.NetworkInformation.Ping())
                    {
                        try
                        {
                            //指定アドレスにPingを送信する 17.10.31 ここでも例外が発生する事が判明
                            System.Net.NetworkInformation.PingReply reply = p.Send(this.ethernetCommunicationSetupPannel1.Value.IPAddress);

                            //結果を取得
                            if (reply.Status != System.Net.NetworkInformation.IPStatus.Success)
                            {
                                this.showMessage(string.Format("Ping 送信失敗"));

                                return;
                            }

                        } // try


                        catch (Exception ex)
                        {
                            this.showMessage(ex.Message);
                        }

                    } // using ping*/
                    break;

                default:
                    break;

                    #endregion
            }

            //　インスタンス生成
            var actProg = (ActProgTypeLib.ActProgTypeClass)this.Value;
            if (actProg == null)
            {
            }
            else
            {
                // オープン
                {
                    int iRet_open = actProg.Open();

                    if (iRet_open == 0)
                    {
                    }
                    else
                    {
                        string errorMsg = 
                            Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(iRet_open) ??
                            string.Format("0x{0:x8} [HEX]", iRet_open);

                        this.showMessage("エラー：{0}", errorMsg);

                        return;
                    }
                }


                // クローズ
                {
                    int iRet_close = actProg.Close();

                    if (iRet_close == 0)
                    {
                        this.showMessage(string.Format("正常終了"));
                    }
                    else
                    {
                        string errorMsg =    
                            Mtec.UtilityLibrary.Mitsubishi.MXComponent.Tools.GetErrorMessage(iRet_close) ??    
                            string.Format("0x{0:x8} [HEX]", iRet_close);

                        this.showMessage("エラー：{0}", errorMsg);
                        return;
                    }
                }
            }
        }


        #region protected member ※何故かここに書かないとブラウザが機能しない
        
        protected GroupBox groupBox1;
        protected ComboBox comboBox_ProtocolType;
        protected Label label_protocolType;
        protected Label label_UnitType;
        protected ComboBox comboBox_UnitType;
        protected Label label_CpuType;
        protected ComboBox comboBox_CpuType;
        protected Label label_message;
        protected SerialCommunicationSetupPannel_forMXComponent serialCommunicationSetupPannel_forMXComponent1;
        protected EthernetCommunicationSetupPannel ethernetCommunicationSetupPannel1;
        protected MessageTextBox TextBox_message;
        protected Button button_confirm_connection;

        #endregion


    }

    public class EthernetCommunicationSetupPannel : UserControl
    {
        protected Button button_ping;
        protected Label label_IPAddress;
        //protected TextBox textBox_message;
        protected MessageTextBox textBox_message;

        protected TextBox textBox_IPAddress;

        public EthernetCommunicationSetupPannel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button_ping = new System.Windows.Forms.Button();
            this.label_IPAddress = new System.Windows.Forms.Label();
            this.textBox_IPAddress = new System.Windows.Forms.TextBox();
            this.textBox_message = new MessageTextBox() ;
            this.SuspendLayout();
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(5, 30);
            this.button_ping.Name = "button_ping";
            this.button_ping.Size = new System.Drawing.Size(60, 23);
            this.button_ping.TabIndex = 27;
            this.button_ping.Text = "Ping";
            this.button_ping.UseVisualStyleBackColor = true;
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // label_IPAddress
            // 
            this.label_IPAddress.Location = new System.Drawing.Point(3, 10);
            this.label_IPAddress.Name = "label_IPAddress";
            this.label_IPAddress.Size = new System.Drawing.Size(62, 12);
            this.label_IPAddress.TabIndex = 25;
            this.label_IPAddress.Text = "IP Address";
            this.label_IPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_IPAddress
            // 
            this.textBox_IPAddress.Location = new System.Drawing.Point(71, 7);
            this.textBox_IPAddress.Name = "textBox_IPAddress";
            this.textBox_IPAddress.Size = new System.Drawing.Size(121, 19);
            this.textBox_IPAddress.TabIndex = 24;
            this.textBox_IPAddress.Text = "192.168.1.1";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(71, 32);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(121, 19);
            this.textBox_message.TabIndex = 28;
            // 
            // EthernetCommunicationSetupPannel
            // 
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.button_ping);
            this.Controls.Add(this.label_IPAddress);
            this.Controls.Add(this.textBox_IPAddress);
            this.Name = "EthernetCommunicationSetupPannel";
            this.Size = new System.Drawing.Size(211, 143);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public EthernetSettingFormat Value
        {
            get => new EthernetSettingFormat
            {
                IPAddress = this.textBox_IPAddress.Text,
            };

            set
            {
                this.textBox_IPAddress.Text = value.IPAddress;
            }
        }


        private void button_ping_Click(object sender, EventArgs e)
        {
            //Pingオブジェクトの作成
            using (var p = new System.Net.NetworkInformation.Ping())
            {
                try
                {
                    //指定アドレスにPingを送信する 17.10.31 ここでも例外が発生する事が判明
                    System.Net.NetworkInformation.PingReply reply = p.Send(this.textBox_IPAddress.Text);

                    //結果を取得
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        this.showMessage(string.Format("成功"));
                    }
                    else
                    {
                        this.showMessage(string.Format("失敗"));
                        return;
                    }

                } // try


                catch (Exception ex)
                {
                    this.showMessage(ex.Message);
                }

            } // using ping
        }

        private void showMessage(string msg)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { this.showMessage(msg); }));
            }
            else
            {
                this.textBox_message.Text = msg;
            }
        }
    }

    public class SerialCommunicationSetupPannel_forMXComponent : UserControl
    {
        protected ComboBox comboBox_DataBits;
        protected Label label_control;
        protected ComboBox comboBox_BaudRate;
        protected ComboBox comboBox_Port;
        protected Label label_BaudRate;
        protected Label label_StopBits;
        protected Label label_DataBits;
        protected ComboBox comboBox_Control;
        protected ComboBox comboBox_StopBits;
        protected Label label_PortName;
        protected ComboBox comboBox_ParityBits;
        protected Label label_ParityBits;

        public SerialCommunicationSetupPannel_forMXComponent()
        {
            this.InitializeComponent();

            this.comboBox_Port.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber));
            this.comboBox_BaudRate.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate));
            this.comboBox_DataBits.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits));
            this.comboBox_ParityBits.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity));
            this.comboBox_StopBits.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits));
            this.comboBox_Control.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl));

            this.Value = new SerialPortSettingFormatMX();
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual SerialPortSettingFormatMX Value
        {
            get
            {
                var ret = new SerialPortSettingFormatMX
                {
                    PortNumber = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber)this.comboBox_Port.SelectedItem,
                    BaudRate = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate)this.comboBox_BaudRate.SelectedItem,
                    DataBits = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits)this.comboBox_DataBits.SelectedItem,
                    StopBits = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits)this.comboBox_StopBits.SelectedItem,
                    ParityBits = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity)this.comboBox_ParityBits.SelectedItem,
                    ActControl = (Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl)this.comboBox_Control.SelectedItem,
                };

                return ret;
            }
            set
            {
                if (value == null) value = new SerialPortSettingFormatMX();

                // enum
                this.comboBox_Port.SelectedItem = value.PortNumber;
                this.comboBox_BaudRate.SelectedItem = value.BaudRate;
                this.comboBox_DataBits.SelectedItem = value.DataBits;
                this.comboBox_StopBits.SelectedItem = value.StopBits;
                this.comboBox_ParityBits.SelectedItem = value.ParityBits;
                this.comboBox_Control.SelectedItem = value.ActControl;
            }
        }

        private void InitializeComponent()
        {
            this.comboBox_DataBits = new System.Windows.Forms.ComboBox();
            this.label_control = new System.Windows.Forms.Label();
            this.comboBox_BaudRate = new System.Windows.Forms.ComboBox();
            this.comboBox_Port = new System.Windows.Forms.ComboBox();
            this.label_BaudRate = new System.Windows.Forms.Label();
            this.label_StopBits = new System.Windows.Forms.Label();
            this.label_DataBits = new System.Windows.Forms.Label();
            this.comboBox_Control = new System.Windows.Forms.ComboBox();
            this.comboBox_StopBits = new System.Windows.Forms.ComboBox();
            this.label_PortName = new System.Windows.Forms.Label();
            this.comboBox_ParityBits = new System.Windows.Forms.ComboBox();
            this.label_ParityBits = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_DataBits
            // 
            this.comboBox_DataBits.FormattingEnabled = true;
            this.comboBox_DataBits.Location = new System.Drawing.Point(77, 82);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_DataBits.TabIndex = 41;
            // 
            // label_control
            // 
            this.label_control.Location = new System.Drawing.Point(10, 137);
            this.label_control.Name = "label_control";
            this.label_control.Size = new System.Drawing.Size(61, 12);
            this.label_control.TabIndex = 40;
            this.label_control.Text = "Control";
            this.label_control.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_BaudRate
            // 
            this.comboBox_BaudRate.FormattingEnabled = true;
            this.comboBox_BaudRate.Location = new System.Drawing.Point(77, 5);
            this.comboBox_BaudRate.Name = "comboBox_BaudRate";
            this.comboBox_BaudRate.Size = new System.Drawing.Size(121, 20);
            this.comboBox_BaudRate.TabIndex = 39;
            // 
            // comboBox_Port
            // 
            this.comboBox_Port.FormattingEnabled = true;
            this.comboBox_Port.Location = new System.Drawing.Point(77, 31);
            this.comboBox_Port.Name = "comboBox_Port";
            this.comboBox_Port.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Port.TabIndex = 38;
            // 
            // label_BaudRate
            // 
            this.label_BaudRate.Location = new System.Drawing.Point(9, 12);
            this.label_BaudRate.Name = "label_BaudRate";
            this.label_BaudRate.Size = new System.Drawing.Size(62, 12);
            this.label_BaudRate.TabIndex = 34;
            this.label_BaudRate.Text = "BaudRate";
            this.label_BaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_StopBits
            // 
            this.label_StopBits.Location = new System.Drawing.Point(10, 111);
            this.label_StopBits.Name = "label_StopBits";
            this.label_StopBits.Size = new System.Drawing.Size(61, 12);
            this.label_StopBits.TabIndex = 30;
            this.label_StopBits.Text = "StopBits";
            this.label_StopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_DataBits
            // 
            this.label_DataBits.Location = new System.Drawing.Point(7, 86);
            this.label_DataBits.Name = "label_DataBits";
            this.label_DataBits.Size = new System.Drawing.Size(64, 12);
            this.label_DataBits.TabIndex = 31;
            this.label_DataBits.Text = "DataBits";
            this.label_DataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_Control
            // 
            this.comboBox_Control.FormattingEnabled = true;
            this.comboBox_Control.Location = new System.Drawing.Point(77, 134);
            this.comboBox_Control.Name = "comboBox_Control";
            this.comboBox_Control.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Control.TabIndex = 36;
            // 
            // comboBox_StopBits
            // 
            this.comboBox_StopBits.FormattingEnabled = true;
            this.comboBox_StopBits.Location = new System.Drawing.Point(77, 108);
            this.comboBox_StopBits.Name = "comboBox_StopBits";
            this.comboBox_StopBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_StopBits.TabIndex = 37;
            // 
            // label_PortName
            // 
            this.label_PortName.Location = new System.Drawing.Point(38, 36);
            this.label_PortName.Name = "label_PortName";
            this.label_PortName.Size = new System.Drawing.Size(33, 12);
            this.label_PortName.TabIndex = 33;
            this.label_PortName.Text = "Port";
            this.label_PortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_ParityBits
            // 
            this.comboBox_ParityBits.FormattingEnabled = true;
            this.comboBox_ParityBits.Location = new System.Drawing.Point(77, 57);
            this.comboBox_ParityBits.Name = "comboBox_ParityBits";
            this.comboBox_ParityBits.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ParityBits.TabIndex = 35;
            // 
            // label_ParityBits
            // 
            this.label_ParityBits.Location = new System.Drawing.Point(11, 60);
            this.label_ParityBits.Name = "label_ParityBits";
            this.label_ParityBits.Size = new System.Drawing.Size(61, 12);
            this.label_ParityBits.TabIndex = 32;
            this.label_ParityBits.Text = "ParityBits";
            this.label_ParityBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SerialCommunicationSetupPannel_forMXComponent
            // 
            this.Controls.Add(this.comboBox_DataBits);
            this.Controls.Add(this.label_control);
            this.Controls.Add(this.comboBox_BaudRate);
            this.Controls.Add(this.comboBox_Port);
            this.Controls.Add(this.label_BaudRate);
            this.Controls.Add(this.label_StopBits);
            this.Controls.Add(this.label_DataBits);
            this.Controls.Add(this.comboBox_Control);
            this.Controls.Add(this.comboBox_StopBits);
            this.Controls.Add(this.label_PortName);
            this.Controls.Add(this.comboBox_ParityBits);
            this.Controls.Add(this.label_ParityBits);
            this.Name = "SerialCommunicationSetupPannel_forMXComponent";
            this.Size = new System.Drawing.Size(204, 158);
            this.ResumeLayout(false);

        }


    }


    #region reiong - model

    public class EthernetSettingFormat
    {
        public virtual string IPAddress { get; set; } = "192.168.1.1";
    }

    public class SerialPortSettingFormatMX
    {
        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber PortNumber { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActPortnumber.PORT_1;

        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate BaudRate { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActBaudrate.BAUDRATE_9600;

        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits DataBits { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActDataBits.DATABIT_7;

        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits StopBits { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActStopBits.STOPBIT_ONE;

        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity ParityBits { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActParity.EVEN_PARITY;

        public virtual Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl ActControl { get; set; } = Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControl.TRC_DTR_OR_RTS;
    }

    #endregion


    #region region - delegate

    public class ActControlPannelEventArg : EventArgs
    {
        public string Message { get; set; }
    }

    public delegate void ActControlPannelHandler(object sender, ActControlPannelEventArg e);

    #endregion


    // いちいち読み込まないでいいように、内部で持つ事に 18.12.11
    public class MessageTextBox : System.Windows.Forms.TextBox
    {
        private System.Diagnostics.Stopwatch _stopWatch;

        public MessageTextBox()
        {
            //InitializeComponent の代わりの処理

            this.Disposed += (sender, args) =>
            {
                this._stopWatch?.Reset();
                //this._stopWatch = null;
            };
        }

        [System.ComponentModel.Category("動作")]
        [System.ComponentModel.Description("表示テキストが自動的に消えるまでの時間を指定します。")]
        [System.ComponentModel.DefaultValue(5000)]
        public int TextDismissMilliseconds
        {
            get => this._textDismissMilliseconds;
            set => this._textDismissMilliseconds = value < 0 ? 0 : value;
        }
        private int _textDismissMilliseconds  = 5000;


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
                        Task.Run(() =>
                        {
                            // 5秒まつ                        
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
