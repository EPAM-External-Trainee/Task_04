using System.Collections.Generic;
using System.Net.Sockets;

namespace Chat.Interfaces
{
    /// <summary>Interface that describes how to store client messages</summary>
    public interface IClientMessagesStorage
    {
        /// <summary>Adding a message to the <see cref="Dictionary{TKey, TValue}"/> storage</summary>
        /// <param name="tcpClient">The client whose message will be added</param>
        /// <param name="message">Message to add</param>
        void AddMessage(TcpClient tcpClient, string message);

        /// <summary>Getting all client messages</summary>
        IEnumerable<string> AllClientMessages { get; }
    }
}