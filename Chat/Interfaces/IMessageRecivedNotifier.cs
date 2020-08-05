using System;
using System.Net.Sockets;

namespace Chat.Interfaces
{
    /// <summary>Interface that allows you to subscribe to an event <see cref="MessageRecived"/> when a message arrives</summary>
    public interface IMessageRecivedNotifier
    {
        /// <summary>Event when a node receives a message</summary>
        event Action<TcpClient, string> MessageRecived;
    }
}