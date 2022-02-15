using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityLibrary.Devices.MIDI
{
    public class MidiDeviceOperatorBase //: IDisposable
    {
        public MidiDeviceOperatorBase()
        {
            this._semaphore_sendMsg = new System.Threading.SemaphoreSlim(1, 1);
        }


        #region region - protected member


        protected static int m_uDeviceID;        //デバイスＩＤ

        //protected static MidiDelegate callbackMethods; // 19.05.03 未使用だったらしい

        protected static int hMidiOutShort;    // MIDI 出力デバイスのハンドル[単発用]
        protected static int hMidiOutStream;  // MIDI 出力デバイスのハンドル[ストリーム用]

        // 19.05.03 追加
        protected static uint hMidiIn;        // MIDI 入力デバイスのハンドル

        // 20.05.05 追加
        protected System.Threading.SemaphoreSlim _semaphore_sendMsg;


        // internalな理由：protected なdelegate が 名前空間に直接記述できない
        internal MidiOutProc _midiOutProc;  //MIDI Stream 用のコールバック関数を保持する

        internal MidiInProc _midiInProc;  //MIDI In 用のコールバック関数を保持する


        #endregion        


        #region DLL import : General

        //[System.Runtime.InteropServices.DllImport("winmm.dll", EntryPoint = "midiOutLongMsg")]←EntryPointの指定方法
        [System.Runtime.InteropServices.DllImport("winmm.dll")]             //midiの出力ポートのオープン
        protected static extern MMRESULT midiOutOpen(ref int lphMidiOutShort, int uDeviceID, int dwCallback, int dwInstance, int dwFlags);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]             //midiの出力ポートのクローズ
        protected static extern MMRESULT midiOutClose(int hMidiOutShort);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]             //4byte ショートメッセージの送信
        protected static extern MMRESULT midiOutShortMsg(int hMidiOutShort, UInt32 dwMsg);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]             //デバイス数を得る
        protected static extern int midiOutGetNumDevs();

        //[DllImport("Winmm.dll", CharSet = CharSet.Auto)] ←こいつが悪さしていた。意味を知りたい。10 10 31
        [System.Runtime.InteropServices.DllImport("Winmm.dll")]
        protected static extern MMRESULT midiOutGetDevCaps(uint uDeviceID, out MidiOutCaps lpMidiOutCaps, uint cbMidiOutCaps);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//全チャンネルノートオフ
        protected static extern MMRESULT midiOutReset(Int32 hMidiOutShort);



        #endregion


        #region region - 19.05.03 追加部分[MIDI in]

        // 参考 http://program.chrofieyue.net/midiin/createport.htm

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern MMRESULT midiInStart(uint hMidiIn);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern MMRESULT midiInStop(uint hMidiIn);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern MMRESULT midiInReset(uint hMidiIn);


        //デバイス数を得る
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern int midiInGetNumDevs();


        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern MMRESULT midiInGetDevCaps(uint uDeviceID, out MidiInCaps lpMidiInCaps, uint cbMidiInCaps);


        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        protected static extern MMRESULT midiInClose(uint hMidiIn);
        

        [System.Runtime.InteropServices.DllImport("winmm.dll", SetLastError = true)]
        // dwFlags : このパラメータに CALLBACK_FUNCTION を指定する場合は、MIM_DATA メッセージとともに MIM_MOREDATA メッセージもコールバックされる
        protected static extern MMRESULT midiInOpen(out uint lphMidiIn, uint uDeviceID, MidiInProc dwCallback, IntPtr dwInstance, MIDIFlags dwFlags);

        // 19.05.04 参考資料
        //private static extern MMRESULT midiInOpen(out IntPtr lphMidiIn, uint uDeviceID, IntPtr dwCallback, IntPtr dwInstance, uint dwFlags);
        //private static extern MMRESULT midiInOpen(out uint lphMidiIn, uint uDeviceID, uint dwCallback, uint dwInstance, uint dwFlags);

        #endregion


        #region DLL import: LongMsg 

        [System.Runtime.InteropServices.DllImport("winmm.dll")]     //バッファ用意。MMRESULT
        private static extern MMRESULT midiOutPrepareHeader(int hMidiOutShort, ref MIDIHDR lpMidiOutHdr, uint uSize);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]     //8byte ロングメッセージの送信。
        private static extern MMRESULT midiOutLongMsg(int hMidiOutShort, ref MIDIHDR lpMidiOutHdr, uint uSize);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]     //バッファ開放。
        private static extern MMRESULT midiOutUnprepareHeader(Int32 hMidiOutShort, ref MIDIHDR lpMidiOutHdr, uint cbMidiOutHdr);

        #endregion


        #region DLL import: MIDI Stream


        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        //private static extern MMRESULT midiStreamOpen(ref int handle, ref int deviceID, int reserved, MidiOutProc proc, int instance, uint flag);
        // ↓19.05.05 修正
        private static extern MMRESULT midiStreamOpen(ref int handle, ref int deviceID, int reserved, MidiOutProc proc, int instance, MIDIFlags flag);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//MMRESULT midiStreamOut   
        private static extern MMRESULT midiStreamOut(int hMidiOutStream, ref MIDIHDR lpMidiHdr, uint cbMidiHdr);
        //第二引数はref じゃないと 11が返る。（エラー）

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//再生
        private static extern MMRESULT midiStreamRestart(int hms);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern MMRESULT midiStreamProperty(int hStream, ref MIDIPROPTIMEDIV lppropdata, uint dwProperty);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern MMRESULT midiStreamProperty(int hStream, ref MIDIPROPTEMPO lppropdata, uint dwProperty);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern MMRESULT midiStreamPosition(int hStream, ref MMTIME pmmt, uint cbmmt);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern MMRESULT midiStreamClose(int hStream);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//一時停止
        private static extern MMRESULT midiStreamPause(int hStream);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//resetの事か？
        private static extern MMRESULT midiStreamStop(int hStream);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]//再生
        private static extern MMRESULT midiStreamReset(int hms);

        #endregion


        #region DLL import : Timer

        //マルチメディアタイマー
        [System.Runtime.InteropServices.DllImport("winmm.dll", EntryPoint = "timeSetEvent")]//MMRESULT
        private static extern int timeGetDevCaps(ref TIMECAPS ptc, uint cbtc);

        private struct TIMECAPS //
        {
            uint wPeriodMin;//通常1      0しか返ってこない 10 11 20      
            uint wPeriodMax;//通常65535  0しか返ってこない 10 11 20
        }

        #endregion



        //dwEventに代入するフラグ　※ショートはフラグ＋イベントデータ。　
        #region const :MEVT

        protected const uint MEVT_F_SHORT = 0x00000000;   //イベントはショートイベントです
        protected const uint MEVT_F_CALLBACK = 0x40000000;   //イベントが実行されようとしているときのコールバック
        protected const uint MEVT_F_LONG = 0x80000000;   //イベントはロングイベントです

        // イベントコード
        protected const uint MEVT_SHORTMSG = 0x00;
        protected const uint MEVT_NOP = 0x02;         //何もしない時に使う？コールバックは有効
        protected const uint MEVT_TEMPO = 0x01;
        protected const uint MEVT_LONGMSG = 0x80;
        protected const uint MEVT_COMMENT = 0x82;         //ストリームデータをストリーム形式でファイルに格納しようとした場合に、このイベントは記録情報を格納するのに用いられることになります。
        protected const uint MEVT_VERSION = 0x84;

        #endregion

        #region const MOM

        /*
        protected const int MOM_OPEN = 0x3C7;
        protected const int MOM_CLOSE = 0x3C8;
        protected const int MOM_DONE = 0x3C9;    //バッファが再生された
        protected const int MM_MOM_POSITIONCB = 0x3CA;
        */
        #endregion


        // MMTIME構造体を使うときセットするフラグ 
        // ※但しフラグ通りに取得できたためしがない。→ 常に mSecond
        #region - const: TIME

        public const uint TIME_MS = 0x0001;   //time in milliseconds
        public const uint TIME_SAMPLES = 0x0002;   //number of wave samples
        public const uint TIME_BYTES = 0x0004;   //current byte offset
        public const uint TIME_SMPTE = 0x0008;   //SMPTE time
        public const uint TIME_MIDI = 0x0010;   //MIDI time
        public const uint TIME_TICKS = 0x0020;   //Ticks within MIDI stream

        #endregion

        /*

        
            */

        //この２つの構造体と共に使うフラグ
        //上の２つと下の２つを合わせて用いるので、フラグのパターンは２×２の４通り。
        //ただし、テンポのSETは結局これではできないらしいので、結果３パターンとなる。
        #region - const : MIDIPROP

        protected const uint MIDIPROP_SET = 0x80000000;
        protected const uint MIDIPROP_GET = 0x40000000;
        protected const uint MIDIPROP_TIMEDIV = 1;
        protected const uint MIDIPROP_TEMPO = 2;

        #endregion


        #region region - private methods


        //コールバックを受け取る為のコールバックメソッド
        private void HandleMessage(int handle, int msg, int instance, int param1, int param2)
        {
            //BaseClass.callbackMethods(msg);   //発生イベントのコード番号を送る。
        }


        // MIDI IN のコールバックを受け取るメソッド 19.05.03 追加
        // 参考　https://codeday.me/jp/qa/20190309/385356.html

        protected virtual void midiInProcCallbackMethod(IntPtr hMidiIn, MIDIMessage wMsg, IntPtr dwInstance, uint dwMidiMessage, uint dwTimestamp)
        //private void midiInMessageRecieved(int hMidiIn, int wMsg, int dwInstance, int dwParam1, int dwParam2)
        {
        }


        #endregion


        #region region -MIDISTREAM 構造体を返すメソッド

        protected MIDIHDR MIDIEVENT_to_MIDIHDR(MIDIEVENT mEvent)
        {
            //単発ショートメッセージでは未使用

            Func<int> nParamSelect_func = () =>
            {
                if (mEvent.dwParam == null) { return 0; }
                else { return mEvent.dwParam.Length; }
            };


            //ローカル変数
            int nParam = nParamSelect_func();

            int evtSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIEVENT));//構造体のデータ量
            int pSize = ((nParam - 1) * sizeof(uint)) + evtSize;


            uint hdrSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIHDR));   //構造体のデータ量
            byte[] bytes = new byte[pSize];


            #region region - ローカルのラムダ式

            Func<int> BytesRecorded_func = () =>
            {
                return pSize;
                //if (m_pMidiHdr == null) { return evtSize; }
                //else if (m_pMidiHdr.Count == 0) { return evtSize; }

                //else
                //{
                //    return
                //        m_pMidiHdr[m_pMidiHdr.Count - 1].dwBytesRecorded + evtSize;
                //}
            };

            #endregion


            //構造体をバイト配列に変換
            IntPtr ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(pSize);    //メモリ確保？
            System.Runtime.InteropServices.Marshal.StructureToPtr(mEvent, ptr, false);
            System.Runtime.InteropServices.Marshal.Copy(ptr, bytes, 0, pSize);
            var gch = System.Runtime.InteropServices.GCHandle.Alloc(bytes, System.Runtime.InteropServices.GCHandleType.Pinned);


            //戻り値の設定
            MIDIHDR hdr = new MIDIHDR
            {
                dwFlags = 0,    //MIDIStreamの規格で0に設定する必要がある。

                dwReserved = new uint[4],   //配列の初期化
                //dwReserved = new uint[MIDIHDR.dwReservedSize],   //配列の初期化
                //dwReserved = new IntPtr[4],   //配列の初期化


                lpData = gch.AddrOfPinnedObject(),
                dwBufferLength = (uint)bytes.Length,
                dwBytesRecorded = (uint)BytesRecorded_func(),
            };
            
            gch.Free(); //ガベージ・ハンドルの開放

            return hdr;
        }
        
        #endregion


        #region region -MIDISTREAM オープン・クローズ関連メソッド

        protected MMRESULT midiStream_Open(int deviceNumber)
        {
            this._midiOutProc = this.HandleMessage;
            m_uDeviceID = deviceNumber;


            MMRESULT ret = midiStreamOpen(ref hMidiOutStream,
                ref m_uDeviceID, 1, this._midiOutProc, 0, MIDIFlags.CALLBACK_FUNCTION);

            return ret;
        }

        protected MMRESULT midiStream_Close()
        {
            MMRESULT ret = midiStreamClose(hMidiOutStream);
            return ret;
        }

        #endregion


        #region region -MIDISTREAMプロセス関連メソッド

        protected MMRESULT midiOutPrepareHeader(ref MIDIHDR hdr)  //ヘッダ（バッファ？）の準備
        {
            Func<int> handle_func = () =>
            {
                if (hMidiOutShort != 0) { return hMidiOutShort; }
                if (hMidiOutStream != 0) { return hMidiOutStream; }

                else
                {   //コールバックでエラーを返そう。これは暫定処理 11 02 15 
                    return 0;
                }
            };
            
            uint uSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIHDR)); //構造体のデータ量

            MMRESULT ret = midiOutPrepareHeader(handle_func(), ref hdr, uSize);
            return ret;
        }

        protected MMRESULT midiStreamOut(ref MIDIHDR hdr)
        {
            uint hdrSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIHDR));   //構造体のデータ量
            MMRESULT ret = midiStreamOut(hMidiOutStream, ref hdr, hdrSize);

            return ret;
        }

        #endregion


        #region region -MIDISTREAM 再生・停止関連メソッド

        public MMRESULT MidiStreamRestart()
        {
            MMRESULT ret;//戻り値の代入
            ret = midiStreamRestart(hMidiOutStream);
            return ret;
        }

        public MMRESULT MidiStreamReset()
        {
            MMRESULT ret;//戻り値の代入
            ret = midiStreamReset(hMidiOutStream);
            return ret;
        }

        /// <summary>
        /// チャンネルノートオフ
        /// </summary>
        /// <returns>処理結果</returns>
        public MMRESULT MidiStreamStop()//全チャンネルノートオフ
        {
            MMRESULT ret = midiStreamStop(hMidiOutShort);
            return ret;
        }

        /// <summary>
        /// 全チャンネルノートオフ
        /// </summary>
        public MMRESULT MidiOutReset()
        {
            MMRESULT ret = midiOutReset(hMidiOutShort);
            return ret;
        }

        public MMRESULT MidiStreamPause()
        {
            MMRESULT ret = midiStreamPause(hMidiOutStream);
            return ret;
        }

        public MMTIME MidiStreamPosition(uint wTypeFlag)
        {
            //現在再生している位置を取得する
            // mm しか取得できない？ 11 02 07
            //wTypeFlag 結局１しか指定できない。
            //構造体があってるかどうかすら把握できない。

            MMTIME mmt = new MMTIME { wType = wTypeFlag };


            uint mmtSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MMTIME)); //構造体のデータ量

            MMRESULT ret = midiStreamPosition(hMidiOutStream, ref mmt, mmtSize);

            return mmt;
        }

        #endregion


        #region region - MIDIPROP に関するメソッド
        //------------------------------------------------------------
        public MMRESULT Property_SetTime(uint TimeDivVal)
        {
            //分解能の設定
            uint flag = MIDIPROP_TIMEDIV | MIDIPROP_SET;
            MIDIPROPTIMEDIV proptime = new MIDIPROPTIMEDIV();

            proptime.cbStruct = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIPROPTIMEDIV));
            proptime.dwTimeDiv = TimeDivVal;

            MMRESULT ret = midiStreamProperty(hMidiOutStream, ref proptime, flag);
            return ret;
        }
        //------------------------------------------------------------
        public uint Property_GetTime()
        {
            MMRESULT ret;
            uint flag = MIDIPROP_TIMEDIV | MIDIPROP_GET;

            MIDIPROPTIMEDIV proptime = new MIDIPROPTIMEDIV();
            proptime.cbStruct = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIPROPTIMEDIV));

            ret = midiStreamProperty(hMidiOutStream, ref proptime, flag);
            return proptime.dwTimeDiv;
        }
        //------------------------------------------------------------
        public double Property_GetTempo()
        {
            uint flag = MIDIPROP_GET | MIDIPROP_TEMPO;

            MIDIPROPTEMPO proptime = new MIDIPROPTEMPO();
            proptime.cbStruct = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIPROPTEMPO));

            MMRESULT ret = midiStreamProperty(hMidiOutStream, ref proptime, flag);

            if (!(proptime.dwTempo == 0)) { return 60000000 / proptime.dwTempo; }
            else { return 0; }
        }
        //------------------------------------------------------------
        #endregion


        #region region - public Method - General


        /// <summary>
        /// 常用データ送信メソッド。※ロングメッセージもショートメッセージも処理可能です。
        /// </summary>
        public MMRESULT SendMessage(byte[] data)
        {
            if (data.Length > 64 * 1024)   //64kbを超えるデータの時 ※ありえないとは思うが、MIDIの仕様上。
            {
                throw new InvalidOperationException();
            }
            else if (data.Length == 0)
            {
                return MMRESULT.MMSYSERR_ERROR;
            }
            else if (data.Length <= 4)
            {
                uint msg = BitConverter.ToUInt32(data, 0);

                this._semaphore_sendMsg.Wait();

                MMRESULT mret = midiOutShortMsg(hMidiOutShort, msg);

                this._semaphore_sendMsg.Release();

                return mret;
            }
            else
            {
                uint hdrSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIHDR));
                MIDIHDR hdr = LongMsg.Get_SystemExclusiveHDR(data);


                MMRESULT ret = midiOutPrepareHeader(ref hdr);           //データ変換

                this._semaphore_sendMsg.Wait();

                MMRESULT mret = midiOutLongMsg(hMidiOutShort, ref hdr, hdrSize); //送信

                this._semaphore_sendMsg.Release();

                return mret;
            }
        }


        /* 19.05.05 もう使っていないらしい
        /// <summary>
        /// midiOutShortMsgのラッパー的メソッド。
        /// </summary>
        public MMRESULT SendShortMsg(uint msg)
        {
            MMRESULT ret = midiOutShortMsg(hMidiOutShort, msg);
            return ret;
        }
        */

        #endregion

    }



    /// <summary>
    /// 静的メンバのみで構成
    /// </summary>
    public class ShortMsg
    {
        //privateコンストラクタ ※インスタンス生成できなくする
        private ShortMsg()
        {
        }


        #region - noteOff           [0x80]

        /*
        public static byte[] GetByteArray_NoteOff(uint Ch, uint noteNum)
        {
            //uintを取得
            uint msg = NoteOffMsg(Ch, noteNum, 0);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }*/

        public static byte[] GetByteArray_NoteOff(uint Ch, uint noteNum, uint offVelocity =0)
        {
            //uintを取得
            uint msg = NoteOffMsg(Ch, noteNum, offVelocity);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }

        /* 20.05.08 初期値付き引数メソッドにしたので不要に
        /// <summary>
        /// offVelocity省略時のオーバーロード
        /// </summary>
        public static uint NoteOffMsg(uint Ch, uint noteNum)
        {
            return ShortMsg.NoteOffMsg(Ch, noteNum, 0);
        }
        */

        public static uint NoteOffMsg(uint Ch, uint noteNum, uint offVelocity = 0)
        {
            noteNum *= 0x100;   //第１データバイト
            offVelocity *= 0x10000; //第２データバイト

            uint msg = offVelocity | noteNum | (uint)StatusByte.NoteOff | Ch;

            return msg;

        }//void noteOff

        #endregion

        #region - noteOn            [0x90]

        public static uint NoteOnMsg(uint Ch, uint noteNum, uint velocity)
        {
            /*
            noteNum *= 0x100;   //第１データバイト
            velocity *= 0x10000;//第２データバイト
            uint msg = velocity | noteNum | (uint)StatusByte.NoteOn | Ch;
            */
            // ↓19.05.05 修正
            uint uNoteNum = noteNum     << 8;   //第１データバイト
            uint uVelocity = velocity   << 16;  //第２データバイト
            uint msg = uVelocity | uNoteNum | (uint)StatusByte.NoteOn | Ch;

            
            return msg;
        }

        public static byte[] GetByteArray_NoteOn(uint Ch, uint noteNum, uint velocity)
        {
            //uintを取得
            uint msg = NoteOnMsg(Ch, noteNum, velocity);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }

        #endregion

        #region - ControlChange     [0xB0]

        public static byte[] GetByteArray_ControlChange(uint Ch, uint data, ControlChange CC)
        {
            uint msg = ShortMsg.ControlChangeMsg(Ch, data, CC);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }

        public static uint ControlChangeMsg(uint Ch, uint data, ControlChange controlChange)
        {
            //第1データバイトがコントロールチェンジ
            //第2データバイトがコントロールの値

            uint statusByte = 0xB0 | Ch;
            uint firstDataByte = ((uint)controlChange) << 8;
            uint secondDataByte = data << 16;
            
            return secondDataByte | firstDataByte | statusByte;
        }

        #endregion

        #region - ProgramChange     [0xC0]

        public static byte[] GetByteArray_ProgramChange(uint Ch, uint ProgramNum)
        {
            uint msg = ShortMsg.ProgramChange(Ch, ProgramNum);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }
        public static byte[] GetByteArray_ProgramChange(uint Ch, uint MSB, uint LSB)
        {
            uint msg = ShortMsg.ProgramChange(Ch, MSB, LSB);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }

        public static uint ProgramChange(uint Ch, uint ProgramNum)
        {
            ProgramNum *= 0x100;   //第１データバイト
            return ProgramNum | (uint)StatusByte.ProgramChange | Ch;
        }
        /// <summary>
        /// ※動作未確認
        /// </summary>
        public static uint ProgramChange(uint Ch, uint MSB, uint LSB)
        {
            MSB *= 0x100;   //第１データバイト
            LSB *= 0x10000; //第２データバイト

            return LSB | MSB | (uint)StatusByte.ProgramChange | Ch;
        }

        #endregion

        #region - PitchBendChange   [0xE0]      //※結局 MSB LSB 両方に値を入れないと動かないらしい

        public static byte[] GetByteArray_PitchBendChange(uint Ch, uint MSB, uint LSB)
        {
            uint msg = ShortMsg.PitchBendChange(Ch, MSB, LSB);

            //バイト配列に変換
            byte[] ret = BitConverter.GetBytes(msg);

            return ret;
        }

        public static uint PitchBendChange(uint Ch, uint MSB, uint LSB)
        {
            MSB *= 0x100;   //第１データバイト
            LSB *= 0x10000; //第２データバイト
            uint statusByte = 0xE0 | Ch;

            return LSB | MSB | statusByte;
        }
        #endregion

    }//Class - ShortMsg


    /// <summary>
    /// 静的メンバのみで構成
    /// </summary>
    public class LongMsg
    {
        //private コンストラクタ（インスタンス生成はできない）
        private LongMsg()
        {
        }


        #region region - プロパティ(メソッド)

        // XG 音源で実験する
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partMode">
        /// 0	normal part
        /// 1	DRUM(edit unalbe)
        /// 2	DRUM Setup1
        /// 3	DRUM Setup2
        /// 4	DRUM Setup3
        /// 5	DRUM Setup4
        /// </param>
        /// <remarks>XG only</remarks>
        public static byte[] GetByteArray_DRUMSetup(uint ch, uint partMode)
        { return new byte[] { 0xF0, 0x43, 0x10, 0x4C, 0x08, (byte)ch, 0x07, (byte)partMode, 0xF7 }; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch">0-15</param>
        /// <param name="partMode">0-2</param>
        /// <remarks>GS only</remarks>
        public static byte[] GetByteArray_UseForRhythmPart(uint ch, uint partMode)
        { return new byte[] { 0xF0, 0x41, 0x10, 0x42, 0x12, 0x40, (byte)(0x10 + ch + 1), 0x15, (byte)partMode, (byte)partMode, 0xF7 }; }

        public static byte[] GetByteArray_XGSystemOn
        { get { return new byte[] { 0xF0, 0x43, 0x10, 0x4C, 0x00, 0x00, 0x7E, 0x00, 0xF7 }; } }

        public static byte[] GetByteArray_GMSystemOn
        { get { return new byte[] { 0x00, 0x7E, 0x7F, 0x09, 0x01, 0xF7 }; } }

        public static byte[] GetByteArray_GSReset
        { get { return new byte[] { 0xF0, 0x41, 0x10, 0x42, 0x12, 0x40, 0x00, 0x7F, 0x00, 0x41, 0xF7 }; } }

        public static byte[] GetByteArray_MasterVolume(uint MSB)
        {
            //基本的にはMSBしか使わないらしいので、そのオーバーロード
            //return LongMsg.getByteArray_MasterVolume(MSB, 0);

            byte deviceID = 0x7F; //全デバイス
            return new byte[] { 0xF0, 0x7F, deviceID, 0x04, 0x01, 0, (byte)MSB, 0xF7 };
        }

        public static byte[] GetByteArray_MasterVolume(uint MSB, uint LSB)
        {
            byte deviceID = 0x7F; //全デバイス
            return new byte[] { 0xF0, 0x7F, deviceID, 0x04, 0x01, (byte)LSB, (byte)MSB, 0xF7 };
        }

        #endregion

        // MIDIEVENT がうまく行かなかった為暫定的に作る
        #region region - MIDIHDR を返す静的ファンクション群

        public static MIDIHDR Get_SystemExclusiveHDR(byte[] data)
        {
            if (data.Length > 64 + 1024)
            { throw new InvalidOperationException(); }  //もしくはコールバック

            //ローカル変数
            uint hdrSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MIDIHDR));
            MIDIHDR hdr = new MIDIHDR();

            hdr.dwReserved = new uint[4];

            System.Runtime.InteropServices.GCHandle dataHandle = System.Runtime.InteropServices.GCHandle.Alloc(data, System.Runtime.InteropServices.GCHandleType.Pinned);

            try
            {
                hdr.lpData = dataHandle.AddrOfPinnedObject();
                hdr.dwBufferLength = (uint)data.Length;
                hdr.dwFlags = 0;

            }
            finally
            {
                dataHandle.Free();
            }

            return hdr;
        }

        #endregion

    }//Class - LongMsg


    #region region - class

    /// <summary>  
    /// MIDI APIで使用する定数です。  
    /// </summary>  
    public static class MidiPortConst
    {
        /// <summary>  
        /// TCHARで数えた時の文字数です。  
        /// </summary>  
        public const int MaxPNameLen = 32;
    }

    #endregion


    #region region - struct

    public struct MIDIPROPTIMEDIV  //分解能設定時に使用
    {
        public uint cbStruct;   //DWORD
        public uint dwTimeDiv;  //DWORD
    };

    public struct MIDIPROPTEMPO    //名前は違うがMIDIPROPTIMEDIV とまったく同じ構造体
    {
        public uint cbStruct;
        public uint dwTempo;
    }


    //MIDI 出力デバイスの能力についての情報を記述。
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MidiOutCaps
    {
        /// <summary>  
        /// MIDIハードウェアのメーカーIDです。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)] // ※U2 ushort の事
        public ushort wMid;
        /// <summary>  
        /// Product IDです。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)] // ※U2 ushort の事
        public ushort wPid;
        /// <summary>  
        /// ドライバーのバージョンです。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)] // ※U4 4byte 符号なし整数 → uint の事
        public uint vDriverVersion;

        /// <summary>  
        /// ポートの名前です。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MidiPortConst.MaxPNameLen)]
        public string szPname;

        /// <summary>  
        /// wTechnology値です。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public MidiModuleType wTechnology;
        
        /// <summary>  
        /// 最大ボイス数を取得します。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort wVoices;
        
        /// <summary>  
        /// 最大同時発音数を取得します。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort wNotes;
        
        /// <summary>  
        /// チャンネルマスクを取得します。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort wChannelMask;

        /// <summary>  
        /// dwSupport値です。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public MidiPortCapability dwSupport;

        /// <summary>  
        /// チャンネルマスクを取得します。  
        /// </summary>  
        public bool[] GetChannelMask()
        {
            bool[] mask = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                mask[i] = (wChannelMask & (1 << i)) != 0;
            }
            return mask;
        }

        /// <summary>  
        /// チャンネルマスクを設定します。  
        /// </summary>  
        public void SetChannelMask(byte ch, bool value)
        {
            wChannelMask &= (ushort)(0xFFFF - 1 << ch);
            wChannelMask |= value ? (ushort)(1 << ch) : (ushort)0;
        }

        /// <summary>  
        /// デバイスドライバのメジャーバージョンを取得します。  
        /// </summary>  
        public byte MajorVersion
        {
            get { return (byte)(vDriverVersion >> 8); }
        }

        /// <summary>  
        /// デバイスドライバのマイナーバージョンを取得します。  
        /// </summary>  
        public byte MinorVersion
        {
            get { return (byte)(vDriverVersion & 0xFF); }
        }
    }

    /// <summary>  
    /// MIDI入力ポートの情報を表します。  
    /// </summary>  
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct MidiInCaps
    {
        /// <summary>  
        /// MIDIハードウェアのメーカーIDです。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort wMid;
        /// <summary>  
        /// Product IDです。  
        /// </summary>  

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U2)]
        public ushort wPid;
        
        /// <summary>  
        /// ドライバーのバージョンです。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint vDriverVersion;

        /// <summary>  
        /// ポートの名前です。  
        /// </summary>  
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MidiPortConst.MaxPNameLen)]
        public string szPname;

        /// <summary>  
        /// デバイスドライバのメジャーバージョンを取得します。  
        /// </summary>  
        public byte MajorVersion
        {
            get { return (byte)(vDriverVersion >> 8); }
        }

        /// <summary>  
        /// デバイスドライバのマイナーバージョンを取得します。  
        /// </summary>  
        public byte MinorVersion
        {
            get { return (byte)(vDriverVersion & 0xFF); }
        }
    }


    //暫定処理 一時的にpublicにする
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MIDIHDR
    //ロングメッセージ、またはMIDIストリームのバッファを識別するために使われるヘッダ。
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.SysInt)]
        public IntPtr lpData;           // offset  0- 3  MIDIデータアドレス   LPSTR

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]   //U4 → 4Byte Uint
        public uint dwBufferLength;     //バッファサイズ       DWORD

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint dwBytesRecorded;    //実際のデータサイズ   DWORD
        //英語版MSDNではdwBufferLengthよりも少ないか、等しくあるべきと記載

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint dwUser;             //カスタムユーザデータ DWORD

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public MidiHdrFlag dwFlags;     //フラグ MidiHdrFlag?  DWORD

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.SysInt)]//予約(NULL)使わない
        public IntPtr lpNext;           //11 21変更　どっちだ？


        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint reserved;           //予約(0)使わない     DWORD
        //[System.Runtime.InteropServices.MarshalAs(UnmanagedType.SysInt)]
        //public IntPtr reserved;         //10 11 21 IntPtrという情報あり


        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U4)]
        public uint dwOffset;           //バッファのオフセット DWORD
        //MEVT_F_CALLBACKがdwEvent(MIDIEVENT構造体)に実装されているとき？

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] dwReserved;           //11 21 変更 ※MSDNでは4だが？8という情報あり
        //public IntPtr[] dwReserved;       //IntPtr[] になっている情報もある

        //public static int dwReservedSize { get { return 4; } }
    }
    //※IntPtr ポインタまたはハンドルを表すときに使用されるプラットフォーム固有の型。
    //アセンブリ:  mscorlib (mscorlib.dll 内) ビット選択型int？ハンドルも保持できる。
    //------------------------------------------------------------


    //何につかうのか・・・？
    public struct MIDISTRMBUFFVER
    {
        public uint dwVersion;
        public uint dwMid;
        public uint dwOEMVersion;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct MIDIEVENT  //MIDIストリームで使用する構造体 ショートメッセージ用
    {
        public uint dwDeltaTime;   // 直前のイベントからの経過時間   DWORD ※DWORDはulongらしい
        public uint dwStreamID;    // 予約パラメータ。必ず０   DWORD
        public uint dwEvent;       // ショートメッセージを格納  DWORD

        //[System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public uint[] dwParam;     // ロングメッセージで使う


        //コメントアウト
        //public string eventHexStr
        //{ get { return Convert.ToString(dwEvent,16); } }

        //public static int dwEventSize
        //{ get { return 1; } }
    }

    public struct SMPTE    //SMPTE smpte
    {
        public uint hour;    //BYTE →uintで代用が利きそう
        public uint min;
        public uint sec;
        public uint frame;
        public uint fps;
        public uint dummy;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2)]//予約 DWORD
        public uint[] pad;//要素数２
    }

    //------------------------------------------------------------
    //自作版 MMTIME ※mSecond以外で取得できない 11 02 13
    //------------------------------------------------------------
    public struct MMTIME
    {
        public uint wType;      //DWORD → ulong ？
        public uint ms;

        public uint sample;
        public uint cb;
        public uint ticks;
        public SMPTE smpte;
        public uint songptrpos;
    }




    #endregion


    #region region - enum


    public enum ControlChange : uint
    {
        //第１データバイト
        ModulationDepth =   1,
        DataEntery_MSB =    6,
        Volume =            7,
        Pan =               10,
        Expression =        11,
        DataEntery_LSB =    38,
        Hold1 =             64,
        NRPN_LSB =          98,
        NRPN_MSB =          99,
        RPN_LSB =           100,
        RPN_MSB =           101,
        AllSoundOff =       120,
        ResetAllContorol =  121,
        AllNoteOff =        123,
    }

    /// <summary>
    /// midiStreamOpen の 第6引数:fdwOpen のフラグ。コールバック方法の指定。
    /// </summary>
    public enum MIDIFlags : uint
    {
        CALLBACK_NULL = 0x000000,       // コールバックを使用しません。
        CALLBACK_WINDOW = 0x010000,     // dwCallback is a HWND パラメータがウィンドウハンドルであることを指定します。
        CALLBACK_THREAD = 0x020000,     // パラメータがスレッド ID であることを指定します。
        CALLBACK_FUNCTION = 0x030000,   // 'dwCallback is a FARPROC  パラメータがコールバック関数のアドレスであることを指定します。
        CALLBACK_EVENT = 0x050000,      // パラメータがイベントハンドルであることを指定します。
        CALLBACK_TYPEMASK = 0x070000,
    }

    public enum MidiHdrFlag : uint
    {
        None = 0,		//フラグがセットされていません。// 0x0000
        Done = 1,		//バッファの使用が完了しました。// 0x0001
        Prepared = 2,	//バッファの準備が完了しました　// 0x0010
        InQueue = 4		//バッファは再生待ちです。      // 0x0100  
    }

    public enum MIDIMessage : uint
    {
        MIM_OPEN = 0x3C1,
        MIM_CLOSE = 0x3C2,
        MIM_DATA = 0x3C3,
        MIM_LONGDATA = 0x3C4,
        MIM_ERROR = 0x3C5,
        MIM_LONGERROR = 0x3C6,

        MOM_POSITIONCB = 0x3CA,
        MIM_MOREDATA = 0x3CC,

        MOM_OPEN = 0x3C7,
        MOM_CLOSE = 0x3C8,
        MOM_DONE = 0x3C9,    //バッファが再生された
    }


    /// <summary>  
    /// MIDIポートの種類を表すフラグです。
    /// </summary>  
    /// <remarks>MidiOutCaps内で使用</remarks>
    public enum MidiModuleType : ushort
    {
        /// <summary>  
        /// このポートはハードウェアポートです。  
        /// </summary>  
        Hardware = 1,
        /// <summary>  
        /// このポートはソフトウェアシンセサイザです。  
        /// </summary>  
        Synthesizer = 2,
        /// <summary>  
        /// このポートは矩形シンセサイザです。  
        /// </summary>  
        SquareSynth = 3,
        /// <summary>  
        /// このポートはFMシンセサイザです。  
        /// </summary>  
        FMSynth = 4,
        /// <summary>  
        /// このポートはMIDIマッパーです。  
        /// </summary>  
        MidiMapper = 5,
        /// <summary>  
        /// このポートはウェーブテーブルシンセサイザです。  
        /// </summary>  
        Wavetable = 6,
        /// <summary>  
        /// このポートはソフトウェアシンセサイザです。  
        /// </summary>  
        SoftwareSynth = 7
    }


    public enum MMRESULT : uint
    {
        MMSYSERR_NOERROR = 0,
        MMSYSERR_ERROR = 1,
        MMSYSERR_BADDEVICEID = 2,    //指定されたデバイス ID は範囲外です。
        MMSYSERR_NOTENABLED = 3,
        MMSYSERR_ALLOCATED = 4,    //指定されたリソースはすでに割り当てられています。
        MMSYSERR_INVALHANDLE = 5,    //無効なハンドル
        MMSYSERR_NODRIVER = 6,
        MMSYSERR_NOMEM = 7,    //メモリの確保またはロックに失敗しました。
        MMSYSERR_NOTSUPPORTED = 8,
        MMSYSERR_BADERRNUM = 9,
        MMSYSERR_INVALFLAG = 10,
        MMSYSERR_INVALPARAM = 11,   //指定されたハンドルまたはフラグのパラメータは無効です。
        MMSYSERR_HANDLEBUSY = 12,
        MMSYSERR_INVALIDALIAS = 13,
        MMSYSERR_BADDB = 14,
        MMSYSERR_KEYNOTFOUND = 15,
        MMSYSERR_READERROR = 16,
        MMSYSERR_WRITEERROR = 17,
        MMSYSERR_DELETEERROR = 18,
        MMSYSERR_VALNOTFOUND = 19,
        MMSYSERR_NODRIVERCB = 20,
        MMSYSERR_MOREDATA = 21,
        MMSYSERR_LASTERROR = 22,

        WAVERR_BADFORMAT = 32,
        WAVERR_STILLPLAYING = 33,
        WAVERR_UNPREPARED = 34,

        MIDIERR_UNPREPARED = 64,
        MIDIERR_STILLPLAYING = 65,
        MIDIERR_NOMAP = 66,
        MIDIERR_NOTREADY = 67,//66という情報も
        MIDIERR_NODEVICE = 68,//67という情報も
        MIDIERR_INVALIDSETUP = 69,//	MIDI ポートが見つかりません。このエラーは MIDI Mapper がオープンされようとしたときのみ発生します。
        //???MIDIERR_LASTERROR = 69,
        MIDIERR_BADOPENMODE = 70
    }

    
    //ステータス・バイトの番号
    public enum StatusByte : uint
    {
        NoteOff = 0x80,
        NoteOn = 0x90,
        PolyphonicKeyPressure = 0xA0, //使ったこと無い
        ControlChange = 0xB0,
        ProgramChange = 0xC0,
        ChannelPressure = 0xD0, //使ったこと無い
        PitchBendChange = 0xE0,
    }


    /// <summary>  
    /// MIDIポートの能力を示すフラグです。  
    /// </summary>  
    /// <remarks>MidiOutCaps内で使用</remarks>
    [Flags]
    public enum MidiPortCapability : uint
    {
        /// <summary>  
        /// ポートはボリュームコントロールをサポートします。  
        /// </summary>  
        Volume = 1,
        /// <summary>  
        /// ポートは左右独立のボリュームコントロールをサポートします。  
        /// </summary>  
        LRVolume = 2,
        /// <summary>  
        /// ポートはキャッシュをサポートします。  
        /// </summary>  
        Cache = 4,
        /// <summary>  
        /// ポートはMIDIストリームAPIをネイティブサポートします。  
        /// </summary>  
        Stream = 8
    }



    #endregion


    #region region - delegate

    internal delegate void MidiOutProc(int handle, int msg, int instance, int param1, int param2);

    // 19.05.03 追加
    //internal delegate void MidiInProc(int handle, int msg, int instance, int param1, int param2);
    public delegate void MidiInProc(IntPtr hMidiIn, MIDIMessage wMsg, IntPtr dwInstance, uint dwParam1, uint dwParam2);


    public delegate void MidiDelegate(int eventCode);

    #endregion


    public static class Utility
    {
        public static string[] GMProgramArray()
        {
            return new string[]{
                "Acoustic Grand Piano",
                "Bright Acoustic Piano",
                "Electric Grand Piano",
                "Honkey-tonk Piano",
                "Electric Piano1",
                "Electric Piano2",
                "Harpsichord",
                "Clavi",
                "Celesta",
                "Glockenspiel",
                "Music Box",
                "Vibraphone",
                "Marinmba",
                "Xylophone",
                "Tubular Bells",
                "Dulcimer",
                "Drawbar Organ",
                "Percussive Organ",
                "Rock Organ",
                "Church Organ",
                "Reed Organ",
                "Accordion",
                "Harmonica",
                "Tango Accordion",
                "Acoustic Guitar(nylon)",
                "Acoustic Guitar(steel)",
                "Electric Guitar(jazz)",
                "Electric Guitar(clean)",
                "Electric Guitar(muted)",
                "Overdriven Guitar",
                "Distortion Guitar",
                "Guitar harmonics",
                "Acoustic Bass",
                "Electric Bass(finger)",
                "Electric Bass(pick)",
                "Fretless Bass",
                "Slap Bass1",
                "Slap Bass2",
                "Synth Bass1",
                "Synth Bass2",
                "Violin",
                "Viola",
                "Cello",
                "Contrabass",
                "Tremolo Strings",
                "Pizzicato Strings",
                "Orchestral Harp",
                "Timpani",
                "String Emsemble1",
                "String Emsemble2",
                "Synth String1",
                "Synth String2",
                "Choir Aahs",
                "Voice Oohs",
                "Synth Vox",
                "Orchestra Hit",
                "Trumpet",
                "Trombone",
                "Tuba",
                "Muted Trumpet",
                "French Horn",
                "Brass Section",
                "Synth Brass1",
                "Synth Brass2",
                "Soprano Sax",
                "Alto Sax",
                "Tenor Sax",
                "Baritone Sax",
                "Oboe",
                "English Horn",
                "Bossoon",
                "Clarinet",
                "Piccolo",
                "Flute",
                "Recorder",
                "Pan Flute",
                "Blown Bottle",
                "Shakuhachi",
                "Whistle",
                "Ocarina",
                "Lead1(square)",
                "Lead2(sawtooth",
                "Lead3(calliope)",
                "Lead4(chiff)",
                "Lead5(charang)",
                "Lead6(voice)",
                "Lead7(fifths)",
                "Lead8(bass + lead)",
                "Pad1(new age)",
                "Pad2(warm)",
                "Pad3(polysynth)",
                "Pad4(choir)",
                "Pad5(bowed)",
                "Pad6(metallic)",
                "Pad7(halo)",
                "Pad8(sweep)",
                "Fx1(rain)",
                "Fx2(soundtrack)",
                "Fx3(crystal)",
                "Fx4(atmosphere)",
                "Fx5(brightness)",
                "Fx6(goblins)",
                "Fx7(echoes)",
                "Fx8(sci-fi)",
                "Sitar",
                "Banjo",
                "Shamisen",
                "Koto",
                "Kalimba",
                "Bag pipe",
                "Fiddle",
                "Shanai",
                "Tinkle Bell",
                "Agogo",
                "Steel Drums",
                "Woodblock",
                "Taiko",
                "Melodic Tom",
                "Synth Drum",
                "Reverse Cymbal",
                "Guitar Fret Noise",
                "Breath Noise",
                "Seashore",
                "Bird Tweet",
                "Telephone Ring",
                "Helicopter",
                "Applause",
                "Gunshot"
            };
        }
    }
}
