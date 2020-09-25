namespace Resporter.Models
{
    public class PlaygroundsDatabaseSettings : IPlaygroundsDatabaseSettings
    {
        public string PlaygroundsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPlaygroundsDatabaseSettings
    {
        string PlaygroundsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
