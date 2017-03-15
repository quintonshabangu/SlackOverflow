using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Domain;
using WebApp.Models.DTOs;

namespace WebApp.MappingProfiles
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            Mapper.CreateMap<Post, PostDTO>();
            Mapper.CreateMap<PostDTO, Post>()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}