using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtec.UtilityLibrary.Tools
{
    public static class Clac
    {
        public static Type Max<Type>(Type a, Type b)
            where Type : IComparable //IComparableインターフェイスを継承している ※正直よく分からない 12 12 25
        {
            return a.CompareTo(b) > 0 ? a : b;
        }


        #region region - getRandom
        public static int GetRandom(int min, int max)
        {
            //11 01 15 チェック済み

            //Int32と同じサイズのバイト配列にランダムな値を設定する
            //byte[] bs = new byte[sizeof(int)];
            byte[] bs = new byte[4];    //Int32→4バイト
            System.Security.Cryptography.RNGCryptoServiceProvider rng =
                new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bs);

            //Int32に変換する
            int r = System.BitConverter.ToInt32(bs, 0);


            //-----乱数を指定の最大値と最小値の範囲に調整する-------------

            //最大値と最小値 逆転してる場合の対策
            int _max = System.Math.Max(max, min);
            int _min = System.Math.Min(max, min);

            int range = (_max - _min) + 1;
            //この値で余りを求める。余り→max[未満]になってしまう。
            //max[以下]にしたい為+1する。

            r = System.Math.Abs(r % range);   //余りを求める
            r += _min;                 //最小値分のトリム
            //-----------------------------------------------------------

            return r;
        }
        #endregion


        #region region - getRandomFromListT
        public static T GetRandomFromListT<T>(IEnumerable<T> a)
        {
            int qty = a.Count();
            int r = Clac.GetRandom(0, qty - 1);

            return a.ToArray()[r];
        }
        #endregion


        #region region limitedValue
        public static void limitedValue(ref int targetValue, int LowerLimit, int UpperLimit)
        {
            if (targetValue > UpperLimit) { targetValue = UpperLimit; }
            if (targetValue < LowerLimit) { targetValue = LowerLimit; }
        }
        public static void limitedValue(ref double targetValue, double LowerLimit, double UpperLimit)
        {
            if (targetValue > UpperLimit) { targetValue = UpperLimit; }
            if (targetValue < LowerLimit) { targetValue = LowerLimit; }
        }
        #endregion

    }//Clac


}//IsaLibrary.Tools
