using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Resporter.Models
{
    /// <summary>
    /// Представляет описание модели мероприятия.
    /// </summary>
    public class SportEvent
    {
        /// <summary>
        /// Возвращает идентификатор мероприятия.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// Возвращает или задает название мероприятия.
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }
        /// <summary>
        /// Возвращает или задает идентификатор пользователя-организатора мероприятия.
        /// </summary>
        [BsonElement("organizatorId")]
        public string OrganizatorId { get; set; }
        /// <summary>
        /// Возвращает или задает текстовое описание мероприятия.
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Инициализирует новый экземлпяр класса <see cref="SportEvent"/> с заданными параметрами.
        /// </summary>
        /// <param name="title">Название мероприятия.</param>
        /// <param name="organizatorId">Идентификатор пользователя-организатора.</param>
        /// <param name="description">Текстовое описание мероприятия.</param>
        public SportEvent(string title, string organizatorId, string description = "")
        {
            Title = title;
            OrganizatorId = organizatorId;
            Description = description;
        }
    }
}
