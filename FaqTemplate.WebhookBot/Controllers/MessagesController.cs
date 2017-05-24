using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FaqTemplate.WebhookBot.Controllers
{
    [RoutePrefix("messages")]
    public class MessagesController : ApiController
    {
        [Route("teste/{data}")]
        [HttpGet]
        public string TesteData(string data)
        {
            return data;
        }
    }
}
