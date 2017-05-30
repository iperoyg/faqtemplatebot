using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaqTemplate.Core.Domain;
using FaqTemplate.Core.Services;
using Takenet.MessagingHub.Client.Sender;
using System.Threading;
using Takenet.MessagingHub.Client;
using Lime.Protocol;

namespace FaqTemplate.Bot.Services
{
    public class SimpleQnaFaqService : IResponseService
    {
        private readonly IMessagingHubSender _sender;
        private readonly IFaqService<string> _faqService;

        public SimpleQnaFaqService(
            IMessagingHubSender sender,
            IFaqService<string> faqService)
        {
            _sender = sender;
            _faqService = faqService;
        }

        public async Task<FaqResponse<string>> AnswerQuestionAsync(FaqRequest question, Node to, CancellationToken cancellationToken)
        {
            var response = await _faqService.AskThenIAnswer(question);

            if (response.Score >= 0.8)
            {
                await _sender.SendMessageAsync($"{response.Answer}", to, cancellationToken);
            }
            else if (response.Score >= 0.5)
            {
                await _sender.SendMessageAsync($"Eu acho que a resposta para o que você precisa é:", to, cancellationToken);
                cancellationToken.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                await _sender.SendMessageAsync($"{response.Answer}", to, cancellationToken);

            }
            else
            {
                await _sender.SendMessageAsync($"Infelizmente eu ainda não sei isso! Mas vou me aprimorar, prometo!", to, cancellationToken);
            }

            return response;
        }
    }
}
