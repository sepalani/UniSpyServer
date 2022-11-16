using System.Net;
using Moq;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Request;
using UniSpyServer.Servers.NatNegotiation.Handler.CmdHandler;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using Xunit;
namespace UniSpyServer.Servers.NatNegotiation.Test
{

    public class HandlerTest
    {
        Client _client;
        public HandlerTest()
        {
            var serverMock = new Mock<IServer>();
            serverMock.Setup(s => s.ServerID).Returns(new System.Guid());
            serverMock.Setup(s => s.ServerName).Returns("NatNegotiation");
            serverMock.Setup(s => s.Endpoint).Returns(new IPEndPoint(IPAddress.Any, 99));
            var connectionMock = new Mock<IUdpConnection>();
            connectionMock.Setup(s => s.RemoteIPEndPoint).Returns(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999));
            connectionMock.Setup(s => s.Server).Returns(serverMock.Object);
            connectionMock.Setup(s => s.ConnectionType).Returns(NetworkConnectionType.Udp);
            _client = new Client(connectionMock.Object);
        }
        [Fact]
        public void InitTest()
        {
            var rawRequest = new byte[] {
            0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
            0x00,
            0x00, 0x00, 0x03, 0x09, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var request = new InitRequest(rawRequest);
            var handler = new InitHandler(_client, request);
            handler.Handle();
        }
        [Fact]
        public void AddressTest()
        {
            var rawRequest = new byte[] { 0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03, 0x0a, 0x00, 0x00, 0x03, 0x09, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var request = new AddressCheckRequest(rawRequest);
            var handler = new AddressCheckHandler(_client, request);
            handler.Handle();
        }
        [Fact]
        public void ErtTest()
        {
            var rawRequest = new byte[] {
            0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
            0x03,
            0x00, 0x00, 0x03, 0x09,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var request = new ErtAckRequest(rawRequest);
            var handler = new ErtAckHandler(_client, request);
            handler.Handle();
        }
        [Fact]
        public void NatifyTest()
        {
            var rawRequest = new byte[] {
            0xfd, 0xfc, 0x1e, 0x66, 0x6a, 0xb2, 0x03,
            0x0c,
            0x00, 0x00, 0x03, 0x09,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var request = new NatifyRequest(rawRequest);
            var handler = new NatifyHandler(_client, request);
            handler.Handle();
        }

        [Fact]
        public void ReportTest()
        {
            var r1 = new UniSpyServer.Servers.NatNegotiation.Entity.Structure.Misc.NatReportRecord()
            {
                ServerId = System.Guid.Empty,
                PublicIPAddress = IPAddress.Parse("202.91.34.186"),
                PrivateIPAddress = IPAddress.Parse("192.168.1.1")
            };

            var r2 = new UniSpyServer.Servers.NatNegotiation.Entity.Structure.Misc.NatReportRecord()
            {
                ServerId = System.Guid.Empty,
                PublicIPAddress = IPAddress.Parse("202.91.34.186"),
                PrivateIPAddress = IPAddress.Parse("192.168.1.1")
            };
            Assert.Equal(r1,r2);

            var req = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0x00, 0x00, 0x02, 0x9A, 0xCC, 0x00, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var request = new ReportRequest(req);
            var handler = new ReportHandler(_client, request);
            handler.Handle();
        }
    }
}