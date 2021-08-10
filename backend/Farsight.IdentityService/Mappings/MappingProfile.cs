using AutoMapper;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Models.DTOs.Listings;

namespace Farsight.IdentityService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FarsightUser, UserListItem>()
                .ForMember(uli => uli.IsEmailVerified, memberOptions => memberOptions.MapFrom(fu => fu.EmailConfirmed));
        }
    }
}