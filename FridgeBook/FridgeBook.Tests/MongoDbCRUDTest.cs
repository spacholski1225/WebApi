using FridgeBook.Data;
using MongoDB.Bson.Serialization.Attributes;
using NUnit.Framework;
using System;

namespace FridgeBook.Tests
{
    public class MongoDbTest
    {
        public MongoDbCRUD mockDb { get; set; }
        [SetUp]
        public void Setup()
        {
            mockDb = new MongoDbCRUD("MockDatabase");
        }

        [Test]
        public void InsertRecord_NewObject_NotEmpty()
        {
            mockDb.InsertRecord("MockObject", new NewObject { NickName = "Szymon" });

            Assert.IsNotNull(mockDb.LoadRecords<NewObject>("MockObject"));
        }

    }

    public class NewObject
    {
        [BsonId]
        public Guid Id { get; set; }
        public string NickName { get; set; }
    }
}