using Newtonsoft.Json;

namespace FifaTrader.Models
{
    public class PlayerSearchModel
    {
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("expires")]
        public int TimeRemaining { get; set; }
    }
}
