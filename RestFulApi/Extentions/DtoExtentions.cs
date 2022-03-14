using Dtos;
using RestFulApi.Entities;

namespace RestFulApi.Extentions
{
    public static class DtoExtentions
    {
        public static Item CreateDtoAsItem(this CreateItemDto createItemDto)
            => new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTime.Now
            };

        public static ItemDto ItemAsItemDto(this Item item)
            => new()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate,
            };
    }
}
