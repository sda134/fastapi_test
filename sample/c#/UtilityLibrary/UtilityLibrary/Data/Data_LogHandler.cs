using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//追加したもの
using System.Diagnostics; //StackFrame  mscorlib.dll の参照が必要


namespace UtilityLibrary.Data
{
    // 2011 年に書いたコードらしいので、注意する　17.09.29

    // 参考までに、簡易ログをとるには、このような方法もある
    // System.IO.File.AppendAllText(@"C:\data\log.txt", (DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + "Initialize log7" + Environment.NewLine));


    public static class LogHandler
    {
        //public static string LogDateTimeFormat = "MMdd-HH:mm:ss";
        //
        public static string LogDateTimeFormat = "MMdd-HH:mm:ss.fff";

        public static string DirectoryNameDateTimeFormat = "yyyy_MM";

        public static string FileNameDateTimeFormat = "MMdd_";

        public static string FileName = "Log.csv";

        public enum LogTypeEnum { CSV, Tab }

        public static LogTypeEnum LogType = LogTypeEnum.CSV;


        private static System.Threading.SemaphoreSlim semaphore_write = new System.Threading.SemaphoreSlim(1, 1); // (1,1) 


        public static string FileNameFullPath
        {
            get
            {
                //フォルダ名
                //string logDirPath = System.Environment.CurrentDirectory
                //    + @"\Log\" + DateTime.Now.ToString(LogHandler.DirectoryNameDateTimeFormat);
                // ↓ 18.06.07 変更
                string logDirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)
                    + @"\Log\" + DateTime.Now.ToString(LogHandler.DirectoryNameDateTimeFormat);


                //フォルダがなかったらログフォルダ名作成
                if (!System.IO.Directory.Exists(logDirPath))
                {
                    System.IO.Directory.CreateDirectory(logDirPath);
                }

                //ログファイルパス
                return logDirPath + @"\" + DateTime.Now.ToString(LogHandler.FileNameDateTimeFormat) + LogHandler.FileName;
            }
        }


        /// <summary>
        /// ログの書き込み。大本のメソッド
        /// </summary>
        /// <param name="log">書き込むデータの構造体</param>
        /// <param name="methodWrite">呼び出し元メソッドを書くか、書かないか</param>
        public static void WriteData(LogFormat log)
        {

#if DEBUG
            // 18.01.11 追加
            Console.WriteLine("log [{0:HH}:{0:mm}:{0:ss}]:{1} - {2}", DateTime.Now, log.Message, log.Detail);
#else
            // Relese バージョンは Debug ログを書き込まない
            if (log.LogType == LogTypeFlag.Debug) return;
#endif

            // メソッド名書き込み条件　※毎回書き込むのはログが見にくくなるため選択可能にする
            bool IsWriteMethod = (log.MethodWrite || log.LogType == LogTypeFlag.Error);

            // １つ前に実行されたメソッド情報
            var struck_1 = new StackFrameHandler(1);

            // csv ファイルからの復元も一応想定して、たとえ空白でも順番を固定して書き込むこととする 17.09.29
            var writeDataList = new List<string>
            {
                DateTime.Now.ToString(LogDateTimeFormat),

                log.LogType.ToStringFromEnum(),
                log.ID,

                log.Message,
                log.Detail,

                log.Ex != null? log.Ex.Message : "",

                IsWriteMethod? struck_1.ClassName:"",
                IsWriteMethod? struck_1.MethodName:"",
            };


            try
            {
                // Semaphore で書き込みを制御
                LogHandler.semaphore_write.Wait();

                //ストリームクラスのインスタンス生成
                using (var sw = new System.IO.StreamWriter
                    (LogHandler.FileNameFullPath, true, System.Text.Encoding.GetEncoding("Shift-JIS")))
                {

                    sw.WriteLine(string.Join(",", writeDataList));
                    //sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // ふつう false にはならないが、念の為
                if (LogHandler.semaphore_write.CurrentCount < 1 /*  = MaxCount */)
                {
                    // Semaphore を解放
                    LogHandler.semaphore_write.Release();
                }
            }


            // 例外が起こった場合の念のための処理
            if (LogHandler.semaphore_write.CurrentCount < 1 /*  = MaxCount */)
            {
                // 前回書いてる途中で強制終了した場合？
                // 意外と頻繁に発生する 18.01.16

                LogHandler.semaphore_write.Release();
            }
        }

        /// <summary>
        /// 日にちの文字列を返す
        /// </summary>
        static public string dateStr
        { get { return DateTime.Now.ToString("yyyy/MM/dd"); } }


        /// <summary>
        /// 時間の文字列を返す
        /// </summary>
        static public string timeStr
        //{ get { return DateTime.Now.ToString("HH:mm:ss"); } }
        // ↓ 17.11.09 ver 1.0.1.1 変更
        {
            get { return DateTime.Now.ToString("HH:mm:ss.fff"); }
        }
    }


    public class StackFrameHandler
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">StackFrameにいれるパラーメータと同じ感覚で使う</param>
        public StackFrameHandler(int value)
        {
            var sf = new System.Diagnostics.StackFrame(value + 1);

            System.Reflection.MethodBase method = sf.GetMethod();

            this.ClassName = method.ReflectedType.FullName;
            this.MethodName = method.Name;
        }

        public string MethodName { get; set; }
        public string ClassName { get; set; }

    }



    public class LogFormat
    {

        /// <summary>
        /// エラー、注意、警告等。ログのタイプを指定する
        /// </summary>
        public LogTypeFlag LogType { get; set; }     //ログのタイプ


        /// <summary>
        /// メッセージ。原則、使用する事
        /// </summary>
        public string Message { get; set; }          //メッセージ


        /// <summary>
        /// 詳細メッセージ。省略可
        /// </summary>
        public string Detail { get; set; }        //詳細


        /// <summary>
        /// ログにメソッド名を記録するかしないか
        /// </summary>
        public bool MethodWrite
        {
            get { return this._methodWrite; }
            set { this._methodWrite = value; }
        }   
        private bool _methodWrite = false;


        /// <summary>
        /// エラーの場合の為。tryでキャッチしたexをそのまま代入して下さい。
        /// </summary>
        public Exception Ex { get; set; }



        /// <summary>
        /// プログラムソース内のどこにあるのか、検索ですぐ分かるようにする
        /// </summary>
        public string ID;

    }


    public enum LogTypeFlag : uint
    {
        Log = 0,        //記録

        Error = 1,      //エラー
        Attention = 2,  //注意
        Warning = 3,    //警告

        Debug = 10,     //デバッグ
    }


    public static partial class ExtensionMethod
    {
        public static string ToStringFromEnum(this LogTypeFlag value)
        {
            switch (value)
            {
                case LogTypeFlag.Log: return "ログ";
                case LogTypeFlag.Error: return "エラー";
                case LogTypeFlag.Attention: return "注意";
                case LogTypeFlag.Warning: return "警告";
                case LogTypeFlag.Debug: return "デバッグ";
                default: return "";
            }
        }
    }
}
    

