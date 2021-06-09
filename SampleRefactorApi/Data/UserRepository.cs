using Dapper;
using Microsoft.Data.Sqlite;
using SampleRefactorApi.Models;
using System.Data;
using System.Threading.Tasks;

namespace SampleRefactorApi.Data
{
    public class UserRepository : IRepository<User, int>
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(string connectionString)
        {
            _dbConnection = new SqliteConnection(connectionString);
        }

        /// <inheritdoc cref="IRepository{TModel, TKey}.Add(TModel)"/>
        public int Add(User item) => _dbConnection.Execute($"INSERT INTO user (id, name) VALUES({item.Id}, '{item.Name}')");

        /// <inheritdoc cref="IRepository{TModel, TKey}.Add(TModel)"/>
        public Task<int> AddFaster(User item) => _dbConnection.ExecuteAsync($"INSERT INTO user (id, name) VALUES({item.Id}, '{item.Name}')");

        /// <inheritdoc cref="IRepository{TModel, TKey}.Get(TKey)"/>
        public User Get(int id) => _dbConnection.QuerySingle<User>($"SELECT * FROM user WHERE id = {id}");
    }
}