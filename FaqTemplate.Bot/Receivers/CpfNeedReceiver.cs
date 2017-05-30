using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using FaqTemplate.Bot.Models;
using Takenet.MessagingHub.Client.Sender;
using FaqTemplate.Core.Services;
using Takenet.MessagingHub.Client.Extensions.Session;
using Takenet.MessagingHub.Client.Extensions.Bucket;
using Takenet.MessagingHub.Client;
using FaqTemplate.Bot.Extensions;
using System.Diagnostics;
using FaqTemplate.Core.Domain;
using System.Text.RegularExpressions;
using Takenet.MessagingHub.Client.Extensions.Resource;
using FaqTemplate.Bot.Services;

namespace FaqTemplate.Bot.Receivers
{
    public class CpfNeedReceiver : BaseMessageReceiver
    {
        private readonly IStateManager _stateManager;
        private readonly IBucketExtension _bucketExtension;
        private readonly ISessionManager _sessionManager;
        private readonly Settings _settings;
        private readonly IMessagingHubSender _sender;
        private readonly IResponseService _responseService;

        public CpfNeedReceiver(IMessagingHubSender sender,
            IResponseService responseService,
            ISessionManager sessionManager,
            IBucketExtension bucketExtension,
            IStateManager stateManager,
            Settings settings) : base(bucketExtension)
        {
            _sender = sender;
            _responseService = responseService;
            _settings = settings;
            _sessionManager = sessionManager;
            _bucketExtension = bucketExtension;
            _stateManager = stateManager;

        }

        public override async Task ReceiveAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cpf = message.Content.ToString();
            // --> .*(([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})).*
            var regex = "((^\\d)*\\d\\s*\\d\\s*\\d(\\s|\\-|\\.)*\\d\\s*\\d\\s*\\d(\\s|\\-|\\.)*\\d\\s*\\d\\s*\\d(\\s|\\-|\\.)*\\d\\s*\\d(^\\d)*)";
            var coll = Regex.Matches(cpf, regex);
            var result = coll[0].Groups[0].Value;
            cpf = result.Replace(".", "").Replace(" ", "").Replace("-", "").Replace("\\", "");


            var userContext = await GetUserContext(message.From);
            if (userContext == null)
            {
                userContext = new UserContext();
                await SetUserContext(message.From, userContext);
            }
            userContext.CPF = cpf;

            var request = new FaqRequest { Ask = userContext.PreviouslyInteraction };
            var response = await _responseService.AnswerQuestionAsync(request, message.From, cancellationToken);

            userContext.PreviouslyInteraction = null;

            await SetUserContext(message.From, userContext);

        }

      
    }
}
