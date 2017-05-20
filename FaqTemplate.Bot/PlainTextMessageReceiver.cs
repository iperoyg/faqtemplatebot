using System;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using System.Diagnostics;
using FaqTemplate.Core.Services;
using FaqTemplate.Core.Domain;

namespace FaqTemplate.Bot
{
    public class PlainTextMessageReceiver : IMessageReceiver
    {
        private readonly IFaqService<string> _faqService;
        private readonly IMessagingHubSender _sender;
        private readonly Settings _settings;

        public PlainTextMessageReceiver(IMessagingHubSender sender, IFaqService<string> faqService, Settings settings)
        {
            _sender = sender;
            _faqService = faqService;
            _settings = settings;
        }

        public async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"From: {message.From} \tContent: {message.Content}");

            var request = new FaqRequest { Ask = message.Content.ToString() };
            var response = await _faqService.AskThenIAnswer(request);

            if (response.Score >= 0.8)
            {
                await _sender.SendMessageAsync($"{response.Answer}", message.From, cancellationToken);
            }
            else if(response.Score >= 0.5)
            {
                await _sender.SendMessageAsync($"Eu acho que a resposta para o que você precisa é:", message.From, cancellationToken);
                cancellationToken.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
                await _sender.SendMessageAsync($"{response.Answer}", message.From, cancellationToken);

            }
            else
            {
                await _sender.SendMessageAsync($"Infelizmente eu ainda não sei isso! Mas vou me aprimorar, prometo!", message.From, cancellationToken);
            }

            await _sender.SendMessageAsync($"{response.Score}: {response.Answer}", message.From, cancellationToken);
        }
    }
}
