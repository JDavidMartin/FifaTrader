using FifaTrader.APIHandler.Interfaces;
using FifaTrader.Models.EnvVariables;
using Microsoft.Extensions.Options;

namespace FifaTrader.APIHandler.HttpHandlers.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private FifaYear _fifaYearConfiguration;

        private string _baseApiUrl;
        private string _searchBase;
        private string _bidBase;
        private string _watchListBase;
        private string _tradePileBase;
        private string _unassignedBase;
        public UrlBuilder(IOptions<FifaYear> fifaYear)
        {
            _fifaYearConfiguration = fifaYear.Value;
            _baseApiUrl = "https://utas.mob.v4.prd.futc-ext.gcp.ea.com/ut/game";
            _searchBase = $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/transfermarket?type=player";
            _bidBase = $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/trade/";
            _watchListBase = $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/watchlist";
            _tradePileBase = $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/tradepile";
            _unassignedBase = $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/purchased/items";
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
            return $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/auctionhouse";
        }

        public string GetCheckTokenUrl()
        {
            return $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/activeMessage";
        }

        public string GetItemUrl()
        {
            return $"{_baseApiUrl}/{_fifaYearConfiguration.Year}/item";
        }

        public string GetTransferListUrl()
        {
            return _tradePileBase;
        }

        public string GetTransferTargetsUrl()
        {
            return _watchListBase;
        }

        public string GetUnassignedPileUrl()
        {
            return _unassignedBase;
        }
    }
}
