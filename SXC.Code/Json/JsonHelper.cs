using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Code.Json
{
    public class JsonHelper
    {
    }

    #region JsonCustomDecimalConvert
    public class JsonCustomDecimalConvert : CustomCreationConverter<decimal>
    {
        public virtual int Digits { get; private set; }

        public JsonCustomDecimalConvert()
        {
            this.Digits = 2;
        }

        public JsonCustomDecimalConvert(int digits)
        {
            this.Digits = digits;
        }

        public override decimal Create(Type objectType)
        {
            return 0.00M;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var formatter = ((decimal)value).ToString("N" + Digits.ToString());
                writer.WriteValue(formatter);
            }

        }
    }


    /// <summary>
    /// 自定义数值类型序列化转换器(无小数位)
    /// </summary>
    public class JsonCustomDecimalWith0DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 0; }
        }
    }

    /// <summary>
    /// 自定义数值类型序列化转换器(保留1位)
    /// </summary>
    public class JsonCustomDecimalWith1DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 1; }
        }
    }

    /// <summary>
    /// 自定义数值类型序列化转换器(保留2位)
    /// </summary>
    public class JsonCustomDecimalWith2DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 2; }
        }
    }

    /// <summary>
    /// 自定义数值类型序列化转换器(保留3位)
    /// </summary>
    public class JsonCustomDecimalWith3DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 3; }
        }
    }

    /// <summary>
    /// 自定义数值类型序列化转换器(保留4位)
    /// </summary>
    public class JsonCustomDecimalWith4DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 4; }
        }
    }

    /// <summary>
    /// 自定义数值类型序列化转换器(保留5位)
    /// </summary>
    public class JsonCustomDecimalWith5DigitsConvert : JsonCustomDecimalConvert
    {
        public override int Digits
        {
            get { return 5; }
        }
    }
    #endregion


    #region 数字小数位
    public class DecimalDigitsConverter : JsonConverter
    {
        public virtual int Digits { get; private set; }

        public DecimalDigitsConverter()
        {
            this.Digits = 2;
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);  
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var formatter = ((decimal)value).ToString("N" + Digits.ToString());
                writer.WriteValue(formatter);
            }
        }
    }

    #endregion

    #region 日期
    public class CommonDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
    #endregion
}
