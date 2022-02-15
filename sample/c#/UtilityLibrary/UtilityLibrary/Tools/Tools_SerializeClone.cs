using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 使用意志を明確にする為、完全ユニークな名前空間とする 17.12.13
namespace UtilityLibrary.Tools.SerializeClone
{
    public static partial class StaticMethods
    {
        // 使用、未使用を選べるように名前空間をはっきりと区別する
        /// <summary>
        /// オブジェクトのディープコピーを返します。
        /// </summary>
        /// <remarks>指定の型のメンバが全てシリアライズ可能である必要があります</remarks>
        public static T GetSerializedClone<T>(T target) // 参考： http://l-s-d.sakura.ne.jp/2016/04/class_obj_copy/
        {
            T instance;

            using (var stream = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                try
                {
                    formatter.Serialize(stream, target);
                    stream.Position = 0;
                    instance = (T)formatter.Deserialize(stream);
                }
                finally
                {
                    stream.Close();
                }
            }
            return instance;
        }
    }


    // 使用、未使用を選べるように名前空間をはっきりと区別する
    static public partial class ExtensionMethod
    {
        // 拡張メソッド版
        public static T SerializeClone<T>(this T arg)
        {
            T instance;
            using (var stream = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                try
                {
                    formatter.Serialize(stream, arg);
                    stream.Position = 0;
                    instance = (T)formatter.Deserialize(stream);
                }
                finally
                {
                    stream.Close();
                }
            }
            return instance;
        }
    }
}