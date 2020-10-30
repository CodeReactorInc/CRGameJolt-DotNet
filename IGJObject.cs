using CodeReactor.CRGameJolt.Connector;

namespace CodeReactor.CRGameJolt
{
    /// <summary>
    /// A class that can be updated using a <see cref="Connector.WebCaller"/> and are shared on all classes that process GameJolt Game API response
    /// </summary>
    public interface IGJObject
    {
        /// <value>
        /// A shared and modificable <see cref="Connector.WebCaller"/> instance 
        /// </value>
        WebCaller WebCaller { get; set; }

        /// <summary>
        /// A method that can be used to reload or update a local or online data
        /// </summary>
        /// <param name="webCaller">A <see cref="WebCaller"/> used to send the GameJolt Game API call</param>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Update(WebCaller webCaller);

        /// <summary>
        /// A alternative method to <see cref="Update(WebCaller)"/> but using <see cref="WebCaller"/> property
        /// </summary>
        /// <exception cref="GameJoltAPIException">Throwed if GameJolt Game API return a non-success response</exception>
        void Update();
    }
}
