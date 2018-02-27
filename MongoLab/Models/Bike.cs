using MongoDB.Bson;

namespace MongoLab.Models
{
    public class Bike
    {
        public ObjectId Id { get; set; }
        public int NumberOfGears { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Size Size { get; set; }
    }
}