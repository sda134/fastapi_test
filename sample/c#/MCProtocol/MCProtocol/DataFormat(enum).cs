﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{

    #region region - MC Protocol 関連


    public enum CpuSeries
    {
        Default,
        Melsec_QL,
        Melsec_iQ_R,
    }


    public enum CommunicationProtocol
    {
        TCP_IP,
        UDP_IP
    }


    public enum MCProtocolFrame
    {
        _3EFrame,
        _1EFrame,
    }

    public enum MELSECType
    {
        MELSEC_QL,
        MELSEC_iQ_R,
    }


    #endregion


    #region region - MX Component と同じ値にしているもの

    // 自身で試した CPU のみコメントアウトして有効にしていく
    public enum CpuType : int
    {
        Default = 0,
        
        /*
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
        */

        Q03UDVCPU = 0x00D1,            // Q03UDVCPU
        
        /*
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
        */

        FX3UCCPU = 0x0208,         // FX3U/FX3UC

        /*
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
        */

        // iQ-F CPU
        FX5UCPU = 0x0210,          //  FX5U CPU
    }

    #endregion


    #region region - 要求データ関連

    public enum UnitControlType
    {
        RemoteRun = 0x1001,

        RemoteStop = 0x1002,

        RemotePause = 0x1003,

        RemoteRatchClear = 0x1005,

        RemoteReset = 0x1006,

        GetCPUTypeName = 0x0101,
    }


    public enum SubHeader_1E : byte
    {
        ReadBit = 0x00,
        ReadWord = 0x01,
        WriteBit = 0x02,
        WriteWord = 0x03,
        BitRandomTest = 0x04,
        WordRandomTest = 0x05,
        //0x06, モニタデータ登録？
        //0x07, モニタデータ登録？
        BitMonitor = 0x08,
        WordMonitor = 0x09,

        Remote_Run = 0x13,
        Remote_Stop = 0x14,
        GetPCName = 0x15,
        ResponseTest = 0x16,        // 折り返しテスト


        RegisterRead = 0x17,
        RegisterWrite = 0x18,
        RegisterRandomTest = 0x19,
        RegisterMonitorRegister = 0x1A,
        RegisterMonitor = 0x1B,
        RegisterWriteDirect = 0x3B,
        RegisterReadDirect = 0x3C,
        UnitRead = 0x0E,
        UnitWrite = 0x0F,
    }

    public enum CpuStatus
    {
        Run = 0,
        Stop = 1,
        Pause = 2,
    }

    public enum ClearMode : byte
    {
        NoClear = 0x00,
        ClearExceptRatch = 0x01,
        AllClear = 0x02,
    }

    #endregion
}
