using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mtec.Internal.Mitsubishi.MXLogger
{
    public class LogFieldsFormat
    {
        public List<LogGroupFormat> LogGroups { get; set; }
    }

    public class LogConfig
    {
        public static string FileNameFullPath =>
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\LogFields.xml";


        private static LogFieldsFormat _instance;


        public static LogFieldsFormat Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogFieldsFormat();

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
            return Mtec.UtilityLibrary.Data.XMLSerializeOperator<LogFieldsFormat>.DeserializeFromXmlFile(LogConfig.FileNameFullPath, out _instance);
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
            return Mtec.UtilityLibrary.Data.XMLSerializeOperator<LogFieldsFormat>.SerializeToXmlFile(LogConfig.FileNameFullPath, _instance);
#endif
        }

        #endregion



        #region region - public methods

        public static void AddGroup(LogGroupFormat groupInfo)
        {
            if (LogConfig.Instance.LogGroups == null) LogConfig._instance.LogGroups = new List<LogGroupFormat>();

            LogConfig._instance.LogGroups.Add(groupInfo);
        }

        public static void DeleteGroup()
        {

        }

        #endregion
    }
}
