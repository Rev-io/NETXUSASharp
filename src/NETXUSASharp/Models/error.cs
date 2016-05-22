namespace NETXUSASharp.Models
{
    public class error
	{
        public int? code { get; set; }
        public string message { get; set; }
        public Enums.YesNo? fixable { get; set; }
        public int? line { get; set; }
    }
}