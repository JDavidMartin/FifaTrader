using System.Collections.Generic;
using System.Linq;

namespace FifaTrader.Models.ModelBuilders
{
    public class TradeIdsBuilder : ITradeIdsBuilder
    {
        public TradeIdsModel GetTradeIds(List<BidViewModel> allPlayers)
        {
            var ids = allPlayers.Where(x => x.Status != "highest").Select(x => x.TradeId);

            return new TradeIdsModel
            {
                TradeIds = string.Join(",", ids)
            };
        }
    }
}
