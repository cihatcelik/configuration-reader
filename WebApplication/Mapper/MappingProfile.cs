using AutoMapper;
using WebApplication.Models;
using Entity;

namespace WebApplication.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Configuration, ConfigurationViewModel>().ReverseMap();
        }
    }
}
