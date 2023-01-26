using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFulApi.Entities;
using RestFulApi.Extentions;
using RestFulApi.Repositories;
using static RestFulApi.Dtos;

namespace RestFulApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepository, ILogger<ItemsController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync(string? name=null)
        {
            var items = (await _itemRepository.GetAsync()).Select(item=>item.ItemAsItemDto());

            if (!string.IsNullOrWhiteSpace(name))
                items = items.Where(item => item.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return  items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _itemRepository.GetItemAsync(id);

            if (item is null)
                return NotFound();

            return item.ItemAsItemDto();
        }

        //201 for success
        //400 for bad request
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = createItemDto.CreateDtoAsItem();

            await _itemRepository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id },item.ItemAsItemDto());
        }

        //204 which is NoContent() in order to successed
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            Item itemFromDb = await _itemRepository.GetItemAsync(id);

            if (itemFromDb is null)
                return NotFound();

            itemFromDb.Name=updateItemDto.Name;
            itemFromDb.Price=updateItemDto.Price;
            itemFromDb.Description=updateItemDto.Description;

            await _itemRepository.UpdateItemAsync(itemFromDb);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var itemFromDb = await _itemRepository.GetItemAsync(id);

            if (itemFromDb is null)
                return NotFound();

            await _itemRepository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
