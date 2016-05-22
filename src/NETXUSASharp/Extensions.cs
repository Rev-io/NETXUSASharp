using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NETXUSASharp
{
    public static class Extensions
    {
        public static string ToXml<T>(this T value)
        {
            if (value == null) return null;
            using (var myStringWriter = new StringWriter())
            using (XmlWriter myXmlWriter = XmlWriter.Create(myStringWriter,
                new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true }))
            {
                var mySerializer = new XmlSerializer(value.GetType());
                mySerializer.Serialize(myXmlWriter, value);
                return myStringWriter.ToString();
            }
        }
        public static T ToObject<T>(this string value)
        {
            if (value == null || value == "") return default(T);
            var mySerializer = new XmlSerializer(Activator.CreateInstance<T>().GetType());
            return (T)mySerializer.Deserialize(new StringReader(value));
        }
    }
}
