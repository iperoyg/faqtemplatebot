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
using Takenet.MessagingHub.Client.Extensions.Session;
using Takenet.MessagingHub.Client.Extensions.Bucket;
using FaqTemplate.Bot.Models;
using FaqTemplate.Bot.Extensions;
using Takenet.MessagingHub.Client.Extensions.Resource;
using Lime.Messaging.Contents;
using FaqTemplate.Bot.Services;

namespace FaqTemplate.Bot.Receivers
{
    public class PlainTextMessageReceiver : BaseMessageReceiver
    {
        private readonly IMessagingHubSender _sender;
        private readonly Settings _settings;
        private readonly IBucketExtension _bucketExtension;
        private readonly IStateManager _stateManager;
        private readonly IResourceExtension _resourceExtension;
        private readonly IResponseService _responseService;
        private readonly IStateService<string> _state;

        public PlainTextMessageReceiver(
            IMessagingHubSender sender,
            IResponseService responseService,
            IBucketExtension bucketExtension,
            IStateManager stateManager,
            IResourceExtension resourceExtension,
            Settings settings) : base(bucketExtension)
        {
            _sender = sender;
            _responseService = responseService;
            _settings = settings;
            _bucketExtension = bucketExtension;
            _stateManager = stateManager;
            _resourceExtension = resourceExtension;
        }

        public override async Task ReceiveAsync(Message message, CancellationToken cancellationToken)
        {
            Trace.TraceInformation($"From: {message.From} \tContent: {message.Content}");

            var userContext = await GetUserContext(message.From);
            if(userContext == null)
            {
                userContext = new UserContext();
                await SetUserContext(message.From, userContext);
            }

            if(string.IsNullOrWhiteSpace(userContext.CPF))
            {
                userContext.PreviouslyInteraction = message.Content.ToString();
                await SetUserContext(message.From, userContext);
                await _stateManager.SetStateAsync(message.From.ToIdentity(), "needCpf");
                var menu = await _resourceExtension.GetAsync<Select>("testerec");
                await _sender.SendMessageAsync("Antes de continuarmos, rola de vc me informar seu cpf?", message.From, cancellationToken);
                await _sender.SendMessageAsync(menu, message.From, cancellationToken);
                return;
            }
            

            var request = new FaqRequest { Ask = message.Content.ToString() };
            var response = await _responseService.AnswerQuestionAsync(request, message.From, cancellationToken);
            
        }

        
    }
}
