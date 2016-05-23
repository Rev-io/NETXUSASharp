using System.Xml.Serialization;
using NETXUSASharp.Enums;

namespace NETXUSASharp.Models
{
    public class result
    {
        public string resourceURL { get; set; }
        public string number { get; set; }
        public string PO { get; set; }

        public int? GetOrderNumber()
        {
            return (number != null) && number.StartsWith("O-", true, System.Globalization.CultureInfo.CurrentCulture) ? (int?)int.Parse(number.Substring(2)) : null;
        }
    }
}