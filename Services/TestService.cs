using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Resporter.Models;

namespace Resporter.Services
{
    public class TestService
    {
        IMongoCollection<Test> _tests;

        public TestService()
        {
            MongoClient client = new MongoClient("mongodb+srv://evgenii:AB70vd410u@cluster0.nv4c2.azure.mongodb.net/Test?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("Test");

            _tests = database.GetCollection<Test>("Test");
        }
    }
}
