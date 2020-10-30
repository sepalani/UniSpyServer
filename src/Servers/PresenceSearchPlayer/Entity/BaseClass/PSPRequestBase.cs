﻿using GameSpyLib.Abstraction.Interface;
using PresenceSearchPlayer.Entity.Enumerator;
using System.Collections.Generic;

namespace PresenceSearchPlayer.Entity.Structure.Model
{
    public class PSPRequestBase : IRequest
    {
        protected Dictionary<string, string> _rawRequest;

        public ushort OperationID { get; protected set; }

        public PSPRequestBase(Dictionary<string, string> recv)
        {
            _rawRequest = recv;
        }

        public virtual GPError Parse()
        {
            if (_rawRequest.ContainsKey("id"))
            {
                ushort operationID;
                if (!ushort.TryParse(_rawRequest["id"], out operationID))
                {
                    return GPError.Parse;
                }
                OperationID = operationID;
            }

            return GPError.NoError;
        }

        object IRequest.Parse()
        {
            return Parse();
        }

        public object GetInstance()
        {
            return this;
        }
    }
}
