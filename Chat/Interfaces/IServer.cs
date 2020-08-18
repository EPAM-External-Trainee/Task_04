using System.Net.Sockets;

namespace Chat.Interfaces
{
    /// <summary>Interface that describes the TCP server functionality</summary>
    public interface IServer
    {
        /// <summary>Method for listening for incoming connection requests</summary>
        /// <remarks>Executed in a separate thread</remarks>
        void ListeningProcess();

        /// <summary>Receiving messages from clients</summary>
        /// <param name="tcpClient">The client from which the message is received</param>
        /// <remarks>Executed in <see cref="NetworkNode.ThreadForReceivingMessages"/></remarks>
        void MessageReceivingProcess(dynamic tcpClient);

        /// <summary>Getting a message from <see cref="NetworkNode.NetworkStream"/></summary>
        /// <param name="client"></param>
        /// <returns>Received message</returns>
        string GetMessage(TcpClient client);

        /// <summary>Sending a message to all server clients</summary>
        /// <param name="message">Send messages</param>
        void BroadcastMessage(string message);
    }
}