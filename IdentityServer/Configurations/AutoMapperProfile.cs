using AutoMapper;
using IdentityServer.Models;
using IdentityServer.Protos;

namespace IdentityServer.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserResponse>()
                .ForMember(response => response.Avatar, 
                    option =>
                        option.MapFrom(user => (user.Avatar ?? "")));
        }
    }
}