using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RestFulApi.Repositories;
using RestFulApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// if saw a Guid type convert it to string
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
// if saw a DataTimeOffset type convert it to string
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

//Add MongoDb
builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    var settings=builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IItemRepository, MongoDbRepository>();
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false; 
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
