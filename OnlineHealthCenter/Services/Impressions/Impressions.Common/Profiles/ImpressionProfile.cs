using Impressions.Common.DTOs;
using Impressions.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Impressions.Common.Profiles
{
    internal class ImpressionProfile : Profile
    {
        public ImpressionProfile()
        {
            CreateMap<CreateImpressionDto, Impression>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.PatientID,
                    opt => opt.MapFrom(src => src.PatientID)
                )
                .ForMember(
                    dest => dest.DoctorID,
                    opt => opt.MapFrom(src => src.DoctorID)
                )
                .ForMember(
                    dest => dest.Headline,
                    opt => opt.MapFrom(src => src.Headline)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                )
                .ForMember(
                    dest => dest.ImpressionDateTime,
                    opt => opt.MapFrom(src => DateTime.Now)
                );

            CreateMap<UpdateImpressionDto, Impression>()
                .ForMember(
                    dest => dest.PatientID,
                    opt => opt.MapFrom(src => src.PatientID)
                )
                .ForMember(
                    dest => dest.DoctorID,
                    opt => opt.MapFrom(src => src.DoctorID)
                )
                .ForMember(
                    dest => dest.Headline,
                    opt => opt.MapFrom(src => src.Headline)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                )
                .ForMember(
                    dest => dest.ImpressionDateTime,
                    opt => opt.MapFrom(src => src.ImpressionDateTime)
                );

            CreateMap<Impression, ImpressionDto>() 
                .ForMember(
                    dest => dest.DoctorID,
                    opt => opt.MapFrom(src => src.DoctorID)
                )
                .ForMember(
                    dest => dest.Headline,
                    opt => opt.MapFrom(src => src.Headline)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                )
                .ForMember(
                    dest => dest.ImpressionDateTime,
                    opt => opt.MapFrom(src => src.ImpressionDateTime)
                );

            CreateMap<Impression, ImpressionIdentityDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.DoctorID,
                    opt => opt.MapFrom(src => src.DoctorID)
                )
                .ForMember(
                    dest => dest.Headline,
                    opt => opt.MapFrom(src => src.Headline)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.Mark,
                    opt => opt.MapFrom(src => src.Mark)
                )
                .ForMember(
                    dest => dest.ImpressionDateTime,
                    opt => opt.MapFrom(src => src.ImpressionDateTime)
                );
        }
    }
}
