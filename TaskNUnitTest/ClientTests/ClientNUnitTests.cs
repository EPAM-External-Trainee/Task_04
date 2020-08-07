using Chat.Models.ClientSide;
using Chat.Models.ServerSide;
using Chat.Models.ServerSide.MessageStore;
using NUnit.Framework;
using System.Linq;

namespace TaskNUnitTest.ClientTests
{
    /// <summary>Testing methods of <see cref="ServerClient"/> class</summary>
    class ClientNUnitTests
    {
        /// <summary>
        /// Testing <see cref="ServerClient.SendMessage(string)"/ method>
        /// </summary>
        /// <param name="serverLocalHostIp">Server IP address</param>
        /// <param name="serverPort">Server port number</param>
        /// <param name="expectedMessage">Send message</param>
        [TestCase("127.0.0.1", 8888, "Первое тестовое сообщение")]
        [TestCase("127.0.0.2", 8080, "Второе тестовое сообщение")]
        [Description("Testing SendMessageToServer method and handling MessageRecived event using a lambda expression")]
        public void SendMessageToServer_PositiveTestResult(string serverLocalHostIp, int serverPort, string expectedMessage)
        {
            ClientMessagesStorage messageStorage = new ClientMessagesStorage();
            Server server = new Server(serverLocalHostIp, serverPort);
            ServerClient chatClient = new ServerClient(serverLocalHostIp, serverPort);

            server.MessageRecived += (client, message) =>
            {
                messageStorage.AddMessage(client, message);
                Assert.AreEqual(expectedMessage, messageStorage.AllClientMessages.First());
            };

            chatClient.SendMessage(expectedMessage);
        }
    }
}