using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReactor.CRGameJolt.DataStorage
{
    /// <summary>
    /// Specify the type of data of a GJDataValue and he's buffer
    /// </summary>
    public enum GJDataType
    {
        /// <value>
        /// Say to GJDataValue use StringBuffer
        /// </value>
        STRING,
        /// <value>
        /// Say to GJDataValue use IntegerBuffer
        /// </value>
        INTEGER,
        /// <value>
        /// Say to GJDataValue that aren't buffers in use
        /// </value>
        NULL
    }
}
