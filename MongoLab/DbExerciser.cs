using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace MongoLab
{
    public class DbExerciser
    {
        private readonly MongoClient _mongoClient;

        private IMongoDatabase _db;

        public DbExerciser()
        {
            _mongoClient = new MongoClient();
        }

        public void ConnectDb(string dbName)
        {
            _db = _mongoClient.GetDatabase(dbName);
        }

        public void AddToCollection<T>(T item)
        {
            var collection = _db.GetCollection<T>(typeof(T).FullName);
            collection.InsertOne(item);
        }

        public List<T> GetAll<T>()
        {
            return _db.GetCollection<T>(typeof(T).FullName).AsQueryable().ToList();
        }

        public T Get<T>(Expression<Func<T,bool>> filter)
        {
            return _db.GetCollection<T>(typeof(T).FullName).FindSync(filter).FirstOrDefault();
        }
    }
}
