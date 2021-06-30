using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs;

namespace Farsight.Backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Portfolio, PortfolioSimple>();
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
        }
    }
}