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
            AddToCollection(item, typeof(T).FullName);
        }

        public void AddToCollection<T>(T item, string collectionName)
        {
            var collection = _db.GetCollection<T>(collectionName);
            collection.InsertOne(item);
        }

        public List<dynamic> GetAll(string collectionName)
        {
            return _db.GetCollection<dynamic>(collectionName).AsQueryable().ToList();
        }

        public List<T> GetAll<T>()
        {
            return _db.GetCollection<T>(typeof(T).FullName).AsQueryable().ToList();
        }

        public T Get<T>(Expression<Func<T,bool>> filter)
        {
            return Get(filter, typeof(T).FullName);
        }

        public T Get<T>(Expression<Func<T, bool>> filter, string collectionName)
        {
            return _db.GetCollection<T>(collectionName).FindSync(filter).FirstOrDefault();
        }
    }
}
