namespace CodeReactor.CRGameJolt.Connector
{
    /// <summary>
    /// Setup a web protocol to <see cref="WebCaller"/> and <see cref="URLConstructor"/>
    /// </summary>
    /// <seealso cref="URLConstructor"/>
    /// <seealso cref="WebCaller"/>
    public enum WebProtocol
    {
        /// <value>
        /// Say to <see cref="URLConstructor"/> construct using "http://" protocol
        /// </value>
        HTTP,
        /// <value>
        /// Say to <see cref="URLConstructor"/> construct using "https://" protocol
        /// </value>
        HTTPS
    }
}
