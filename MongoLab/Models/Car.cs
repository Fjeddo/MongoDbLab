using MongoDB.Bson;

namespace MongoLab.Models
{
    public class Car
    {
        public ObjectId Id { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
