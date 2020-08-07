using Chat.Abstract;
using Chat.MyConverters;
using Chat.Structs;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ClientSide
{
    /// <summary>Сlass that describes the TCP client functionality</summary>
    public class ServerClient : NetworkNode
    {
        /// <summary>Field for storing <see cref="TcpClient"/> object</summary>
        private TcpClient _TcpClient;
        private Client _client;

        /// <summary><inheritdoc cref="NetworkNode.MessageRecived"/></summary>
        public override event Action<Client, ClientMessage> MessageRecived;

        /// <summary><inheritdoc cref="NetworkNode(string, int)"/></summary>
        public ServerClient(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _TcpClient = new TcpClient();
            _TcpClient.Connect(LocalHostIP, LocalHostPort);

            NetworkStream = _TcpClient.GetStream();

            ThreadForReceivingMessages = new Thread(MessageReceivingProcess);
            ThreadForReceivingMessages.Start();
        }

        /// <summary>Sending a message to the server</summary>
        /// <param name="message">Send message</param>
        public void SendMessage(Client client, ClientMessage message)
        {
            byte[] data = MyConverter.GetBytesFromClientMessage(message);
            _TcpClient?.GetStream().Write(data, 0, data.Length);

            data = MyConverter.GetBytesFromClient(client);
            _TcpClient?.GetStream().Write(data, 0, data.Length);
        }

        /// <summary>Receiving a message from the server</summary>
        /// <remarks>Executed in <see cref="NetworkNode.ThreadForReceivingMessages"/>thread</remarks>
        private void MessageReceivingProcess()
        {
            while (true)
            {
                var clientMessage = new ClientMessage();
                do
                {
                    if (NetworkStream.CanRead)
                    {
                        var data = new byte[StreamBufferSize];
                        NetworkStream.Read(data, 0, data.Length);
                        MyConverter.GetClientMessageFromBytes(data, ref clientMessage);
                    }
                }
                while (NetworkStream.DataAvailable);

                MessageRecived?.Invoke(_client, clientMessage);
            }
        }
    }
}