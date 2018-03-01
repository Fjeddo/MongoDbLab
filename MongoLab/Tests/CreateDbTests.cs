using System;
using FluentAssertions;
using MongoDB.Bson;
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

            // Adding car to Bikes collection, yes...
            sut.AddToCollection(new Car
            {
                Brand = "Nissan",
                Id = new ObjectId(Guid.NewGuid().ToString("N")),
                Model = "Micra",
                Year = 1979
            }, typeof(Bike).FullName);

            // Get all cars, as cars from the cars collection
            var cars = sut.GetAll<Car>();
            cars.Should().NotBeEmpty();

            // Get all "things" in the bikes collection, where it exists one car, the Nissan Micra
            var mixedVehiclesInBikeCollection = sut.GetAll(typeof(Bike).FullName);
            mixedVehiclesInBikeCollection.Should().NotBeEmpty();

            // Get single bike from the bikes collection
            var bike = sut.Get<Bike>(b => b.NumberOfGears > 11);
            bike.Should().NotBeNull();

            // Get the Nissan Micra from the bikes collection. NOTE that year property not present on bikes.
            var nissan = sut.Get<Car>(c => c.Year < 1980, typeof(Bike).FullName);
            nissan.Should().NotBeNull();

            // Get the Nissan Micra from the bikes collection. NOTE that year property not present on bikes.
            var nissanAgain = sut.Get<Car>(c => c.Brand == "Nissan", typeof(Bike).FullName);
            nissanAgain.Should().NotBeNull();

            nissan.Should().BeEquivalentTo(nissanAgain);

            Assert.True(true);
        }
    }
}
