using FaqTemplate.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaqTemplate.Core.Domain;
using System.Net.Http;
using System.Net.Http.Headers;
using FaqTemplate.Infrastructure.Domain;
using Newtonsoft.Json;
using System.Web;

namespace FaqTemplate.Infrastructure.Services
{
    public class QnaMakerFaqService : IFaqService<string>
    {
        private readonly HttpClient _httpClient;
        private readonly QnaMakerConfiguration _configuration;

        public QnaMakerFaqService(QnaMakerConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<FaqResponse<string>> AskThenIAnswer(FaqRequest request)
        {
            var qnaRequest = new QnaMakerRequest { Question = request.Ask };
            var requestContent = new StringContent(JsonConvert.SerializeObject(qnaRequest), Encoding.UTF8, "application/json");
            var requestMessage = GetRequestMessage(requestContent);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseContent = await response.Content.ReadAsStringAsync();
            var qnaResponse = JsonConvert.DeserializeObject<QnaMakerResponse>(responseContent);
            
            return new FaqResponse<string> { Answer = HttpUtility.HtmlDecode(qnaResponse.Answer), Score = qnaResponse.Score };
        }

        private HttpRequestMessage GetRequestMessage(HttpContent content)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://westus.api.cognitive.microsoft.com/qnamaker/v1.0/knowledgebases/{_configuration.KnowledgbaseBaseId}/generateAnswer"),
                Method = HttpMethod.Post,
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("Ocp-Apim-Subscription-Key", _configuration.OcpApimSubscrptionKey);
            return request;
        }

    }
}
