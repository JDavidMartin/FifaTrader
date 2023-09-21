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
                Pending = true,
                TimeRemaining = x.TimeRemaining,
                TradeId = x.TradeId
            }).ToList();
        }

        public List<BidViewModel> PopulateDefaultFieldsOfBidViews(List<BidViewModel> players)
        {

            foreach(var player in players)
            {
                if (player.TimeRemaining == -1)
                {
                    player.Pending = false;
                }
                if(player.Status == "none")
                {
                    player.Status = player.TradeState;
                }
            }

            return players;
        }
    }
}
