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
        /// <summary>Testing <see cref="Server.BroadcastMessage(string)"/ method></summary>
        /// <param name="serverLocalHostIp">Server IP address</param>
        /// <param name="serverPort">Server port number</param>
        /// <param name="actualMessage">Message in Russian</param>
        /// <param name="expectedMessage">Translated message in Russian</param>
        [TestCase("127.0.0.2", 8888, "Я знаю отличную шутку про TCP, но если она до вас не дойдет, то я повторю.", "Ya znayu otlichnuyu shutku pro TCP, no esli ona do vas ne dojdet, to ya povtoryu.")]
        [TestCase("127.0.0.2", 7777, "Раз два три", "Raz dva tri")]
        [Description("Testing BroadcastMessage method and handling MessageRecived event using the anonymous method")]
        public void BroadcastMessage_PositiveTestResult(string serverLocalHostIp, int serverPort, string actualMessage, string expectedMessage)
        {
            Translator traslator = new Translator();
            Server server = new Server(serverLocalHostIp, serverPort);
            Client client = new Client(serverLocalHostIp, serverPort);

            client.MessageRecived += delegate(TcpClient client, string message)
            {
                Assert.AreEqual(expectedMessage, traslator.TranslateMessage(actualMessage));
            };

            server.BroadcastMessage(expectedMessage);       
        }
    }
}