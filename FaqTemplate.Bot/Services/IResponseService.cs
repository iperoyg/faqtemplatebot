using FaqTemplate.Core.Domain;
using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FaqTemplate.Bot.Services
{
    public interface IResponseService
    {
        Task<FaqResponse<string>> AnswerQuestionAsync(FaqRequest question, Node to, CancellationToken cancellationToken);
    }
}
