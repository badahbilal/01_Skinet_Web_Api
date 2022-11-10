using AutoMapper;
using Core.Entities;
using SkinetWebApi.Dtos;

namespace SkinetWebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>();
        }
    }
}
