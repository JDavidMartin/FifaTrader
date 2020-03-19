using FifaTrader.Models;
using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IPutRequestMaker
    {
        Task<HttpStatusCode> PutBidOnPlayer(string url, string accessToken, int bidPrice);

        Task<HttpStatusCode> MovePlayerToTradePile(string url, string accessToken, MovePlayerBodyModel playerDetails);
    }
}
