using FaqTemplate.Core.Domain;
using FaqTemplate.Infrastructure.Domain;
using FaqTemplate.Infrastructure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Tests.Services
{
    [TestFixture]
    public class QnaMakerFaqServiceTest
    {
        private QnaMakerFaqService _qnaMakerFaqService;


        [SetUp]
        public void SetUp()
        {
            var qnaConfig = new QnaMakerConfiguration
            {
                
            };
            _qnaMakerFaqService = new QnaMakerFaqService(qnaConfig);
        }

        [Test]
        public async Task CallQna_ShouldReturn_Something()
        {
            //Arrange
            var faqRequest = new FaqRequest { Ask = "Hi" };
            //Act
            var response = await _qnaMakerFaqService.AskThenIAnswer(faqRequest);
            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Answer, Is.Not.Null);
            Assert.That(response.Score, Is.Positive);
        }

        [TestCase("o que é investimento?", "Investimento é a ação de aplicar seu dinheiro agora, esperando que ele renda e dê retornos no futuro.", 0.8f)]
        [TestCase("defina investimento.", "Investimento é a ação de aplicar seu dinheiro agora, esperando que ele renda e dê retornos no futuro.", 0.8f)]
        public async Task CallQna_ShouldReturn_CorrectAnswer(string question, string expectedAnswer, float confidenceThreshold)
        {
            //Arrange
            var faqRequest = new FaqRequest { Ask = question };
            //Act
            var response = await _qnaMakerFaqService.AskThenIAnswer(faqRequest);
            //Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Answer, Is.Not.Null);
            Assert.That(response.Answer, Is.EqualTo(expectedAnswer));
            Assert.That(response.Score, Is.Positive);
            Assert.That(response.Score, Is.GreaterThan(confidenceThreshold));
        }

    }
}
