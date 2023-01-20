using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;

namespace IdentityServer.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<User, NewUserDTO>().ReverseMap();
        }
    }
}
