using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Resporter.Models;

namespace Resporter.Services
{
    /// <summary>
    /// Предоставляет инструменты для создания, чтения, обновления и удаления данных пользователя из базы данных.
    /// </summary>
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IUsersDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() => _users.Find(user => true).ToList();

        /// <summary>
        /// Возвращает экземпляр пользователя по указанному идентификатору из базы данных.
        /// </summary>
        /// <param name="username">Идентификатор пользователя.</param>
        /// <returns>Экземпляр пользователя или null, если пользователь не найден.</returns>
        public async Task<User> GetUser(string username)
        {
            return await _users.Find<User>(user => user.Username == username).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Создает в базе данных запись о пользователе.
        /// </summary>
        /// <param name="user">Добавляемый пользователь.</param>
        /// <returns></returns>
        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public async Task<ActionResult<User>> AddNew(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public void Update(string login, User userIn) => _users.ReplaceOne(user => user.Username == userIn.Username, userIn);

        public void Remove(User userIn) => _users.DeleteOne(user => user.Username == userIn.Username);

        public void Remove(string login) => _users.DeleteOne(user => user.Username == login);
    }
}
