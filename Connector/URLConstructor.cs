using System.Security.Cryptography;
using System.Text;

namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Construct urls to make GameJolt Game API calls
    /// </summary>
    public class URLConstructor
    {
        /// <value>
        /// This represent the Game Id on GameJolt
        /// </value>
        public string GameId;
        /// <value>
        /// GameJolt Game API private key to sign the calls
        /// </value>
        private string GameKey;
        /// <value>
        /// String representation of Game API version
        /// </value>
        private APIVersion GameAPIVersion;
        /// <value>
        /// The signature type provided for signer
        /// </value>
        public SignatureType SignatureType;
        
        /// <summary>
        /// This initialize URLConstructor without anything from default
        /// </summary>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key getted from Manage Achivements tab</param>
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
        /// This initialize URLConstructor with Game API version in v1.2
        /// </summary>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key getted from Manage Achivements tab</param>
        /// <param name="signatureType">The encoding of signature</param>
        public URLConstructor(string gameId, string gameKey, SignatureType signatureType) : this(gameId, gameKey, signatureType, APIVersion.V1_2) { }

        /// <summary>
        /// This initialize URLConstructor with Signature Type in MD5
        /// </summary>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key getted from Manage Achivements tab</param>
        /// <param name="apiVersion">Game API version, not all are supported</param>
        public URLConstructor(string gameId, string gameKey, APIVersion apiVersion) : this(gameId, gameKey, SignatureType.MD5, apiVersion) { }

        /// <summary>
        /// This initialize URLConstructor with Signature Type in MD5 and Game API version in v1.2
        /// </summary>
        /// <param name="gameId">The Game Id from url of your game</param>
        /// <param name="gameKey">Game private key getted from Manage Achivements tab</param>
        public URLConstructor(string gameId, string gameKey) : this(gameId, gameKey, SignatureType.MD5, APIVersion.V1_2) { }

        /// <summary>
        /// Convert from enum APIVersion to a string that are a valid in GameJolt Game API
        /// </summary>
        /// <param name="apiVersion"></param>
        /// <returns></returns>
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
                    throw new InvalidAPIVersionException("Unknown version");
            }
        }

        /// <summary>
        /// Construct and sign a Game API URL ready to call
        /// </summary>
        /// <param name="endpoint">Game API endpoint</param>
        /// <param name="query">A URL enconded query string array</param>
        /// <returns>Constructed Game API url ready to call</returns>
        public string Call(string endpoint, string[] query)
        {
            string url = "https://api.gamejolt.com/api/game/" + APIVersionToString(GameAPIVersion) + "/" + endpoint + "/?game_id=" + GameId + "&format=xml";
            foreach(string singleQuery in query) {
                url += "&" + singleQuery;
            }
            return url + "&signature=" + Sign(url + GameKey);
        }

        /// <summary>
        /// Sign a Game API call URL with game key and signature type from SignatureType
        /// </summary>
        /// <param name="url">Game API call url to sign</param>
        /// <returns>Signed call url</returns>
        public string Sign(string url)
        {
            return Sign(url, SignatureType);
        }

        /// <summary>
        /// Sign a Game API call URL with game key
        /// </summary>
        /// <param name="url">Game API call url to sign</param>
        /// <param name="signatureType">A signature type to specify sign</param>
        /// <returns>Signed call url</returns>
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
    }
}
