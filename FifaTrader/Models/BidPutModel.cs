using Newtonsoft.Json;

namespace FifaTrader.Models
{
    public class BidPutModel
    {
        [JsonProperty("bid")]
        public int BidPrice { get; set; }
    }
}
