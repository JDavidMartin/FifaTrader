using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FifaTrader.APIHandler.Interfaces
{
    public interface IHttpWrapper
    {
        Task<HttpResponseMessage> GetAsync(string uri);

        Task<HttpResponseMessage> PostAsync(string uri, HttpContent body);

        Task<HttpResponseMessage> PutAsync(string uri, HttpContent body);

        Task<HttpResponseMessage> DeleteAsync(string uri);

        void SetAccessToken(string accessToken);
    }
}
