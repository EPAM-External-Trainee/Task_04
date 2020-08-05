using Chat.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Models.ServerSide
{
    public class Server : NetworkNode
    {
        private TcpListener _tcpListener;
        private List<TcpClient> _tcpClients;
        private Thread _threadForServerWork;
        public override event Action<TcpClient, string> MessageRecived;

        public Server(string localHostIp, int localHostPort) : base(localHostIp, localHostPort)
        {
            _tcpListener = new TcpListener(LocalHostIP, LocalHostPort);
            _tcpClients = new List<TcpClient>();

            _tcpListener.Start();

            _threadForServerWork = new Thread(Listen);
            _threadForServerWork.Start();
        }

        public void Listen()
        {
            while (true)
            {
                TcpClient tcpClient = _tcpListener.AcceptTcpClient();
                _tcpClients.Add(tcpClient);

                ThreadForWorkWithClient = new Thread(Process);
                ThreadForWorkWithClient.Start(tcpClient);
            }
        }

        public void Process(dynamic tmp)
        {
            using var client = tmp as TcpClient;
            NetworkStream = client.GetStream();
            string message;

            while (true)
            {
                message = GetMessage(client);
            }
        }

        private string GetMessage(TcpClient client)
        {
            var builder = new StringBuilder();
            do
            {
                var data = new byte[StreamBufferSize];
                int bytes = NetworkStream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (NetworkStream.DataAvailable);

            MessageRecived?.Invoke(client, builder.ToString());
            return builder.ToString();
        }

        // Сообщение всем клиентам
        //public void BroadcastMessage(string message, string id)
        //{
        //    byte[] data = Encoding.Unicode.GetBytes(message);
        //    //var client = clients.FirstOrDefault(c => c.Id == id);
        //    //client?.Stream.Write(data, 0, data.Length);

        //    for (int i = 0; i < clients.Count; i++)
        //    {
        //        if (clients[i].Id != id) // если id клиента не равно id отправляющего
        //        {
        //            clients[i].Stream.Write(data, 0, data.Length); //передача данных
        //        }
        //    }
        //}

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            foreach (var client in _tcpClients)
            {
                client.GetStream().Write(data, 0, data.Length);
            }
        }

        // Отключение всех клиентов
        //protected internal void Disconnect()
        //{
        //    tcpListener.Stop();
        //
        //    for (int i = 0; i < clients.Count; i++)
        //    {
        //        clients[i].Close(); 
        //    }
        //}
    }
}