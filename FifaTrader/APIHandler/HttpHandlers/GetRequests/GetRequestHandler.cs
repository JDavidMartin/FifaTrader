using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.GetRequests
{
    public class GetRequestHandler : IGetRequestHandler
    {
        private readonly IGetRequestMaker _getRequestMaker;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public GetRequestHandler(IGetRequestMaker getRequestMaker, IUrlBuilder urlBuilder,
            IStatusCodeHandler statusCodeHandler)
        {
            _getRequestMaker = getRequestMaker;
            _urlBuilder = urlBuilder;
            _statusCodeHandler = statusCodeHandler;
        }

        public async Task<string> CheckToken(string accessToken)
        {
            var url = _urlBuilder.GetCheckTokenUrl();
            var responseCode = await _getRequestMaker.MakeGetRequestStatusCode(url, accessToken);
            return _statusCodeHandler.HandleTokenCheckStatusCode(responseCode);
        }

        public Task<List<BidViewModel>> GetTransferList(string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BidViewModel>> GetTransferTargets(string accessToken)
        {
            var url = _urlBuilder.GetTransferTargetsUrl();
            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var auctionInfo = JsonConvert.DeserializeObject<auctionSearchModel>(response);

                return auctionInfo.AuctionInfo;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<BidViewModel>();
            }
        }

        public async Task<List<BidViewModel>> SearchForSpecificPlayer(int playerId, int bidPrice, string accessToken)
        {
            var url = _urlBuilder.BuildSearchUrl(playerId, bidPrice);

            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var auctionInfo = JsonConvert.DeserializeObject<auctionSearchModel>(response);

                return auctionInfo.AuctionInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<BidViewModel>();
            }
        }
    }
}
