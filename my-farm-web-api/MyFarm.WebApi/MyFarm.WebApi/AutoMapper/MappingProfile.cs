using AutoMapper;
using MyFarm.ApiModels.Animal;
using MyFarm.Data.Entities;

namespace MyFarm.WebApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AnimalEntity, AnimalModel>();
            CreateMap<AnimalModel, AnimalEntity>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
