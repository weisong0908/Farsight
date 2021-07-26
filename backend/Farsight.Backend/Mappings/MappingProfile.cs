using System;
using System.Linq;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Extensions;

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
            CreateMap<Holding, HoldingDetailed>()
                .ForMember(hd => hd.CostHistory, memberOptions => memberOptions.MapFrom(h => h.Trades.GetHoldingCostHistory()));
            CreateMap<HoldingCreate, Holding>();
            CreateMap<HoldingUpdate, Holding>();

            CreateMap<Trade, TradeSimple>()
                .ForMember(ts => ts.Date, memberOptions => memberOptions.MapFrom(t => t.Date.GetDateString()));
            CreateMap<TradeCreate, Trade>()
                .ForMember(t => t.Date, memberOptions => memberOptions.MapFrom(tc => tc.Date.FromNewYorkThenToUtcDateTime()));
            CreateMap<TradeUpdate, Trade>()
                .ForMember(t => t.Date, memberOptions => memberOptions.MapFrom(tu => tu.Date.FromNewYorkThenToUtcDateTime()));

            CreateMap<PolygonAggregateBar, StockClosePrice>()
                .ForMember(scp => scp.ClosePrice, memberOptions => memberOptions.MapFrom(pab => pab.Close))
                .ForMember(scp => scp.Date, memberOptions => memberOptions.MapFrom(pab => pab.Timestamp.GetDateString()));

            CreateMap<PolygonTickerDetails, StockInfo>();
            CreateMap<PolygonTicker, Stock>();
        }
    }
}