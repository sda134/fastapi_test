using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDeviceValuesManager
{
    public class ProductSetting
    {
        public static string DirectoryPath =>
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\ProductsData";
    }

        
    public class ProductSettingFormat
    {
        public string ProductName { get; set; }

        public List<SingleProductFormat> SingleProductsData { get; set; }

        public SingleProductFormat DefaultData { get; set; }
    }


    [Serializable]
    public class SingleProductFormat : ICloneable
    {
        public string RecordName { get; set; }

        public string SerialCode { get; set; }

        // 19.01.10_2 [ver 0.1.2.0] 追加
        public string Reference { get; set; }


        public Mtec.UtilityLibrary.Mitsubishi.MXComponent.ActControlSettingFormat ActControlSetting { get; set; }

        public List<PropertyGroupFormat> PropertyGroups { get; set; }

        // 以下、19.05.06 [ver 0.2.0.0] で追加
        public List<TextFieldGroupFormat>TextFieldGroups { get; set; }


        public object Clone() => this.MemberwiseClone();
    }


    [Serializable]
    public class PropertyGroupFormat : ICloneable
    {
        public string GroupName { get; set; }

        public List<Mtec.UtilityLibrary.Mitsubishi.DeviceFieldFormat> Fields;

        public object Clone() => this.MemberwiseClone();
    }


    // 以下、19.05.06 [ver 0.2.0.0] で追加
    [Serializable]
    public class TextFieldGroupFormat : ICloneable
    {
        public string GroupName { get; set; }


        public List<TextFieldFormat> Fields;

        public object Clone() => this.MemberwiseClone();
    }

    [Serializable]
    public class TextFieldFormat
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public string Remark { get; set; }
    }
}
