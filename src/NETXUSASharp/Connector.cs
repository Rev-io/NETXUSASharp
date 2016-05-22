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

        /// <summary>
        /// Send an HTTP request to a NETXUSA endpoint and return the response content.
        /// </summary>
        /// <param name="method">GET, POST, or DELETE</param>
        /// <param name="path">E.g. /order or /order/1234</param>
        /// <param name="content">XML to send (or empty for GET & DELETE requests)</param>
        /// <returns>Response content from the HTTP request</returns>
        public string Send(Enums.HttpVerbs method, string path, string content = null)
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(new Uri(new Uri(_URL), path));
            myRequest.Timeout = _TIMEOUT;
            myRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes($"{_USERNAME}:{_PASSWORD}"))}");
            myRequest.ContentType = "application/xml";
            myRequest.Accept = "*/*";
            myRequest.Method = method.ToString();

            if (method == Enums.HttpVerbs.Post)
            {
                using (var myWriter = new System.IO.StreamWriter(myRequest.GetRequestStream()))
                {
                    myWriter.Write(content);
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

            using (var myReader = new System.IO.StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.ASCII))
            {
                return myReader.ReadToEnd();
            }
        }

        public void Dispose()
        {
        }
    }
}
