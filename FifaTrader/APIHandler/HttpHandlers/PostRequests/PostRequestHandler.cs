using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.HttpHandlers.PostRequests
{
    public class PostRequestHandler : IPostRequestHandler
    {
        private readonly IPostRequestMaker _postRequestMaker;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public PostRequestHandler(IPostRequestMaker postRequestMaker,IUrlBuilder urlBuilder,
            IStatusCodeHandler statusCodeHandler)
        {
            _postRequestMaker = postRequestMaker;
            _urlBuilder = urlBuilder;
            _statusCodeHandler = statusCodeHandler;
        }
        public async Task<string> SellPlayer(string playerId, string accessToken, int startPrice, int binPrice)
        {
            var url = _urlBuilder.GetAuctionUrl();
            var sellModel = new SellPlayerModel
            {
                StartPrice = startPrice,
                BinPrice = binPrice,
                Duration = 3600,
                ItemData = new SellItemDataModel
                {
                    playerId = playerId
                }
            };

            var statusCode = await _postRequestMaker.SellPlayer(url, accessToken, sellModel);

            var sellMessage = _statusCodeHandler.HandleSellingStatusCode(statusCode);

            return sellMessage;
        }
    }
}
