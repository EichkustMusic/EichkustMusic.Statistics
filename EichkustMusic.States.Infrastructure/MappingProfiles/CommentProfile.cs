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
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentForCreateDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
