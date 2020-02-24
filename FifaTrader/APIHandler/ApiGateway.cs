using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using FifaTrader.Models.ModelBuilders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler
{
    public class ApiGateway : IApiGateway
    {
        private readonly IGetRequestHandler _getRequestHandler;
        private readonly IBidViewModelBuilder _modelBuilder;

        public ApiGateway(IGetRequestHandler getRequestHandler, IBidViewModelBuilder modelBuilder)
        {
            _getRequestHandler = getRequestHandler;
            _modelBuilder = modelBuilder;
        }

        public async Task<List<BidViewModel>> FetchPlayers(int playerId, int bidPrice, string accessToken)
        {
            var searchList = await _getRequestHandler.SearchForSpecificPlayer(playerId, bidPrice, accessToken);
            var players = _modelBuilder.ConvertSearchModelToBidView(searchList);
            return players;
        }
    }
}
