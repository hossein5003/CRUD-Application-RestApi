using Dtos;
using RestFulApi.Entities;
using AutoMapper;

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
