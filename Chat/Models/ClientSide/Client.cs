using Chat.Abstract;
using Chat.Interfaces;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ClientSide
{
    /// <summary>Сlass that describes the TCP client functionality</summary>
    public class Client : NetworkNode, IClient
    {
        /// <summary>Field for storing <see cref="TcpClient"/> object</summary>
        private TcpClient _client;

        /// <inheritdoc cref="NetworkNode.MessageRecived"/>
        public override event Action<TcpClient, string> MessageRecived;

        /// <inheritdoc cref="NetworkNode(string, int)"/>
        public Client(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _client = new TcpClient();
            _client.Connect(LocalHostIP, LocalHostPort);

            NetworkStream = _client.GetStream();

            ThreadForReceivingMessages = new Thread(MessageReceivingProcess);
            ThreadForReceivingMessages.Start();
        }

        /// <inheritdoc cref="IClient.SendMessage(string)"/>
        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            _client?.GetStream().Write(data, 0, data.Length);
        }

        /// <inheritdoc cref="IClient.MessageReceivingProcess"/>
        public void MessageReceivingProcess()
        {
            while (true)
            {
                var builder = new StringBuilder();
                do
                {
                    if (NetworkStream.CanRead)
                    {
                        byte[] data = new byte[StreamBufferSize];
                        int bytes = NetworkStream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                }
                while (NetworkStream.DataAvailable);
                MessageRecived?.Invoke(_client, builder.ToString());
            }
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString() => $"Network node: {GetType().Name}, IP address: {LocalHostIP}, host number: {LocalHostPort}.";

        /// <inheritdoc cref="object.Equals(object)"/>
        public override bool Equals(object obj) => obj is Client client && LocalHostIP == client.LocalHostIP && LocalHostPort == client.LocalHostPort;

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => HashCode.Combine(LocalHostIP, LocalHostPort, NetworkStream, ThreadForReceivingMessages, _client);
    }
}