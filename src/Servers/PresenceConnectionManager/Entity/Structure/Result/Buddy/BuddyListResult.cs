﻿using PresenceConnectionManager.Abstraction.BaseClass;
using System.Collections.Generic;

namespace PresenceConnectionManager.Entity.Structure.Result
{
    internal sealed class BuddyListResult : PCMResultBase
    {
        public List<uint> ProfileIDList;

        public BuddyListResult()
        {
            ProfileIDList = new List<uint>();
        }
    }
}
