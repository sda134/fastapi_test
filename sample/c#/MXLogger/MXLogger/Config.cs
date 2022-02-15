using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public class ConfigFormat
    {
        public string LogFileSaveDirectory { get; set; }

        public int Interval_mSec { get; set; } = 1000;

        // 19.04.29 追加
        public bool NeedCSVFieldsSorted { get; set; } = false;

        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat ActSetting { get; set; }
    }


    public class Config
    {
        public static string DirectoryPath => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


        public static string FileNameFullPath => Config.DirectoryPath + @"\Config.xml";


        private static ConfigFormat _instance;


        public static ConfigFormat Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConfigFormat();

                // 18.12.12 追加
                if (string.IsNullOrWhiteSpace(_instance.LogFileSaveDirectory)||
                    !System.IO.Directory.Exists(_instance.LogFileSaveDirectory))
                {
                    // 標準logフォルダすらなかったら作成する
                    if (!System.IO.Directory.Exists(Config.DirectoryPath + @"\log"))
                        System.IO.Directory.CreateDirectory(Config.DirectoryPath + @"\log");

                    // 設定値の変更
                    _instance.LogFileSaveDirectory = Config.DirectoryPath + @"\log";
                }

                return _instance;
            }

            set => _instance = value;
        }


        #region region - save, load

        public static bool LoadFromXmlFile()
        {
#if NET45
            Task<bool> task = Task.Run(async () =>
            {
                _instance = null;
                _instance = await Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.DeserializeFromXmlFileAsync(Config.FileNameFullPath);

                return _instance != null;
            });
            return task.Result;
#else
            return Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.DeserializeFromXmlFile(Config.FileNameFullPath, out _instance);
#endif
        }

        public static bool SaveToXmlFile()
        {
#if NET45
            Task<bool> task = Task.Run(async () =>
            {
                return await
                    Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.SerializeToXmlFileAsync(Config.FileNameFullPath, _instance);
            });

            return task.Result;
#else
            return Mtec.UtilityLibrary.Data.XMLSerializeOperator<ConfigFormat>.SerializeToXmlFile(Config.FileNameFullPath, _instance);
#endif
        }

        #endregion
    }
}
