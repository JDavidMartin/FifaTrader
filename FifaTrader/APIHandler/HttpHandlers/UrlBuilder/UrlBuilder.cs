using FifaTrader.APIHandler.Interfaces;

namespace FifaTrader.APIHandler.HttpHandlers.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private string searchBase = "https://utas.external.s2.fut.ea.com/ut/game/fifa22/transfermarket?type=player";
        private string bidBase = "https://utas.external.s2.fut.ea.com/ut/game/fifa22/trade/";
        private string watchListBase = "https://utas.external.s2.fut.ea.com/ut/game/fifa22/watchlist";

        public string BuildBidUrl(string tradeId)
        {
            var bidQuery = $"{tradeId}/bid";

            return bidBase + bidQuery;
        }

        public string BuildDeletePlayerUrl(string tradeIds)
        {
            var query = $"?tradeId={tradeIds}";
            return watchListBase + query;
        }

        public string BuildSearchForLeagueRarityUrl(int leagueId, int rarityId, int bidPrice)
        {
            string parameterQuery;
            if (bidPrice <= 1000)
            {
                parameterQuery = $"&leag={leagueId}&rarityIds={rarityId}&lev=gold&macr={bidPrice - 50}&num=21&start=0";
            }
            else
            {
                parameterQuery = $"&leag={leagueId}&rarityIds={rarityId}&lev=gold&macr={bidPrice - 100}&num=21&start=0";
            }
            return searchBase + parameterQuery;
        }

        public string BuildSearchUrl(int playerId, int bidPrice)
        {
            string parameterQuery;
            if (bidPrice <= 1000)
            {
                parameterQuery = $"&maskedDefId={playerId}&macr={bidPrice - 50}&num=21&start=0";
            }
            else
            {
                parameterQuery = $"&maskedDefId={playerId}&macr={bidPrice - 100}&num=21&start=0";
            }
            return searchBase + parameterQuery;
        }

        public string GetAuctionUrl()
        {
            return "https://utas.external.s2.fut.ea.com/ut/game/fifa22/auctionhouse";
        }

        public string GetCheckTokenUrl()
        {
            return "https://utas.external.s2.fut.ea.com/ut/game/fifa22/user/credits";
        }

        public string GetItemUrl()
        {
            return "https://utas.external.s2.fut.ea.com/ut/game/fifa22/item";
        }

        public string GetTransferTargetsUrl()
        {
            return watchListBase;
        }
    }
}
