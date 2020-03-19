using FifaTrader.Models;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IDeleteMaker
    {
        public Task MakeDeleteRequest(string token, string uri);
    }
}
