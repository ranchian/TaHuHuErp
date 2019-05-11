using AutoMapper;
using THH.Model;
using THH.Model.Dto;

namespace THH.Web.ModelAutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User,UserDto>();
        }
    }
}
