using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//以下、追加したもの
using System.Collections;//arraylist
using System.Windows.Forms;//messagebox


namespace Mtec.UtilityLibrary.Tools
{
    public static class Text
    {
        /// <summary>
        /// パラメータの文字列に、見やすくなるのに適したタブ数のタブをつけた値を返す
        /// </summary>
        /// <param name="msg">元の文字列</param>
        /// <param name="tabQtyMax">最大タブ数の設定値</param>
        /// <returns></returns>
        public static string tabAddProcess(string msg, int tabQtyMax)
        {
            if (msg == null) { return ""; }

            var Shift_JIS = System.Text.Encoding.GetEncoding("Shift_JIS");

            //文字数を求める
            int length = Shift_JIS.GetByteCount(msg);


            //タブ数を計算する
            int tabQty = ((tabQtyMax * 8) - length) / 8;

            //8で割り切れなければ１つ追加
            if ((length) % 8 != 0) { tabQty += 1; };

            //タブ数がマイナスだったらパラメータをそのまま返す
            if (tabQty < 0) { return msg; }

            //追加するタブ数
            var tabString = new string('\t', tabQty);

            return msg + tabString;
        }

        public static bool File_Search(string fileFullPath)
        {
            //ファイル有無 有る→1 ない→0
            //try
            if (System.IO.File.Exists(fileFullPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------

        public static bool txtWrite(string fileFullPath, List<string> msg, bool append)
        {
            System.IO.StreamWriter stream =
                new System.IO.StreamWriter(fileFullPath, append,//第二引数 append 追記する場合true 上書きの場合false

            System.Text.Encoding.GetEncoding("Shift-JIS"));

            try
            {
                foreach (string s in msg)
                {
                    stream.Write(s + "\r\n");//Cはバイナリが基本な為
                }
                stream.Close();//閉じる

                return true;
            }//try

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                stream.Close();//閉じる
                return false;
            }
        }

        public static bool TxtRead(string filename, ref List<string> msg)
        {
            System.IO.StreamReader stream = null;
            msg = new List<string>();

            try
            {
                stream = new System.IO.StreamReader(filename, System.Text.Encoding.GetEncoding("Shift-JIS"));

                string read = null;
                //恐らくデバッグで見やすくする為に設定した変数

                while (!(stream.EndOfStream))
                {
                    read = stream.ReadLine();
                    msg.Add(read);
                }

                stream.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //ArrayList ErrMsg_Collection = new ArrayList();
                //errorLog_make(ErrMsg_Collection, ex, "");
                return false;
            }//catch
            return true;
        }


        //-----------------------------------------------------------------------------
        public static bool IsNumeric(string stTarget)
        {
            double dNullable;

            return double.TryParse(stTarget, System.Globalization
                .NumberStyles.Any, null, out dNullable);
        }
        //-----------------------------------------------------------------------------

    }

}
