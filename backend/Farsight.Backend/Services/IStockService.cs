using System.Threading.Tasks;
using Farsight.Backend.Models.DTOs;

namespace Farsight.Backend.Services
{
    public interface IStockService
    {
        Task<PolygonTickerDetails> GetTickerDetails(string ticker);
        Task<PolygonAggregatesResponse> GetDailyClosePrice(string ticker, string from, string to);
    }
}