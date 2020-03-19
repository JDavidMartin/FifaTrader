using FifaTrader.APIHandler.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.GetRequests
{
    public class GetRequestMaker : IGetRequestMaker
    {
        private readonly IHttpWrapper _wrapper;

        public GetRequestMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public async Task<HttpStatusCode> MakeGetRequestStatusCode(string url, string accessToken)
        {
            _wrapper.SetAccessToken(accessToken);
            var response = await _wrapper.GetAsync(url);

            return response.StatusCode;
        }

        async Task<string> IGetRequestMaker.MakeGetRequest(string url, string accessToken)
        {
            _wrapper.SetAccessToken(accessToken);

            var response = await _wrapper.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
