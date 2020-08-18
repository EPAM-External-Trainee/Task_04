using Chat.Models.ClientSide;
using Chat.Models.ClientSide.MessageTranslator;
using Chat.Models.ServerSide;
using NUnit.Framework;
using System.Net.Sockets;

namespace TaskNUnitTest
{
    /// <summary>Testing methods of <see cref="Server"/> class</summary>
    [TestFixture]
    public class ServerNUnitTests
    {
        private Server _server;
        private Translator _translator;
        private const string serverIP = "127.0.0.2";
        private const int serverPort = 8080;

        [OneTimeSetUp]
        public void Init()
        {
            _server = new Server(serverIP, serverPort);
            _translator = new Translator();
        }

        /// <summary>Testing <see cref="Server.BroadcastMessage(string)"/ method></summary>
        /// <param name="serverLocalHostIp">Server IP address</param>
        /// <param name="serverPort">Server port number</param>
        /// <param name="actualMessage">Message in Russian</param>
        /// <param name="expectedMessage">Translated message in Russian</param>
        [TestCase("127.0.0.2", 8080, "Я знаю отличную шутку про TCP, но если она до вас не дойдет, то я повторю.", "Ya znayu otlichnuyu shutku pro TCP, no esli ona do vas ne dojdet, to ya povtoryu.")]
        [TestCase("127.0.0.2", 8080, "Раз два три", "Raz dva tri")]
        [Description("Testing BroadcastMessage method and handling MessageRecived event using the anonymous method")]
        public void BroadcastMessage_PositiveTestResult(string serverLocalHostIp, int serverPort, string actualMessage, string expectedMessage)
        {
            Client client = new Client(serverLocalHostIp, serverPort);
            client.MessageRecived += delegate (TcpClient client, string message)
            {
                Assert.AreEqual(expectedMessage, _translator.TranslateMessage(actualMessage));
            };

            _server.BroadcastMessage(expectedMessage);
        }
    }
}