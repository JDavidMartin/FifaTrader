using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.PutRequests
{
    public class PutRequestHandler : IPutRequestHandler
    {
        private readonly IUrlBuilder _urlBuilder;
        private readonly IPutRequestMaker _putRequestMaker;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public PutRequestHandler(IUrlBuilder urlBuilder, IPutRequestMaker putRequestMaker,
            IStatusCodeHandler statusCodeHandler)
        {
            _urlBuilder = urlBuilder;
            _putRequestMaker = putRequestMaker;
            _statusCodeHandler = statusCodeHandler;
        }

        public async Task<HttpStatusCode> MovePlayerToTradePile(string tradeId, string playerId, string accessToken)
        {
            var url = _urlBuilder.GetItemUrl();
            var moveModel = new MovePlayerBodyModel
            {
                ItemData = new List<MovePlayerDataModel>
                {
                    new MovePlayerDataModel
                    {
                        Pile = "trade",
                        PlayerId = playerId,
                        tradeId = tradeId
                    }

                }
            };

            var response = await _putRequestMaker.MovePlayerToTradePile(url, accessToken, moveModel);

            return response;
        }

        public async Task<string> PutBidOnPlayer(string tradeId, int bidPrice, string accessToken)
        {
            var url = _urlBuilder.BuildBidUrl(tradeId);
            var bidResponse = await _putRequestMaker.PutBidOnPlayer(url, accessToken, bidPrice);
            var bidMessage = _statusCodeHandler.HandleBiddingStatusCode(bidResponse);

            return bidMessage;
        }
    }
}
