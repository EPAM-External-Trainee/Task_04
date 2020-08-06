using Chat.Models.ClientSide;
using Chat.Models.ServerSide;
using Chat.Models.ServerSide.MessageStore;
using NUnit.Framework;
using System.Linq;

namespace TaskNUnitTest.ClientTests
{
    /// <summary>Testing methods of <see cref="Client"/> class</summary>
    [TestFixture]
    public class ClientNUnitTests
    {
        private Server _server;
        private ClientMessagesStorage _messageStorage;
        private const string serverIP = "127.0.0.1";
        private const int serverPort = 8888;

        [OneTimeSetUp]
        public void Init()
        {
            _server = new Server(serverIP, serverPort);
            _messageStorage = new ClientMessagesStorage();
        }

        /// <summary>Testing <see cref="Client.SendMessage(string)"/ method></summary>
        /// <param name="serverLocalHostIp">Server IP address</param>
        /// <param name="serverPort">Server port number</param>
        /// <param name="expectedMessage">Send message</param>
        [TestCase("127.0.0.1", 8888, "Первое тестовое сообщение")]
        [TestCase("127.0.0.1", 8888, "Второе тестовое сообщение")]
        [Description("Testing SendMessageToServer method and handling MessageRecived event using a lambda expression")]
        public void SendMessageToServer_PositiveTestResult(string serverLocalHostIp, int serverPort, string expectedMessage)
        {
            Client chatClient = new Client(serverLocalHostIp, serverPort);
            _server.MessageRecived += (client, message) =>
            {
                _messageStorage.AddMessage(client, message);
                Assert.AreEqual(expectedMessage, _messageStorage.AllClientMessages.First());
            };

            chatClient.SendMessage(expectedMessage);
        }
    }
}