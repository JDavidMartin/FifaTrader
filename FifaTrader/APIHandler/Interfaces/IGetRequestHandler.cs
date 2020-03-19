using FifaTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IGetRequestHandler
    {
        Task<List<BidViewModel>> SearchForSpecificPlayer(int playerId, int bidPrice, string accessToken);

        Task<List<BidViewModel>> GetTransferTargets(string accessToken);

        Task<List<BidViewModel>> GetTransferList(string accessToken);

        Task<string> CheckToken(string accessToken);
    }
}
