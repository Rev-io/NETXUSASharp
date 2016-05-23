namespace NETXUSASharp.Models
{
    [System.Xml.Serialization.XmlType(Namespace = "https://www.netxusa.com")]
    public class order
    {
        public string resourceURL { get; set; }
        public string number { get; set; }
        public string PO { get; set; }
        public string type { get; set; }
        public string NETXUSASalesAssociate { get; set; }
        public string resellerUser { get; set; }
        public System.DateTime? createDate { get; set; }
        public System.DateTime? lastChangedDate { get; set; }
        public string status { get; set; }
        public Enums.YesNo? holdRequest { get; set; }
        [System.Xml.Serialization.XmlArrayItem("email")]
        public string[] emails { get; set; }
        public string defaultLocation { get; set; }
        public shipping shipping { get; set; }
        public tracking[] trackingInformation { get; set; }
        public address billingAddress { get; set; }
        public address shippingAddress { get; set; }
        public address endUserAddress { get; set; }
        public additionalOption[] additionalOptions { get; set; }
        public payment payment { get; set; }
        public Enums.YesNo? acceptTermsAndConditions { get; set; }
        public string BOSplit { get; set; }
        public lineItem[] lineItems { get; set; }
        public invoice[] invoices { get; set; }

        public int? GetOrderNumber()
        {
            return (number != null) && number.StartsWith("O-", true, System.Globalization.CultureInfo.CurrentCulture) ? (int?)int.Parse(number.Substring(2)) : null;
        }
    }
}