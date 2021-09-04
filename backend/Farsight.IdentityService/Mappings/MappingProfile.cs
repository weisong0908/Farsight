using System;
using AutoMapper;
using Farsight.IdentityService.Models;
using Farsight.IdentityService.Models.DTOs.Individuals;
using Farsight.IdentityService.Models.DTOs.Listings;
using IdentityServer4.Extensions;

namespace Farsight.IdentityService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Listings
            CreateMap<FarsightUser, UserListItem>()
                .ForMember(uli => uli.IsEmailVerified, memberOptions => memberOptions.MapFrom(fu => fu.EmailConfirmed));

            CreateMap<FarsightUser, UserCard>()
                .ForMember(uc => uc.ProfilePicture, memberOptions => memberOptions.MapFrom(fu => fu.ProfilePicture.IsNullOrEmpty() ? "" : Convert.ToBase64String(fu.ProfilePicture)));

            //Individuals
            CreateMap<FarsightUser, UserProfile>()
                .ForMember(uc => uc.ProfilePicture, memberOptions => memberOptions.MapFrom(fu => fu.ProfilePicture.IsNullOrEmpty() ? "" : Convert.ToBase64String(fu.ProfilePicture)));
        }
    }
}