using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FifaTrader.Models
{
    public class auctionSearchModel
    {
        [JsonPropertyName("auctionInfo")]
        public List<BidViewModel> AuctionInfo { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("credits")]
        public int Credits { get; set; }
    }
}
