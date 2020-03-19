using Newtonsoft.Json;
using System.Collections.Generic;

namespace FifaTrader.Models
{
    public class MovePlayerBodyModel
    {
        [JsonProperty("itemData")]
        public List<MovePlayerDataModel> ItemData { get; set; }
    }

    public class MovePlayerDataModel
    {
        [JsonProperty("id")]
        public string PlayerId { get; set; }

        [JsonProperty("pile")]
        public string Pile { get; set; }

        [JsonProperty("tradeId")]
        public string tradeId { get; set; }
    }
}
