using Chat.Models.ClientSide.MessageTranslator;
using NUnit.Framework;

namespace TaskNUnitTest.TranslatorTests
{
    /// <summary>Testing methods of <see cref="Translator"/> class</summary>
    [TestFixture]
    public class TranslatorNUnitTests
    {
        private readonly Translator translator = new Translator();

        /// <summary>Testing <see cref="Translator.TranslateMessage(string)"/ method></summary>
        /// <param name="messageInRus">Message in Russian</param>
        /// <param name="expectedMessageInEng">Message in English</param>
        [TestCase("Hello", "Хелло")]
        [TestCase("Я бы тебе рассказал анекдот про UDP, но, боюсь, до тебя он может не дойти.", "Ya bi tebe rasskazal anekdot pro UDP, no, boyusj, do tebya on mozhet ne dojti.")]
        [Description("Testing TranslateMessage method")]
        public void TestMessageTranslation_EngToRus(string messageInRus, string expectedMessageInEng)
        {
            Assert.AreEqual(expectedMessageInEng, translator.TranslateMessage(messageInRus));
        }
    }
}