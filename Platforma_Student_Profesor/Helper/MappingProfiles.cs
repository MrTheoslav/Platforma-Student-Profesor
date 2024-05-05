using AutoMapper;
using MODEL.DTO;
using MODEL.Models;
using System.Security.Cryptography;

namespace API.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {

            CreateMap<Role, RoleDTO>()
                    .ForMember(dst => dst.RoleID, opt => opt.MapFrom(src => src.RoleID))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
