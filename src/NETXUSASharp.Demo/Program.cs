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

                var order = Order.Get(c, 1024090);
                Console.WriteLine(order.ToXml());

                //var new_order = GetOrderRequest();
                //Console.WriteLine(new_order.ToXml());

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
                PO = "TEST ORDER 12",
                acceptTermsAndConditions = Enums.YesNo.yes,
                shipping = new Models.shipping
                {
                    shipper = Enums.ShippingCarrier.UPS,
                    method = Enums.ShippingMethod.Ground
                },
                billingAddress = new Models.address
                {
                    firstName = "Johnathon",
                    lastName = "Johnsville",
                    company = "Acme Company Inc",
                    address1 = "296 Mount Vernon Cv",
                    city = "Atlanta",
                    state = "GA",
                    zip = "30328",
                    country = "US",
                    phone = "866-470-5500"
                },
                payment = new Models.payment
                {
                    @default = ""
                },
                shippingAddress = new Models.address
                {
                    firstName = "Johnathon",
                    lastName = "Johnsville",
                    company = "Acme Company Inc",
                    address1 = "296 Mount Vernon Cv",
                    city = "Atlanta",
                    state = "GA",
                    zip = "30328",
                    country = "US",
                    phone = "866-470-5500"
                },
                endUserAddress = new Models.endUserAddress
                {
                    company = "Acme Company Inc",
                    city = "Atlanta",
                    state = "GA",
                    zip = "30328",
                },
                lineItems = new Models.lineItem[] {
                    new Models.lineItem {
                        manufacturer = "Yealink",
                        partNumber = "SIP-T21P_E2",
                        quantity = 1,
                        provisioningTemplate = new Models.provisioningTemplate
                        {
                            useDefaultProvisioningTemplate = ""
                        }
                    },
                    new Models.lineItem {
                        manufacturer = "Yealink",
                        partNumber = "SIP-T21P_E2",
                        quantity = 1,
                        provisioningTemplate = new Models.provisioningTemplate
                        {
                            useDefaultProvisioningTemplate = ""
                        }
                    }
                },
                additionalOptions = new Models.additionalOption[] {
                    new Models.additionalOption {
                        name = "Customer PO",
                        value = "30000"
                    },
                    new Models.additionalOption {
                        name = "Customer ID",
                        value = "4000"
                    }
                }
            };
        }
    }
}
