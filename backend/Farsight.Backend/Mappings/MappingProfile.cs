using System;
using System.Linq;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Helpers;

namespace Farsight.Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Portfolio, PortfolioSimple>()
                .ForMember(ps => ps.HoldingCount, memberOptions => memberOptions.MapFrom(p => p.Holdings.Count));
            CreateMap<Portfolio, PortfolioDetailed>();
            CreateMap<PortfolioCreate, Portfolio>();
            CreateMap<PortfolioUpdate, Portfolio>();

            CreateMap<Holding, HoldingSimple>()
                .ForMember(hs => hs.Quantity, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingQuantity());
                })
                .ForMember(hs => hs.Cost, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingCost());
                });
            CreateMap<Holding, HoldingDetailed>();
            CreateMap<HoldingCreate, Holding>();
            CreateMap<HoldingUpdate, Holding>();

            CreateMap<Trade, TradeSimple>()
                .ForMember(ts => ts.Date, memberOptions => memberOptions.MapFrom(t => t.Date.ToLocalTime()));
            CreateMap<TradeCreate, Trade>()
                .ForMember(t => t.Date, memberOptions => memberOptions.MapFrom(tc => DateTime.Parse(tc.Date).ToUniversalTime()));
            CreateMap<TradeUpdate, Trade>()
                .ForMember(t => t.Date, memberOptions => memberOptions.MapFrom(tu => DateTime.Parse(tu.Date).ToUniversalTime()));

            CreateMap<AlphavantageWeeklyAdjustedResponseTimeSeriesElement, StockClosePrice>()
                .ForMember(scp => scp.ClosePrice, memberOptions => memberOptions.MapFrom(awartse => decimal.Parse(awartse.ClosePrice)))
                .ForMember(scp => scp.WeekStartDate, memberOptions => memberOptions.MapFrom(awartse => DateTime.Parse(awartse.Date).AddDays(-7)));
        }
    }
}