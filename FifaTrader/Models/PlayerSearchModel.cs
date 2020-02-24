using System.Text.Json.Serialization;

namespace FifaTrader.Models
{
    public class PlayerSearchModel
    {
        [JsonPropertyName("tradeId")]
        public int TradeId { get; set; }

        [JsonPropertyName("expires")]
        public int TimeRemaining { get; set; }

        [JsonPropertyName("price")]
        public int CurrentPrice { get; set; }
    }
}
