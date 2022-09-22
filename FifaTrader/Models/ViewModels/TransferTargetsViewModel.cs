using System.Collections.Generic;

namespace FifaTrader.Models.ViewModels
{
    public class TransferTargetsViewModel
    {
        public int coins { get; set; }

        public List<BidViewModel> Bids { get; set; }

        public int numberOfPlayers { get; set; }
    }
}
