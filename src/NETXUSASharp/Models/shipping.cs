namespace NETXUSASharp.Models
{
    public class shipping
    {
        public Enums.ShippingCarrier? shipper { get; set; }
        public Enums.ShippingMethod? method { get; set; }
        public decimal? quote { get; set; }
        public object options { get; set; }
    }
}