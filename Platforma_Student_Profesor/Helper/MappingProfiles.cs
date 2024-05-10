using AutoMapper;
using MODEL.DTO;
using MODEL.Models;
using System.Security.Cryptography;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Role, RoleDTO>()
                    .ForMember(dst => dst.RoleID, opt => opt.MapFrom(src => src.RoleID))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<RoleDTO, Role>()
                    .ForMember(dst => dst.RoleID, opt => opt.MapFrom(src => src.RoleID))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Repository, RepositoryDTO>();
            CreateMap<RepositoryDTO, Repository>();

            CreateMap<UserDTO, User>()
               .ForMember(dst => dst.RoleID, opt => opt.MapFrom(src => src.RoleID))
               .ForMember(dst => dst.UserFirstName, opt => opt.MapFrom(src => src.UserFirstName))
               .ForMember(dst => dst.UserLastName, opt => opt.MapFrom(src => src.UserLastName))
               .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dst => dst.EnterDate, opt => opt.MapFrom(src => src.EnterDate));
            CreateMap<User, UserDTO>()
              .ForMember(dst => dst.RoleID, opt => opt.MapFrom(src => src.RoleID))
              .ForMember(dst => dst.UserFirstName, opt => opt.MapFrom(src => src.UserFirstName))
              .ForMember(dst => dst.UserLastName, opt => opt.MapFrom(src => src.UserLastName))
              .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dst => dst.EnterDate, opt => opt.MapFrom(src => src.EnterDate));

        }
    }
}