using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{
    // ファイヤーウォール　ポート解放　一時的
    // https://github.com/ttsuki/ttsuki/blob/master/Net/UPnPWanService.cs


    // mcプロトコルの通信方法
    // http://ryoe777.blog.fc2.com/blog-entry-7.html


    public partial class MCComponent
    {

        #region region - 外から設定する public property


        public CpuType CpuType { get; set; }


        public CpuSeries CpuSeries
        {
            get
            {
                switch (this.CpuType)
                {
                    case CpuType.Q03UDVCPU: return CpuSeries.Melsec_QL;
                    case CpuType.FX3UCCPU: return CpuSeries.Default;
                    case CpuType.FX5UCPU: return CpuSeries.Default;
                    default: return CpuSeries.Default;
                }
            }
        }


        public CommunicationProtocol Protocol { get; set; } = CommunicationProtocol.TCP_IP;


        public MCProtocolFrame Frame { get; set; } = MCProtocolFrame._3EFrame;


        public MELSECType MELSECType { get; set; } = MELSECType.MELSEC_QL;


        public string RemoteIPAddress
        {
            get => this._remoteIPAddress;
            set => this._remoteIPAddress = value;
        }
        private string _remoteIPAddress;


        public int RemotePortNumber
        {
            get => this._remotePortNumber;
            set => this._remotePortNumber = value;
        }
        private int _remotePortNumber = -1;


        public bool UseOnDemand
        {
            set
            {
                // オンデマンド時はPC 番号をFE に指定するとの事 17.12.22
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// アクセス先のネットワークNo.を指定します。
        /// 初期値:0　範囲:1-239 ※ただし指定なし = 0
        /// </summary>
        public int NetWorkNumber
        {
            get => this._netWorkNumber;
            set
            {
                if (value < 0)
                    value = 0;

                if (value > 239)
                    value = 0;

                this._netWorkNumber = value;
            }
        }
        private int _netWorkNumber;


        /// <summary>
        /// アクセス先のネットワークユニットの局番を指定します。
        /// 初期値:255(0xFF)　範囲:1-120 ※ただし指定なし = 0
        /// </summary>
        public int PCNumber
        {
            get => this._pCNumber;
            set
            {
                if (value > 239)
                    value = 0;

                if (value < 0)
                    value = 0;

                this._netWorkNumber = value;

            }
        }
        private int _pCNumber = 0xFF;



        /// <summary>
        /// ネットワークを経由してマルチドロップ接続局にアクセスする場合に，マルチドロップ接続元ユニットの先頭入出力番号を指定します。
        /// 要求先ユニットI/O番号
        /// </summary>
        public int TargetIONumber
        {
            set
            {
                // 正直よくわからない 17.12.22
                // マルチドロップ接続局
                // マルチCPU システムの CPU ユニット
                // 二重化システムのCPU ユニット
                // 上記の時は固定値
                // マルチドロップ局にアクセスする場合

                throw new NotImplementedException();
            }
        }
        private int _targetIONumber = 0x03Ff; // = 1023


        /// <summary>
        /// ネットワークを経由してマルチドロップ接続局にアクセスする場合に，アクセス先ユニットの局番を指定します。
        /// 要求先ユニット局番号：範囲 0-31
        /// </summary>
        public int TargetUnitNumber
        {
            set
            {
                if (value < 0)
                    value = 0;

                if (value > 31)
                    value = 0;

                this._targetUnitNumber = value;

                // 正直よくわからない 17.12.22
                //throw new NotImplementedException();

            }
        }
        private int _targetUnitNumber = 0;


        /// <summary>
        /// 監視タイマ　範囲 0-65535; 0 は無限待ち
        /// </summary>
        /// <remarks>value * 250mSec となる</remarks>
        public int WatchTimer
        {
            get => this._watchTimer;
            set
            {
                if (value < 0)
                    value = 0;

                // if (value > 65535) value = 65535;
                // ↓ 18.01.26 変更 実際の待ち時間 mSec が 65500 になるように
                if (value > 262) value = 262;

                this._watchTimer = value;

            }
        }
        private int _watchTimer = 0x10; // = 16 * 250mSec = 4000mSec



        /// <summary>
        /// データ送信後の結果を待つ時間。単位 mSec
        /// </summary>
        /// <remarks>0 - 65500 [≒ 1min]</remarks>
        public int WatchTimerMiliseconds
        {
            get
            {
                if (_watchTimer == 0) return 65500;
                else
                    return this._watchTimer * 250;
            }
        }

        /*
                public int Timeout_SendDataCallBack
                {
                    get => this._timeout_SendDataCallBack;
                    set => this._timeout_SendDataCallBack = value;
                }
        //        public int _timeout_SendDataCallBack = 4000;
        */



        // 18.01.29 特に設定する必要がない事が判明

        public string LocalIPAddres
        {
            get => this._localIPAddress;

            set
            {
                this._localIPAddress = value;
            }
        }
        private string _localIPAddress = null;


        public int LocalPortNumber
        {
            get => this._localPortNumber;

            set
            {
                this._localPortNumber = value;
            }
        }
        private int _localPortNumber = 7000;

        #endregion

    }
}
