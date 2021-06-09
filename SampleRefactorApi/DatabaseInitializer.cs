using Microsoft.Data.Sqlite;

namespace SampleRefactorApi
{
    /// <summary>
    /// Initialisies in-memory databases
    /// Not part of refactor.
    /// </summary>
    public class DatabaseInitializer
    {
        private const string Database = "Data Source=identity.db";

        public static void Initialize()
        {
            Users();
            Roles();
            UserRoles();
        }

        private static void Users()
        {
            using var connection = new SqliteConnection(Database);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DROP TABLE IF EXISTS user;" +
                "CREATE TABLE user (id INTEGER PRIMARY KEY, name TEXT NULL, email TEXT NULL);" +
                "INSERT INTO user (name, email) VALUES ('Admin', 'sysadmin@company.example');";
            command.ExecuteScalar();
            connection.Close();
        }

        private static void Roles()
        {
            using var connection = new SqliteConnection(Database);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DROP TABLE IF EXISTS role;" +
                "CREATE TABLE role (id INTEGER PRIMARY KEY, name TEXT NOT NULL);" +
                "INSERT INTO role (name) VALUES ('Admin');" +
                "INSERT INTO role (name) VALUES ('Viewer');";
            command.ExecuteScalar();
            connection.Close();
        }

        private static void UserRoles()
        {
            using var connection = new SqliteConnection(Database);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "DROP TABLE IF EXISTS userrole;" +
                "CREATE TABLE userrole (id INTEGER PRIMARY KEY, userId INTEGER NOT NULL, roleId INTEGER NOT NULL);" +
                "INSERT INTO userrole (userId, roleId) VALUES (1, 1);";
            command.ExecuteScalar();
            connection.Close();
        }
    }
}