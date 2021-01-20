namespace CodeReactor.CRGameJolt.Users
{
    /// <summary>
    /// Is the type of user in GameJolt Game API
    /// </summary>
    /// <seealso cref="GameJoltUser"/>
    public enum UserType
    {
        /// <value>
        /// Represent a normal user in GameJolt
        /// </value>
        User,
        /// <value>
        /// Represent a game developer in GameJolt
        /// </value>
        Developer,
        /// <value>
        /// Represent a moderator in GameJolt
        /// </value>
        Moderator,
        /// <value>
        /// Represent a administrator in GameJolt
        /// </value>
        Administrator
    }
}
