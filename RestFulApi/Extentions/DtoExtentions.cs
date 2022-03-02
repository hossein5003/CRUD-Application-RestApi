using Dtos;
using RestFulApi.Entities;

namespace RestFulApi.Extentions
{
    public static class DtoExtentions
    {
        public static Item CreateDtoAsItem(this CreateItemDto createItemDto)
        {
            return new Item()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTime.Now
            };
        }
    }
}
