namespace NETXUSASharp.Enums
{
    public enum ShippingMethod
    {
        Ground,

        [System.Xml.Serialization.XmlEnum(Name = "3-day")]
        ThreeDay,

        [System.Xml.Serialization.XmlEnum(Name = "2-day")]
        TwoDay,

        Overnight,

        [System.Xml.Serialization.XmlEnum(Name = "Priority Overnight")]
        PriorityOvernight,

        [System.Xml.Serialization.XmlEnum(Name = "Overnight Early AM")]
        OvernightEarlyAM,

        [System.Xml.Serialization.XmlEnum(Name = "International Priority")]
        InternationalPriority,

        [System.Xml.Serialization.XmlEnum(Name = "International Economy")]
        InternationalEconomy
    }
}
