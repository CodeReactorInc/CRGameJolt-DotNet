namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// All signature types valid in GameJolt Game API
    /// </summary>
    /// <seealso cref="URLConstructor"/>
    public enum SignatureType
    {
        /// <summary>
        /// Say to <see cref="URLConstructor.Sign(string, SignatureType)"/> use MD5 cipher algorithm
        /// </summary>
        MD5,
        /// <summary>
        /// Say to <see cref="URLConstructor.Sign(string, SignatureType)"/> use SHA1 cipher algorithm
        /// </summary>
        SHA1
    }
}
