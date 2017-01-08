using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Infrastructure.Domain
{
    public class QnaMakerResponse
    {
        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }
        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }
    }
}
