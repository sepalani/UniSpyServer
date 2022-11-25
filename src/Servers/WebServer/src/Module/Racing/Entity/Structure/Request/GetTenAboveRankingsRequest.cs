using System.Linq;
using UniSpyServer.Servers.WebServer.Abstraction;


namespace UniSpyServer.Servers.WebServer.Entity.Structure.Request.Racing
{

    public class GetTenAboveRankingsRequest : RequestBase
    {
        public int GameId { get; set; }
        public int RegionId { get; set; }
        public int CourseId { get; set; }
        public int ProfileId { get; set; }
        public GetTenAboveRankingsRequest(string rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            base.Parse();
            var gameid = _contentElement.Descendants().Where(p => p.Name.LocalName == "gameid").First().Value;
            GameId = int.Parse(gameid);
            var regionid = _contentElement.Descendants().Where(p => p.Name.LocalName == "regionid").First().Value;
            RegionId = int.Parse(regionid);
            var courseid = _contentElement.Descendants().Where(p => p.Name.LocalName == "courseid").First().Value;
            CourseId = int.Parse(courseid);
            var profileid = _contentElement.Descendants().Where(p => p.Name.LocalName == "profileid").First().Value;
            ProfileId = int.Parse(profileid);
        }
    }
}