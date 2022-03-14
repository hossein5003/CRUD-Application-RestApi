using RestFulApi.Entities;
using static RestFulApi.Dtos;

namespace RestFulApi.Extentions
{
    public static class DtoExtentions
    {
        public static Item CreateDtoAsItem(this CreateItemDto createItemDto)
            => new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTime.Now
            };

        public static ItemDto ItemAsItemDto(this Item item)
            => new(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
    }
}
