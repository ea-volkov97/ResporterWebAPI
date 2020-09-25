using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Resporter.Models;

namespace Resporter.Services
{
    public class EventService
    {
        private readonly IMongoCollection<SportEvent> _events;

        public EventService(IEventsDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<SportEvent>(settings.EventsCollectionName);
        }

        public SportEvent GetEventById(string id)
        {
            return null;
        }

        public void CreateNewEvent(SportEvent sportEvent)
        {
            _events.InsertOne(sportEvent);
        }
    }
}
