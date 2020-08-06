using Chat.Abstract;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ClientSide
{
    /// <summary>Сlass that describes the TCP client functionality</summary>
    public class Client : NetworkNode
    {
        /// <summary>Field for storing <see cref="TcpClient"/> object</summary>
        private TcpClient _client;

        /// <summary><inheritdoc cref="NetworkNode.MessageRecived"/></summary>
        public override event Action<TcpClient, string> MessageRecived;

        /// <summary><inheritdoc cref="NetworkNode(string, int)"/></summary>
        public Client(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _client = new TcpClient();
            _client.Connect(LocalHostIP, LocalHostPort);

            NetworkStream = _client.GetStream();

            ThreadForReceivingMessages = new Thread(MessageReceivingProcess);
            ThreadForReceivingMessages.Start();
        }

        /// <summary>Sending a message to the server</summary>
        /// <param name="message">Send message</param>
        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            _client?.GetStream().Write(data, 0, data.Length);
        }

        /// <summary>Receiving a message from the server</summary>
        /// <remarks>Executed in <see cref="NetworkNode.ThreadForReceivingMessages"/>thread</remarks>
        public void MessageReceivingProcess()
        {
            while (true)
            {
                byte[] data = new byte[StreamBufferSize];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    if (NetworkStream.CanRead)
                    {
                        bytes = NetworkStream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                }
                while (NetworkStream.DataAvailable);
                MessageRecived?.Invoke(_client, builder.ToString());
            }
        }

        public override string ToString() => $"Network node: {GetType().Name}, IP address: {LocalHostIP}, host number: {LocalHostPort}.";
    }
}