﻿namespace GameStatus.Entity.Enumerate
{
    /// <summary>
    /// In gamespy protocol there are no error response message
    /// from server to client, which mean we only need to make
    /// internal error system.
    /// </summary>
    internal enum GSErrorCode
    {
        General,
        Parse,
        Database,
        NoError
    }
}
