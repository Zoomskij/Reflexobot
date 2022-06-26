using AutoMapper;
using Reflexobot.Entities;
using Reflexobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Mapper
{
    public class ScenarioProfile : Profile
    {
        public ScenarioProfile()
        {
            CreateMap<Scenario, ScenarioDto>()
                .ForMember(dest => dest.Guid,opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Label,opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Text) ? src.Text : src.Command));

            CreateMap<ScenarioDto, Scenario>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Label));

            //CreateMap<IEnumerable<Scenario>, IEnumerable<ScenarioDto>>();
            //CreateMap<IEnumerable<Scenario>, IEnumerable<ScenarioDto>>().ReverseMap();
        }
    }
}
