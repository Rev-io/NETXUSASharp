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
        private const string _DEFAULT_URL_PRODUCTION = "https://api.netxusa.com/";
        private const string _DEFAULT_URL_SANDBOX = "https://api.sandbox.netxusa.com/";

        private string _USERNAME;
        private string _PASSWORD;

        /// <summary>
        /// NETXUSA's endpoint URL - default value exists but can be overridden
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Class used for managing the connection to and communicating with NETXUSA's API.
        /// </summary>
        /// <param name="username">Username for NETXUSA' API issued by NETXUSA</param>
        /// <param name="password">Password for NETXUSA's API issued by NETXUSA</param>
        /// <param name="url">Optional - override the URL of NETXUSA's API - can be used for testing, etc.</param>
        /// <param name="test_mode">Optional - defaults to false.  If true, will use the default NETXUSA Sandbox API URL</param>
        public Connector(string username, string password, string url = "", bool test_mode = false)
        {
            _USERNAME = username;
            _PASSWORD = password;

            if (url != null && url != "")
            {
                URL = url;
            } else
            {
                URL = test_mode ? _DEFAULT_URL_SANDBOX : _DEFAULT_URL_PRODUCTION;
            }
        }

        public TResponse Send<TResponse>(Enums.HttpVerbs method, string path)
            where TResponse : class, new()
        {
            return Send<String, TResponse>(method, path, null);
        }

        /// <summary>
        /// Combines a base URL and a path and returns a Uri
        /// </summary>
        /// <param name="url">Base URL optionally including a path.  E.g. http://rev.io/ or http://rev.io/directory/</param>
        /// <param name="path">Relative path to be combined with URL</param>
        /// <returns>System.Uri</returns>
        private Uri PathCombineUrl(string url, string path)
        {
            var myBaseUri = new Uri(url.EndsWith("/") ? url : url + "/");
            var myPath = path.StartsWith("/") ? path.Substring(1) : path;

            return new Uri(myBaseUri, myPath);
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
            var myRequest = (HttpWebRequest)WebRequest.Create(PathCombineUrl(URL, path));
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
            catch { }

            if (myResponseObject == null)
            {
                myResponseObject = new TResponse();
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
