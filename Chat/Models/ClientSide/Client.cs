using Chat.Abstract;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ClientSide
{
    public class Client : NetworkNode
    {
        private const int _streamBufferSize = 64;
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _process;

        public event Action<TcpClient, string> RecivedMessage;

        public Client(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _client = new TcpClient();
            _client.Connect(LocalHostIp, LocalHostPort);

            _stream = _client.GetStream();

            _process = new Thread(ReceiveMessage);
            _process.Start();
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
                byte[] data = new byte[_streamBufferSize];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    if (_stream.CanRead)
                    {
                        bytes = _stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }

                }
                while (_stream.DataAvailable);
                RecivedMessage?.Invoke(_client, builder.ToString());
            }
        }

        //void Disconnect()
        //{
        //    _stream?.Close();
        //    _client?.Close();
        //}
    }
}