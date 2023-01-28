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
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => 0)
                );

            CreateMap<UpdateDoctorDto, Doctor>();
            CreateMap<Doctor, DoctorDto>();


            CreateMap<CreateNurseDto, Nurse>();
            CreateMap<Nurse, NurseDto>();
             
        }
    }
}
