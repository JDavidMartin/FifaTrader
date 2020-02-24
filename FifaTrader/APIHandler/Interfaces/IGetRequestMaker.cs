using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IGetRequestMaker
    {
        Task<string> MakeGetRequest(string url, string accessToken);
    }
}
