using FaqTemplate.Core.Services;
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
    class SimpleMemoryCacheServiceTest
    {
        private ICacheService<string> _cacheService;

        [SetUp]
        public void SetUp()
        {
            _cacheService = new SimpleMemoryCacheService<string>();   
        }

        [TestCase("chave", "valor")]
        public async Task SimpleCache_WhenAddValue_ShouldGet_CorrectValue(string key, string value)
        {
            //Arrange
            
            //Act
            await _cacheService.AddOrUpdate(key, value);
            var cached = await _cacheService.Get(key);

            //Assert
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached, Is.EqualTo(value));

        }
    }
}
