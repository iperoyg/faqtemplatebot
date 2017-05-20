using FaqTemplate.Core.Domain;
using FaqTemplate.Core.Services;
using FaqTemplate.Infrastructure.Domain;
using FaqTemplate.Infrastructure.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takenet.MessagingHub.Client.Host;

namespace FaqTemplate.Bot
{
    public class ServiceProvider : Container, IServiceContainer
    {
        public ServiceProvider()
        {
            Initialize();
        }

        public void RegisterService(Type serviceType, Func<object> instanceFactory)
        {
        }

        public void RegisterService(Type serviceType, object instance)
        {
            if (serviceType == typeof(Settings))
            {
                var settings = instance as Settings;
                var qnaConfig = new QnaMakerConfiguration { KnowledgbaseBaseId = settings.QnaMaker.BaseId, OcpApimSubscrptionKey = settings.QnaMaker.Key };
                var cache = new SimpleMemoryCacheService<FaqResponse<string>>();
                RegisterSingleton<ICacheService<FaqResponse<string>>>(() => cache);
                RegisterSingleton<IFaqService<string>>(new QnaMakerFaqService(qnaConfig, cache));
            }
            RegisterSingleton(serviceType, instance);
        }

        private void Initialize()
        {
            this.Options.AllowOverridingRegistrations = true;

        }
    }
}
