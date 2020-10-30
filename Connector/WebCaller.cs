using System.Net;
using System.Xml.Linq;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// A simple way to get content of web using <see cref="Connector.URLConstructor"/>
    /// </summary>
    /// <seealso cref="Connector.URLConstructor"/>
    /// <seealso cref="WebProtocol"/>
    public class WebCaller
    {
        /// <value>
        /// Protocol to be passed to <see cref="URLConstructor"/>
        /// </value>
        public WebProtocol Protocol { get; set; }
        /// <value>
        /// The <see cref="Connector.URLConstructor"/> prepared to create the URLs
        /// </value>
        public URLConstructor URLConstructor { get; set; }

        /// <summary>
        /// Create a <see cref="WebCaller"/> with a <see cref="Connector.URLConstructor"/> and a custom web protocol
        /// </summary>
        /// <param name="constructor">The constructor to build the URLs</param>
        /// <param name="protocol">Protocol to be used in <see cref="URLConstructor"/></param>
        public WebCaller(URLConstructor constructor, WebProtocol protocol)
        {
            Protocol = protocol;
            URLConstructor = constructor;
        }

        /// <summary>
        /// Create a <see cref="WebCaller"/> with the <see cref="Connector.URLConstructor"/> and <see cref="WebProtocol.HTTPS"/> protocol
        /// </summary>
        /// <param name="constructor">The constructor to build the URLs</param>
        public WebCaller(URLConstructor constructor) : this(constructor, WebProtocol.HTTPS) { }

        /// <summary>
        /// Use the <see cref="URLConstructor"/> to download data from GameJolt Game API as a <c>string</c> using a <c>WebClient</c>
        /// </summary>
        /// <param name="endpoint">GameJolt Game API endpoint to call</param>
        /// <param name="query">Query string formatted in "key=url enconded value"</param>
        /// <returns>Response from GameJolt Game API</returns>
        public string GetAsText(string endpoint, string[] query)
        {
            string url = URLConstructor.Call(endpoint, query, Protocol);
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        /// <summary>
        /// Use the <see cref="URLConstructor"/> to download data from GameJolt Game API parsed in a <c>XDocument</c>
        /// </summary>
        /// <param name="endpoint">GameJolt Game API endpoint to call</param>
        /// <param name="query">Query string formatted in "key=url enconded value"</param>
        /// <returns>Response from GameJolt Game API</returns>
        public XDocument GetAsXML(string endpoint, string[] query)
        {
            return XDocument.Parse(GetAsText(endpoint, query));
        }
    }
}
