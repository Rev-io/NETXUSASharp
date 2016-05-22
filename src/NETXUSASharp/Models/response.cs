using System.Xml.Serialization;

namespace NETXUSASharp.Models
{
    [XmlRoot(Namespace = "https://www.netxusa.com", ElementName = "response")]
    public class response<T> where T: body
    {
        public Enums.RequestResult requestResult { get; set; }
        public notice[] notices { get; set; }
        public error[] errors { get; set; }
        public T body { get; set; }
    }
}