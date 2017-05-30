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
using Takenet.MessagingHub.Client.Extensions.Bucket;

namespace FaqTemplate.Bot.Receivers
{
    public class PlainCommandMessageReceiver : BaseMessageReceiver
    {
        private readonly IMessagingHubSender _sender;

        public PlainCommandMessageReceiver(
            IMessagingHubSender sender, 
            IBucketExtension bucket) : base(bucket)
        {
            _sender = sender;
        }

        public override async Task ReceiveAsync(Message envelope, CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = envelope.Content.ToString().ToLower();

            if("/ajuda".Equals(command))
            {
                await _sender.SendMessageAsync($"Ok! Eu sou InvestBot, um chatbot que responde perguntas mais frequentes sobre investimentos.", envelope.From, cancellationToken);
            }
            
        }
    }
}
