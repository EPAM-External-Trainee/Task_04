using Chat.Interfaces;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Chat.Abstract
{
    public abstract class NetworkNode : IMessageRecivedNotifier
    {
        protected const int StreamBufferSize = 64;

        protected NetworkNode(string ip, int port)
        {
            LocalHostIP = ip;
            LocalHostPort = port;
        }

        protected string LocalHostIP { get; set; }

        protected int LocalHostPort { get; set; }

        protected NetworkStream NetworkStream { get; set; }

        protected Thread ThreadForClienWork { get; set; }

        public abstract event Action<TcpClient, string> MessageRecived;
    }
}