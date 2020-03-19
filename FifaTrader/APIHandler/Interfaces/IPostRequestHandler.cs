using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IPostRequestHandler
    {
        Task<string> SellPlayer(string playerId, string accessToken, int startPrice, int BinPrice);
    }
}
