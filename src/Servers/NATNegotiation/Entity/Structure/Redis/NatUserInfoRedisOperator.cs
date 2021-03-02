﻿using UniSpyLib.Abstraction.BaseClass.Redis;
using UniSpyLib.Extensions;

namespace NATNegotiation.Entity.Structure.Redis
{
    internal sealed class NatUserInfoRedisOperator : UniSpyRedisOperatorBase<NatUserInfoRedisKey, NatUserInfo>
    {
        public static new RedisDataBaseNumber _dbNumber = RedisDataBaseNumber.NatNeg;
    }
}
