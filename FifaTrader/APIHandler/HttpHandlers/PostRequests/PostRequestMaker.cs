using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.PostRequests
{
    public class PostRequestMaker : IPostRequestMaker
    {
        private readonly IHttpWrapper _wrapper;

        public PostRequestMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        public async Task<HttpStatusCode> SellPlayer(string url, string accessToken, SellPlayerModel sellPlayerModel)
        {
            _wrapper.SetAccessToken(accessToken);
            var body = new StringContent(JsonConvert.SerializeObject(sellPlayerModel));
            var response = await _wrapper.PostAsync(url, body);

            return response.StatusCode;
        }
    }
}
