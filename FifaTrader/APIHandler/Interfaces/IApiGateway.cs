using FifaTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IApiGateway
    {
        Task<List<BidViewModel>> FetchPlayers(int playerId, int bidPrice, string accessToken);
    }
}
