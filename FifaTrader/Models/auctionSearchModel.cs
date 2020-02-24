using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FifaTrader.Models
{
    public class auctionSearchModel
    {
        [JsonPropertyName("auctionInfo")]
        public List<PlayerSearchModel> AuctionInfo { get; set; }
    }
}
