using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.DeleteRequests
{
    public class DeleteHandler : IDeleteHandler
    {
        private readonly IDeleteMaker _deleteMaker;
        private readonly ITradeIdsBuilder _tradeIdsBuilder;
        private readonly IUrlBuilder _urlBuilder;

        public DeleteHandler(IDeleteMaker deleteMaker, ITradeIdsBuilder tradeIdsBuilder, IUrlBuilder urlBuilder)
        {
            _deleteMaker = deleteMaker;
            _tradeIdsBuilder = tradeIdsBuilder;
            _urlBuilder = urlBuilder;
        }
        public async Task DeleteExpiredPlayers(string accessToken, List<BidViewModel> players)
        {
            var tradeIds = _tradeIdsBuilder.GetTradeIds(players);
            var url = _urlBuilder.BuildDeletePlayerUrl(tradeIds.TradeIds);
            await _deleteMaker.MakeDeleteRequest(accessToken, url);
        }
    }
}
