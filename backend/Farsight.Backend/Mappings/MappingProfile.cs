using System;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;

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

            CreateMap<Holding, HoldingSimple>();
            CreateMap<Holding, HoldingDetailed>();
            CreateMap<HoldingCreate, Holding>();
            CreateMap<HoldingUpdate, Holding>();

            CreateMap<Trade, TradeSimple>();
            CreateMap<TradeCreate, Trade>();
            CreateMap<TradeUpdate, Trade>();

            CreateMap<AlphavantageWeeklyAdjustedResponseTimeSeriesElement, StockClosePrice>()
                .ForMember(scp => scp.ClosePrice, memberOptions => memberOptions.MapFrom(awartse => decimal.Parse(awartse.ClosePrice)))
                .ForMember(scp => scp.WeekStartDate, memberOptions => memberOptions.MapFrom(awartse => DateTime.Parse(awartse.Date).AddDays(-7)));
        }
    }
}