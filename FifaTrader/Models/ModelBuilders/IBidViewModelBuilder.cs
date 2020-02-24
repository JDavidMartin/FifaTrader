using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaTrader.Models.ModelBuilders
{
    public interface IBidViewModelBuilder
    {
        List<BidViewModel> ConvertSearchModelToBidView(List<PlayerSearchModel> searchModels);
    }
}
