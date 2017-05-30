using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Sender;

namespace FaqTemplate.Bot.Extensions
{
    public class MessagingHubSenderDecorator : IMessagingHubSender
    {
        private readonly IMessagingHubSender _sender;

        public MessagingHubSenderDecorator(IMessagingHubSender sender)
        {
            _sender = sender;
        }

        public async Task<Command> SendCommandAsync(Command command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = await _sender.SendCommandAsync(command, cancellationToken);
            return c;
        }

        public async Task SendCommandResponseAsync(Command command, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sender.SendCommandResponseAsync(command, cancellationToken);
        }

        public async Task SendMessageAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {

            await _sender.SendMessageAsync(message, cancellationToken);
        }

        public async Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _sender.SendNotificationAsync(notification, cancellationToken);
        }
    }
}
