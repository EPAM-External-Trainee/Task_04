using Chat.Abstract;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ClientSide
{
    public class Client : NetworkNode
    {
        private TcpClient _client;

        public override event Action<TcpClient, string> MessageRecived;

        public Client(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _client = new TcpClient();
            _client.Connect(LocalHostIP, LocalHostPort);

            NetworkStream = _client.GetStream();

            ThreadForWorkWithClient = new Thread(ReceiveMessage);
            ThreadForWorkWithClient.Start();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            _client.GetStream().Write(data, 0, data.Length);
        }

        public void EnterChat()
        {
            string message = "";
            while (true)
            {
                SendMessage(message);
            }
        }

        public void ReceiveMessage()
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

        //void Disconnect()
        //{
        //    _stream?.Close();
        //    _client?.Close();
        //}
    }
}