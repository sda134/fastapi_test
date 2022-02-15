using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Mtec.UtilityLibrary.Mitsubishi.MCProtocol;


namespace TestingDialog
{

    public partial class MainDialog : Form
    {

        private MCComponent mc;


        public MainDialog()
        {
            InitializeComponent();

            // デザイナでやらず、直接記入した方が不具合も起こらず確実 18.01.25
            this.Icon = Properties.Resources.Icon_main;

            this.mc = new MCComponent();
            
            
            // ComboBox の項目の設定
            this.comboBox_frame.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MCProtocol.MCProtocolFrame));
            this.comboBox_protocol.DataSource = Enum.GetValues(typeof(Mtec.UtilityLibrary.Mitsubishi.MCProtocol.CommunicationProtocol));

            this.textBox_LocalIP.Text = Properties.Settings.Default.LocalIP;
            this.numericUpDown_LocalPort.Value = Properties.Settings.Default.LocalPort;
            this.textBox_RemoteIP.Text = Properties.Settings.Default.RemoteIP;
            this.numericUpDown_RemotePort.Value = Properties.Settings.Default.RemotePort;
            this.textBox_deviceName.Text = Properties.Settings.Default.Device;
            

            // Set の時は SelectedItem じゃないとダメらしい 18.01.22
            this.comboBox_frame.SelectedItem = Properties.Settings.Default.Frame;
            this.comboBox_protocol.SelectedItem = Properties.Settings.Default.Protocol;


            this.binding();
        }


        private void binding()
        {
            this.mc.RemoteIPAddress = this.textBox_RemoteIP.Text;
            this.mc.RemotePortNumber = (int)this.numericUpDown_RemotePort.Value; ;

            // this.mc.LocalPortNumber = (int)this.numericUpDown_LocalPort.Value;
            // this.mc.LocalIPAddres = this.textBox_LocalIP.Text;

            

            // Get の時は SelectedValue でもいいらしい
            this.mc.Frame = (MCProtocolFrame)this.comboBox_frame.SelectedItem;
            this.mc.Protocol = (CommunicationProtocol)this.comboBox_protocol.SelectedItem;

            //this.mc.Frame = MCProtocolFrame._1EFrame;

        }


        #region region - button click

        private void button_apply_Click(object sender, EventArgs e)
        {
            this.binding();
        }        
        
        private void button_reset_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_RemoteReset();
            this.refreshDisplay(ret, "");
        }

        private void button_RemoteRun_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_RemoteRun(false, ClearMode.NoClear);
            this.refreshDisplay(ret, "");
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_RemoteStop();
            this.refreshDisplay(ret, "");
        }

        private void button_cpuName_Click(object sender, EventArgs e)
        {
            string cpuName;

            int ret = mc.Send_GetCPUName(out cpuName);
            this.refreshDisplay(ret, cpuName);
        }

        private void button_response_Click(object sender, EventArgs e)
        {
            using (var dlg = new Mtec.UtilityLibrary.Forms.InputBox())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // prompt ダイアログに表示される文字列
                    string sendingString = dlg.Value;

                    // 応答電文を格納する変数
                    string recievedString;

                    // 送信
                    int ret = mc.Send_ResponseTest(sendingString, out recievedString);

                    this.refreshDisplay(ret, recievedString);
                }
            }
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_RemotePause(false);
            this.refreshDisplay(ret, "");
        }

        private void button_test_Click(object sender, EventArgs e)
        {
        }

        private void button_errorReset_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_ErrorReset();
            this.refreshDisplay(ret, "");
        }

        private void button_ratchClear_Click(object sender, EventArgs e)
        {
            int ret = mc.Send_RatchClear();
            this.refreshDisplay(ret, "");
        }

        private void button_write_Click(object sender, EventArgs e)
        {
            var stringValList = this.textBox_outValue.Text.Split(',');

            int ret = -1;


            if (this.radioButton_16bit.Checked)
            {
                var shortList = new List<short>();

                short shVal = 0;

                shortList = (from str in stringValList
                             where short.TryParse(str, out shVal)
                             select shVal).ToList();

                if (shortList.Count == stringValList.Length)
                    ret = mc.SetDevice(this.textBox_deviceName.Text, shortList.ToArray());
            }
            else if (this.radioButton_32bit.Checked)
            {
                var intList = new List<int>();

                int iVal = 0;

                intList = (from str in stringValList
                           where int.TryParse(str, out iVal)
                           select iVal).ToList();

                if (intList.Count == stringValList.Length)
                    ret = mc.SetDevice(this.textBox_deviceName.Text, intList.ToArray());
            }
            else if (this.radioButton_float.Checked)
            {
                var floatList = new List<float>();

                float fVal = .0f;

                floatList = (from str in stringValList
                             where float.TryParse(str, out fVal)
                             select fVal).ToList();

                if (floatList.Count == stringValList.Length)
                    ret = mc.SetDevice(this.textBox_deviceName.Text, floatList.ToArray());
            }
            else if (this.radioButton_bool.Checked)
            {
                var bitList = new List<bool>();

                int iVal = 0;

                // 0 = false ; それ以外は全て true
                bitList = (from str in stringValList
                             where int.TryParse(str, out iVal)
                             select iVal != 0).ToList();

                if (bitList.Count == stringValList.Length)
                    ret = mc.SetDevice(this.textBox_deviceName.Text, bitList.ToArray());
            }

            this.refreshDisplay(ret, string.Join(",", stringValList));
        }

        private void button_read_Click(object sender, EventArgs e)
        {
            int ret = 0;

            string outValueString = "";

            int devCount = (int)this.numericUpDown_deviceCount.Value;


            if (this.radioButton_16bit.Checked)
            {
                short[] shData;


                // 18.01.30 時間計測
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();


                


                ret = mc.GetDevice(this.textBox_deviceName.Text, devCount, out shData);


                // 18.01.30 時間計測
                sw.Stop();
                Console.WriteLine("MC GetDevice: {0} mSec", sw.ElapsedMilliseconds);



                outValueString = shData == null ? "null" : string.Join(",", shData);
            }
            else if (this.radioButton_32bit.Checked)
            {
                int[] iData;

                ret = mc.GetDevice(this.textBox_deviceName.Text, devCount, out iData);

                outValueString = iData == null ? "null" : string.Join(",", iData);

            }
            else if (this.radioButton_float.Checked)
            {
                float[] fData;

                ret = mc.GetDevice(this.textBox_deviceName.Text, devCount, out fData);

                outValueString = fData == null ? "null" : string.Join(",", fData);
            }
            else if (this.radioButton_bool.Checked)
            {
                bool[] bData;

                ret = mc.GetDevice(this.textBox_deviceName.Text, devCount, out bData);

                outValueString = bData == null ? "null" : string.Join(",", (bData.Select(x => x ? "1" : "0")));
            }

            this.refreshDisplay(ret, outValueString);
        }

        private void button_bfm_write_Click(object sender, EventArgs e)
        {
            throw (new NotImplementedException());
        }

        private void button_bfm_read_Click(object sender, EventArgs e)
        {
            ushort[] readData;

            int ret = mc.ReadBuffer((int)this.numericUpDown_bfmNumber.Value, (int)this.numericUpDown_bfm_readCount.Value, out readData);


            string outValueString = readData == null ? "null" : string.Join(",", readData);

            this.refreshDisplay(ret, outValueString);
        }

        #endregion


        private void refreshDisplay(int returnValue, int outValue)
        {
            this.refreshDisplay(returnValue, outValue.ToString());
        }

        private void refreshDisplay(int returnValue, string outValue)
        {
            // 戻り値
            this.textBox_retValue.Text = returnValue.ToString("X");


            // エラーメッセージなど
            if (returnValue != 0)
            {
                string msg = ErrorMessage.GetErrorMessage(returnValue);

                if (msg != null)
                    this.textBox_message.Text = msg;
                else
                    this.textBox_message.Text = "登録エラーメッセージなし。マニュアルを参照。";
            }
            else
                this.textBox_message.Text = "";



            if (string.IsNullOrWhiteSpace(outValue))
                this.textBox_outValue.Text = "";
            else
                this.textBox_outValue.Text = outValue;

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LocalIP = this.textBox_LocalIP.Text;
            Properties.Settings.Default.LocalPort = (int)this.numericUpDown_LocalPort.Value;
            Properties.Settings.Default.RemoteIP = this.textBox_RemoteIP.Text;
            Properties.Settings.Default.RemotePort = (int)this.numericUpDown_RemotePort.Value;
            Properties.Settings.Default.Frame = (MCProtocolFrame)this.comboBox_frame.SelectedItem;
            Properties.Settings.Default.Protocol = (CommunicationProtocol)this.comboBox_protocol.SelectedItem;
            Properties.Settings.Default.Device = this.textBox_deviceName.Text;

            Properties.Settings.Default.Save();
        }



        #region region - 実験


        private byte[] set_monitor()
        {
            return new byte[]
            {
                // コマンド
                0x03,0x04,

                // サブコマンド
                0x00,0x00,

                // Wordアクセス数
                0x02, 0x00,

                // DoubleWord アクセス数 →常に0 でいいと思う 18.01.08
                0x00, 0x00,

                // 先頭アドレス BMF#1
                0x01,0x00,0x00,0x00,

                // ワード長さ
                0x01,0x00,
            };
        }

        private byte[] randomRead()
        {
            return new byte[]
            {
                // コマンド
                0x03,0x04,

                // サブコマンド
                0x00,0x00,

                // Wordアクセス数
                0x02,

                // DoubleWord アクセス数 →常に0 でいいと思う 18.01.08
                0x00,

                // n1 アドレス
                0x01,0x00,0x00,0xA8,

                // n2 アドレス
                0x05,0x00,0x00,0xA8,
            };
        }

        #endregion


        private void button_FileInfo_Click(object sender, EventArgs e)
        {
            byte[] recievedData;


            mc.SendRequestData(new byte[]
            {
                // コマンド
                0x11,0x18,

                // サブコマンド
                0x00,0x00,

                // 固定値
                0x20,0x20,0x20,0x20,
                
                // ドライブNo.
                0x00,0x00,
                
                // 先頭ファイルNo.
                0x00,0x00,
                
                // ファイル要求数
                0x07,0x00,

                // ファイル名
                0x41, 0x42, 0x43, 0x2E, 0x51, 0x50, 0x47,

            }, out recievedData);
        }
    }
}
