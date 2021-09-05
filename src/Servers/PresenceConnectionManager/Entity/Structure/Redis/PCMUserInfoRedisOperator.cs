﻿using PresenceConnectionManager.Structure.Data;
using UniSpyLib.Abstraction.BaseClass.Redis;

namespace PresenceConnectionManager.Handler.SystemHandler.Redis
{
    internal sealed class PCMUserInfoRedisOperator :
        UniSpyRedisOperator<PCMUserInfoRedisKey, PCMUserInfo>
    {
    }
}
