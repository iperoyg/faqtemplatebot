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
        public Task AddOrUpdate(string key, T value)
        {
            return Task.CompletedTask;
        }

        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public Task<T> Get(string key)
        {
            return Task.FromResult(default(T));
        }

        public Task<T> Remove(string key)
        {
            return Task.FromResult(default(T));
        }
    }
}
