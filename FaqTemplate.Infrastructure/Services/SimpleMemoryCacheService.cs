using FaqTemplate.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Infrastructure.Services
{
    public class SimpleMemoryCacheService<T> : ICacheService<T>
    {
        private IDictionary<string, T> _cache;

        public SimpleMemoryCacheService()
        {
            _cache = new Dictionary<string, T>();
        }

        public Task AddOrUpdate(string key, T value)
        {
            _cache.Remove(key);
            _cache.Add(key, value);
            return Task.CompletedTask;
        }

        public Task Clear()
        {
            _cache.Clear();
            return Task.CompletedTask;
        }

        public Task<T> Get(string key)
        {
            var data = _cache.ContainsKey(key) ? _cache[key] : default(T);
            return Task.FromResult(data);
        }

        public Task<T> Remove(string key)
        {
            var data = _cache.ContainsKey(key) ? _cache[key] : default(T);
            _cache.Remove(key);
            return Task.FromResult(data);
        }
    }
}
