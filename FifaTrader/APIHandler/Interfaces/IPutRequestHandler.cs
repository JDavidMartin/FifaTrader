using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IPutRequestHandler
    {
        Task<string> PutBidOnPlayer(string tradeId, int bidPrice, string accessToken);

        Task<HttpStatusCode> MovePlayerToTradePile(string tradeId, string playerId, string accessToken);
    }
}
