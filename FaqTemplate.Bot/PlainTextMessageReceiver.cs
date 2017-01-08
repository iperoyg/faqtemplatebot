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
            var result = await _faqService.AskThenIAnswer(request);

            await _sender.SendMessageAsync($"{result.Score}: {result.Answer}", message.From, cancellationToken);
        }
    }
}
