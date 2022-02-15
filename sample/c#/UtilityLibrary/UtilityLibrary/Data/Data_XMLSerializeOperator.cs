using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//追加
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace UtilityLibrary.Data
{
    // 参考：http://www.atmarkit.co.jp/ait/articles/1704/19/news021.html

    /*
     プロジェクトファイル( .csproj )に以下を追加する必要があります。
    <PropertyGroup>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 2.0 ">$(DefineConstants)NET10;NET20;$(DefineConstants)</DefineConstants>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 3.5 ">$(DefineConstants)NET10;NET20;NET30;NET35;$(DefineConstants)</DefineConstants>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 4.0 ">$(DefineConstants)NET10;NET20;NET30;NET35;NET40;$(DefineConstants)</DefineConstants>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 4.5 ">$(DefineConstants)NET10;NET20;NET30;NET35;NET40;NET45;$(DefineConstants)</DefineConstants>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 4.6 ">$(DefineConstants)NET10;NET20;NET30;NET35;NET40;NET45;NET46;$(DefineConstants)</DefineConstants>
        <DefineConstants Condition=" $(TargetFrameworkVersion.Replace('v', '')) &gt;= 4.7 ">$(DefineConstants)NET10;NET20;NET30;NET35;NET40;NET45;NET46;NET47;$(DefineConstants)</DefineConstants>
    </PropertyGroup>
     */


    public class XMLSerializeOperator<T>
    {
        

        #region region - localメンバ、publicプロパティ


        protected T _instance;

        public T Instance
        {
            get
            {
                // コンストラクタでインスタンス生成しているので、null になる可能性はかなり低いが
                if (this._instance == null)
                    this._instance = (T)Activator.CreateInstance(typeof(T), new object[] { });

                return this._instance;
            }
            set { this._instance = value; }
        }



        // ファイル保存場所
        protected string _fileNameFullPath;
           // System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\XmlFile";


        /// <summary>
        /// ファイル名にアクセスする、読み取り専用フィールド
        /// </summary>
        public string FileNameFullPath { get => this._fileNameFullPath; }



        // 排他ロックに使うSemaphoreSlimオブジェクト .net 4.0
#if NET40
        private System.Threading.SemaphoreSlim _semaphore = new System.Threading.SemaphoreSlim(1, 1);
#endif

        // 例外が発生した場合の処理をするイベントハンドラ
        //public EventHandler EventCallback = (obj, e) => { };
        // ↓ 18.09.03 変更
        public event XMLSerializeOperatorEventHandler EventCallback = (obj, e) => { };

        
       #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileNameFullPath">ファイルの保存パスを指定</param>
        public XMLSerializeOperator(string fileNameFullPath)
        {
            // クラスの private member で値を保持
            this._fileNameFullPath = fileNameFullPath;

            // ディレクトリ名
            string dir = System.IO.Path.GetDirectoryName(this._fileNameFullPath);

            try
            {
                // ディレクトリがなかったら作成　※これは例外になるので作成するが、基本的にはクラス使用先でマネージしてもらう
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                // インスタンス生成                    
                this._instance = (T)Activator.CreateInstance(typeof(T), new object[] { });
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs
                {
                    Message = ex.Message,
                    Ex = ex,
                });
            }
        }


        #region region - main methods


        public bool SerializeToXmlFile()
        {
            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (var streamWriter = new System.IO.StreamWriter(this._fileNameFullPath, false, Encoding.UTF8))
                {
                    //シリアル化し、XMLファイルに保存する
                    xmlSerializer.Serialize(streamWriter, this._instance);

                    //ファイルを閉じる
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs { Ex = ex });
                return false;
            }
            return true;
        }


        public bool DeserializeFromXmlFile()
        {
            //XmlSerializerオブジェクトを作成
            var serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            try
            {
                //読み込むファイルを開く
                using (var sr = new System.IO.StreamReader(this._fileNameFullPath, new System.Text.UTF8Encoding(false)))
                {
                    //XMLファイルから読み込み、逆シリアル化する
                    this._instance = (T)serializer.Deserialize(sr);

                    //ファイルを閉じる
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 読み書きが早く、自由にな編集を防ぐバイナリファイルで書き込みます
        /// </summary>
        public bool SerializeToBinaryXmlFile()
        {
            try
            {
                //BinaryFormatterオブジェクトを作成
                var bf = new BinaryFormatter();

                //ファイルを開く
                using (var fs = new System.IO.FileStream(this._fileNameFullPath, System.IO.FileMode.Create))
                {
                    //シリアル化し、バイナリファイルに保存する
                    bf.Serialize(fs, this._instance);

                    //閉じる
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new  XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }

            return true;
        }


        public bool DeserializeFromBinaryXmlFile()
        {
            try
            {
                using (var fs = new FileStream(this._fileNameFullPath, FileMode.Open, FileAccess.Read))
                {
                    var bf = new BinaryFormatter();
                    //読み込んで逆シリアル化する

                    this._instance = (T)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージの表示
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }

            return true;
        }


        #endregion

    
        #region region - async methods
#if NET45
        /// <summary>
        /// 読み書きが早く、自由にな編集を防ぐバイナリファイルで書き込みます
        /// </summary>
        public async System.Threading.Tasks.Task<bool> SerializeToBinaryXmlFileAsync()
        {
            await _semaphore.WaitAsync(); // ロックを取得する

            try
            {
                //BinaryFormatterオブジェクトを作成
                var bf = new BinaryFormatter();

                //ファイルを開く
                using (var fs = new System.IO.FileStream(this._fileNameFullPath, System.IO.FileMode.Create))
                {
                    //シリアル化し、バイナリファイルに保存する
                    bf.Serialize(fs, this._instance);

                    //閉じる
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs{ Ex = ex });
                this._instance = default(T);
                return false;
            }
            finally
            {
                _semaphore.Release(); // ロックを解放する
            }


            return true;
        }


        /// <summary>
        /// 読み書きが早く、自由にな編集を防ぐバイナリファイルを読み込みます
        /// </summary>
        public async System.Threading.Tasks.Task<bool> DeserializeFromBinaryXmlFileAsync()
        {
            await _semaphore.WaitAsync(); // ロックを取得する

            try
            {
                using (var fs = new FileStream(this._fileNameFullPath, FileMode.Open, FileAccess.Read))
                {
                    var bf = new BinaryFormatter();
                    //読み込んで逆シリアル化する

                    this._instance = (T)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージの表示
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }
            finally
            {
                _semaphore.Release(); // ロックを解放する
            }

            return true;
        }


        /// <summary>
        /// シリアライズします
        /// </summary>
        public async System.Threading.Tasks.Task<bool> SerializeToXmlFileAsync()
        {
            await _semaphore.WaitAsync(); // ロックを取得する

            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (var streamWriter = new StreamWriter(this._fileNameFullPath, false, Encoding.UTF8))
                {
                    await System.Threading.Tasks.Task.Run(() => xmlSerializer.Serialize(streamWriter, this._instance));
                    await streamWriter.FlushAsync();  // .NET Framework 4.5以降
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }
            finally
            {
                _semaphore.Release(); // ロックを解放する
            }

            return true;
        }


        /// <summary>
        /// デシリアライズします
        /// </summary>
        public async System.Threading.Tasks.Task<bool> DeserializeFromXmlFileAsync()
        {
            await _semaphore.WaitAsync(); // ロックを取得する

            try
            {
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                var xmlSettings = new System.Xml.XmlReaderSettings()
                {
                    CheckCharacters = false,
                };

                using (var streamReader = new StreamReader(this._fileNameFullPath, Encoding.UTF8))
                using (var xmlReader = System.Xml.XmlReader.Create(streamReader, xmlSettings))
                {
                    this._instance = await System.Threading.Tasks.Task.Run(() => (T)xmlSerializer.Deserialize(xmlReader));
                }
            }
            catch (Exception ex)
            {
                this.EventCallback.Invoke(this, new  XMLSerializeOperatorEventArgs { Ex = ex });
                this._instance = default(T);
                return false;
            }
            finally
            {
                _semaphore.Release(); // ロックを解放する
            }

            return true;
        }
#endif


        #endregion

        
        #region region - static member

        public static bool SerializeToXmlFile(string fileFullPath, T objectInstance)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            xml._instance = objectInstance;
            return xml.SerializeToXmlFile();
        }

        public static bool DeserializeFromXmlFile(string fileFullPath, out T objectInstance)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            xml.EventCallback += Xml_EventCallback;

            bool bRet =  xml.DeserializeFromXmlFile();
            objectInstance = xml._instance;

            return bRet;
        }

        private static void Xml_EventCallback(object sender, XMLSerializeOperatorEventArgs e)
        {

        }

        #endregion


        #region region - static async member

#if NET45

        public static async System.Threading.Tasks.Task<bool> SerializeToXmlFileAsync(string fileFullPath, T objectInstance)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            ;
            xml._instance = objectInstance;
            return await xml.SerializeToXmlFileAsync();
        }
        

        // 非同期メソッドは out, ref が使えないとの事
        public static async System.Threading.Tasks.Task<T> DeserializeFromXmlFileAsync(string fileFullPath)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            bool bRet = await xml.DeserializeFromXmlFileAsync();
            return xml._instance;
        }



        public static async System.Threading.Tasks.Task<bool> SerializeToBinaryXmlFileAsync(string fileFullPath, T objectInstance)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            ;
            xml._instance = objectInstance;
            return await xml.SerializeToBinaryXmlFileAsync();
        }
        


        public static async System.Threading.Tasks.Task<T> DeserializeFromBinaryXmlFileAsync(string fileFullPath)
        {
            var xml = new XMLSerializeOperator<T>(fileFullPath);
            bool bRet = await xml.DeserializeFromBinaryXmlFileAsync();
            return xml._instance;
        }


#endif

        #endregion

    }

    #region region - delegate

    public class XMLSerializeOperatorEventArgs : EventArgs
    {
        public Exception Ex { get; set; }

        public string Message { get; set; }

        public long ElapsedMilliseconds { get; set; }
    }


    [System.Runtime.InteropServices.ComVisible(true)]
    public delegate void XMLSerializeOperatorEventHandler(object sender, XMLSerializeOperatorEventArgs e);


    
    #endregion

}
