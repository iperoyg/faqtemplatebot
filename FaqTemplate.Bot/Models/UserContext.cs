using Lime.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Bot.Models
{
    [DataContract]
    public class UserContext : Document
    {
        public static MediaType MEDIA_TYPE { get { return MediaType.Parse("application/lime.faqbot.usercontext+json"); } }

        public UserContext() : base(MEDIA_TYPE) { }

        [DataMember(Name = "cpf")]
        public string CPF { get; set; }

        [DataMember(Name = "prevInteraction")]
        public string PreviouslyInteraction { get; set; }

    }
}
