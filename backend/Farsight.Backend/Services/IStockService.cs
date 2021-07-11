using System;
using System.Threading.Tasks;
using Farsight.Backend.Models.DTOs;

namespace Farsight.Backend.Services
{
    public interface IStockService
    {
        Task<AlphavantageWeeklyAdjustedResponse> GetHistoricalClosePrice(string ticker);
    }
}