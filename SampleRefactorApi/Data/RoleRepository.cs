using Dapper;
using Microsoft.Data.Sqlite;
using SampleRefactorApi.Models;
using System.Collections.Generic;

namespace SampleRefactorApi.Data
{
    public class RoleRepository : IRepository<Role, int>
    {
        private readonly string _connectionString;

        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc cref="IRepository{TModel, TKey}.Add(TModel)"/>
        public int Add(Role item)
        {
            using var connection = new SqliteConnection(_connectionString);
            return connection.Execute($"INSERT INTO role VALUES({item.Id}, {item.Name})");
        }

        /// <inheritdoc cref="IRepository{TModel, TKey}.Get(TKey)"/>
        public Role Get(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            return connection.QuerySingleOrDefault<Role>($"SELECT * FROM role WHERE id = @id", new { id });
        }

        /// <summary>
        /// Gets a collection of <see cref="Role"/>s for a provided <see cref="User"/>
        /// </summary>
        /// <param name="user">The user to find roles for</param>
        /// <returns>A collection of Roles associated with this user.</returns>
        public IEnumerable<Role> GetUserRole(User user)
        {
            using var connection = new SqliteConnection(_connectionString);
            var userRoles = connection.Query($"SELECT * FROM userrole WHERE userId = @userId", new { userId = user.Id });

            foreach (var userRole in userRoles)
            {
                yield return Get((int)userRole.roleId);
            }
        }
    }
}