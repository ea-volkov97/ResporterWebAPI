using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Resporter.Models
{
    /// <summary>
    /// Представляет описание модели пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Возвращает или задает имя пользователя. Имя пользователя является первичным ключом в базе данных.
        /// </summary>
        [BsonId]
        [BsonElement("username")]
        public string Username { get; private set; }

        /// <summary>
        /// Возвращает или задает список друзей пользователя.
        /// </summary>
        [BsonElement("friends")]
        private List<string> _friendsIds = new List<string>();

        /// <summary>
        /// Возвращает или задает список посещенных пользователем мероприятий.
        /// </summary>
        [BsonElement("visitedSportEvents")]
        private List<string> _visitedSportEventsIds = new List<string>();

        /// <summary>
        /// Возвращает или задает список предстоящих мероприятий, в которых участвует пользователь.
        /// </summary>
        [BsonElement("upcomingSportEvents")]
        private List<string> _upcomingSportEventsIds = new List<string>();

        /// <summary>
        /// Возвращает количество друзей пользователя.
        /// </summary>
        [BsonIgnore]
        public int FriendsCount => _friendsIds.Count;

        /// <summary>
        /// Возвращает количество посещенных пользователем мероприятий.
        /// </summary>
        [BsonIgnore]
        public int VisitedSportEventsCount => _visitedSportEventsIds.Count;

        /// <summary>
        /// Возвращает количество предстоящих мероприятий, в которых принимает участие пользователь.
        /// </summary>
        [BsonIgnore]
        public int UpcomingSportEventsCount => _upcomingSportEventsIds.Count;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="User"/> по указанному логин пользователя.
        /// </summary>
        /// <param name="username">Логин пользователя.</param>
        public User(string username)
        {
            Username = username;
        }

        public void AddFriend(User user)
        {
            _friendsIds.Add(user.Username);
            user._friendsIds.Add(Username);
        }

        /// <summary>
        /// Возвращает друга пользователя по индексу в списке друзей.
        /// </summary>
        /// <param name="index">Индекс друга.</param>
        /// <returns>Пользователь.</returns>
        public string GetFriendIdByIndex(int index)
        {
            return _friendsIds[index];
        }

        public string GetVisitedSportEventIdByIndex(int index)
        {
            if (index < 0 || index >= VisitedSportEventsCount)
                return _visitedSportEventsIds[index];
            else
                return null;
        }

        public string GetUpcomingSportEventIdByIndex(int index)
        {
            if (index < 0 || index >= UpcomingSportEventsCount)
                return _upcomingSportEventsIds[index];
            else
                return null;
        }
    }
}
