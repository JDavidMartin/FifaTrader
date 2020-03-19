using FifaTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IDeleteHandler
    {
        public Task DeleteExpiredPlayers(string accessToken, List<BidViewModel> players);
    }
}
