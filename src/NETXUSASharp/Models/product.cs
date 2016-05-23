namespace NETXUSASharp.Models
{
    public class product
    {
        public string resourceURL { get; set; }
        public string manufacturer { get; set; }
        public string name { get; set; }
        public string partNumber { get; set; }
        public string productFullName { get; set; }
        public string shortDescription { get; set; }
        public decimal? price { get; set; }
        public decimal? MSRP { get; set; }
        public Enums.YesNo? specialOrder { get; set; }
        public service[] services { get; set; }
        public inventory inventory { get; set; }
    }
}