using Chat.Structs;
using System;

namespace Chat.Models.ClientSide
{
    public class Client
    {
        private readonly ServerClient _serverClient;

        public Client(string localHostIp, int localHostPort, int id, string nickname)
        {
            _serverClient = new ServerClient(localHostIp, localHostPort);
            ID = id;
            Nickname = nickname;
        }

        public int ID { get; private set; }

        public string Nickname { get; private set; }
        
        public void SendMessage(string message)
        {
            ClientMessage clientMessage = new ClientMessage(Guid.NewGuid().ToString(), message, DateTime.Now);
            _serverClient.SendMessage(this, clientMessage);
        }

        public void AddSubToEvent(Action<Client, ClientMessage> action) => _serverClient.MessageRecived += action;

        public void RemvodeSubToEvent(Action<Client, ClientMessage> action) => _serverClient.MessageRecived -= action;
    }
}