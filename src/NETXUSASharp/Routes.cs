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
        public static string OrderSearch(Enums.OrderSearchField searchField, string value)
        {
            return $"/order/search?{searchField}={System.Uri.EscapeUriString(value)}";
        }
        public static string ProductGet(string manufacturer, string partNumber)
        {
            return $"/product/{manufacturer}/{partNumber}";
        }
    }
}
