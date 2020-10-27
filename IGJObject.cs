using CodeReactor.CRGameJolt.Connector;

namespace CodeReactor.CRGameJolt
{
    /// <summary>
    /// A class that can be updated using a WebCaller
    /// </summary>
    public interface IGJObject
    {
        /// <value>
        /// A shared and modificable WebCaller instance 
        /// </value>
        WebCaller WebCaller { get; set; }

        /// <summary>
        /// A stardand method to GJAutoUpdateTask
        /// </summary>
        /// <param name="webCaller">WebCaller to send the Game API requests</param>
        void Update(WebCaller webCaller);

        /// <summary>
        /// A symlink to Update(WebCaller) but using internal WebCaller
        /// </summary>
        void Update();
    }
}
