using Chat.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Chat.Models.ServerSide.MessageStore
{
    /// <summary>Class that describes how to store client messages</summary>
    public class ClientMessagesStorage : Dictionary<TcpClient, List<string>>, IClientMessagesStorage
    {
        ///<inheritdoc cref="IClientMessagesStorage.AddMessage(TcpClient, string)"/>
        public void AddMessage(TcpClient tcpClient, string message)
        {
            if(TryGetValue(tcpClient, out var value))
            {
                value.Add(message);
                return;
            }

            Add(tcpClient, new List<string> { message });
        }

        ///<inheritdoc cref="IClientMessagesStorage.AllClientMessages"/>
        public IEnumerable<string> AllClientMessages => this?.SelectMany(pair => pair.Value);
    }
}