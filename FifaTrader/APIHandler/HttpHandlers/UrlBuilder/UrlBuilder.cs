using FifaTrader.APIHandler.Interfaces;

namespace FifaTrader.APIHandler.HttpHandlers.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private string searchBase = "https://utas.external.s2.fut.ea.com/ut/game/fifa20/transfermarket?type=player";
        public string BuildSearchUrl(int playerId, int bidPrice)
        {
            var parameterQuery = $"&maskedDefId={playerId}&macr={bidPrice - 100}&num=21&start=0";

            return searchBase + parameterQuery;
        }
    }
}
