
using Newtonsoft.Json;

namespace FifaTrader.Models
{
    public class BidViewModel
    {
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("currentBid")]
        public int BidPrice { get; set; }

        [JsonProperty("bidState")]
        public string Status { get; set; }

        [JsonProperty("expires")]
        public int TimeRemaining { get; set; }

        public bool Pending { get; set; } = true;

        [JsonProperty("tradeState")]
        public string TradeState { get; set; }

        [JsonProperty("itemData")]
        public BidViewItemData ItemData { get; set; }
    }

    public class BidViewItemData
    {
        [JsonProperty("id")]
        public string playerId { get; set; }
    }
}
