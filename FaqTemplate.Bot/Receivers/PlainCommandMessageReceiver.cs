using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using Takenet.MessagingHub.Client.Sender;
using Takenet.MessagingHub.Client;

namespace FaqTemplate.Bot.Receivers
{
    public class PlainCommandMessageReceiver : IMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public PlainCommandMessageReceiver(IMessagingHubSender sender)
        {
            _sender = sender;
        }

        public async Task ReceiveAsync(Message envelope, CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = envelope.Content.ToString();

            if("/ajuda".Equals(command))
            {
                await _sender.SendMessageAsync($"Ok! Eu sou InvestBot, um chatbot que responde perguntas mais frequentes sobre investimentos.", envelope.From, cancellationToken);
            }
            
        }
    }
}
