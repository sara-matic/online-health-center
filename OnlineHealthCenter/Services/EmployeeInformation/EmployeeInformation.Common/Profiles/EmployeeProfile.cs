using AutoMapper;
using EmployeeInformation.Common.DTOs.DoctorDTOs;
using EmployeeInformation.Common.DTOs.NurseDTOs;
using EmployeeInformation.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInformation.Common.Profiles
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateDoctorDto, Doctor>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName)
                )
                .ForMember(
                    dest => dest.MedicalSpecialty,
                    opt => opt.MapFrom(src => src.MedicalSpecialty)
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.Biography,
                    opt => opt.MapFrom(src => src.Biography)
                )
                .ForMember(
                    dest => dest.ImageFile,
                    opt => opt.MapFrom(src => src.ImageFile)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                );

            CreateMap<UpdateDoctorDto, Doctor>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.FirstName,
                   opt => opt.MapFrom(src => src.FirstName)
               )
               .ForMember(
                   dest => dest.LastName,
                   opt => opt.MapFrom(src => src.LastName)
               )
               .ForMember(
                   dest => dest.MedicalSpecialty,
                   opt => opt.MapFrom(src => src.MedicalSpecialty)
               )
               .ForMember(
                   dest => dest.Title,
                   opt => opt.MapFrom(src => src.Title)
               )
               .ForMember(
                   dest => dest.Biography,
                   opt => opt.MapFrom(src => src.Biography)
               )
               .ForMember(
                   dest => dest.ImageFile,
                   opt => opt.MapFrom(src => src.ImageFile)
               )
               .ForMember(
                   dest => dest.Mark,
                   opt => opt.MapFrom(src => src.Mark)
               );

            CreateMap<Doctor, DoctorDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName)
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.LastName)
                )
                .ForMember(
                    dest => dest.MedicalSpecialty,
                    opt => opt.MapFrom(src => src.MedicalSpecialty)
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.Biography,
                    opt => opt.MapFrom(src => src.Biography)
                )
                .ForMember(
                    dest => dest.ImageFile,
                    opt => opt.MapFrom(src => src.ImageFile)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                );

            CreateMap<CreateNurseDto, Nurse>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => Guid.NewGuid())
               )
               .ForMember(
                   dest => dest.FirstName,
                   opt => opt.MapFrom(src => src.FirstName)
               )
               .ForMember(
                   dest => dest.LastName,
                   opt => opt.MapFrom(src => src.LastName)
               )
               .ForMember(
                   dest => dest.ImageFile,
                   opt => opt.MapFrom(src => src.ImageFile)
               );

            CreateMap<Nurse, NurseDto>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => src.Id)
               )
               .ForMember(
                   dest => dest.FirstName,
                   opt => opt.MapFrom(src => src.FirstName)
               )
               .ForMember(
                   dest => dest.LastName,
                   opt => opt.MapFrom(src => src.LastName)
               )
               .ForMember(
                   dest => dest.ImageFile,
                   opt => opt.MapFrom(src => src.ImageFile)
               );
        }
    }
}
