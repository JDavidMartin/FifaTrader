using System.Text.Json.Serialization;

namespace FifaTrader.Models
{
    public class BidViewModel
    {
        public int TradeId { get; set; }
        public int BidPrice { get; set; }
        public string Status { get; set; }
        public int TimeRemaining { get; set; }
        public bool Pending { get; set; } = true;
    }
}
