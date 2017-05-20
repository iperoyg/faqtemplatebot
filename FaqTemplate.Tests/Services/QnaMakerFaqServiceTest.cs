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
            var qnaConfig = new QnaMakerConfiguration {};
            _qnaMakerFaqService = new QnaMakerFaqService(qnaConfig);
        }

        [Test]
        public async Task CallQna_ShouldReturnSomething()
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
    }
}
