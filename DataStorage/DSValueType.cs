namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Specify the type of a <see cref="DataStorageValue"/> and which buffer he need use
    /// </summary>
    /// <seealso cref="DataStorageValue"/>
    public enum DSValueType
    {
        /// <value>
        /// Say to <see cref="DataStorageValue"/> use <see cref="DataStorageValue.StringBuffer"/>
        /// </value>
        STRING,
        /// <value>
        /// Say to <see cref="DataStorageValue"/> use <see cref="DataStorageValue.IntegerBuffer"/>
        /// </value>
        INTEGER
    }
}
