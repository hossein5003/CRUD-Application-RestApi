using RestFulApi.Entities;

namespace RestFulApi.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAsync();
        Task<Item> GetItemAsync(Guid id);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}