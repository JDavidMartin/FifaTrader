using System.Collections.Generic;
using System.Linq;

namespace FifaTrader.Models.ModelBuilders
{
    public class BidViewModelBuilder : IBidViewModelBuilder
    {
        public List<BidViewModel> ConvertSearchModelToBidView(List<PlayerSearchModel> searchModels)
        {
            if (searchModels.Count < 1)
            {
                return new List<BidViewModel>
                {
                    new BidViewModel
                    {
                        Status = "Error",
                        Pending = false,
                    }
                };
            }

            return searchModels.Select(x => new BidViewModel
            {
                Status = "Pending",
                BidPrice = x.CurrentPrice,
                Pending = true,
                TimeRemaining = x.TimeRemaining,
                TradeId = x.TradeId
            }).ToList();
        }
    }
}
