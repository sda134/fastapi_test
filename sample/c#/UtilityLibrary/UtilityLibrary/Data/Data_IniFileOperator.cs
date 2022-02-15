using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtec.UtilityLibrary.Data
{
    public class IniFileOperator
    {

        #region region - DLL Import

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString
            (string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA")]
        public static extern uint GetPrivateProfileStringByByteArray
            (string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL")]
        public static extern uint WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region region - メンバフィールド
        //-------------------------------------------------------
        private string fileName;


        //-------------------------------------------------------
        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileFullPath">ファイルパスを指定して下さい</param>
        public IniFileOperator(string fileFullPath)
        {
            if (System.IO.File.Exists(fileFullPath))
            {
                this.fileName = fileFullPath;
            }
            else
            {
                // ファイルの作成と Open
                //var fs = System.IO.File.Create(fileFullPath);
                // //閉じる（ストリームはガベージコレクションに対応していない）
                // fs.Close();

                // ファイルの作成と Open
                // ↓ 変更 18.03.02　というか、いつ書いたコード？
                using (var fs = System.IO.File.Create(fileFullPath))
                {
                    fs.Close();
                }
            }             
        }


        /// <summary>
        /// iniファイルに値を書き込む
        /// </summary>
        public bool setData(string section, string key, string value)
        {
            // キーと値を書き加える
            uint nRet = WritePrivateProfileString(section, key, value, fileName);

            //BOOLはTRUEが１
            return (nRet != 0);
        }


        /// <summary>
        /// iniファイルから値を読み込む
        /// </summary>
        public bool getData(string section, string key, ref string value)
        {
            // データを格納する変数
            var sb = new StringBuilder(1024);

            // APIを利用してiniファイルのデータを読み込む
            uint nRet = GetPrivateProfileString
                (section, key, "default", sb, (uint)sb.Capacity, fileName);

            // パラメータに値を入れる
            value = sb.ToString();

            //BOOLはTRUEが１
            return (nRet != 0);

        }
        //----------------------------------------------------------------



        /// <summary>
        /// 第４パラメータがuintの時のオーバーロード
        /// </summary>
        public bool getData(string section, string key, ref uint value)
        {
            // 整数値を読み出す
            value = GetPrivateProfileInt(section, key, 0, fileName);

            // 読み込みに失敗した時は、第３引数の値が、０が返ってくる為
            return (value != 0);
        }
        //----------------------------------------------------------------



        /// <summary>
        /// 指定セクションのキーの一覧を得る
        /// </summary>
        public List<string> getKeyList(string section)
        {
            byte[] ar = new byte[1024];

            uint resultSize = GetPrivateProfileStringByByteArray
                (section, null, "default", ar, (uint)ar.Length, fileName);

            string sRet = Encoding.Default.GetString(ar, 0, (int)resultSize - 1);

            string[] keys = sRet.Split('\0');

            return new List<string>(keys);

        }
        //----------------------------------------------------------------



        /// <summary>
        /// 指定ファイルのセクションの一覧を得る
        /// </summary>
        /// <returns></returns>
        public List<string> getSectionList()
        {
            byte[] ar = new byte[1024];

            uint resultSize = GetPrivateProfileStringByByteArray
                (null, null, "default", ar, (uint)ar.Length, fileName);

            string result = Encoding.Default.GetString(ar, 0, (int)resultSize - 1);

            string[] sctArray = result.Split('\0');

            return new List<string>(sctArray);

        }
        //----------------------------------------------------------


        /// <summary>
        /// 1つのキーと値のペアを削除する
        /// </summary>
        public bool remove(string section, string key)
        {
            uint nRet = WritePrivateProfileString(section, key, null, fileName);

            //BOOLはTRUEが１
            return (nRet != 0);
        }


        /// <summary>
        /// 指定セクション内の全てのキーと値のペアを削除する
        /// </summary>
        public bool remove(string section)
        {
            uint nRet = WritePrivateProfileString(section, null, null, fileName);

            //BOOLはTRUEが１
            return (nRet != 0);
        }

    }
}