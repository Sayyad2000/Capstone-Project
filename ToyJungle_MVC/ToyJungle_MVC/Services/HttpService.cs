using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThePlayCastleMVC.Models;

namespace ThePlayCastleMVC.Services
{
    public class HttpService
    {
        private HttpClient client = new HttpClient();

        private HttpClient GetConfiguredClient()
        {
            this.client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("X-Contract-Id", Properties.XContractId);
            //client.DefaultRequestHeaders.Add("X-Conversation-Id", Properties.ConversationId.ToString());
            client.DefaultRequestHeaders.Add("Accept-Language", "en");
            return client;
        }
        public async Task<T> Get<T>(string Url)
        {
            HttpResponseMessage response = await client.GetAsync(Url);
            var Result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(Result);
        }
        public async Task<ApiResponse> PostAsync(string uri, object inputModel)
        {

            //client = GetConfiguredClient();
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);




            var data = JsonConvert.SerializeObject(inputModel);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, httpContent);
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(content);
            return result;
        }

    }
}
