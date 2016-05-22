namespace NETXUSASharp.Models
{
    public class lineItem
    {
        public string manufacturer { get; set; }
        public string partNumber { get; set; }
        public provisioningTemplate provisioningTemplate { get; set; }
        public string productResourceURL { get; set; }
        public string productFullName { get; set; }
        public int? quantity { get; set; }
        public int? BOQuantity { get; set; }
        public int? shipQuantity { get; set; }
        public decimal? price { get; set; }
        public decimal? MSRP { get; set; }
        public Enums.YesNo? specialOrder { get; set; }
        public serialization[] serializationInformation { get; set; }
    }
}