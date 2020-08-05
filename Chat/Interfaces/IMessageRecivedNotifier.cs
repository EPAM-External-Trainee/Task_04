using System;
using System.Net.Sockets;

namespace Chat.Interfaces
{
    public interface IMessageRecivedNotifier
    {
        event Action<TcpClient, string> MessageRecived;
    }
}