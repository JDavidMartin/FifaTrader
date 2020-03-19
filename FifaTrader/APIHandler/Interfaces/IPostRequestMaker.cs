using FifaTrader.Models;
using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IPostRequestMaker
    {
        Task<HttpStatusCode> SellPlayer(string url, string accessToken, SellPlayerModel sellPlayerModel);
    }
}
