namespace FifaTrader.APIHandler.Interfaces
{
    public interface IUrlBuilder
    {
        public string BuildSearchUrl(int playerId, int bidPrice);
        public string BuildBidUrl(string tradeId);
        public string GetTransferTargetsUrl();
        public string GetItemUrl();
        public string GetAuctionUrl();
        public string GetCheckTokenUrl();
        public string BuildDeletePlayerUrl(string tradeIds);
    }
}
