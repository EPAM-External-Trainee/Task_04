using Chat.Models.ClientSide.MessageTranslator;
using NUnit.Framework;

namespace TaskNUnitTest.TranslatorTests
{
    class TranslatorNUnitTests
    {
        private readonly Translator translator = new Translator();

        [TestCase("Hello", "Хелло")]
        [TestCase("Я бы тебе рассказал анекдот про UDP, но, боюсь, до тебя он не дойдет.", "Ya bi tebe rasskazal anekdot pro UDP, no, boyusj, do tebya on ne dojdet.")]
        public void TestMessageTranslation_EngToRus(string messageOnRus, string expectedMessageOnEng)
        {
            Assert.AreEqual(expectedMessageOnEng, translator.TranslateMessage(messageOnRus));
        }
    }
}