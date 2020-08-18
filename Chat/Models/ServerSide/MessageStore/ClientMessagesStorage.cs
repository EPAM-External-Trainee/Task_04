using Chat.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Chat.Models.ServerSide.MessageStore
{
    /// <summary>Class that describes how to store client messages</summary>
    public class ClientMessagesStorage : Dictionary<TcpClient, List<string>>, IClientMessagesStorage
    {
        /// <summary>Adding a message to the <see cref="Dictionary{TKey, TValue}"/> storage</summary>
        /// <param name="tcpClient">The client whose message will be added</param>
        /// <param name="message">Message to add</param>
        public void AddMessage(TcpClient tcpClient, string message)
        {
            if(TryGetValue(tcpClient, out var value))
            {
                value.Add(message);
                return;
            }

            Add(tcpClient, new List<string> { message });
        }

        /// <summary>Getting all client messages</summary>
        public IEnumerable<string> AllClientMessages => this?.SelectMany(pair => pair.Value);
    }
}