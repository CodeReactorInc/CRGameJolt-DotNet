namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// All signature types valid in GameJolt Game API
    /// </summary>
    /// <seealso cref="URLConstructor"/>
    public enum SignatureType
    {
        /// <value>
        /// Say to <see cref="URLConstructor.Sign(string, SignatureType)"/> use MD5 cipher algorithm
        /// </value>
        MD5,
        /// <value>
        /// Say to <see cref="URLConstructor.Sign(string, SignatureType)"/> use SHA1 cipher algorithm
        /// </value>
        SHA1
    }
}
