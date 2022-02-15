using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json; // 参照　からライブラリの追加が必要


namespace UtilityLibrary.Data
{
    public class JsonSerializeOperator<T>
    {
        protected string _fileNameFullPath;

        
        public readonly DataContractJsonSerializerSettings Settings =
            new DataContractJsonSerializerSettings 
			{
				UseSimpleDictionaryFormat = true,				
			};

        public event JsonSerializeOperatorEventHandler EventCallback = (obj, e) => { };


        public JsonSerializeOperator(string fileNameFullPath)
        {
            // クラスの private member で値を保持
            this._fileNameFullPath = fileNameFullPath;

            // ディレクトリ名
            string dir = System.IO.Path.GetDirectoryName(this._fileNameFullPath);

            // ディレクトリがなかったら作成　※これは例外になるので作成するが、基本的にはクラス使用先でマネージしてもらう
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            // ここで例外が起こる場合は、そのまま例外を起こす事。 20.01.14
        }



        #region region - main methods


        public bool SerializeToJsonFile(T instance)
        {
            try
            {
                using (var stream = new System.IO.FileStream(this._fileNameFullPath, System.IO.FileMode.Create))
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream:true, indent:true, indentChars:"  "))
                {
                    var ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T), Settings);
                    ser.WriteObject(writer, instance);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {         
                this.EventCallback.Invoke(this, new JsonSerializeOperatorEventArgs { Ex = ex });
                return false;
            }
            return true;
        }


        public bool DeserializeFromJsonFile(out T instance)
        {            
            try
            {
                instance = (T)Activator.CreateInstance(typeof(T), new object[] { });

                using (var stream = System.IO.File.OpenRead(this._fileNameFullPath))
//                using (var stream = new System.IO.FileStream(this._fileNameFullPath, System.IO.FileMode.Open))
                {
                    stream.Position = 0;  // 実験
                    var ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T), Settings);
                    instance = (T)ser.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new JsonSerializeOperatorEventArgs { Ex = ex });
                instance = default(T);
                return false;
            }

            return true;
        }


        #endregion



        #region region - static member

        public static bool SerializeToJsonFile(string fileFullPath, T objectInstance)
        {
            var jsn = new JsonSerializeOperator<T>(fileFullPath);
            return jsn.SerializeToJsonFile(objectInstance);
        }

        public static bool DeserializeFromJsonFile(string fileFullPath, out T objectInstance)
        {
            var jsn = new JsonSerializeOperator<T>(fileFullPath);
            return jsn.DeserializeFromJsonFile(out objectInstance);
        }

        private static void XmlJson_EventCallback_EventCallback(object sender, JsonSerializeOperatorEventArgs e)
        {

        }

        #endregion

    }

    #region region - delegate

    public class JsonSerializeOperatorEventArgs : EventArgs
    {
        public Exception Ex { get; set; }

        public string Message { get; set; }
    }


    [System.Runtime.InteropServices.ComVisible(true)]
    public delegate void JsonSerializeOperatorEventHandler(object sender, JsonSerializeOperatorEventArgs e);



    #endregion

}

