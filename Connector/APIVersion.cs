namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Specify a GameJolt Game API version to inside of <see cref="URLConstructor"/>
    /// </summary>
    /// <seealso cref="URLConstructor"/>
    public enum APIVersion
    {
        /// <summary>
        /// Value that represent GameJolt Game API v1.2
        /// </summary>
        V1_2 = 0x0102,
        /// <summary>
        /// Value that represent GameJolt Game API v1.1 (Not supported)
        /// </summary>
        V1_1 = 0x0101,
        /// <summary>
        /// Value that represent GameJolt Game API v1.0 (Not supported)
        /// </summary>
        V1_0 = 0x0100
    }
}
