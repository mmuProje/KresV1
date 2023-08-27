using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Kres.Models.EntityLayer
{
    public static class XmlSerialization
    {
        /// <summary>
        /// XML Serialize
        /// </summary>
        public static string Serailize(this object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        /// <summary>
        /// XML Deserialize
        /// </summary>
        public static T Deserialize<T>(this string objectData)
        {
            return (T)Deserialize(objectData, typeof(T));
        }

        /// <summary>
        /// XML Deserialize
        /// </summary>
        public static object Deserialize(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}