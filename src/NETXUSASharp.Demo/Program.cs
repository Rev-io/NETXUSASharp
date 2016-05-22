using System;
using NETXUSASharp.Controllers;

namespace NETXUSASharp.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var auth = new Auth())
            using (var c = new Connector(auth.url, auth.username, auth.password))
            {
                var product = Product.Get(c, "Yealink", "SIP-T21P_E2");
                Console.WriteLine(product.ToXml());

                var order = Order.Get(c, 1023945);
                Console.WriteLine(order.ToXml());
            };

            Console.WriteLine("Done - press any key to exit.");
            Console.ReadKey();
        }
    }
}
