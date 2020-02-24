using FifaTrader.APIHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.GetRequests
{
    public class GetRequestMaker : IGetRequestMaker
    {
        async Task<string> IGetRequestMaker.MakeGetRequest(string url, string accessToken)
        {
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-UT-SID", accessToken);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException();
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
