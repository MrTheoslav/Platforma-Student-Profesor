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

            CreateMap<UserAssigmnentDTO, UserAssigmnent>()
                .ForMember(dst => dst.AssigmnentID, opt => opt.MapFrom(src => src.AssigmnentID))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dst => dst.Mark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.SendDate, opt => opt.MapFrom(src => src.SendDate));
            CreateMap<UserAssigmnent, UserAssigmnentDTO>()
                .ForMember(dst => dst.AssigmnentID, opt => opt.MapFrom(src => src.AssigmnentID))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dst => dst.Mark, opt => opt.MapFrom(src => src.Mark))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.SendDate, opt => opt.MapFrom(src => src.SendDate));

            CreateMap<AssignmentDTO, Assignment>()
                .ForMember(dst => dst.AssignmentID, opt => opt.MapFrom(src => src.AssignmentID))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FileSize, opt => opt.MapFrom(src => src.FileSize))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dst => dst.RepositoryID, opt => opt.MapFrom(src => src.RepositoryID));
            CreateMap<Assignment, AssignmentDTO>()
                .ForMember(dst => dst.AssignmentID, opt => opt.MapFrom(src => src.AssignmentID))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FileSize, opt => opt.MapFrom(src => src.FileSize))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dst => dst.RepositoryID, opt => opt.MapFrom(src => src.RepositoryID));

            CreateMap<MODEL.Models.File, FileDTO>()
                .ForMember(dst => dst.FileID, opt => opt.MapFrom(src => src.FileID))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dst => dst.AssigmentID, opt => opt.MapFrom(src => src.AssigmentID))
                .ForMember(dst => dst.FileName, opt => opt.MapFrom(src => src.FileName));
            CreateMap<FileDTO, MODEL.Models.File>()
                .ForMember(dst => dst.FileID, opt => opt.MapFrom(src => src.FileID))
                .ForMember(dst => dst.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dst => dst.AssigmentID, opt => opt.MapFrom(src => src.AssigmentID))
                .ForMember(dst => dst.FileName, opt => opt.MapFrom(src => src.FileName));

        }
    }
}