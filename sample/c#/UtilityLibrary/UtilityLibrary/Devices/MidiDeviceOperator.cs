using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibrary.Devices.MIDI
{
    public class MidiDeviceOperator : MidiDeviceOperatorBase
    {
        public MidiDeviceOperator()
        {
        }

        ~MidiDeviceOperator()
        {
            if (MidiDeviceOperatorBase.hMidiOutShort != 0)
                this.MidiShortOut_Close();
            if (MidiDeviceOperatorBase.hMidiIn != 0)
                this.MidiIn_Close();

            if (MidiDeviceOperatorBase.hMidiOutStream != 0)
                base.midiStream_Close();
        }


        #region region - privete member : timedriven


        //前回の再生停止位置を保持する
        private long _timedrivenLastTime;

        //再生位置を保持するストップウオッチ
        private System.Diagnostics.Stopwatch _timedrivenStopWatch = new System.Diagnostics.Stopwatch();

        //再生用スレッド
        //private System.Threading.Thread _timedrivenPlayingThread;


        private List<MidiTimedriven> _timedrivenBuff = new List<MidiTimedriven>();

        public TimedrivenPlayStatus _timedrivenStatus { get; private set; }

        #endregion


        #region region - event

        public event MidiInMessageRecievedEventHandler MidiInMessageRecieved = (obj, e) =>{};

        #endregion


        #region region public static properties

        protected static int Handle_MidiShortOut { get => MidiDeviceOperatorBase.hMidiOutShort; }

        protected static int Handle_MidiOut_Stream { get => MidiDeviceOperatorBase.hMidiOutStream; }

        protected static uint Handle_MidiIn { get => MidiDeviceOperatorBase.hMidiIn; }

        public static string[] OutDeviseNameList
        {
            get
            {
                // デバイス名を取得
                // 11 02 17 配列を返すように変更、静的メソッドに変更

                int len = midiOutGetNumDevs();        //デバイス数取得

                var dNameList = new List<string>();    //デバイス名称を入れていく


                for (uint i = 0; i < len; i++)
                {
                    // 19.05.03 ref からout へ変更。 ※このライブラリは C# 初めて間もなく作ったので、いろいろ修正が必要なはず
                    //var outCaps = new MIDIOUTCAPS();

                    //サイズ取得
                    uint capsSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MidiOutCaps));

                    MMRESULT res = midiOutGetDevCaps(i, out MidiOutCaps outCaps, capsSize);  //実行

                    dNameList.Add(outCaps.szPname);
                }

                return dNameList.ToArray(); //配列に変換して返す
            }
        }

        // 19.05.03 作成
        public static string[] InDeviseNameList
        {
            get
            {
                int len = midiInGetNumDevs();        //デバイス数取得
                var dNameList = new List<string>();    //デバイス名称を入れていく

                for (uint i = 0; i < len; i++)
                {
                    uint capsSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MidiInCaps));    //サイズ取得
                    MMRESULT res = midiInGetDevCaps(i, out MidiInCaps inCaps, capsSize);  //実行
                    dNameList.Add(inCaps.szPname);
                }

                return dNameList.ToArray(); //配列に変換して返す
            }
        }

        #endregion


        #region region public properties

        public bool IsMidiInOpen { get => MidiDeviceOperator.Handle_MidiIn != 0; }
        public bool IsMidiShortOutOpen { get => MidiDeviceOperator.Handle_MidiShortOut != 0; }

        #endregion


        #region region - public methods : Timedriven


        public Task Create_TimedrivenPlayTask()
        {
            // インスタンス生成
            this._timedrivenStopWatch = new System.Diagnostics.Stopwatch();

            // バッファの並び替え、グループ分け
            var table = from t in this._timedrivenBuff
                        where /* playFlags == null || playFlags.Exists(e => e == 0) ||
                        playFlags.FindIndex(x => x == t.EnableMark) != -1 && */
                        t.MiliSecond >= this._timedrivenLastTime
                        orderby t.MiliSecond
                        group t by t.MiliSecond;

            var task = new Task( () =>
            {
                #region region - 再生処理

                //状態を変更
                this._timedrivenStatus = TimedrivenPlayStatus.Playing;

                //ストップウォッチを開始する
                this._timedrivenStopWatch.Start();

                foreach (var t in table)
                {
                    uint timing = t.Key;

                    //メッセージループ
                    while (timing - this._timedrivenLastTime > this._timedrivenStopWatch.ElapsedMilliseconds)
                    {
                        if (this._timedrivenStatus == TimedrivenPlayStatus.Pause)
                        {
                            //注意！breakではない。→この先の処理は行われない。
                            return;
                        }
                        else if (this._timedrivenStatus == TimedrivenPlayStatus.Stop)
                        {
                            break;
                        }
                    }

                    foreach (var d in t)
                    {
                        // temporary   try を入れる？
                        var mret = this.SendMessage(d.Data);
                    }
                }


                //ストップウオッチをリセット
                this._timedrivenStopWatch.Reset();

                #endregion

            });

            return task;

        }


        public void Timedriven_Pause()
        {
            //オール・ノート・オフ
            this.MidiOutReset();

            //状態を変える
            this._timedrivenStatus = TimedrivenPlayStatus.Pause;

            //停止位置の記録
            this._timedrivenLastTime =
                this._timedrivenLastTime
                + this._timedrivenStopWatch.ElapsedMilliseconds;

            //ストップウオッチを初期化
            this._timedrivenStopWatch.Reset();

        }


        public void Timedriven_PauseAndBack(uint mSecond)
        {
            //オール・ノート・オフ
            this.MidiOutReset();

            //状態を変える
            this._timedrivenStatus = TimedrivenPlayStatus.Pause;

            //停止位置の記録
            this._timedrivenLastTime =
                this._timedrivenLastTime
                + this._timedrivenStopWatch.ElapsedMilliseconds - mSecond;

            //ストップウオッチを初期化
            this._timedrivenStopWatch.Reset();
        }


        public void Timedriven_PauseAndBackToTheTop()
        {
            //全チャンネルノートオフ
            this.MidiOutReset();

            //状態を変える
            this._timedrivenStatus = TimedrivenPlayStatus.Pause;
        }



        public void Timedriven_AddRange(IEnumerable<MidiTimedriven> timedrivenList)
        {
            this._timedrivenBuff.AddRange(timedrivenList);
        }
        public void Timedriven_Clear()
        {
            this._timedrivenBuff.Clear();
        }

        #endregion


        #region region - public Method - Open, Close

        public MMRESULT MidiShortOut_Open(int deviseID)
        {
            MidiDeviceOperatorBase.m_uDeviceID = deviseID;
            
            MMRESULT ret = midiOutOpen(ref hMidiOutShort, deviseID, 0, 0, 0);

            return ret;
        }


        public MMRESULT MidiShortOut_Close()
        {
            MMRESULT ret = midiOutClose(hMidiOutShort);

            if (ret == MMRESULT.MMSYSERR_NOERROR)
            {
                hMidiOutShort = 0;   //ハンドルの初期化
            }

            return ret;
        }

        // 19.05.03 追加
        public MMRESULT MidiIn_Open(uint deviseID)
        {
            // クラス内変数でデバイス番号を保持

            // 20.02.26 これはおかしい
            //MidiDeviceOperatorBase.hMidiIn = deviseID;

            // コールバック関数の指定
            this._midiInProc = this.midiInProcCallbackMethod;

            MMRESULT ret_open = midiInOpen(out uint handle, deviseID, this._midiInProc, IntPtr.Zero, MIDIFlags.CALLBACK_FUNCTION);


            if (ret_open == UtilityLibrary.Devices.MIDI.MMRESULT.MMSYSERR_NOERROR)
            {
                // ハンドルを保持
                MidiDeviceOperatorBase.hMidiIn = handle;

                var ret_start = midiInStart(MidiDeviceOperatorBase.hMidiIn);
            }

            return ret_open;
        }

        // 19.05.03 追加
        public MMRESULT MidiIn_Close()
        {
            // オールノートオフが必要？

            //MMRESULT ret_stop = midiInStop(MidiDeviceOperatorBase.hMidiIn);
            MMRESULT ret_close = midiInClose(MidiDeviceOperatorBase.hMidiIn);

            if (ret_close == MMRESULT.MMSYSERR_NOERROR)
            {
                MidiDeviceOperatorBase.hMidiIn = 0;   //ハンドルの初期化
            }

            return ret_close;
        }

        public MMRESULT MidiIn_Reset() => midiInReset(hMidiIn);
        public MMRESULT MidiIn_Start() => midiInStart(MidiDeviceOperatorBase.hMidiIn);

        #endregion


        #region region - public Method - General


        public MMRESULT Send_ProgramChange(uint ch, uint MSB)
        {
            return this.Send_ProgramChange(ch, MSB, 0);
        }

        public MMRESULT Send_ProgramChange(uint ch, uint MSB, uint LSB)
        {
            var dataBytes = ShortMsg.GetByteArray_ProgramChange(ch, MSB, LSB);

            return this.SendMessage(dataBytes);
        }

        public MMRESULT Send_PitchBendChange(uint ch, int value)
        {
            if (value > 8191 || value < -8192)
            {
                throw (new Exception(""));
            }

            uint LSB = (uint)(value + 8192) / 128;
            uint MSB = (uint)(value + 8192) % 128;


            var dataBytes = ShortMsg.GetByteArray_PitchBendChange(ch, MSB, LSB);

            return this.SendMessage(dataBytes);
        }

        #endregion


        #region region - public Method - RPN, NRPN


        public void Send_RegisteredParametaerNumber(uint MSB, uint LSB, uint DATA_MSB)
        {
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, MSB, ControlChange.RPN_MSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, LSB, ControlChange.RPN_LSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, DATA_MSB, ControlChange.DataEntery_MSB));

            //Null送信
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, 127, ControlChange.RPN_MSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, 127, ControlChange.RPN_LSB));
        }


        public void Send_RegisteredParametaerNumber(uint MSB, uint LSB, uint DATA_MSB, uint DATA_LSB)
        {
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, MSB, ControlChange.RPN_MSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, LSB, ControlChange.RPN_LSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, DATA_MSB, ControlChange.DataEntery_MSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, DATA_LSB, ControlChange.DataEntery_MSB));

            //Null送信
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, 127, ControlChange.RPN_MSB));
            SendMessage(ShortMsg.GetByteArray_ControlChange(0, 127, ControlChange.RPN_LSB));
        }


        public MMRESULT Send_MasterVolume(uint value)
        {
            var bytes = LongMsg.GetByteArray_MasterVolume(value);

            var ret = this.SendMessage(bytes);

            return ret;
        }


        public void Send_MasterFineTune(double hertz)
        {
            //Centを求める
            double cent = Math.Log(hertz / 440.0) / Math.Log(2) * 1200;

            //MIDI用の値に直す
            double value = cent * 8192.0 / 100.0;

            uint msb = (uint)(value / 128.0) + 64;    // ※注意 64 が０基点
            uint lsb = (uint)(value % 128.0);         //msbの余り

            //送信
            Send_RegisteredParametaerNumber(0, 1, msb, lsb);
        }


        public void Send_PitchBendRange(uint value)
        {
            // MSB 0, LSB 0
            this.Send_RegisteredParametaerNumber(0, 0, value);
        }

        /// <summary>
        /// 指定チャンネルのすべてのノートナンバーの音がOffされます。
        /// メーカーによって解釈が異なるため、期待した挙動でない可能性があります。
        /// </summary>
        public void Send_AllNoteOff(uint ch)
        {           
            //0xBx (x = チャンネル:0 - 15)	123 0

            SendMessage(ShortMsg.GetByteArray_ControlChange(ch, 0, ControlChange.AllNoteOff));
        }

        /// <summary>
        /// より強制力を持った All Note Off です。
        /// 1993年の改定でMIDI規格に追加された。
        /// </summary>
        public void Send_AllSoundOff(uint ch)
        {
            //0xBx (x = チャンネル:0 - 15)	120 0
            SendMessage(ShortMsg.GetByteArray_ControlChange(ch, 0, ControlChange.AllSoundOff));
        }



        //??? 13.06.18なんだこれ
        public void Send_FineTune(uint value)
        {
            this.Send_RegisteredParametaerNumber(0, 2, value);
        }


        #endregion


        protected override void midiInProcCallbackMethod(IntPtr hMidiIn, MIDIMessage wMsg, IntPtr dwInstance, uint dwMidiMessage, uint dwTimestamp)
        {           
            var byteArray = BitConverter.GetBytes(dwMidiMessage);

            uint ch = (uint)(byteArray[0] % 0x10);

            // 参考 https://docs.microsoft.com/ja-jp/windows/desktop/Multimedia/mim-data
            // 第4バイト Not used.

            this.MidiInMessageRecieved.Invoke(this, new MidiInMessageRecievedEventArgs
            {
                MIDIMessage = wMsg,
                ReciedvedData = byteArray.Skip(1).Take(2).ToArray(),
                StatusByte = (StatusByte)(byteArray[0] - ch),
                Ch =  ch,
            });
        }
    }


    #region region - class, enum

    
    public class MidiTimedriven
    {
        /// <summary>
        /// データそのもの
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// 秒数 ※再生開始からの総秒数(ミリ)で入力
        /// </summary>
        public uint MiliSecond;

        /// <summary>
        /// 詳細内容 ※後で見やすくする為だけのもので、機能的には無意味
        /// </summary>
        public string Detail { get; set; }


        public int EnableMark { get; set; }

    }


    public enum TimedrivenPlayStatus : uint
    {
        Stop,
        Playing,
        Pause,
    }


    #endregion


    #region region - delegate


    public delegate void MidiInMessageRecievedEventHandler(object sender, MidiInMessageRecievedEventArgs e);


    public class MidiInMessageRecievedEventArgs : EventArgs
    {
        public MIDIMessage MIDIMessage { get; set; }
        public byte[] ReciedvedData { get; set; }
        public StatusByte StatusByte { get; set; }
        public uint Ch { get; set; }
    }

    #endregion

}
