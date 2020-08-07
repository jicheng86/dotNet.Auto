using Auto.Web.Models;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Web.Helpers
{
    /// <summary>
    /// JsonConvert扩展类（Newtonsoft）
    /// </summary>
    public static class JsonConvertExtend
    {
        /// <summary>
        /// jsonText转换到class
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JsonToClass<T>(this string jsonText) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(jsonText));
        }

        /// <summary>
        /// jsonText转换到class（查询参数扩展）
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JsonToClass<T>(this JObject jsonText) where T : class, new()
        {
            if (jsonText == null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(jsonText));
        }
        /// <summary>
        /// 字符串转换成对象
        /// </summary>
        /// <typeparam name="T">目标class</typeparam>
        /// <param name="jsonText">待转换text</param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return default;
            };
            T t = JsonConvert.DeserializeObject<T>(jsonText);
            return t;
        }
        /// <summary>
        /// JsonToClasss
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="jsonText">Json字符串</param>
        /// <returns>实体</returns>
        public static T ParamsFromJson<T>(this string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return default;
            };
            T t = Activator.CreateInstance<T>();
            using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(t.GetType());
            return (T)serializer.ReadObject(memoryStream);
        }

        /// <summary>
        /// 实体转换成jsonText
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ClassToJson<T>(this T t)
        {
            if (t == null)
            {
                throw new ArgumentException(nameof(t));
            }
            string json = JsonConvert.SerializeObject(t);
            return json;
        }
        /// <summary>
        /// 转换成json，去掉为null的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ClassToJsonNoNull<T>(this T t)
        {
            if (t == null)
            {
                throw new ArgumentException(nameof(t));
            }
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(t, Formatting.Indented, jsonSetting);
            return json;
        }

    }
}
