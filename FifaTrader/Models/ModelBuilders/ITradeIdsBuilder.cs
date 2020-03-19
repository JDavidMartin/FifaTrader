using System.Collections.Generic;

namespace FifaTrader.Models.ModelBuilders
{
    public interface ITradeIdsBuilder
    {
        public TradeIdsModel GetTradeIds(List<BidViewModel> allPlayers);
    }
}
