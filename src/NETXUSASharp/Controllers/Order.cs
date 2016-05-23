using NETXUSASharp.Models;
namespace NETXUSASharp.Controllers
{
    /// <summary>
    /// Used to manage orders with NETXUSA:  send (Post), lookup (Get), and cancel (Delete).  There is not an update method; instead, 
    /// it is expected you would cancel and then send a new one.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// NETXUSA uses the DELETE method to cancel an order.  It does not delete it, but instead attempts to change the status to canceled.
        /// </summary>
        public static response<body_order> Delete(Connector aConnection, int orderId)
        {
            return aConnection.Send<response<body_order>>(Enums.HttpVerbs.Delete, Routes.OrderDelete(orderId));
        }

        /// <summary>
        /// Lookup an existing order by NETXUSA's order ID.
        /// </summary>
        public static response<body_order> Get(Connector aConnection, int orderId)
        {
            return aConnection.Send<response<body_order>>(Enums.HttpVerbs.Get, Routes.OrderGet(orderId));
        }

        /// <summary>
        /// Submits a new order to NETXUSA
        /// </summary>
        public static response<body_order> Post(Connector aConnection, order anOrder)
        {
            return aConnection.Send<order, response<body_order>>(Enums.HttpVerbs.Post, Routes.OrderPost(), anOrder);
        }
    }
}
