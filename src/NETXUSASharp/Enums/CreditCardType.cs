namespace NETXUSASharp.Enums
{
    public enum CreditCardType
    {
       Visa,
       MasterCard,
       [System.Xml.Serialization.XmlEnum(Name = "American Express")]
       AmericanExpress        
    }
}
