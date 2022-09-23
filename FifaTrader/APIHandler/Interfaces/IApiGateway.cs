using FifaTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IApiGateway
    {
        Task<List<BidViewModel>> FetchPlayers(int playerId, int bidPrice, string accessToken);

        Task<List<BidViewModel>> FetchPlayersByLeague(int leagueId, int rarityId, int bidPrice, string accessToken, string positionId, int nationId);

        Task<string> BidOnPlayer(string tradeId, int bidPrice, string accessToken);

        Task<auctionSearchModel> GetTransferTargets(string accessToken);

        Task<string> SellPlayer(string tradeId, string playerId, string accessToken, int startPrice, int BinPrice);

        Task<string> CheckToken(string accessToken);

        Task ClearExpiredPlayers(string accessToken, List<BidViewModel> allPlayers);
    }
}
