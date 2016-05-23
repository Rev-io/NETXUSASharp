using NETXUSASharp.Enums;
namespace NETXUSASharp.Interfaces
{
    interface IHttpLog
    {
        HttpVerbs log_http_verb { get; set; }
        string log_request_body { get; set; }
        string log_response_body { get; set; }
        string log_url { get; set; }
    }
}
