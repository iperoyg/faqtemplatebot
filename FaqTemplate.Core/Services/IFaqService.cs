using FaqTemplate.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Core.Services
{
    public interface IFaqService<T>
    {
        Task<FaqResponse<T>> AskThenIAnswer(FaqRequest request);
    }
}
