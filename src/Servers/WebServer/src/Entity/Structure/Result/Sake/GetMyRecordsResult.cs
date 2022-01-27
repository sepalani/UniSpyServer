using System.Collections.Generic;
using UniSpyServer.Servers.WebServer.Abstraction;

namespace UniSpyServer.Servers.WebServer.Entity.Structure.Result.Sake
{
    public class GetMyRecordsResult : ResultBase
    {
        public List<RecordFieldObject> Records { get; private set; }
        public GetMyRecordsResult()
        {
            Records = new List<RecordFieldObject>();
        }
    }
}