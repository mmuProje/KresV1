using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Kres.Models.EntityLayer
{
    public class Serializer:DataAccess
    {
        public static T DeSerialize<T>(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.LoadXml(xml);

            XmlNodeReader reader = new XmlNodeReader(xmlDoc.DocumentElement);
            XmlSerializer ser = new XmlSerializer(typeof(T));

            return (T)ser.Deserialize(reader);
        }

        public static XmlDocument Serialize(object serializeObject)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer ser = new XmlSerializer(serializeObject.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            ser.Serialize(writer, serializeObject, ns);

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(sb.ToString());
            xmlDoc.RemoveChild(xmlDoc.FirstChild);

            return xmlDoc;
        }
    }
}