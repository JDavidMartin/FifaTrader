using Newtonsoft.Json;

namespace FifaTrader.Models
{
    public class TradeIdsModel
    {
        [JsonProperty("tradeid")]
        public string TradeIds { get; set; }
    }
}
