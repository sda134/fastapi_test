using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mtec.UtilityLibrary.Mitsubishi;

namespace Mtec.Internal.Mitsubishi.MXDebugger
{
    public class ConfigFormat
    {
        public EncoderParameterFormat Encorder { get; set; } = new EncoderParameterFormat();

        public List<LogGroupFormat> FieldTables { get; set; }

        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActUnitType SimulatorType { get; set; }

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
