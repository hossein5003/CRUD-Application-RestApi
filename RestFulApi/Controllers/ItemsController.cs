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
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
        {
            var items = await _itemRepository.GetAsync();
            return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _itemRepository.GetItemAsync(id);

            if (item is null)
                return NotFound();

            return Ok(_mapper.Map<ItemDto>(item));
        }

        //201 for success
        //400 for bad request
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = createItemDto.CreateDtoAsItem();

            await _itemRepository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, _mapper.Map<ItemDto>(item));
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
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var itemFromDb = await _itemRepository.GetItemAsync(id);

            if (itemFromDb is null)
                return NotFound();

            await _itemRepository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
