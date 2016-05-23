using System;
using System.Net;

namespace NETXUSASharp
{
    public class Connector : IDisposable
    {
        /// <summary>
        /// Timeout setting used for each HTTP request (in milliseconds).
        /// </summary>
        private const int _TIMEOUT = 1000*60*5;

        private string _URL;
        private string _USERNAME;
        private string _PASSWORD;

        public Connector(string url, string username, string password)
        {
            _URL = url;
            _USERNAME = username;
            _PASSWORD = password;
        }

        public TResponse Send<TResponse>(Enums.HttpVerbs method, string path)
            where TResponse : class, new()
        {
            return Send<String, TResponse>(method, path, null);
        }

        /// <summary>
        /// Send an HTTP request to a NETXUSA endpoint and return the response content.
        /// </summary>
        /// <param name="method">GET, POST, or DELETE</param>
        /// <param name="path">E.g. /order or /order/1234</param>
        /// <param name="content">Object to serialize and send (or empty for GET & DELETE requests)</param>
        /// <returns>Response content from the HTTP request</returns>
        public TResponse Send<TRequest, TResponse>(Enums.HttpVerbs method, string path, TRequest content) 
            where TRequest : class
            where TResponse : class, new()
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(new Uri(new Uri(_URL), path));
            myRequest.Timeout = _TIMEOUT;
            myRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes($"{_USERNAME}:{_PASSWORD}"))}");
            myRequest.ContentType = "application/xml";
            myRequest.Accept = "*/*";
            myRequest.Method = method.ToString();

            string myRequestXml = null;
            if (method == Enums.HttpVerbs.Post && content != null)
            {
                using (var myWriter = new System.IO.StreamWriter(myRequest.GetRequestStream()))
                {
                    myRequestXml = content.ToXml();
                    myWriter.Write(myRequestXml);
                }
            }

            var myResponse = default(WebResponse);
            try
            {
                myResponse = myRequest.GetResponse();
            }
            catch (WebException ex)
            {
                myResponse = ex.Response;
            }

            string myResponseXml = default(string);
            using (var myReader = new System.IO.StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.ASCII))
            {
                myResponseXml = myReader.ReadToEnd();
            }

            TResponse myResponseObject = default(TResponse);
            try
            {
                myResponseObject = myResponseXml.ToObject<TResponse>();
            }
            catch
            { 
                myResponseObject = Activator.CreateInstance<TResponse>();
            }

            if (myResponseObject is Interfaces.IHttpLog) {
                ((Interfaces.IHttpLog)myResponseObject).log_http_verb = method;
                ((Interfaces.IHttpLog)myResponseObject).log_request_body = myRequestXml;
                ((Interfaces.IHttpLog)myResponseObject).log_response_body = myResponseXml;
                ((Interfaces.IHttpLog)myResponseObject).log_url = myRequest.RequestUri.ToString();
            }

            return myResponseObject;
        }

        public void Dispose()
        {
        }
    }
}
