using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Core.Domain
{
    public class FaqResponse<T> : IComparable<FaqResponse<T>> where T : IComparable
    {
        public T Answer { get; set; }
        public float Score { get; set; }

        public int CompareTo(FaqResponse<T> obj)
        {
            var compareAnswer = Answer.CompareTo(obj.Answer);
            if(compareAnswer == 0)
            {
                return Score > obj.Score ? 1 : Score == obj.Score ? 0 : -1;
            }
            return compareAnswer;
        }
    }
}
