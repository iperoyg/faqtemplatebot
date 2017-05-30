using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lime.Protocol;
using Takenet.MessagingHub.Client.Listener;
using FaqTemplate.Bot.Models;
using System.Diagnostics;
using Takenet.MessagingHub.Client.Extensions.Bucket;
using FaqTemplate.Bot.Extensions;

namespace FaqTemplate.Bot.Receivers
{
    public abstract class BaseMessageReceiver : IMessageReceiver
    {
        private readonly IBucketExtension _bucket;

        public BaseMessageReceiver(IBucketExtension bucket)
        {
            _bucket = bucket;
        }

        public abstract Task ReceiveAsync(Message message, CancellationToken cancellationToken = default(CancellationToken));


        protected async Task SetUserContext(Node from, UserContext userContext)
        {
            try
            {
                await _bucket.SetAsync(from.Name, userContext);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"From: {from} \tContent: {userContext.ToJsonString()}\tEx: {ex.ToString()}");
            }
        }

        protected async Task<UserContext> GetUserContext(Node from)
        {
            try
            {
                var data = await _bucket.GetAsync<UserContext>(from.Name);
                return data;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"From: {from} \tEx: {ex.ToString()}");
                return null;
            }
        }
    }
}
