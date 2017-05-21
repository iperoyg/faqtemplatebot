using FaqTemplate.Core.Domain;
using FaqTemplate.Core.Services;
using FaqTemplate.Infrastructure.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FaqTemplate.Tests.Services
{
    [TestFixture]
    class SimpleMemoryCacheServiceTest
    {
        private ICacheService<string> _cacheServiceForStringValue;
        private ICacheService<FaqResponse<string>> _cacheServiceForFaqResponseValue;


        [SetUp]
        public void SetUp()
        {
            _cacheServiceForStringValue = new SimpleMemoryCacheService<string>();
            _cacheServiceForFaqResponseValue = new SimpleMemoryCacheService<FaqResponse<string>>();
        }

        #region SIMPLE CACHE FOR STRING
        [TestCase("chave", "valor")]
        public async Task SimpleCacheForString_WhenAddAndGetValue_ShouldGet_SameValue(string key, string value)
        {
            //Arrange

            //Act
            await _cacheServiceForStringValue.AddOrUpdate(key, value);
            var cached = await _cacheServiceForStringValue.Get(key);

            //Assert
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached, Is.EqualTo(value));

        }

        [TestCase("chave", "valor")]
        public async Task SimpleCacheForString_WhenAddAndRemoveValue_ShouldGet_SameValue(string key, string value)
        {
            //Arrange

            //Act
            await _cacheServiceForStringValue.AddOrUpdate(key, value);
            var cacheRemoved = await _cacheServiceForStringValue.Remove(key);

            //Assert
            Assert.That(cacheRemoved, Is.Not.Null);
            Assert.That(cacheRemoved, Is.EqualTo(value));

        }

        [TestCase("chave", "valor")]
        public async Task SimpleCacheForString_WhenAddAndRemoveValueTwice_ShouldGet_NullValue(string key, string value)
        {
            //Arrange

            //Act
            await _cacheServiceForStringValue.AddOrUpdate(key, value);
            await _cacheServiceForStringValue.Remove(key);
            var cacheRemoved = await _cacheServiceForStringValue.Remove(key);

            //Assert
            Assert.That(cacheRemoved, Is.Null);

        }
        #endregion

        #region SIMPLE CACHE FOR FAQ RESPONSE
        [Test]
        public async Task SimpleCacheForFaqResponse_WhenAddAndGetValue_ShouldGet_SameValue()
        {
            //Arrange
            var key = "key";
            var value = new FaqResponse<string>() { Answer = "value", Score = 1 };

            //Act
            await _cacheServiceForFaqResponseValue.AddOrUpdate(key, value);
            var cached = await _cacheServiceForFaqResponseValue.Get(key);

            //Assert
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached.CompareTo(value) == 0);

        }

        [Test]
        public async Task SimpleCacheForFaqResponse_WhenAddAndRemoveValue_ShouldGet_SameValue()
        {
            //Arrange
            var key = "key";
            var value = new FaqResponse<string>() { Answer = "value", Score = 1 };

            //Act
            await _cacheServiceForFaqResponseValue.AddOrUpdate(key, value);
            var cached = await _cacheServiceForFaqResponseValue.Remove(key);

            //Assert
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached.CompareTo(value) == 0);

        }

        [Test]
        public async Task SimpleCacheForFaqResponse_WhenAddAndRemoveValueTwice_ShouldGet_NullValue()
        {
            //Arrange
            var key = "key";
            var value = new FaqResponse<string>() { Answer = "value", Score = 1 };

            //Act
            await _cacheServiceForFaqResponseValue.AddOrUpdate(key, value);
            await _cacheServiceForFaqResponseValue.Remove(key);
            var cached = await _cacheServiceForFaqResponseValue.Remove(key);

            //Assert
            Assert.That(cached, Is.Null);

        }

        [Test]
        public async Task SimpleCacheForFaqResponse_WhenAddAndUpdateValue_ShouldGet_UpdatedValue()
        {
            //Arrange
            var key = "key";
            var value = new FaqResponse<string>() { Answer = "value", Score = 1 };
            var updatedValue = new FaqResponse<string>() { Answer = "updatedValue", Score = 0.5f };

            //Act
            await _cacheServiceForFaqResponseValue.AddOrUpdate(key, value);
            await _cacheServiceForFaqResponseValue.AddOrUpdate(key, updatedValue);
            var cached = await _cacheServiceForFaqResponseValue.Get(key);

            //Assert
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached.CompareTo(value) != 0);
            Assert.That(cached.CompareTo(updatedValue) == 0);

        } 
        #endregion

    }
}
