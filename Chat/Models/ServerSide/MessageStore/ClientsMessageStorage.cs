using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Chat.Models.ServerSide.MessageStore
{
    public class ClientsMessageStorage : Dictionary<TcpClient, List<string>>
    {
        public void AddMessage(TcpClient tcpClient, string message)
        {
            if(TryGetValue(tcpClient, out var value))
            {
                value.Add(message);
                return;
            }

            Add(tcpClient, new List<string> { message });
        }

        public List<string> AllClientMessages => this?.SelectMany(pair => pair.Value).ToList();
    }
}