using FifaTrader.APIHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers
{
    public class HttpWrapper : IHttpWrapper
    {
        private HttpClient _client;
        public HttpWrapper()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await _client.DeleteAsync(uri);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent body)
        {
            return await _client.PostAsync(uri, body); ;
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, HttpContent body)
        {
            return await _client.PutAsync(uri, body);
        }

        public void SetAccessToken(string accessToken)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("X-UT-SID", accessToken);
        }
    }
}
