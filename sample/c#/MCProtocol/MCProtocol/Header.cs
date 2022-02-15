using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.UtilityLibrary.Mitsubishi.MCProtocol
{


    // C++ のヘッダーファイルみたいに設計図として使う。※実装したメソッドは消去
    public interface IMCComponent
    {
        void SetCpuStatus(CpuStatus status);

        void SetClockData(DateTime dateTime);

        int SetClockData(out DateTime dateTime);


        event EventHandler OnDeviceStatus;


        int ReadDeviceBlock(string startDevice, out float[] Data);



        int WriteBuffer(int lStartIO, int lAddress, int lSize, ref short lpsData);


        int WriteDeviceRandom(string szDeviceList, int lSize, ref int lplData);


        int WriteDeviceRandom(string[] deviceList, int[] data);

    }
}
