using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NETXUSASharp
{
    public static class Extensions
    {
        private const string DEFAULT_NAMESPACE = "https://www.netxusa.com";

        public static string ToXml<T>(this T value)
        {
            if (value == null) return null;
            using (var myStringWriter = new StringWriter())
            using (XmlWriter myXmlWriter = XmlWriter.Create(myStringWriter, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, }))
            {
                var myNamespaces = new XmlSerializerNamespaces();
                myNamespaces.Add("", DEFAULT_NAMESPACE);
                var mySerializer = new XmlSerializer(value.GetType(), DEFAULT_NAMESPACE);
                mySerializer.Serialize(myXmlWriter, value, myNamespaces);
                var myXml = myStringWriter.ToString();

                var myDoc = new XmlDocument();
                myDoc.LoadXml(myXml);

                XmlNamespaceManager myManager = new XmlNamespaceManager(myDoc.NameTable);
                myManager.AddNamespace("", DEFAULT_NAMESPACE);
                myManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                XmlNodeList nullFields = myDoc.SelectNodes("//*[@xsi:nil='true']", myManager);
                if (nullFields != null && nullFields.Count > 0)
                {
                    for (int i = 0; i < nullFields.Count; i++)
                    {
                        nullFields[i].ParentNode.RemoveChild(nullFields[i]);
                    }
                }
                using (var myStringWriter2 = new StringWriter())
                using (XmlWriter myXmlWriter2 = XmlWriter.Create(myStringWriter2, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true }))
                {
                    myDoc.WriteTo(myXmlWriter2);
                    myXmlWriter2.Flush();
                    myStringWriter2.Flush();
                    return myStringWriter2.ToString();
                }
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
