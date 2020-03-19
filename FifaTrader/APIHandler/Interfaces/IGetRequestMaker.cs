using System.Net;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IGetRequestMaker
    {
        Task<string> MakeGetRequest(string url, string accessToken);
        Task<HttpStatusCode> MakeGetRequestStatusCode(string url, string accessToken);
    }
}
