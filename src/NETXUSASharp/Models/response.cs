using System.Xml.Serialization;
using NETXUSASharp.Enums;

namespace NETXUSASharp.Models
{
    [XmlRoot(Namespace = "https://www.netxusa.com", ElementName = "response")]
    public class response<T> : Interfaces.IHttpLog 
        where T: body
    {
        public RequestResult requestResult { get; set; }
        public notice[] notices { get; set; }
        public error[] errors { get; set; }
        public T body { get; set; }

        [XmlIgnore]
        public HttpVerbs log_http_verb { get; set; }
        [XmlIgnore]
        public string log_request_body { get; set; }
        [XmlIgnore]
        public string log_response_body { get; set; }
        [XmlIgnore]
        public string log_url { get; set; }
    }
}