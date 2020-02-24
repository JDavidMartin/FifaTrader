using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.GetRequests
{
    public class GetRequestHandler : IGetRequestHandler
    {
        private readonly IGetRequestMaker _getRequestMaker;
        private readonly IUrlBuilder _urlBuilder;

        public GetRequestHandler(IGetRequestMaker getRequestMaker, IUrlBuilder urlBuilder)
        {
            _getRequestMaker = getRequestMaker;
            _urlBuilder = urlBuilder;
        }
        public Task<List<BidViewModel>> GetTransferList(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<BidViewModel>> GetTransferTargets(string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlayerSearchModel>> SearchForSpecificPlayer(int playerId, int bidPrice, string accessToken)
        {
            var url = _urlBuilder.BuildSearchUrl(playerId, bidPrice);

            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var auctionInfo = JsonSerializer.Deserialize<auctionSearchModel>(response);

                return auctionInfo.AuctionInfo;
            }
            catch
            {
                return new List<PlayerSearchModel>();
            }
        }
    }
}
