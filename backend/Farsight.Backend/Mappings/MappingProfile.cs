using System;
using System.Linq;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;
using Farsight.Backend.Extensions;
using Farsight.Backend.Models.DTOs.DashboardWidgets;
using Portfolio = Farsight.Backend.Models.Portfolio;
using Farsight.Backend.Models.DTOs.Listings;
using Farsight.Backend.Models.DTOs.Individuals;
using Farsight.Backend.Models.DTOs.Requests;
using Farsight.Backend.Models.DTOs.Responses;

namespace Farsight.Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Dashboard widgets
            CreateMap<string, Models.DTOs.DashboardWidgets.Portfolio>()
                .ForMember(p => p.Name, memberOptions => memberOptions.MapFrom(s => s));
            CreateMap<Tuple<string, int>, TopHolding>()
                .ForMember(dwh => dwh.Ticker, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Item1);
                })
                .ForMember(dwh => dwh.Quantity, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Item2);
                });
            CreateMap<Tuple<string, TradeType, int>, RecentTrade>()
                .ForMember(rt => rt.Ticker, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Item1);
                })
                .ForMember(rt => rt.TradeType, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Item2);
                })
                .ForMember(rt => rt.Quantity, memberOptions =>
                {
                    memberOptions.MapFrom(t => t.Item3);
                });

            //Listings
            CreateMap<Portfolio, PortfolioListItem>()
                .ForMember(pli => pli.HoldingCount, memberOptions => memberOptions.MapFrom(p => p.Holdings.Count));

            CreateMap<Holding, HoldingListItem>()
                .ForMember(hli => hli.Quantity, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingQuantity());
                })
                .ForMember(hli => hli.UnitCost, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingUnitCost());
                });

            //Individual items
            CreateMap<Portfolio, PortfolioItem>();
            CreateMap<Holding, PortfolioItemHolding>()
                .ForMember(pih => pih.Quantity, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingQuantity());
                })
                .ForMember(pih => pih.UnitCost, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingUnitCost());
                })
                .ForMember(pih => pih.MarketPrice, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => 10);
                });

            CreateMap<Holding, HoldingItem>()
                .ForMember(hi => hi.CostHistory, memberOptions => memberOptions.MapFrom(h => h.Trades.GetHoldingItemCostHistory()))
                .ForMember(hi => hi.InvestedAmount, memberOptions => memberOptions.MapFrom(h => h.Trades.GetHoldingQuantity() * h.Trades.GetHoldingUnitCost()));
            CreateMap<Trade, HoldingItemTrade>()
                .ForMember(tit => tit.Date, memberOptions => memberOptions.MapFrom(t => t.Date.GetDateString()));


            // Requests
            CreateMap<PortfolioCreate, Portfolio>();
            CreateMap<PortfolioUpdate, Portfolio>();

            //Responses
            CreateMap<Portfolio, PortfolioCreated>();

            CreateMap<Holding, HoldingSimple>()
                .ForMember(hs => hs.Quantity, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingQuantity());
                })
                .ForMember(hs => hs.Cost, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => h.Trades.GetHoldingUnitCost());
                })
                .ForMember(hs => hs.MarketPrice, memberOptions =>
                {
                    memberOptions.PreCondition(h => h.Trades.Count > 0);
                    memberOptions.MapFrom(h => 10);
                });
            // CreateMap<Holding, HoldingDetailed>()
            //     .ForMember(hd => hd.CostHistory, memberOptions => memberOptions.MapFrom(h => h.Trades.GetHoldingCostHistory()));
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

            CreateMap<PolygonTickerDetails, StockInfo>()
                .ForMember(si => si.Sector, memberOptions => memberOptions.MapFrom(ptd => string.IsNullOrWhiteSpace(ptd.Sector) ? "-" : ptd.Sector))
                .ForMember(si => si.Type, memberOptions => memberOptions.MapFrom(ptd => ptd.Type.PolygonTickerTypeToStockType()));
            CreateMap<PolygonTicker, Stock>();
        }
    }
}