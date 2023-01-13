using AutoMapper;
using Reports.Common.DTOs;
using Reports.Common.Entities;

namespace Reports.Common.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<CreateReportDTO, Report>()
                .ForMember(
                    dest => dest.Id, 
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.PatientId, 
                    opt => opt.MapFrom(src => src.PatientId)
                )
                .ForMember(
                    dest => dest.DoctorId, 
                    opt => opt.MapFrom(src => src.DoctorId)
                )
                .ForMember(
                    dest => dest.Comment, 
                    opt => opt.MapFrom(src => src.Comment)
                )
                .ForMember(
                    dest => dest.Diagnosis, 
                    opt => opt.MapFrom(src => src.Diagnosis)
                )
                .ForMember(
                    dest => dest.Prescription,
                    opt => opt.MapFrom(src => src.Prescription)
                )
                .ForMember(
                    dest => dest.CreatedTime,
                    opt => opt.MapFrom(src => DateTime.Now)
                );


            CreateMap<Report, ReportDTO>()
                .ForMember(
                    dest => dest.PatientId, 
                    opt => opt.MapFrom(src => src.PatientId)
                )
                .ForMember(
                    dest => dest.DoctorId,
                    opt => opt.MapFrom(src => src.DoctorId)
                )
                .ForMember(
                    dest => dest.Comment,
                    opt => opt.MapFrom(src => src.Comment)
                )
                .ForMember(
                    dest => dest.Diagnosis,
                    opt => opt.MapFrom(src => src.Diagnosis)
                )
                .ForMember(
                    dest => dest.Prescription,
                    opt => opt.MapFrom(src => src.Prescription)
                )
                .ForMember(
                    dest => dest.CreatedTime,
                    opt => opt.MapFrom(src => src.CreatedTime)
                );
        }
    }
}
