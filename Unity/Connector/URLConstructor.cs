using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Construct a valid URL and sign he to make GameJolt Game API calls
    /// </summary>
    public class URLConstructor
    {
        /// <value>
        /// This represent the Game Id on GameJolt
        /// </value>
        public string GameId { get; set; }
        /// <value>
        /// GameJolt Game API private key to sign the calls
        /// </value>
        private string GameKey;
        /// <value>
        /// GameJolt Game API version used by the <see cref="URLConstructor"/>
        /// </value>
        public APIVersion GameAPIVersion { get; set; }
        /// <value>
        /// The signature type provided for <see cref="Sign(string)"/>
        /// </value>
        public SignatureType SignatureType { get; set; }

        /// <summary>
        /// Create a instance of <see cref="URLConstructor"/> with your definitions
        /// </summary>
        /// <exception cref="InvalidAPIVersionException">Throwed if <paramref name="apiVersion"/> aren't supported or invalid</exception>
        /// <exception cref="InvalidSignatureTypeException">Throwed if <paramref name="signatureType"/> is invalid</exception>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key founded on Manage Achivements tab</param>
        /// <param name="signatureType">The encoding of signature</param>
        /// <param name="apiVersion">Game API version, not all are supported</param>
        public URLConstructor(string gameId, string gameKey, SignatureType signatureType, APIVersion apiVersion)
        {
            switch (apiVersion)
            {
                case APIVersion.V1_2:
                    GameId = gameId;
                    GameKey = gameKey;

                    if (signatureType != SignatureType.MD5 && signatureType != SignatureType.SHA1) throw new InvalidSignatureTypeException("Invalid signature type provided");

                    SignatureType = signatureType;

                    GameAPIVersion = APIVersion.V1_2;

                    break;
                case APIVersion.V1_1:
                    throw new InvalidAPIVersionException("Version v1.1 aren't supported");
                case APIVersion.V1_0:
                    throw new InvalidAPIVersionException("Version v1.0 aren't supported");
                default:
                    throw new InvalidAPIVersionException("Unknown version");
            }
        }

        /// <summary>
        /// Create a instance of <see cref="URLConstructor"/> with <see cref="APIVersion.V1_2"/> by default
        /// </summary>
        /// <exception cref="InvalidAPIVersionException">Throwed if <paramref name="apiVersion"/> aren't supported or invalid</exception>
        /// <exception cref="InvalidSignatureTypeException">Throwed if <paramref name="signatureType"/> is invalid</exception>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key founded on Manage Achivements tab</param>
        /// <param name="signatureType">The encoding of signature</param>
        public URLConstructor(string gameId, string gameKey, SignatureType signatureType) : this(gameId, gameKey, signatureType, APIVersion.V1_2) { }

        /// <summary>
        /// Create a instance of <see cref="URLConstructor"/> with <see cref="SignatureType.MD5"/> by default
        /// </summary>
        /// <exception cref="InvalidAPIVersionException">Throwed if <paramref name="apiVersion"/> aren't supported or invalid</exception>
        /// <exception cref="InvalidSignatureTypeException">Throwed if <paramref name="signatureType"/> is invalid</exception>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key founded on Manage Achivements tab</param>
        /// <param name="apiVersion">Game API version, not all are supported</param>
        public URLConstructor(string gameId, string gameKey, APIVersion apiVersion) : this(gameId, gameKey, SignatureType.MD5, apiVersion) { }

        /// <summary>
        /// Create a instance of <see cref="URLConstructor"/> with <see cref="SignatureType.MD5"/> and <see cref="APIVersion.V1_2"/> by default
        /// </summary>
        /// <exception cref="InvalidAPIVersionException">Throwed if <paramref name="apiVersion"/> aren't supported or invalid</exception>
        /// <exception cref="InvalidSignatureTypeException">Throwed if <paramref name="signatureType"/> is invalid</exception>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key founded on Manage Achivements tab</param>
        public URLConstructor(string gameId, string gameKey) : this(gameId, gameKey, SignatureType.MD5, APIVersion.V1_2) { }

        /// <summary>
        /// Convert from <see cref="APIVersion"/> to a string that are valid in a URL of GameJolt Game API
        /// </summary>
        /// <exception cref="InvalidAPIVersionException">Throwed if <paramref name="apiVersion"/> aren't valid or null</exception>
        /// <param name="apiVersion">Enum that gonna be converted</param>
        /// <returns>String representation of the <paramref name="apiVersion"/></returns>
        public static string APIVersionToString(APIVersion apiVersion)
        {
            switch (apiVersion)
            {
                case APIVersion.V1_2:
                    return "v1_2";
                case APIVersion.V1_1:
                    return "v1_1";
                case APIVersion.V1_0:
                    return "v1_0";
                default:
                    throw new InvalidAPIVersionException("Unknown version provided");
            }
        }

        /// <summary>
        /// Convert from <see cref="WebProtocol"/> to a valid web protocol in string format like "https://"
        /// </summary>
        /// <param name="protocol">Enum that gonna be converted</param>
        /// <exception cref="InvalidWebProtocolException">Throwed if <paramref name="protocol"/> aren't valid or null</exception>
        /// <returns>A web protocol formatted like "https://"</returns>
        public static string WebProtocolToString(WebProtocol protocol)
        {
            switch (protocol)
            {
                case WebProtocol.HTTPS:
                    return "https://";
                case WebProtocol.HTTP:
                    return "http://";
                default:
                    throw new InvalidWebProtocolException("Invalid web protocol provided");
            }
        }

        /// <summary>
        /// Construct and sign the GameJolt Game API URL in string format
        /// </summary>
        /// <param name="endpoint">GameJolt Game API endpoint</param>
        /// <param name="query">A URL enconded query string array</param>
        /// <param name="protocol">Set the web protocol of URL</param>
        /// <seealso cref="Sign(string)"/>
        /// <seealso cref="WebProtocolToString(WebProtocol)"/>
        /// <seealso cref="APIVersionToString(APIVersion)"/>
        /// <returns>Constructed GameJolt Game API URL</returns>
        public string Call(string endpoint, string[] query, WebProtocol protocol)
        {
            string url = WebProtocolToString(protocol) + "api.gamejolt.com/api/game/" + APIVersionToString(GameAPIVersion) + "/" + endpoint + "/?game_id=" + WebUtility.UrlEncode(GameId) + "&format=xml";
            foreach (string singleQuery in query)
            {
                url += "&" + singleQuery;
            }
            return url + "&signature=" + WebUtility.UrlEncode(Sign(url + GameKey));
        }

        /// <summary>
        /// Construct and sign the GameJolt Game API URL in string format with <see cref="WebProtocol.HTTPS"/> by default
        /// </summary>
        /// <param name="endpoint">GameJolt Game API endpoint</param>
        /// <param name="query">A URL enconded query string array</param>
        /// <param name="protocol">Set the web protocol of URL</param>
        /// <seealso cref="Sign(string)"/>
        /// <seealso cref="APIVersionToString(APIVersion)"/>
        /// <returns>Constructed GameJolt Game API URL</returns>
        public string Call(string endpoint, string[] query)
        {
            return Call(endpoint, query, WebProtocol.HTTPS);
        }

        /// <summary>
        /// Create a signature based on algorithm specified on <see cref="SignatureType"/> property
        /// </summary>
        /// <param name="url">URL or string to sign</param>
        /// <returns>Signed string with <see cref="SignatureType"/> cipher algorithm</returns>
        public string Sign(string url)
        {
            return Sign(url, SignatureType);
        }

        /// <summary>
        /// Create a signature based on algorithm specified on <paramref name="signatureType"/> parameter
        /// </summary>
        /// <param name="url">URL or string to sign</param>
        /// <param name="signatureType">The cipher algorithm to use</param>
        /// <exception cref="InvalidSignatureTypeException">Throwed if <paramref name="signatureType"/> is invalid or null</exception>
        /// <returns>Signed string with <paramref name="signatureType"/> cipher algorithm</returns>
        public static string Sign(string url, SignatureType signatureType)
        {
            switch (signatureType)
            {
                case SignatureType.MD5:
                    string signatureMd5 = "";
                    MD5 signerMd5 = MD5.Create();
                    byte[] dataMd5 = signerMd5.ComputeHash(Encoding.UTF8.GetBytes(url));
                    foreach (byte bMd5 in dataMd5)
                    {
                        signatureMd5 += bMd5.ToString("x2");
                    }
                    signerMd5.Clear();
                    return signatureMd5;
                case SignatureType.SHA1:
                    string signatureSha1 = "";
                    SHA1 signerSha1 = SHA1.Create();
                    byte[] dataSha1 = signerSha1.ComputeHash(Encoding.UTF8.GetBytes(url));
                    foreach (byte bSha1 in dataSha1)
                    {
                        signatureSha1 += bSha1.ToString("x2");
                    }
                    signerSha1.Clear();
                    return signatureSha1;
                default:
                    throw new InvalidSignatureTypeException("Invalid signature type provided");
            }
        }

        /// <summary>
        /// Edit the Game Key used by <see cref="Call(string, string[])"/> and <see cref="Call(string, string[], WebProtocol)"/>
        /// </summary>
        /// <param name="gameKey">Game private key founded on Manage Achivements tab</param>
        public void SetGameKey(string gameKey)
        {
            GameKey = gameKey;
        }
    }
}
