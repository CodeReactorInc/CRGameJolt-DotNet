namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Status about the user retrieved from GameJolt Game API
    /// </summary>
    public enum UserStatus
    {
        /// <value>
        /// User account has actived and aren't banned
        /// </value>
        Active,
        /// <value>
        /// User account has been banned from GameJolt Game API
        /// </value>
        Banned
    }
}
