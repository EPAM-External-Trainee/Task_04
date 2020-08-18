using System.Collections.Generic;
using System.Net.Sockets;

namespace Chat.Interfaces
{
    public interface IClientMessagesStorage
    {
        void AddMessage(TcpClient tcpClient, string message);

        IEnumerable<string> AllClientMessages { get; }
    }
}