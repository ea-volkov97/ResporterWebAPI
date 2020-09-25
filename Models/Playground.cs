using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Resporter.Models
{
    public class Playground
    {
        /// <summary>
        /// Возвращает или задает идентификатор площадки.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Возвращает или задает название площадки.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Playground"/> с заданными параметрами.
        /// </summary>
        /// <param name="name"></param>
        public Playground(string name)
        {
            Name = name;
        }
    }
}
