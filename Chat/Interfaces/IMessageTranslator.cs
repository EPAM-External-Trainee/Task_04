using Chat.Enums;

namespace Chat.Interfaces
{
    /// <summary>Interface that describes the ability to translate one message to another using some <see cref="Language"/> languages</summary>
    public interface IMessageTranslator
    {
        /// <summary>Translating a message from one <see cref="Language"/> language to another</summary>
        /// <param name="message">The message you want to translate</param>
        /// <returns>Translated message</returns>
        string TranslateMessage(string message);
    }
}