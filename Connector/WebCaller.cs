using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// A simple way to get content of web using URLConstructor
    /// </summary>
    public class WebCaller
    {
        /// <value>
        /// Protocol to be pass to URLConstructor
        /// </value>
        public WebProtocol Protocol;
        /// <value>
        /// The URLConstructor prepared to create the URLs
        /// </value>
        public URLConstructor URLBuilder;

        /// <summary>
        /// Create a WebCaller with the URLConstructor and a custom web protocol
        /// </summary>
        /// <param name="constructor">The constructor to build the URLs</param>
        /// <param name="protocol">Protocol to be used in URLConstructor</param>
        public WebCaller(URLConstructor constructor, WebProtocol protocol)
        {
            Protocol = protocol;
            URLBuilder = constructor;
        }

        /// <summary>
        /// Create a WebCaller with the URLConstructor and HTTPS protocol
        /// </summary>
        /// <param name="constructor">The constructor to build the URLs</param>
        public WebCaller(URLConstructor constructor) : this(constructor, WebProtocol.HTTPS) { }

        /// <summary>
        /// Use the URLConstructor to download data from Game API as string
        /// </summary>
        /// <param name="endpoint">Game API endpoint to call</param>
        /// <param name="query">Extra query formated in "key=url enconded value"</param>
        /// <returns>Response from GameJolt Game API</returns>
        public string GetAsText(string endpoint, string[] query)
        {
            string url = URLBuilder.Call(endpoint, query, Protocol);
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        /// <summary>
        /// Use the URLConstructor to download data from Game API parsed in XDocument
        /// </summary>
        /// <param name="endpoint">Game API endpoint to call</param>
        /// <param name="query">Extra query formated in "key=url enconded value"</param>
        /// <returns>Response from GameJolt Game API</returns>
        public XDocument GetAsXML(string endpoint, string[] query)
        {
            return XDocument.Parse(GetAsText(endpoint, query));
        }
    }
}
