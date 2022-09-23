using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models.EnvVariables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FifaTrader.APIHandler.HttpHandlers.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private FifaYear _fifaYearConfiguration;

        private string _searchBase;
        private string _bidBase;
        private string _watchListBase;
        private string _tradePileBase;
        public UrlBuilder(IOptions<FifaYear> fifaYear)
        {
            _fifaYearConfiguration = fifaYear.Value;
            _searchBase = $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/transfermarket?type=player";
            _bidBase = $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/trade/";
            _watchListBase = $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/watchlist";
            _tradePileBase = $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/tradepile";
        }

        public string BuildBidUrl(string tradeId)
        {
            var bidQuery = $"{tradeId}/bid";

            return _bidBase + bidQuery;
        }

        public string BuildDeletePlayerUrl(string tradeIds)
        {
            var query = $"?tradeId={tradeIds}";
            return _watchListBase + query;
        }

        public string BuildSearchForLeagueRarityUrl(int leagueId, int rarityId, int bidPrice, string positionId, int nationId)
        {
            string parameterQuery;
            if (bidPrice <= 1000)
            {
                parameterQuery = $"&rarityIds={rarityId}&lev=gold&macr={bidPrice - 50}&num=21&start=0";
            }
            else
            {
                parameterQuery = $"&rarityIds={rarityId}&lev=gold&macr={bidPrice - 100}&num=21&start=0";
            }

            if (leagueId != 0)
            {
                parameterQuery += $"&leag={leagueId}";
            }
            if (!string.IsNullOrEmpty(positionId))
            {
                parameterQuery += $"&pos={positionId}";
            }
            if(nationId != 0)
            {
                parameterQuery += $"&nat={nationId}";
            }

            return _searchBase + parameterQuery;
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
            return _searchBase + parameterQuery;
        }

        public string GetAuctionUrl()
        {
            return $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/auctionhouse";
        }

        public string GetCheckTokenUrl()
        {
            return $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/user/credits";
        }

        public string GetItemUrl()
        {
            return $"https://utas.mob.v1.fut.ea.com/ut/game/{_fifaYearConfiguration.Year}/item";
        }

        public string GetTransferTargetsUrl()
        {
            return _watchListBase;
        }
    }
}
