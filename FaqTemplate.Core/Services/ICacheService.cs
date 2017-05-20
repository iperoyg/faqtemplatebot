using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Core.Services
{
    public interface ICacheService<T>
    {
        Task AddOrUpdate(string key, T value);
        Task<T> Get(string key);
        Task<T> Remove(string key);
        Task Clear();
    }
}
