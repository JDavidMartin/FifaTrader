using System.Net;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IStatusCodeHandler
    {
        string HandleBiddingStatusCode(HttpStatusCode statusCode);

        string HandleSellingStatusCode(HttpStatusCode statusCode);

        string HandleTokenCheckStatusCode(HttpStatusCode statusCode);
    }
}
