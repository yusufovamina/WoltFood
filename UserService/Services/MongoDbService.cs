using MongoDB.Driver;
using UserService.UserService.Data.Models;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IMongoDatabase database)
    {
        _database = database;
    }

    public async Task CreateUserAsync(User user)
    {
        var usersCollection = _database.GetCollection<User>("Users");
        await usersCollection.InsertOneAsync(user);
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var usersCollection = _database.GetCollection<User>("Users");
        return await usersCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }
}
