using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mtec.UtilityLibrary.Tools
{
    /// <summary>
    /// 無限ループが発生しそうな場所で使うクラス 17.12.25 スレッド処理がかなり発達してるので使わないと思う
    /// </summary>
    public class LoopCounter
    {
        /// <summary>
        /// ループ上限
        /// </summary>
        public int CountMax = 1000;

        /// <summary>
        /// プライベートメンバ。現在のループカウント値を保持
        /// </summary>
        private int countDt = 0;


        /// <summary>
        /// ループ上限値以上になった時にメッセージボックスを表示するか
        /// </summary>
        private bool UseMsgBox = false;


        /// <summary>
        /// 現在のループカウント値を保持。
        /// </summary>
        public int count
        {
            get { return countDt; }

            set
            {
                countDt = value;

                //設定した上限値を超えたらイベントを発生させる
                if (countDt >= CountMax)
                {
                    if (this.UseMsgBox)
                    {
                        System.Windows.Forms.MessageBox.Show(this.notice);
                    }
                    EventMethod.Invoke();

                }
            }
        }


        /// <summary>
        /// 上限を超えた時にメッセージボックスに表示するメッセージ
        /// </summary>
        public string notice = "loop count over";

        /// <summary>
        /// カウント上限超えた時にとる行動
        /// </summary>
        public Action EventMethod = () => { };

        /// <summary>
        /// カウントアップ
        /// </summary>
        public void DoCountUp()
        {
            count += 1;
        }

    } // Class LoopCounter


    /// <summary>
    /// StackFrameの、メソッド名とクラス名だけを取り扱いやすくしたクラス
    /// </summary>
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

            this.className = method.ReflectedType.FullName;
            this.methodName = method.Name;
        }

        public string methodName;
        public string className;

    }

    public static class Utility
    {
        private static void ShowNotification(string title, string content)
        {
            /*
            // 情報元
            // https://stackoverflow.com/questions/37541923/how-to-create-informative-toast-notification-in-uwp-app

            var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier(@"Microsoft.Office.Desktop_8wekyb3d8bbwe!Outlook");
            var toastXml = Windows.UI.Notifications.ToastNotificationManager.GetTemplateContent(Windows.UI.Notifications.ToastTemplateType.ToastText02);
            var toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode(title));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode(content));
            var toastNode = toastXml.SelectSingleNode("/toast");
            //            Windows.Data.Xml.Dom.XmlElement audio = toastXml.CreateElement("audio");
            //            audio.SetAttribute("src", "ms-winsoundevent:Notification.SMS");

            var toast = new Windows.UI.Notifications.ToastNotification(toastXml);
            //toast.ExpirationTime = DateTime.Now.AddSeconds(Config.Instance.ToastDisplaySeconds);  // これに値が入っているとメッセージが消えてしまう 0 ならOKかも

            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier(@"Microsoft.Office.Desktop_8wekyb3d8bbwe!Outlook").Show(toast);
            */
        }
    }


    // 確かこんなメソッドかプロパティあったと思う。18.11.30
    /*
    /// <summary>
    /// 便利な道具
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 指定した月の最後の日を返します
        /// </summary>
        public static int lastDay_ofMonth(DateTime dateTime)
        {
            DateTime firstDay_of_NextMonth = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1);
            return firstDay_of_NextMonth.AddDays(-1).Day;
        }
    }
    */

    static partial class ExtensionMethod
    {
    }
}
