using NETXUSASharp.Models;
namespace NETXUSASharp.Controllers
{
    /// <summary>
    /// Used to lookup products with NETXUSA:  get a single product (Get), search for a product (Search).
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Lookup an existing product by NETXUSA's manufacturer name and part number.
        /// </summary>
        public static response<body_product> Get(Connector aConnection, string manufacturer, string partNumber)
        {
            return aConnection.Send<response<body_product>>(Enums.HttpVerbs.Get, Routes.ProductGet(manufacturer, partNumber));
        }
    }
}
