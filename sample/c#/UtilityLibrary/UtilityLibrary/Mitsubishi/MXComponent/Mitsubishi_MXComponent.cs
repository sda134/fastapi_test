using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.UtilityLibrary.Mitsubishi.FxDeviceExtensionMethod;
using System.Runtime.InteropServices;

namespace Mtec.UtilityLibrary.Mitsubishi.MXComponent
{

    // ※ ActProgTypeLib の参照が必要（相互運用型の埋め込み　をfalse に）

    // MitsubishiPLC の参照なしで使える範囲のコードがここに記述してある


    #region  region - enum

    public enum ActUnitType : int
    {
        Default = 0,

        QNCPU = 0x13,          // SERIAL(RS232C)-QCPU Q
        FXCPU = 0x0F,          // SERIAL(RS232C)-FXCPU
        LNCPU = 0x50,          // SERIAL(RS232C)-LCPU
        QNMOTION = 0x1C,           // SERIAL(RS232C)-QMOTION
        QJ71C24 = 0x19,            // SERIAL(C24)-QCPU
        FX485BD = 0x24,            // SERIAL(FX485BD)-FXCPU
        LJ71C24 = 0x54,            // SERIAL(C24)-LCPU
        QJ71E71 = 0x1A,            // Ethernet(QJ71E71)
        FXENET = 0x26,         // Ethernet(FXENET)
        FXENET_ADP = 0x27,         // Ethernet(FX1N-ENET-ADP)
        QNETHER = 0x2C,            // Ethernet(QCPU) IP
        QNETHER_DIRECT = 0x2D,         // Ethernet(QCPU) DIRECT
        LNETHER = 0x52,            // Ethernet(LCPU) IP
        LNETHER_DIRECT = 0x53,         // Ethernet(LCPU) DIRECT
        NZ2GF_ETB = 0x59,          // NZ2GF-ETB
        NZ2GF_ETB_DIRECT = 0x5A,           // NZ2GF-ETB DIRECT
        QNUSB = 0x16,          // USB-QCPU
        LNUSB = 0x51,          // USB-LCPU
        QNMOTIONUSB = 0x1D,            // USB-QMOTION
        G4QNCPU = 0x1B,            // G4-QCPU
        CCLINKBOARD = 0x0C,            // CC-Link Board
        MNETHBOARD = 0x1E,         // MELSECNET/H Board
        MNETGBOARD = 0x2B,         // CC-Link IE Control Board
        CCIEFBOARD = 0x2F,         // CC-Link IE Field Board
        SIMULATOR = 0x0B,         // GX Simulator
        SIMULATOR2 = 0x30,         // GX Simulator2
        SIMULATOR3 = 0x31,         // GX Simulator3
        QBF = 0x1F,            // QBF
        QSS = 0x20,           // Qn SoftLogic
        A900GOT = 0x21,            // GOT
        GOT_QJ71E71 = 0x40,            // GOT Transparent QJ71E71
        GOT_QNETHER = 0x41,            // GOT Transparent Ethernet(QCPU)
        GOT_LNETHER = 0x55,            // GOT Transparent Ethernet(LCPU)
        GOT_NZ2GF_ETB = 0x5B,          // GOT Transparent NZ2GF-ETB
        GOTETHER_QNCPU = 0x56,         // GOT Transparent ETHERNET-QCPU
        GOTETHER_QBUS = 0x58,          // GOT Transparent ETHERNET-QBUS
        GOTETHER_LNCPU = 0x57,         // GOT Transparent ETHERNET-LCPU
        FXETHER = 0x4A,            // EthernetADP-FXCPU
        FXETHER_DIRECT = 0x4B,         // EthernetADP-FXCPU(DIRECT)
        GOTETHER_FXCPU = 0x60,         // GOT Transparent Ethernet(FXCPU)
        GOT_FXETHER = 0x61,            // GOT Transparent FX3U-ENET-ADP
        GOT_FXENET = 0x62,         // GOT FX3U-ENET(-L)
        RJ71C24 = 0x1000,      // SERIAL(C24)-RCPU
        RJ71EN71 = 0x1001,     // Ethernet(RJ71EN71)
        RETHER = 0x1002,       // Ethernet(RCPU) IP
        RETHER_DIRECT = 0x1003,        // Ethernet(QCPU) DIRECT
        RUSB = 0x1004,     // USB-RCPU
        RJ71EN71_DIRECT = 0x1005,      // Ethernet(RJ71EN71) DIRECT
        GOT_RJ71EN71 = 0x1051,     // GOT Transparent RJ71EN71
        GOT_RETHER = 0x1052,       // GOT Transparent Ethernet(RCPU)
        GOTETHER_RJ71C24 = 0x1061,     // GOT Transparent ETHERNET-SERIAL(C24)-RCPU
        FXVCPU = 0x2000,       // SERIAL(RS232C)-FX5CPU
        FXVETHER = 0x2001,     // Ethernet(FX5CPU) IP
        FXVETHER_DIRECT = 0x2002,      // Ethernet(FX5CPU) DIRECT
        GOT_FXVCPU = 0x2005,       // GOT Transparent SERIAL(FX5U)
        GOTETHER_FXVCPU = 0x2006,      // GOT Transparent Ethernet-FX5U
        GOT_FXVETHER = 0x2007,     // GOT Transparent ETHERNET(FX5U)
        LJ71E71 = 0x5C,            // Ethernet(LJ71E71)
        GOT_LJ71E71 = 0x5D,            // GOT Transparent LJ71E71
        GOTETHER_QN_ETHER = 0x6F,         // GOT Transparent(Ether-GOT-Ether-QnCPU)
    }

    public enum ActCpuType : int
    {
        Default = 0,

        Q2ACPU = 0x0011,           // Q2A
        Q2AS1CPU = 0x0012,         // Q2AS1
        Q3ACPU = 0x0013,           // Q3A
        Q4ACPU = 0x0014,           // Q4A
                                   // QCPU Q
        Q02CPU = 0x0022,           // Q02(H) Q
        Q06CPU = 0x0023,           // Q06H   Q
        Q12CPU = 0x0024,           // Q12H   Q
        Q25CPU = 0x0025,           // Q25H   Q
        Q00JCPU = 0x0030,          // Q00J   Q
        Q00CPU = 0x0031,           // Q00    Q
        Q01CPU = 0x0032,           // Q01    Q
        Q12PHCPU = 0x0041,         // Q12PHCPU Q
        Q25PHCPU = 0x0042,         // Q25PHCPU Q
        Q12PRHCPU = 0x0043,            // Q12PRHCPU Q
        Q25PRHCPU = 0x0044,            // Q25PRHCPU Q
        Q25SSCPU = 0x0055,         // Q25SS
        Q03UDCPU = 0x0070,         // Q03UDCPU
        Q04UDHCPU = 0x0071,            // Q04UDHCPU
        Q06UDHCPU = 0x0072,            // Q06UDHCPU
        Q02UCPU = 0x0083,          // Q02UCPU
        Q03UDECPU = 0x0090,            // Q03UDECPU
        Q04UDEHCPU = 0x0091,           // Q04UDEHCPU
        Q06UDEHCPU = 0x0092,           // Q06UDEHCPU
        Q13UDHCPU = 0x0073,            // Q13UDHCPU
        Q13UDEHCPU = 0x0093,           // Q13UDEHCPU
        Q26UDHCPU = 0x0074,            // Q26UDHCPU
        Q26UDEHCPU = 0x0094,           // Q26UDEHCPU
        Q02PHCPU = 0x0045,         // Q02PH  Q
        Q06PHCPU = 0x0046,         // Q06PH  Q
        Q00UJCPU = 0x0080,         // Q00UJCPU
        Q00UCPU = 0x0081,          // Q00UCPU
        Q01UCPU = 0x0082,          // Q01UCPU
        Q10UDHCPU = 0x0075,            // Q10UDHCPU
        Q20UDHCPU = 0x0076,            // Q20UDHCPU
        Q10UDEHCPU = 0x0095,           // Q10UDEHCPU
        Q20UDEHCPU = 0x0096,           // Q20UDEHCPU
        Q50UDEHCPU = 0x0098,           // Q50UDEHCPU
        Q100UDEHCPU = 0x009A,          // Q100UDEHCPU
        Q03UDVCPU = 0x00D1,            // Q03UDVCPU
        Q04UDVCPU = 0x00D2,            // Q04UDVCPU
        Q06UDVCPU = 0x00D3,            // Q06UDVCPU
        Q13UDVCPU = 0x00D4,            // Q13UDVCPU
        Q26UDVCPU = 0x00D5,            // Q26UDVCPU
                                       // ACPU
        A0J2HCPU = 0x0102,         // A0J2H
        A1FXCPU = 0x0103,          // A1FX
        A1SCPU = 0x0104,           // A1S,A1SJ
        A1SHCPU = 0x0105,          // A1SH,A1SJH
        A1NCPU = 0x0106,           // A1(N)
        A2CCPU = 0x0107,           // A2C,A2CJ
        A2NCPU = 0x0108,           // A2(N),A2S
        A2SHCPU = 0x0109,          // A2SH
        A3NCPU = 0x010A,           // A3(N)
        A2ACPU = 0x010C,           // A2A
        A3ACPU = 0x010D,           // A3A
        A2UCPU = 0x010E,           // A2U,A2US
        A2USHS1CPU = 0x010F,           // A2USHS1
        A3UCPU = 0x0110,           // A3U
        A4UCPU = 0x0111,           // A4U
                                   // QCPU A
        Q02A = 0x0141,         // Q02(H)
        Q06A = 0x0142,         // Q06H
                               // LCPU
        L02CPU = 0x00A1,           // L02CPU
        L26CPUBT = 0x00A2,         // L26CPU-BT
        L02SCPU = 0x00A3,          // L02SCPU
        L26CPU = 0x00A4,           // L26CPU
        L06CPU = 0x00A5,           // L06CPU
                                   // C Controller
        Q12DC_V = 0x0058,          // Q12DCCPU-V
        Q24DHC_V = 0x0059,          // Q24DHCCPU-V
        Q24DHC_VG = 0x005C,         // Q24DHCCPU-VG
        Q26DHC_LS = 0x005D,            // Q26DHCCPU-LS
                                       // Q MOTION
        Q172CPU = 0x0621,          // Q172CPU
        Q173CPU = 0x0622,          // Q173CPU
        Q172HCPU = 0x0621,         // Q172HCPU
        Q173HCPU = 0x0622,         // Q173HCPU
        Q172DCPU = 0x0625,         // Q172DCPU
        Q173DCPU = 0x0626,         // Q173DCPU
        Q172DSCPU = 0x062A,            // Q172DSCPU
        Q173DSCPU = 0x062B,            // Q173DSCPU
                                       // QSCPU
        QS001CPU = 0x0060,         // QS001
                                   // FXCPU
        FX0CPU = 0x0201,           // FX0/FX0S
        FX0NCPU = 0x0202,          // FX0N
        FX1CPU = 0x0203,           // FX1
        FX2CPU = 0x0204,           // FX2/FX2C
        FX2NCPU = 0x0205,          // FX2N/FX2NC
        FX1SCPU = 0x0206,          // FX1S
        FX1NCPU = 0x0207,          // FX1N/FX1NC
        FX3UCCPU = 0x0208,         // FX3U/FX3UC
        FX3GCPU = 0x0209,          // FX3G/FX3GC
                                   // BOARD
        BOARD = 0x0401,            // NETWORK BOARD
                                   // MOTION
        A171SHCPU = 0x0601,            // A171SH
        A172SHCPU = 0x0602,            // A172SH
        A273UHCPU = 0x0603,            // A273UH
        A173UHCPU = 0x0604,            // A173UH
                                       // GOT
        A900GOT = 0x0701,          // A900GOT

        // iQ-R CPU
        R04CPU = 0x1001,            // R04CPU
        R08CPU = 0x1002,            // R08CPU
        R16CPU = 0x1003,            // R16CPU
        R32CPU = 0x1004,            // R32CPU
        R120CPU = 0x1005,           // R120CPU

        // iQ-R PROCESS CPU
        R08PCPU = 0x1102,           // R08PCPU
        R16PCPU = 0x1103,           // R16PCPU
        R32PCPU = 0x1104,           // R32PCPU
        R120PCPU = 0x1105,          // R120PCPU

        // iQ-R SAFE CPU
        R08SFCPU = 0x1122,          // R08SFCPU
        R16SFCPU = 0x1123,          // R16SFCPU
        R32SFCPU = 0x1124,          // R32SFCPU
        R120SFCPU = 0x1125,         // R120SFCPU

        // iQ-R EN CPU
        R04ENCPU = 0x1008,          // R04ENCPU
        R08ENCPU = 0x1009,          // R08ENCPU
        R16ENCPU = 0x100A,          // R32ENCPU
        R32ENCPU = 0x100B,          // R64ENCPU
        R120ENCPU = 0x100C,            // R120ENCPU

        // iQ-R Motion
        R16MTCPU = 0x1011,          // R16MTCPU.
        R32MTCPU = 0x1012,          // R32MTCPU.

        // iQ-R CCONTROLLER
        R12CV = 0x1021,           // R12CCPU-V.

        // iQ-F CPU
        FX5UCPU = 0x0210,          //  FX5U CPU
    }

    public enum ActCpuStatus
    {
        RemoteRun = 0,
        RemoteStop = 1,
        RemotePause = 2,
    }

    public enum ActProtocolType
    {
        ///ACTPROGTYPE PROTOCOLTYPE
        PROTOCOL_SERIAL = 0x04,         // Protocol Serial
        PROTOCOL_USB = 0x0D,            // Protocol USB
        PROTOCOL_TCPIP = 0x05,          // Protocol TCP/IP
        PROTOCOL_UDPIP = 0x08,          // Protocol UDP/IP
        PROTOCOL_MNETH = 0x0F,          // Protocol MELSECNET/H
        PROTOCOL_MNETG = 0x14,          // Protocol CC IE Control Board
        PROTOCOL_CCIEF = 0x15,          // Protocol CC IE Field Board
        PROTOCOL_CCLINK = 0x07,         // Protocol CC-LINK Board
        PROTOCOL_SERIALMODEM = 0x0E,    // Protocol MODEM
        PROTOCOL_TEL = 0x0A,            // Protocol TEL
        PROTOCOL_QBF = 0x10,            // Protocol QBF
        PROTOCOL_QSS = 0x11,            // Protocol QSS
        PROTOCOL_USBGOT = 0x13,         // Protocol GOT TRANSPARENT USB
        PROTOCOL_SHAREDMEMORY = 0x06,   // Protocol Simulator
    }


    public enum RemoteOperation
    {
        RemoteRun = 0,
        RemoteStop = 1,
        RemotePause = 2,
    }

    #endregion


    #region  region - enum: serial

    public enum ActPortnumber : int
    {
        PORT_1 = 0x01,
        PORT_2 = 0x02,
        PORT_3 = 0x03,
        PORT_4 = 0x04,
        PORT_5 = 0x05,
        PORT_6 = 0x06,
        PORT_7 = 0x07,
        PORT_8 = 0x08,
        PORT_9 = 0x09,
        PORT_10 = 0x0A,
    }

    public enum ActBaudrate : int
    {
        BAUDRATE_300 = 300,
        BAUDRATE_600 = 600,
        BAUDRATE_1200 = 1200,
        BAUDRATE_2400 = 2400,
        BAUDRATE_4800 = 4800,
        BAUDRATE_9600 = 9600,
        BAUDRATE_19200 = 19200,
        BAUDRATE_38400 = 38400,
        BAUDRATE_57600 = 57600,
        BAUDRATE_115200 = 115200,
    }

    public enum ActDataBits : int
    {
        DATABIT_7 = 7,
        DATABIT_8 = 8,
    }

    public enum ActParity
    {
        NO_PARITY = 0,
        ODD_PARITY = 1,
        EVEN_PARITY = 2,
    }

    public enum ActStopBits : int
    {
        STOPBIT_ONE = 0,
        STOPBITS_TWO = 2,

        // ロボットコントローラの場合
        ONE5STOPBITS = 1,
    }

    public enum ActControl : int
    {
        /// <summary>
        /// DTR 制御
        /// </summary>
        TRC_DTR = 0x01,

        /// <summary>
        /// RTS 制御
        /// </summary>
        TRC_RTS = 0x02,

        /// <summary>
        /// DTR 制御かつRTS 制御
        /// </summary>
        TRC_DRT_AND_RTS = 0x07,

        /// <summary>
        /// DTR 制御またはRTS 制御
        /// </summary>
        TRC_DTR_OR_RTS = 0x08,
    }

    #endregion


    #region region - class : model

    [System.Serializable]
    public class ActControlSettingFormat : ICloneable
    {

        #region MyRegion

        public string ActHostAddress { get; set; }

        public ActProtocolType ActProtocolType { get; set; }

        public ActCpuType ActCpuType { get; set; }

        public ActUnitType ActUnitType { get; set; }

        public ActPortnumber PortNumber { get; set; } = ActPortnumber.PORT_1;

        public ActBaudrate BaudRate { get; set; } = ActBaudrate.BAUDRATE_9600;

        public ActDataBits DataBits { get; set; } = ActDataBits.DATABIT_8;

        public ActStopBits StopBits { get; set; }

        public ActParity ParityBits { get; set; } = ActParity.EVEN_PARITY;

        public ActControl ActControl { get; set; } = ActControl.TRC_DRT_AND_RTS;

        // 19.01.24 追加
        //public int ActLogicalStationNumber { get; set; } = 0;
        public int ActTimeOut { get; set; } = 10000;


        // 19.01.24 追加
        public int ActIONumber { get; set; } = 1023;
        public int ActUnitNumber { get; set; } = 0;
        public int ActDestinationPortNumber { get; set; } = 0;
        public int ActDestinationIONumber { get; set; } = 0;

        #endregion


        /*
        public ActControlSettingFormat(ActControlSettingFormat arg)
        {
            // コピーコンストラクタ　※この方法はめんどくさい
            this.ActHostAddress = arg.ActHostAddress;
        }*/

        public object Clone()
        {
            return (ActControlSettingFormat)this.MemberwiseClone();
        }

        public static explicit operator ActProgTypeLib.ActProgTypeClass(ActControlSettingFormat arg)
        {
            return new ActProgTypeLib.ActProgTypeClass
            {
                ActHostAddress = arg.ActHostAddress,

                ActProtocolType = (int)arg.ActProtocolType,
                ActCpuType = (int)arg.ActCpuType,
                ActUnitType = (int)arg.ActUnitType,
                ActPortNumber = (int)arg.PortNumber,

                ActBaudRate = (int)arg.BaudRate,

                ActDataBits = (int)arg.DataBits,

                ActStopBits = (int)arg.StopBits,

                ActParity = (int)arg.ParityBits,

                ActControl = (int)arg.ActControl,

                // 19.01.24 追加
                //arg. ActLogicalStationNumber , = 0;
                ActTimeOut = arg.ActTimeOut,

                // 19.01.24 追加
                ActIONumber = arg.ActIONumber,
                ActUnitNumber = arg.ActUnitNumber,
                ActDestinationPortNumber = arg.ActDestinationPortNumber,
                ActDestinationIONumber = arg.ActDestinationIONumber,
            };
        }

        
    }

    #endregion


    #region region - class

    // もともとの三菱のライブラリがあまりにも酷いので、継承して少しでも使いやすくする　18.12.07
    public class ActProg : ActProgTypeLib.ActProgTypeClass
    {

        #region region - GetDevice


        public int GetDevice(string szDevice, int length, out byte[] byteArray)
        {
            // out 値をはじめに代入
            byteArray = null;

            // 値の取得
            short[] shVal = new short[length];
            int iRet = base.ReadDeviceBlock2(szDevice, length, out shVal[0]);

            // 取得成功したら
            if (iRet == 0)
            {
                byteArray = (from short sh in shVal
                             from b in BitConverter.GetBytes(sh)
                             select b).ToArray();
            }

            return iRet;
        }


        public override int GetDevice(string szDevice, out Int32 iVal)
        {
            short[] shVal = new short[2];
            int iRet = base.ReadDeviceBlock2(szDevice, 2, out shVal[0]);

            if (iRet == 0)
            {
                // 18.12.07 もう少しきれいに書けないか
                //var byteList = new List<byte>();
                //byteList.AddRange(BitConverter.GetBytes(shVal[0]));
                //byteList.AddRange(BitConverter.GetBytes(shVal[1]));
                // ↓ 18.12.26 変更
                var byteArray = (from short sh in shVal
                                 from b in BitConverter.GetBytes(sh)
                                 select b).ToArray();

                iVal = BitConverter.ToInt32(byteArray, 0);

            }
            else
                iVal = 0;

            return iRet;
        }


        public int GetDevice(string szDevice, out Int16 shVal)
        {
            int iRet = base.GetDevice2(szDevice, out shVal);

            return iRet;
        }


        public int GetDevice(string szDevice, out UInt16 ushVal)
        {
            int iRet = base.GetDevice2(szDevice, out Int16 iVal);

            if (iRet == 0)
            {
                byte[] byteArray = BitConverter.GetBytes(iVal);
                ushVal = BitConverter.ToUInt16(byteArray, 0);
            }
            else
                ushVal = 0;

            return iRet;
        }


        public int GetDevice(string szDevice, out UInt32 uiVal)
        {
            short[] shVal = new short[2];
            int iRet = base.ReadDeviceBlock2(szDevice, 2, out shVal[0]);


            if (iRet == 0)
            {
                // 18.12.07 もう少しきれいに書けないか
                var byteList = new List<byte>();
                byteList.AddRange(BitConverter.GetBytes(shVal[0]));
                byteList.AddRange(BitConverter.GetBytes(shVal[1]));
                uiVal = BitConverter.ToUInt32(byteList.ToArray(), 0);
            }
            else
                uiVal = 0;

            return iRet;
        }


        public int GetDevice(string szDevice, out Single fVal)
        {
            // 32bit float を返すプログラムにする

            // PLC からの値を格納する変数
            short[] shVal = new short[2];
            int iRet = base.ReadDeviceBlock2(szDevice, 2, out shVal[0]);

            // 18.12.07 もう少しきれいに書けないか
            var byteList = new List<byte>();
            byteList.AddRange(BitConverter.GetBytes(shVal[0]));
            byteList.AddRange(BitConverter.GetBytes(shVal[1]));
            fVal = BitConverter.ToSingle(byteList.ToArray(), 0);

            return iRet;
        }


        public int GetDevice_BCD(string szDevice, out UInt16 bcd16Val)
        {
            int iRet = base.GetDevice2(szDevice, out Int16 iVal);
            bcd16Val = 0;

            if (iRet == 0)
            {
                byte[] byteArray = BitConverter.GetBytes(iVal);
                uint ushVal = BitConverter.ToUInt16(byteArray, 0);

                for (int digit = 0; digit < 4; digit++)
                    bcd16Val += (ushort)(((ushVal >> (4 * digit)) & 0xF) * (Math.Pow(10, digit)));
            }

            return iRet;
        }


        public int GetDevice_BCD(string szDevice, out UInt32 bcd32Val)
        {
            short[] shVal = new short[2];
            int iRet = base.ReadDeviceBlock2(szDevice, 2, out shVal[0]);

            bcd32Val = 0;

            if (iRet == 0)
            {
                var byteArray = (from short sh in shVal
                                 from b in BitConverter.GetBytes(sh)
                                 select b).ToArray();

                uint uiVal = BitConverter.ToUInt32(byteArray, 0);

                for (int digit = 0; digit < 4; digit++)
                    bcd32Val += (ushort)(((uiVal >> (4 * digit)) & 0xF) * (Math.Pow(10, digit)));
            }

            return iRet;
        }


        #endregion

        #region region - SetDevice

        public int SetDevice(string szDevice, byte[] arg)
        {
            // バイト配列を short の配列に変換
            var shortArray =        
                arg.Select((byte_val, index) => new { byte_val, index })        
                .GroupBy(i => i.index / 2)        
                .Select(gp => BitConverter.ToInt16(arg, gp.Key * 2)                
                               /*            
                               {    // 以前の処理　いずれ消す 19.04.26
                                    var byteList = gp.Select(x => x.byte_val).ToList();
                                    while (byteList.Count < 2) byteList.Add(0);
                                    var shVal = BitConverter.ToInt16(byteList.ToArray(), 0);
                                    return shVal;
                                    return BitConverter.ToInt16(arg, gp.Key * 2);
                                }*/
                                ).ToArray();

            
            // デバイス名の配列を取得
            string[] devNameArray;
            if (FxDevice.TryParse(szDevice, out FxDevice fxDev))
            {
                devNameArray = shortArray.Select((x, i) =>  string.Format("{0}{1}",
                    fxDev.DeviceType.ToDeviceLetter(),
                    (fxDev.DeviceNumber + i))
                    ).ToArray();
            }
            else
                devNameArray = new string[] { szDevice };

            // MXComponent に送る引数を作成する
            var deviceName = string.Join("\n", devNameArray);

            // データ書き込み
            return base.WriteDeviceRandom2(deviceName, shortArray.Count(), ref shortArray[0]);
        }


        public override int SetDevice(string szDevice, int iVal) => this.SetDevice(szDevice, BitConverter.GetBytes(iVal));



        // 19.04.26 このメソッド、作り直す必要がある
        // 19.04.26 何故かわけの分からない処理がされていた
        /* 
public int SetDevice(string szDevice, byte[] arg)
{
    Func<int, string> shiftedDeviceName = (shift) =>
    {
        #region

        string devLetter = "D";
        int devNumber = 0;

        // 隣のデバイス名を作成する
        // デバイス番号表記は16進数、10進数、8進数の混在だが、ワードデバイスは10進数のみ
        this.deviceLetterList.ForEach(x =>
        {
            int startIdx = szDevice.IndexOf(x);

            if (startIdx != -1)
            {
                devLetter = x;
                string numString = szDevice.Substring(devLetter.Length, szDevice.Length - devLetter.Length);
                int.TryParse(numString, out devNumber);
            }
        });

        return devLetter + (devNumber + shift).ToString();
        if (FxDevice.TryParse(szDevice, out FxDevice fxDev))
            return fxDev.DeviceType.ToDeviceLetter() + (fxDev.DeviceNumber + shift);
        else
            return null;

        #endregion
    };

    var shortArray =
        arg.Select((byte_val, index) => new { byte_val, index })
        .GroupBy(i => i.index / 2)
        .Select(gp =>
        {
            var byteList = gp.Select(x => x.byte_val).ToList();
            while (byteList.Count < 2) byteList.Add(0);
            return BitConverter.ToInt16(byteList.ToArray(), 0);
        }).ToArray();

    string deviceName =
        string.Join("\n",
        shortArray.Select((x, index) => shiftedDeviceName(index)));

    return base.WriteDeviceRandom2(deviceName, shortArray.Count(), ref shortArray[0]);
}
*/



        #endregion


        // 文字数の多い順にする事
        private List<string> deviceLetterList => (new string[] { "GD", "D" }).ToList();


        public int ReadDeviceRandom(IEnumerable<string> deviceList, IEnumerable<Type> typeList, out object[] values)
        {
            // out 変数に先に値を入れておく
            values = null;


            // 19.01.28 追加
            if (deviceList.Count() < 1) return 0;


            // 19.01.07 追加
            if (deviceList.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException("空白か null のデバイス名が含まれています。");
            }


            // 実際に読み取るデバイスのリスト　※float とInt32 は２つのデバイスを取得する必要がある
            var actualDeviceNameList = new List<string>();

            // 型の種類によってデバイス名を調整する
            for (int dIdx = 0; dIdx < deviceList.Count(); dIdx++)
            {
                #region region

                if (dIdx < typeList.Count())
                {
                    switch (Type.GetTypeCode(typeList.ElementAt(dIdx)))
                    {
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Single:
                            {
                                string devLetter = "D";
                                int devNumber = 0;

                                // 隣のデバイス名を作成する
                                // デバイス番号表記は16進数、10進数、8進数の混在だが、ワードデバイスは10進数のみ
                                this.deviceLetterList.ForEach(x =>
                                {
                                    int startIdx = deviceList.ElementAt(dIdx).IndexOf(x);

                                    if (startIdx != -1)
                                    {
                                        devLetter = x;
                                        string numString = deviceList.ElementAt(dIdx).Substring(devLetter.Length, deviceList.ElementAt(dIdx).Length - devLetter.Length);
                                        int.TryParse(numString, out devNumber);
                                    }
                                });

                                // 実用デバイスに登録
                                actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                                actualDeviceNameList.Add(devLetter + (devNumber + 1).ToString());
                            }
                            break;

                        default:
                            // 実用デバイスに登録
                            actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                            break;
                    }
                }

                #endregion
            }

            // MXComponent のメソッドに投げるデバイス名
            string devices = string.Join("\n", actualDeviceNameList);


            // 読み出し値を格納する変数
            short[] sReadValues = new short[actualDeviceNameList.Count];

            // 読み出し
            int iRet = base.ReadDeviceRandom2(devices, actualDeviceNameList.Count, out sReadValues[0]);

            if (iRet == 0)
            {
                // 下のループ処理で使用する変数
                int doneDev = 0;

                // 戻り値になる変数
                var valuesList = new List<object>();

                // 格納していく
                for (int dIdx = 0; dIdx < deviceList.Count(); dIdx++)
                {
                    #region region


                    if (dIdx < typeList.Count())
                    {
                        switch (Type.GetTypeCode(typeList.ElementAt(dIdx)))
                        {
                            case TypeCode.UInt16:
                                {
                                    var byteArray = BitConverter.GetBytes(sReadValues[doneDev]);
                                    valuesList.Add(BitConverter.ToUInt16(byteArray, 0));
                                }
                                doneDev += 1;
                                break;


                            case TypeCode.Int32:
                                {
                                    var byteList = new List<byte>();
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev]));
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev + 1]));
                                    valuesList.Add(BitConverter.ToInt32(byteList.ToArray(), 0));                                    
                                }
                                doneDev += 2;
                                break;

                            case TypeCode.UInt32:
                                {
                                    var byteList = new List<byte>();
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev]));
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev + 1]));
                                    valuesList.Add(BitConverter.ToUInt32(byteList.ToArray(), 0));
                                }
                                doneDev += 2;
                                break;


                            case TypeCode.Single:
                                {
                                    var byteList = new List<byte>();
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev]));
                                    byteList.AddRange(BitConverter.GetBytes(sReadValues[doneDev + 1]));
                                    valuesList.Add(BitConverter.ToSingle(byteList.ToArray(), 0));
                                }
                                doneDev += 2;
                                break;

                            default:
                                valuesList.Add(sReadValues[doneDev]);
                                doneDev += 1;
                                break;
                        }
                    }

                    #endregion
                }

                values = valuesList.ToArray();
            }

            return iRet;
        }


        public int WriteDeviceRandom(IEnumerable<string> deviceList, IEnumerable<object> values)
        {
            // 19.01.07 追加
            if (deviceList.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException("空白か null のデバイス名が含まれています。");
            }

            // 19.01.07 追加
            values.Where((x, i) => x == null).Select((x, i) =>
            {
                var target = values.ElementAt(i);
                target = 0;
                return 0;
            });

            // 実際に読み取るデバイスのリスト　※float とInt32 は２つのデバイスを取得する必要がある
            var actualDeviceNameList = new List<string>();
            var actualValueList = new List<short>();


            // 型の種類によってデバイス名を調整する
            for (int dIdx = 0; dIdx < deviceList.Count(); dIdx++)
            {
                Func<string> nextDeviceName = () =>
                {
                    #region

                    string devLetter = "D";
                    int devNumber = 0;

                    // 隣のデバイス名を作成する
                    // デバイス番号表記は16進数、10進数、8進数の混在だが、ワードデバイスは10進数のみ
                    this.deviceLetterList.ForEach(x =>
                    {
                        int startIdx = deviceList.ElementAt(dIdx).IndexOf(x);

                        if (startIdx != -1)
                        {
                            devLetter = x;
                            string numString = deviceList.ElementAt(dIdx).Substring(devLetter.Length, deviceList.ElementAt(dIdx).Length - devLetter.Length);
                            int.TryParse(numString, out devNumber);
                        }
                    });

                    return devLetter + (devNumber + 1).ToString();

                    #endregion
                };

                #region region

                switch (values.ElementAt(dIdx))
                {
                    case Int32 iVal:
                        {
                            actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                            actualDeviceNameList.Add(nextDeviceName.Invoke());
                            var byteArray = BitConverter.GetBytes(iVal);
                            short sVal1 = BitConverter.ToInt16(byteArray.Take(2).ToArray(), 0);
                            short sVal2 = BitConverter.ToInt16(byteArray.Skip(2).Take(2).ToArray(), 0);

                            // バイトの追加順番に注意！
                            actualValueList.Add(sVal1);
                            actualValueList.Add(sVal2);
                        }
                        break;

                    case UInt16 sVal:
                        {
                            actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                            var byteArray = BitConverter.GetBytes(sVal);
                            short sVal1 = BitConverter.ToInt16(byteArray, 0);

                            actualValueList.Add(sVal1);
                        }
                        break;

                    case UInt32 uiVal:
                        {
                            actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                            actualDeviceNameList.Add(nextDeviceName.Invoke());
                            var byteArray = BitConverter.GetBytes(uiVal);
                            short sVal1 = BitConverter.ToInt16(byteArray.Take(2).ToArray(), 0);
                            short sVal2 = BitConverter.ToInt16(byteArray.Skip(2).Take(2).ToArray(), 0);

                            // バイトの追加順番に注意！
                            actualValueList.Add(sVal1);
                            actualValueList.Add(sVal2);
                        }
                        break;

                    case Single fVal:
                        {
                            actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                            actualDeviceNameList.Add(nextDeviceName.Invoke());
                            var byteArray = BitConverter.GetBytes(fVal);
                            short sVal1 = BitConverter.ToInt16(byteArray.Take(2).ToArray(), 0);
                            short sVal2 = BitConverter.ToInt16(byteArray.Skip(2).Take(2).ToArray(), 0);

                            // バイトの追加順番に注意！
                            actualValueList.Add(sVal1);
                            actualValueList.Add(sVal2);
                        }
                        break;

                    default:    // 16bit int （三菱シーケンサ内のシングル）として扱う
                        actualDeviceNameList.Add(deviceList.ElementAt(dIdx));
                        actualValueList.Add((short)values.ElementAt(dIdx));
                        break;
                }

                # endregion
            }

            // MXComponent のメソッドに投げるデバイス名
            string devices = string.Join("\n", actualDeviceNameList);

            // List は ref で使えないらしい
            var valueArray = actualValueList.ToArray();

            // 配列じゃないと駄目？
            return base.WriteDeviceRandom2(devices, actualValueList.Count, ref valueArray[0]);
        }


        public ActControlSettingFormat Setting
        {
            set
            {
                // 標準設定
                base.ActStationNumber = 255; //通信設定ユーティリティで設定した倫理局番の事 ※Progでは未使用なはずだが、初期値255を入れておかないとエラーになる
                base.ActPortNumber = 0;     // PC 側のポート番号。 =0 で自動選択
                base.ActThroughNetworkType = 0x01;

                // ユーザーからの入力を適応
                base.ActProtocolType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)value.ActProtocolType;
                base.ActCpuType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActCpuType)value.ActCpuType;
                base.ActUnitType = (int)(Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType)value.ActUnitType;


                // Ethernet
                base.ActHostAddress = value.ActHostAddress;

                // Serial
                base.ActPortNumber = (int)value.PortNumber;
                base.ActBaudRate = (int)value.BaudRate;
                base.ActDataBits = (int)value.DataBits;
                base.ActParity = (int)value.ParityBits;
                base.ActControl = (int)value.ActControl;
            }
        }


        #region region - 全メンバーのオーバーライド：メソッド


        public override int Open() => base.Open();

        public override int Close() => base.Close();

        public override int GetCpuType(out string szCpuName, out int lplCpuCode) => base.GetCpuType(out szCpuName, out lplCpuCode);

        public override int SetCpuStatus(int lOperation) => base.SetCpuStatus(lOperation);

        public override int ReadDeviceBlock(string szDevice, int lSize, out int lplData) => base.ReadDeviceBlock(szDevice, lSize, out lplData);

        public override int WriteDeviceBlock(string szDevice, int lSize, ref int lplData) => base.WriteDeviceBlock(szDevice, lSize, ref lplData);

        public override int ReadDeviceRandom(string szDeviceList, int lSize, out int lplData) => base.ReadDeviceRandom(szDeviceList, lSize, out lplData);

        public override int WriteDeviceRandom(string szDeviceList, int lSize, ref int lplData) => base.WriteDeviceRandom(szDeviceList, lSize, ref lplData);

        public override int ReadBuffer(int lStartIO, int lAddress, int lSize, out short lpsData) => base.ReadBuffer(lStartIO, lAddress, lSize, out lpsData);

        public override int WriteBuffer(int lStartIO, int lAddress, int lSize, ref short lpsData) => base.WriteBuffer(lStartIO, lAddress, lSize, ref lpsData);

        public override int GetClockData(out short lpsYear, out short lpsMonth, out short lpsDay, out short lpsDayOfWeek, out short lpsHour, out short lpsMinute, out short lpsSecond) => base.GetClockData(out lpsYear, out lpsMonth, out lpsDay, out lpsDayOfWeek, out lpsHour, out lpsMinute, out lpsSecond);

        public override int SetClockData(short sYear, short sMonth, short sDay, short sDayOfWeek, short sHour, short sMinute, short sSecond) => base.SetClockData(sYear, sMonth, sDay, sDayOfWeek, sHour, sMinute, sSecond);

        //public override int SetDevice(string szDevice, int lData) => base.SetDevice(szDevice, lData);

        public override int CheckDeviceString(string szDevice, int lCheckType, int lSize, out int lplDeviceType, out string lpszDeviceName, out int lplDeviceNumber, out int lplDeviceRadix) => base.CheckDeviceString(szDevice, lCheckType, lSize, out lplDeviceType, out lpszDeviceName, out lplDeviceNumber, out lplDeviceRadix);

        public override int EntryDeviceStatus(string szDeviceList, int lSize, int lMonitorCycle, ref int lplData) => base.EntryDeviceStatus(szDeviceList, lSize, lMonitorCycle, ref lplData);

        public override int FreeDeviceStatus() => base.FreeDeviceStatus();

        public override int ReadDeviceBlock2(string szDevice, int lSize, out short lpsData) => base.ReadDeviceBlock2(szDevice, lSize, out lpsData);
        public override int WriteDeviceBlock2(string szDevice, int lSize, ref short lpsData) => base.WriteDeviceBlock2(szDevice, lSize, ref lpsData);
        public override int ReadDeviceRandom2(string szDeviceList, int lSize, out short lpsData) => base.ReadDeviceRandom2(szDeviceList, lSize, out lpsData);
        public override int WriteDeviceRandom2(string szDeviceList, int lSize, ref short lpsData) => base.WriteDeviceRandom2(szDeviceList, lSize, ref lpsData);
        public override int GetDevice2(string szDevice, out short lpsData) => base.GetDevice2(szDevice, out lpsData);
        public override int SetDevice2(string szDevice, short sData) => base.SetDevice2(szDevice, sData);
        public override int Connect() => base.Connect();
        public override int Disconnect() => base.Disconnect();
        public override int GetCpuListOnEther(int lTimeOut, int lCpuListSize, out int lplCpuList, out int lplCpuListCount) => base.GetCpuListOnEther(lTimeOut, lCpuListSize, out lplCpuList, out lplCpuListCount);

        #endregion


        #region region - 全メンバーのオーバーライド：プロパティ

        public override int ActUnitType { get => base.ActUnitType; set => base.ActUnitType = value; }
        public override int ActProtocolType { get => base.ActProtocolType; set => base.ActProtocolType = value; }
        public override int ActNetworkNumber { get => base.ActNetworkNumber; set => base.ActNetworkNumber = value; }
        public override int ActStationNumber { get => base.ActStationNumber; set => base.ActStationNumber = value; }
        public override int ActUnitNumber { get => base.ActUnitNumber; set => base.ActUnitNumber = value; }
        public override int ActConnectUnitNumber { get => base.ActConnectUnitNumber; set => base.ActConnectUnitNumber = value; }
        public override int ActIONumber { get => base.ActIONumber; set => base.ActIONumber = value; }
        public override int ActCpuType { get => base.ActCpuType; set => base.ActCpuType = value; }
        public override int ActPacketType { get => base.ActPacketType; set => base.ActPacketType = value; }
        public override int ActPortNumber { get => base.ActPortNumber; set => base.ActPortNumber = value; }
        public override int ActBaudRate { get => base.ActBaudRate; set => base.ActBaudRate = value; }
        public override int ActDataBits { get => base.ActDataBits; set => base.ActDataBits = value; }
        public override int ActParity { get => base.ActParity; set => base.ActParity = value; }
        public override int ActStopBits { get => base.ActStopBits; set => base.ActStopBits = value; }
        public override int ActControl { get => base.ActControl; set => base.ActControl = value; }
        public override string ActHostAddress { get => base.ActHostAddress; set => base.ActHostAddress = value; }
        public override int ActCpuTimeOut { get => base.ActCpuTimeOut; set => base.ActCpuTimeOut = value; }
        public override int ActTimeOut { get => base.ActTimeOut; set => base.ActTimeOut = value; }
        public override int ActSumCheck { get => base.ActSumCheck; set => base.ActSumCheck = value; }
        public override int ActSourceNetworkNumber { get => base.ActSourceNetworkNumber; set => base.ActSourceNetworkNumber = value; }
        public override int ActSourceStationNumber { get => base.ActSourceStationNumber; set => base.ActSourceStationNumber = value; }
        public override int ActDestinationPortNumber { get => base.ActDestinationPortNumber; set => base.ActDestinationPortNumber = value; }
        public override int ActDestinationIONumber { get => base.ActDestinationIONumber; set => base.ActDestinationIONumber = value; }
        public override int ActMultiDropChannelNumber { get => base.ActMultiDropChannelNumber; set => base.ActMultiDropChannelNumber = value; }
        public override int ActThroughNetworkType { get => base.ActThroughNetworkType; set => base.ActThroughNetworkType = value; }
        public override int ActIntelligentPreferenceBit { get => base.ActIntelligentPreferenceBit; set => base.ActIntelligentPreferenceBit = value; }
        public override int ActDidPropertyBit { get => base.ActDidPropertyBit; set => base.ActDidPropertyBit = value; }
        public override int ActDsidPropertyBit { get => base.ActDsidPropertyBit; set => base.ActDsidPropertyBit = value; }
        public override string ActPassword { get => base.ActPassword; set => base.ActPassword = value; }
        public override int ActTargetSimulator { get => base.ActTargetSimulator; set => base.ActTargetSimulator = value; }
        public override int ActConnectWay { get => base.ActConnectWay; set => base.ActConnectWay = value; }
        public override string ActATCommand { get => base.ActATCommand; set => base.ActATCommand = value; }
        public override string ActDialNumber { get => base.ActDialNumber; set => base.ActDialNumber = value; }
        public override string ActOutsideLineNumber { get => base.ActOutsideLineNumber; set => base.ActOutsideLineNumber = value; }
        public override string ActCallbackNumber { get => base.ActCallbackNumber; set => base.ActCallbackNumber = value; }
        public override int ActLineType { get => base.ActLineType; set => base.ActLineType = value; }
        public override int ActConnectionCDWaitTime { get => base.ActConnectionCDWaitTime; set => base.ActConnectionCDWaitTime = value; }
        public override int ActConnectionModemReportWaitTime { get => base.ActConnectionModemReportWaitTime; set => base.ActConnectionModemReportWaitTime = value; }
        public override int ActDisconnectionCDWaitTime { get => base.ActDisconnectionCDWaitTime; set => base.ActDisconnectionCDWaitTime = value; }
        public override int ActDisconnectionDelayTime { get => base.ActDisconnectionDelayTime; set => base.ActDisconnectionDelayTime = value; }
        public override int ActTransmissionDelayTime { get => base.ActTransmissionDelayTime; set => base.ActTransmissionDelayTime = value; }
        public override int ActATCommandResponseWaitTime { get => base.ActATCommandResponseWaitTime; set => base.ActATCommandResponseWaitTime = value; }
        public override int ActPasswordCancelResponseWaitTime { get => base.ActPasswordCancelResponseWaitTime; set => base.ActPasswordCancelResponseWaitTime = value; }
        public override int ActATCommandPasswordCancelRetryTimes { get => base.ActATCommandPasswordCancelRetryTimes; set => base.ActATCommandPasswordCancelRetryTimes = value; }
        public override int ActCallbackCancelWaitTime { get => base.ActCallbackCancelWaitTime; set => base.ActCallbackCancelWaitTime = value; }
        public override int ActCallbackDelayTime { get => base.ActCallbackDelayTime; set => base.ActCallbackDelayTime = value; }
        public override int ActCallbackReceptionWaitingTimeOut { get => base.ActCallbackReceptionWaitingTimeOut; set => base.ActCallbackReceptionWaitingTimeOut = value; }

        #endregion

    }

    #endregion


    public static class Tools
    {
        public static string GetErrorMessage(int errorCode)
        {
            // 19.03.06 実験
            uint code = (uint)errorCode;

            switch (code)
            {
                case 0x04072001: return "オープンエラー - 変換オブジェクトの生成に失敗した。";

                case 0xF1000020: return "GX Simulator3未起動エラー";
                case 0x01801006: return "指定ユニットエラー - システム構成や設定パラメータがおかしい。";
                case 0x01802007: return "受信データエラー - 受信したデータが異常なデータである。";
                case 0x01808008: return "ポート接続エラー - Port/IP 相手先が応答しない。";
                case 0x0180840B: return "タイムアウトエラー - タイムアウト時間を経過してもデータ受信できなかった。";
                case 0x01808502: return "USB ドライバコネクトエラー";
                case 0x01809001: return "GX Simulator2 未起動エラー - USB ドライバのコネクトに失敗。";

                // 以下、uint
                case 0xf0000003: return "オープン済みエラー";

                default: return null;
            }
        }
    }


    #region  region - ExtensionMethod_Mitsubishi

    static class ExtensionMethod_Mitsubishi
    {
        public static int SetDevice(this ActProgTypeLib.ActProgTypeClass act, string szDevice, float lplData)
        {
            // bit 配列を作成
            var bits = new bool[32];


            // ゼロの時はゼロ
            if (lplData != 0)
            {
                // 符号
                bits[bits.Length - 1] = lplData < 0;

                // 絶対値を取得
                double value_abs = System.Math.Abs(lplData);

                //仮数_整数部
                int val_integer = (int)value_abs;

                // 虚数部を二進数で表したリスト
                var fractionList = new List<bool>();

                #region region - 仮数_整数部の桁数の計算

                // 整数部の桁数
                int exp_int_digit = 0;

                // 先に２の何乗から計算を始めたらいいかを探す
                while (Math.Pow(2, exp_int_digit) <= val_integer)
                {
                    exp_int_digit++;
                }

                // 指数は一つ下まででいいはずなのでマイナス１する
                exp_int_digit--;

                #endregion

                if (fractionList.Count > 0 && !fractionList[0])
                {
#if DEBUG
                    // 最初のビットは必ず１になるはず
                    System.Diagnostics.Debugger.Break();
#endif
                }

                #region  region - 仮数_小数部の計算

                // 指数の桁数の補正値
                int correction_digit = 0;

                // 仮数部_小数部の計算
                for (int c = 0; c < 24; c++)
                {
                    // 開発用モニタ
                    double calVal = Math.Pow(2, (exp_int_digit - c));

                    if (value_abs - calVal >= 0)
                    {
                        // ２進数のフラグを追加: true
                        fractionList.Add(true);

                        // 整数部を減算しておく
                        value_abs = value_abs - calVal;
                    }
                    else
                    {
                        if (fractionList.Count == 0)
                            // 指数部の桁数補正値
                            correction_digit++;
                        else
                            // ２進数のフラグを追加: false
                            fractionList.Add(false);
                    }

                    // 割り切れたかどうか判断する                    
                    if (value_abs == 0) break;
                }

                // 指数の桁数補正  ※127 は 8bit(=256) を±で割り振る為のバイアス値
                int exponent = exp_int_digit - correction_digit + 127;

                #endregion



                // 仮数部のビットを反転させる
                fractionList.Reverse();

                // 最高ビットを削除
                fractionList.RemoveAt(fractionList.Count - 1);

                #region region - 指数部を２進数に

                // 虚数部を二進数で表したリスト
                var exponentList = new List<bool>();

                int expMax_exp = 0;

                // 先に２の何乗から計算を始めたらいいかを探す
                while (Math.Pow(2, expMax_exp) <= exponent)
                {
                    expMax_exp++;
                }

                // 指数は一つ下まででいいはずなのでマイナス１する
                expMax_exp--;

                for (int c = 0; c < 8; c++)
                {
                    // 今回のループの乗数
                    int exp = expMax_exp - c;

                    // 2~0 まで計算が終わったら、指数計算を終える
                    if (exp < 0) break;

                    // 開発用モニタ
                    int calVal = (int)Math.Pow(2, exp);

                    if (exponent - calVal >= 0)
                    {
                        // ２進数のフラグを追加: true
                        exponentList.Add(true);

                        // 整数部を減算しておく
                        exponent = exponent - calVal;
                    }
                    else
                        // ２進数のフラグを追加: false
                        exponentList.Add(false);
                }

                // 反転させる
                exponentList.Reverse();

                #endregion


                #region region - bool 配列に代入

                // 指数部の代入
                for (int i = 0; i < 8; i++)
                {
                    if (exponentList.Count - 1 < i) break;

                    bits[23 + i] = exponentList[i];
                }

                // 仮数数部の代入
                for (int i = 22; i >= 0; i--)
                {
                    int idx = (fractionList.Count - 1) - (22 - i);

                    if (idx < 0) break;

                    bits[i] = fractionList[idx];
                }

                #endregion

            }

            // ビット配列を扱うクラスのインスタンス
            var bitArray = new System.Collections.BitArray(bits);

            // 格納先の変数を宣言
            var shortVal = new byte[4];

            // 16bit int (short) の配列に変換
            bitArray.CopyTo(shortVal, 0);

            //第１バイト(short)
            short shortVal_1 = BitConverter.ToInt16(shortVal, 0);

            //第２バイト(short)
            short shortVal_2 = BitConverter.ToInt16(shortVal, 2);

            // 書き込みの際に用いる変数
            var writeData = new short[2] { shortVal_1, shortVal_2 };

            // 三菱のライブラリを使用してPLC に書き込み
            int ret = act.WriteDeviceBlock2(szDevice, 2, ref writeData[0]);

            // 戻り値を返す
            return ret;
        }

        public static int GetDevice(this ActProgTypeLib.ActProgTypeClass act, string szDevice, out float lplData)
        {

            // 戻り値
            int ret;

            // PLC からの値を格納する変数
            short[] longValue = new short[2];

            // 2words => 2 x 16bit => 32 bit なので int でよさそうだが、 内部で short (16 bit) 処理されるので、配列は2つ必要
            //ret = act.ReadDeviceBlock(szDevice, 2, out longValue[0]);
            // どうせ 16bit(short) なのだから、short の Method を用いる
            ret = act.ReadDeviceBlock2(szDevice, 2, out longValue[0]);

            // byte 配列に変換
            var byteData = BitConverter.GetBytes((ushort)longValue[0]).ToList();
            var byteData_2 = BitConverter.GetBytes((ushort)longValue[1]);

            // ２つのList を合成
            byteData.AddRange(byteData_2);

            // bit 配列に変換
            var bits = new System.Collections.BitArray(byteData.ToArray());


            #region region - 仮数部の計算

            //仮数部
            double fraction = 0;

            //仮数部 下から7bit分 => 0 - 127 まで
            for (int i = 0; i <= 22; ++i)
            {
                int pow = i - 23;

                if (bits.Get(i))
                {
                    double cur = Math.Pow(2, pow);
                    fraction += cur;
                }
            }

            #endregion


            #region region - 指数部の計算

            // 2 の 0 乗を足す（=1 だが)
            fraction += Math.Pow(2, 0);

            // 指数部
            int exponent = 0;

            for (int i = 23; i <= 30; ++i)
            {
                int pow = i - 23;

                if (bits.Get(i))
                {
                    int cur = (int)Math.Pow(2, pow);
                    exponent += cur;
                }
            }

            #endregion


            // out 引数に値を代入
            lplData = (float)(fraction * Math.Pow(2, (exponent - 127)));

            // 符号
            bool isMinus = bits.Get(bits.Length - 1);

            // 符号をつける
            if (isMinus) lplData *= -1;

            // 戻り値を返す
            return ret;
        }

        /*
        // int 版の拡張メソッドを作る事は不可能。継承して override するしかない。18.05.21
        public static int ReadDeviceRandom(string szDeviceList, ref object[] values)
        {
            // あらかじめすべて32ビットずつ
        }
        */
        //        public static int WriteDeviceRandom2(string szDeviceList, int lSize, ref short lpsData) => base.WriteDeviceRandom2(szDeviceList, lSize, ref lpsData);

    }

    #endregion

    #region  region - ExtensionMethod_Mitsubishi_Enum

    static class ExtensionMethod_Mitsubishi_Enum
    {
        public static string ToStringFromEnum(this ActUnitType arg)
        {
            switch (arg)
            {
                case ActUnitType.SIMULATOR: return "GX Simulator1";
                case ActUnitType.SIMULATOR2: return "GX/MT Simulator2";
                case ActUnitType.SIMULATOR3:return "GX Simulator3";

                case ActUnitType.Default:
                case ActUnitType.QNCPU:
                case ActUnitType.FXCPU:
                case ActUnitType.LNCPU:
                case ActUnitType.QNMOTION:
                case ActUnitType.QJ71C24:
                case ActUnitType.FX485BD:
                case ActUnitType.LJ71C24:
                case ActUnitType.QJ71E71:
                case ActUnitType.FXENET:
                case ActUnitType.FXENET_ADP:
                case ActUnitType.QNETHER:
                case ActUnitType.QNETHER_DIRECT:
                case ActUnitType.LNETHER:
                case ActUnitType.LNETHER_DIRECT:
                case ActUnitType.NZ2GF_ETB:
                case ActUnitType.NZ2GF_ETB_DIRECT:
                case ActUnitType.QNUSB:
                case ActUnitType.LNUSB:
                case ActUnitType.QNMOTIONUSB:
                case ActUnitType.G4QNCPU:
                case ActUnitType.CCLINKBOARD:
                case ActUnitType.MNETHBOARD:
                case ActUnitType.MNETGBOARD:
                case ActUnitType.CCIEFBOARD:
                case ActUnitType.QBF:
                case ActUnitType.QSS:
                case ActUnitType.A900GOT:
                case ActUnitType.GOT_QJ71E71:
                case ActUnitType.GOT_QNETHER:
                case ActUnitType.GOT_LNETHER:
                case ActUnitType.GOT_NZ2GF_ETB:
                case ActUnitType.GOTETHER_QNCPU:
                case ActUnitType.GOTETHER_QBUS:
                case ActUnitType.GOTETHER_LNCPU:
                case ActUnitType.FXETHER:
                case ActUnitType.FXETHER_DIRECT:
                case ActUnitType.GOTETHER_FXCPU:
                case ActUnitType.GOT_FXETHER:
                case ActUnitType.GOT_FXENET:
                case ActUnitType.RJ71C24:
                case ActUnitType.RJ71EN71:
                case ActUnitType.RETHER:
                case ActUnitType.RETHER_DIRECT:
                case ActUnitType.RUSB:
                case ActUnitType.RJ71EN71_DIRECT:
                case ActUnitType.GOT_RJ71EN71:
                case ActUnitType.GOT_RETHER:
                case ActUnitType.GOTETHER_RJ71C24:
                case ActUnitType.FXVCPU:
                case ActUnitType.FXVETHER:
                case ActUnitType.FXVETHER_DIRECT:
                case ActUnitType.GOT_FXVCPU:
                case ActUnitType.GOTETHER_FXVCPU:
                case ActUnitType.GOT_FXVETHER:
                case ActUnitType.LJ71E71:
                case ActUnitType.GOT_LJ71E71:
                case ActUnitType.GOTETHER_QN_ETHER:
                    return arg.ToString();

                default: return "";
            }
        }
    }

    #endregion
}