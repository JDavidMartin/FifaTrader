using Newtonsoft.Json;

namespace FifaTrader.Models
{
    public class SellPlayerModel
    {
        [JsonProperty("startingBid")]
        public int StartPrice { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; } = 3600;

        [JsonProperty("buyNowPrice")]
        public int BinPrice { get; set; }

        [JsonProperty("itemData")]
        public SellItemDataModel ItemData { get; set; }
    }

    public class SellItemDataModel
    {
        [JsonProperty("id")]
        public string playerId { get; set; }
    }
}
