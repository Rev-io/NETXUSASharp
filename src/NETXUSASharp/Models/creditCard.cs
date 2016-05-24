namespace NETXUSASharp.Models
{
    public class creditCard
    {
        public Enums.CreditCardType? type { get; set; }
        public string number { get; set; }
        public int? expirationMonth { get; set; }
        public int? expirationYear { get; set; }
        public int? verificationNumber { get; set; }
    }
}