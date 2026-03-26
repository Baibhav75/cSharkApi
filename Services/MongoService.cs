using MongoDB.Driver;
using MyApiProject.Models;

namespace MyApiProject.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<User> _users;

        public MongoService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDb:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDb:DatabaseName"]);

            _users = database.GetCollection<User>("users");
        }

        public List<User> GetAll() =>
            _users.Find(_ => true).ToList();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User Get(string email, string password) =>
            _users.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
    }
}