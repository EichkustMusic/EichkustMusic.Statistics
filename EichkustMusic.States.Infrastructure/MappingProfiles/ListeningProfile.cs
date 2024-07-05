using AutoMapper;
using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Infrastructure.MappingProfiles
{
    internal class ListeningProfile : Profile
    {
        public ListeningProfile()
        {
            CreateMap<SimpleStatisticsEntityForCreateDto, Listening>();
            CreateMap<Listening, SimpleStatisticsEntityDto>();
        }
    }
}
