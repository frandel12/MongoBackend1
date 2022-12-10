using MongoBackend1.Models;
using System.Text.Json;
using System;

namespace MongoBackend1.DatabaseHelper
{
    public class Database
    {
        public List<User> getUsers()
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://admin:$1234@cluster0.fg4a0fu.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");
            var users = db.GetCollection<BsonDocument>("Users");
            List<BsonDocument> userArray = users.Find(new BsonDocument()).ToList();

            List<User> userList = new List<User>();

            foreach (BsonDocument bsonUser in userArray)
            {
                User user = BsonSerializer.Deserialize<User>(bsonUser);
                userList.Add(user);
            }

            return userList;
        }

        public void insertUser(User user)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://admin:$1234@cluster0.fg4a0fu.mongodb.net/test");
            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            var doc = new BsonDocument
            {
                { "name", user.name },
                { "email", user.email },
                { "phone", user.phone },
                { "address", user.address},
                { "dateIn", user.dateIn }
            };

            users.InsertOne(doc);
        }


        public void deleteUser(User user)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://admin:$1234@cluster0.fg4a0fu.mongodb.net/test");
            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", user._id);

            users.DeleteOne(deleteFilter);
        }

        public void updateUser(string txtid, string txtname, string txtaddress, int txtphone, string txtemail)
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://admin:$1234@cluster0.fg4a0fu.mongodb.net/test");

            IMongoDatabase db = mongoClient.GetDatabase("MongoBackend");

            var users = db.GetCollection<BsonDocument>("Users");

            var doc = new BsonDocument
            {
                { "_id", ObjectId.Parse(txtid) }
            };


            var doc1 = new BsonDocument
            {
                { "_id", ObjectId.Parse(txtid) },
                {"name", txtname },
                { "email", txtemail} ,
                { "phone", txtphone},
                 {"address", txtaddress },

            };

            users.ReplaceOne(doc, doc1);

        }
    }
}
