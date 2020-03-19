using FifaTrader.APIHandler.Interfaces;
using System.Net;

namespace FifaTrader.APIHandler.HttpHandlers
{
    public class StatusCodeHandler : IStatusCodeHandler
    {
        public string HandleBiddingStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case (HttpStatusCode.OK):
                    return "Success";
                case (HttpStatusCode.TooManyRequests):
                    return "TooManyRequests";
                case (HttpStatusCode.UpgradeRequired):
                    return "TooManyRequests";
                case (HttpStatusCode.Unauthorized):
                    return "Expired";
                case (HttpStatusCode.Forbidden):
                    return "ListFull";
                default:
                    return "Error";
            }
        }

        public string HandleSellingStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case (HttpStatusCode.OK):
                    return "Selling";
                default:
                    return "Error";
            }
        }

        public string HandleTokenCheckStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case (HttpStatusCode.OK):
                    return "Valid";
                default:
                    return "Invalid";
            }
        }

    }
}
