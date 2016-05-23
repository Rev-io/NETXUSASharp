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
                var search = Order.Search(c, Enums.OrderSearchField.PO, "TEST ORDER");
                Console.WriteLine(search.ToXml());

                var product = Product.Get(c, "Yealink", "SIP-T21P_E2");
                Console.WriteLine(product.ToXml());

                var order = Order.Get(c, 1024038);
                Console.WriteLine(order.ToXml());

                var new_order = GetOrderRequest();
                Console.WriteLine(new_order.ToXml());

                //var new_order_response = Order.Post(c, new_order);
                //Console.WriteLine(new_order_response.ToXml());
            };

            Console.WriteLine("Done - press any key to exit.");
            Console.ReadKey();
        }

        private static Models.order GetOrderRequest()
        {
            return new Models.order
            {
                PO = "TEST ORDER 4",
                acceptTermsAndConditions = Enums.YesNo.yes,
                shipping = new Models.shipping
                {
                    shipper = "UPS",
                    method = "Ground"
                },
                billingAddress = new Models.address
                {
                    firstName = "John",
                    lastName = "Doe",
                    company = "NETXUSA",
                    address1 = "231 Beverly Road",
                    city = "Greenville",
                    state = "SC",
                    zip = "29609",
                    country = "US",
                    phone = "864-271-9868"
                },
                payment = new Models.payment {
                    other = ""
                },
                shippingAddress = new Models.address
                {
                    firstName = "John",
                    lastName = "Doe",
                    company = "NETXUSA",
                    address1 = "231 Beverly Road",
                    city = "Greenville",
                    state = "SC",
                    zip = "29609",
                    country = "US",
                    phone = "864-271-9868"
                },
                endUserAddress = new Models.address
                {
                    company = "NETXUSA",
                    city = "Greenville",
                    state = "SC",
                    zip = "29609"
                },
                lineItems = new Models.lineItem[] {
                    new Models.lineItem {
                        manufacturer = "Yealink",
                        partNumber = "SIP-T21P_E2",
                        quantity = 1
                    }
                },
                additionalOptions = new Models.additionalOption[] {
                    new Models.additionalOption {
                        name = "Customer PO",
                        value = "99999"
                    },
                    new Models.additionalOption {
                        name = "Customer ID",
                        value = "1234"
                    }
                }
            };
        }
    }
}
