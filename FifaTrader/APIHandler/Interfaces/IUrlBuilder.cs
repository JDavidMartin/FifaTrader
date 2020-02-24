using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IUrlBuilder
    {
        public string BuildSearchUrl(int playerId, int bidPrice);
    }
}
