namespace Chat.Interfaces
{
    /// <summary>Interface that describes the TCP client functionality</summary>
    public interface IClient
    {
        /// <summary>Sending a message to the server</summary>
        /// <param name="message">Send message</param>
        void SendMessage(string message);

        /// <summary>Receiving a message from the server</summary>
        /// <remarks>Executed in a separate thread</remarks>
        void MessageReceivingProcess();
    }
}