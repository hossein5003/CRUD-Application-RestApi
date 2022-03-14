using AutoMapper;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFulApi.Entities;
using RestFulApi.Extentions;
using RestFulApi.Repositories;

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
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _itemRepository.GetAsync()).Select(item=>item.ItemAsItemDto());
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
            var itemFromDb = await _itemRepository.GetItemAsync(id);

            if (itemFromDb is null)
                return NotFound();

            Item updatedItem = itemFromDb with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price,
            };

            await _itemRepository.UpdateItemAsync(updatedItem);

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
