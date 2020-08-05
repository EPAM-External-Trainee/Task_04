using Chat.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat.Abstract
{
    /// <summary>Сlass that describes a network node</summary>
    public abstract class NetworkNode : IMessageRecivedNotifier
    {
        /// <summary>Buffer size for NetworkStream Read operation</summary>
        protected const int StreamBufferSize = 64;

        /// <summary>Creates a new node using the local IP address and port number</summary>
        /// <param name="localIPAddress">Local IP address</param>
        /// <param name="port">Port number</param>
        protected NetworkNode(string localIPAddress, int port)
        {
            LocalHostIP = IPAddress.Parse(localIPAddress);
            LocalHostPort = port;
        }

        /// <summary>Property for storing IP address as <see cref="IPAddress"/> object</summary>
        protected IPAddress LocalHostIP { get; set; }

        /// <summary>Property for storing port number</summary>
        protected int LocalHostPort { get; set; }

        /// <summary>Property for storing <see cref="NetworkStream"/> object</summary>
        protected NetworkStream NetworkStream { get; set; }

        /// <summary>Thread for receving client messages</summary>
        protected Thread ThreadForReceivingMessages { get; set; }

        /// <summary><inheritdoc cref="IMessageRecivedNotifier.MessageRecived"/></summary>
        public abstract event Action<TcpClient, string> MessageRecived;
    }
}