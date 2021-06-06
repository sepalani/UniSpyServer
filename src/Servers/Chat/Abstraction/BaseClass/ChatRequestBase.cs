﻿using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Entity.Structure;
using UniSpyLib.Abstraction.BaseClass;

namespace Chat.Abstraction.BaseClass
{
    public class ChatRequestBase : UniSpyRequestBase
    {
        /// <summary>
        /// True means there are no errors
        /// False means there are errors
        /// </summary>
        public new string RawRequest => (string)base.RawRequest;
        public new string CommandName
        {
            get => (string)base.CommandName;
            set => base.CommandName = value;
        }
        protected string _prefix;
        protected List<string> _cmdParams;
        protected string _longParam;
        public ChatRequestBase() { }
        /// <summary>
        /// create instance for Handler
        /// </summary>
        /// <param name="request"></param>
        public ChatRequestBase(string rawRequest) : base(rawRequest)
        {
            ErrorCode = ChatErrorCode.NoError;
        }

        public override void Parse()
        {
            // at most 2 colon character
            // we do not sure about all command
            // so i block this code here
            List<string> dataFrag = new List<string>();

            if (RawRequest.Where(r => r.Equals(':')).Count() > 2)
            {
                ErrorCode = ChatErrorCode.Parse;
            }

            int indexOfColon = RawRequest.IndexOf(':');

            string rawRequest = RawRequest;
            if (indexOfColon == 0 && indexOfColon != -1)
            {
                int prefixIndex = rawRequest.IndexOf(' ');
                _prefix = rawRequest.Substring(indexOfColon, prefixIndex);
                rawRequest = rawRequest.Substring(prefixIndex);
            }

            indexOfColon = rawRequest.IndexOf(':');
            if (indexOfColon != 0 && indexOfColon != -1)
            {
                _longParam = rawRequest.Substring(indexOfColon + 1);
                //reset the request string
                rawRequest = rawRequest.Remove(indexOfColon);
            }

            dataFrag = rawRequest.Trim(' ').Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            CommandName = dataFrag[0];

            if (dataFrag.Count > 1)
            {
                _cmdParams = dataFrag.Skip(1).ToList();
            }
        }

        public static string GetCommandName(string request)
        {
            // at most 2 colon character
            // we do not sure about all command
            // so i block this code here
            List<string> dataFrag = new List<string>();

            if (request.Where(r => r.Equals(':')).Count() > 2)
            {
                return null;
            }

            int indexOfColon = request.IndexOf(':');

            string rawRequest = request;
            if (indexOfColon == 0 && indexOfColon != -1)
            {
                int prefixIndex = rawRequest.IndexOf(' ');
                var prefix = rawRequest.Substring(indexOfColon, prefixIndex);
                rawRequest = rawRequest.Substring(prefixIndex);
            }

            indexOfColon = rawRequest.IndexOf(':');
            if (indexOfColon != 0 && indexOfColon != -1)
            {
                var longParam = rawRequest.Substring(indexOfColon + 1);
                //reset the request string
                rawRequest = rawRequest.Remove(indexOfColon);
            }

            dataFrag = rawRequest.Trim(' ').Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            return dataFrag[0];
        }
    }
}
