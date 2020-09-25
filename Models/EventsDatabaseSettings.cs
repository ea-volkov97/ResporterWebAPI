namespace Resporter.Models
{
    public class EventsDatabaseSettings : IEventsDatabaseSettings
    {
        public string EventsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IEventsDatabaseSettings
    {
        string EventsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
