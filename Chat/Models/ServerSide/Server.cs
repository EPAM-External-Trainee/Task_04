using Chat.Abstract;
using Chat.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ServerSide
{
    /// <summary>Сlass that describes the TCP server functionality</summary>
    public class Server : NetworkNode, IServer
    {
        /// <summary>Field for storing <see cref="TcpListener"/> object</summary>
        private TcpListener _server;

        /// <summary>Field for storing connected <see cref="TcpClient"/>'s</summary>
        private List<TcpClient> _tcpClients;

        /// <summary>Field for storing <see cref="Thread"/> that will be used to listen for incoming connection requests</summary>
        private Thread _threadForListeningProcess;

        /// <inheritdoc cref="NetworkNode.MessageRecived"/>
        public override event Action<TcpClient, string> MessageRecived;

        /// <inheritdoc cref="NetworkNode(string, int)"/>
        public Server(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _server = new TcpListener(LocalHostIP, LocalHostPort);
            _server.Start();

            _tcpClients = new List<TcpClient>();

            _threadForListeningProcess = new Thread(ListeningProcess);
            _threadForListeningProcess.Start();
        }

        /// <inheritdoc cref="IServer.ListeningProcess"/>
        public void ListeningProcess()
        {
            while (true)
            {
                TcpClient tcpClient = _server.AcceptTcpClient();
                _tcpClients.Add(tcpClient);

                ThreadForReceivingMessages = new Thread(MessageReceivingProcess);
                ThreadForReceivingMessages.Start(tcpClient);
            }
        }

        /// <inheritdoc cref="IServer.MessageReceivingProcess(dynamic)"/>
        public void MessageReceivingProcess(dynamic tcpClient)
        {
            using var client = tcpClient as TcpClient;
            NetworkStream = client?.GetStream();

            while (true)
            {
                GetMessage(client);
            }
        }

        /// <inheritdoc cref="IServer.GetMessage(TcpClient)"/>
        public string GetMessage(TcpClient client)
        {
            var builder = new StringBuilder();
            do
            {
                if (NetworkStream.CanRead)
                {
                    var data = new byte[StreamBufferSize];
                    int bytes = NetworkStream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
            }
            while (NetworkStream.DataAvailable);

            MessageRecived?.Invoke(client, builder.ToString());
            return builder.ToString();
        }

        /// <inheritdoc cref="IServer.BroadcastMessage(string)"/>
        public void BroadcastMessage(string message)
        {
            if(_tcpClients.Count > 0)
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                foreach (var client in _tcpClients)
                {
                    client.GetStream().Write(data, 0, data.Length);
                }
                return;
            }
            throw new Exception("Сurrently there are no connected clients on the server");
        }

        /// <inheritdoc cref="object.ToString"/>
        public override bool Equals(object obj) => obj is Server server && server.LocalHostIP == LocalHostIP && LocalHostPort == server.LocalHostPort;

        /// <inheritdoc cref="object.Equals(object)"/>
        public override int GetHashCode() => HashCode.Combine(LocalHostIP, LocalHostPort, NetworkStream, ThreadForReceivingMessages, _server, _tcpClients, _threadForListeningProcess);

        /// <inheritdoc cref="object.GetHashCode"/>
        public override string ToString() => $"Network node: {GetType().Name}, IP address: {LocalHostIP}, host number: {LocalHostPort}.";
    }
}