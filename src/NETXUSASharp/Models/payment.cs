namespace NETXUSASharp.Models
{
    public class payment
    {
        public string @default { get; set; }
        public string terms { get; set; }
        public creditCard creditCard { get; set; }
        public string COD { get; set; }
        public string other { get; set; }
    }
}