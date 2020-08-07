using System;

namespace Chat.Structs
{
    public struct ClientMessage
    {
        public ClientMessage(string id, string message, DateTime timeOfSending)
        {
            ID = id;
            Message = message;
            TimeOfSending = timeOfSending;
        }

        public string ID { get; private set; }

        public string Message { get; private set; }

        public DateTime TimeOfSending { get; private set; }

        public override bool Equals(object obj) => obj is ClientMessage message && ID == message.ID && Message == message.Message && TimeOfSending == message.TimeOfSending;

        public override int GetHashCode() => HashCode.Combine(ID, Message, TimeOfSending);
    }
}