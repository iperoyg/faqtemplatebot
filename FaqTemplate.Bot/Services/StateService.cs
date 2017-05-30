using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Bot.Services
{
    public class StateService<T> : IStateService<T>
    {
        private T _state;

        public T Get()
        {
            return _state;
        }

        public void Set(T value)
        {
            _state = value;
        }
    }
}
