﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Exception;

namespace Chat.Entity.Structure.Request.General
{
    public class LISTLIMITRequest : ChatRequestBase
    {
        public LISTLIMITRequest(string rawRequest) : base(rawRequest)
        {
        }

        public int MaxNumberOfChannels { get; protected set; }
        public string Filter { get; protected set; }

        public override void Parse()
        {
            base.Parse();


            if (_cmdParams.Count != 2)
            {
                throw new ChatException("The number of IRC cmd params in GETKEY request is incorrect.");
            }
            int max;
            if (!int.TryParse(_cmdParams[0], out max))
            {
                throw new ChatException("The max number format is incorrect.");
            }
            MaxNumberOfChannels = max;

            Filter = _cmdParams[1];
        }
    }
}
