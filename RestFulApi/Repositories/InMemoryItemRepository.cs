using RestFulApi.Entities;

namespace RestFulApi.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        public List<Item> Items = new()
        {
            new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item() { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item() { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
            new Item() { Id = Guid.NewGuid(), Name = "Custom", Price = 100, CreatedDate = DateTimeOffset.UtcNow },
        };

        public async Task CreateItemAsync(Item item)
        {
            Items.Add(item);

            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index=Items.FindIndex(x=>x.Id==id);
            Items.RemoveAt(index);

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await Task.FromResult(Items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await Task.FromResult(Items.FirstOrDefault(item => item.Id == id));
        }

        public async Task UpdateItemAsync(Item item)
        {
            var itemIndex = Items.FindIndex(currentItem => currentItem.Id == item.Id);
            Items[itemIndex] = item;

            await Task.CompletedTask;
        }
    }
}
