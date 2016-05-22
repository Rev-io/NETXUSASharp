namespace NETXUSASharp
{
    public static class Routes
    {
        public static string OrderDelete(int orderId)
        {
            return $"/order/{orderId}";
        }
        public static string OrderGet(int orderId)
        {
            return $"/order/{orderId}";
        }
        public static string OrderPost()
        {
            return "/order";
        }
        public static string ProductGet(string manufacturer, string partNumber)
        {
            return $"/product/{manufacturer}/{partNumber}";
        }
    }
}
