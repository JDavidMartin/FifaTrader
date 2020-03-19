using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.PutRequests
{
    public class PutRequestMaker : IPutRequestMaker
    {
        private readonly IHttpWrapper _wrapper;

        public PutRequestMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public async Task<HttpStatusCode> MovePlayerToTradePile(string url, string accessToken, MovePlayerBodyModel playerDetails)
        {
            _wrapper.SetAccessToken(accessToken);

            //var listData = new List<MovePlayerBodyModel>
            //{
            //    playerDetails
            //};

            var stringContent = new StringContent(JsonConvert.SerializeObject(playerDetails));

            var response = await _wrapper.PutAsync(url, stringContent);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutBidOnPlayer(string url, string accessToken, int bidPrice)
        {
            _wrapper.SetAccessToken(accessToken);

            var bidModel = new BidPutModel
            {
                BidPrice = bidPrice
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(bidModel));

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-UT-SID", accessToken);
                    var response = await client.PutAsync(url, stringContent);

                    return response.StatusCode;
                }
            }
            catch (System.Exception)
            {
                return HttpStatusCode.Unauthorized;
            }
           
            //var response = await _wrapper.PutAsync(url, stringContent);

            //return response.StatusCode;
        }
    }
}
