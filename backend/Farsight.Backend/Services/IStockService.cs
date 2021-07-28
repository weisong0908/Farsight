using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models.DTOs;

namespace Farsight.Backend.Services
{
    public interface IStockService
    {
        Task<IList<PolygonTicker>> GetTickers();
        Task<PolygonTickerDetails> GetTickerDetails(string ticker);
        Task<PolygonAggregatesResponse> GetPreviousClosePrice(string ticker);
        Task<PolygonAggregatesResponse> GetDailyClosePrice(string ticker, string from, string to);
    }
}