using Chat.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat.Abstract
{
    public abstract class NetworkNode : IMessageRecivedNotifier
    {
        protected const int StreamBufferSize = 64;

        protected NetworkNode(string localIPAddress, int port)
        {
            LocalHostIP = IPAddress.Parse(localIPAddress);
            LocalHostPort = port;
        }

        protected IPAddress LocalHostIP { get; set; }

        protected int LocalHostPort { get; set; }

        protected NetworkStream NetworkStream { get; set; }

        protected Thread ThreadForWorkWithClient { get; set; }

        public abstract event Action<TcpClient, string> MessageRecived;
    }
}