using MongoDB.Bson;
using MongoDB.Driver;
using RestFulApi.Entities;

namespace RestFulApi.Repositories
{
    public class MongoDbRepository : IItemRepository
    {
        private readonly string datebaseName = "catalog";
        private readonly string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database=mongoClient.GetDatabase(datebaseName);
            itemsCollection=database.GetCollection<Item>(collectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await itemsCollection.DeleteOneAsync(item=>item.Id==id);
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            //  return itemsCollection.Find(item=> item.id==id).SingleOrDefault();

            var filter = filterBuilder.Eq(item => item.Id , id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            await itemsCollection.ReplaceOneAsync(existingItem => existingItem.Id == item.Id, item);
        }
    }
}
