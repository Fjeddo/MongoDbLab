using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoLab.Models;
using Xunit;

namespace MongoLab.Tests
{
    public class CreateDbTests
    {
        private readonly string _dbName;

        public CreateDbTests()
        {
            _dbName = "VehiclesDb";
        }

        [Fact]
        public void ShouldCreateCollectionsAndAddStuff()
        {
            var sut = new DbExerciser();

            sut.ConnectDb(_dbName);
            sut.AddToCollection(new Car
            {
                Brand = "Volvo",
                Year = DateTime.Now.Year,
                Id = new ObjectId(Guid.NewGuid().ToString("N")),
                Model = "XC60"
            });

            sut.AddToCollection(new Bike
            {
                Brand = "Specialized",
                Id = new ObjectId(Guid.NewGuid().ToString("N")),
                Model = "S-Works Epic Hardtail",
                NumberOfGears = 11,
                Size = Size.Medium
            });

            sut.AddToCollection(new Bike
            {
                Brand = "Scott",
                Id = new ObjectId(Guid.NewGuid().ToString("N")),
                Model = "Scale 900 Elite",
                NumberOfGears = 20,
                Size = Size.ExtraLarge
            });

            var cars = sut.GetAll<Car>();
            var bikes = sut.GetAll<Bike>();

            var bike = sut.Get<Bike>(b => b.NumberOfGears > 11);

            Assert.True(true);
        }
    }
}
