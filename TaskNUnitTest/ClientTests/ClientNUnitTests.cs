using Chat.Models.ClientSide;
using Chat.Models.ServerSide;
using Chat.Models.ServerSide.MessageStore;
using NUnit.Framework;
using System.Linq;

namespace TaskNUnitTest.ClientTests
{
    /// <summary>Testing methods of <see cref="Client"/> class</summary>
    class ClientNUnitTests
    {
        [TestCase("127.0.0.1", 8888, "Первое тестовое сообщение")]
        [TestCase("127.0.0.2", 8080, "Второе тестовое сообщение")]
        public void SendMessageToServer_PositiveTestResult(string serverLocalHostIp, int serverPort, string expectedMessage)
        {
            ClientMessagesStorage messageStorage = new ClientMessagesStorage();
            Server server = new Server(serverLocalHostIp, serverPort);
            Client chatClient = new Client(serverLocalHostIp, serverPort);

            server.MessageRecived += (client, message) =>
            {
                messageStorage.AddMessage(client, message);
                Assert.AreEqual(expectedMessage, messageStorage.AllClientMessages.First());
            };

            chatClient.SendMessage(expectedMessage);
        }
    }
}