using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqTemplate.Infrastructure.Domain
{
    public class QnaMakerRequest
    {
        [JsonProperty(PropertyName = "question")]
        public string Question { get; set; }
    }
}
