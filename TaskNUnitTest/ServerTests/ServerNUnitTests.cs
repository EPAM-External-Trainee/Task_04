using Chat.Models.ClientSide;
using Chat.Models.ClientSide.MessageTranslator;
using Chat.Models.ServerSide;
using NUnit.Framework;
using System.Net.Sockets;
using System.Threading;

namespace TaskNUnitTest
{
    public class ServerNUnitTests
    {
        [TestCase("127.0.0.1", 8888, "Я знаю отличную шутку про TCP, но если она до вас не дойдет, то я повторю.", "Ya znayu otlichnuyu shutku pro TCP, no esli ona do vas ne dojdet, to ya povtoryu.")]
        public void SendMessageToClient_PositiveTestResult(string serverLocalHostIp, int serverPort, string actualMessage, string expectedMessage)
        {
            Translator traslator = new Translator();
            Server server = new Server(serverLocalHostIp, serverPort);
            Client client = new Client(serverLocalHostIp, serverPort);

            client.RecivedMessage += delegate(TcpClient client, string message)
            {
                Assert.AreEqual(expectedMessage, traslator.TranslateMessage(actualMessage));
            };

            new Thread(new ThreadStart(client.EnterChat)).Start();
            new Thread(new ThreadStart(client.EnterChat)).Start();

            server.SendMessage(expectedMessage);
        }
    }
}