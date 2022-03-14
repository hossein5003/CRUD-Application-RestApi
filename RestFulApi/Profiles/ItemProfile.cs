using RestFulApi.Entities;
using AutoMapper;
using static RestFulApi.Dtos;

namespace RestFulApi.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}
