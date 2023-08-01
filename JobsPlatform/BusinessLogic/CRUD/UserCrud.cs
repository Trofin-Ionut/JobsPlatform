using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public static class UserCrud
    {
        static string? query;

        public static async Task CreateUserTable()
        {

            string query = "CREATE TABLE IF NOT EXISTS USER(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(30) NOT NULL, _password VARCHAR(30) NOT NULL)";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateUserTable(string? name, string? password, int? userID)
        {
            query = $"UPDATE USER SET name='{name}',password='{password}' WHERE id='{userID}";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadUserTable()
        {
            query = "SELECT * FROM USER";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    int? id = Convert.ToInt32(reader["id"]);
                    string? name = Convert.ToString(reader["name"]);
                    string? password = Convert.ToString(reader["_password"]);
                    Database.users.Add(new User(name, id, password));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteUserTable(int? userID)
        {
            query = $"DELETE FROM USER WHERE id='{userID};";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertUser(string? name, string? password)
        {
            string query = $"INSERT INTO USER(name,_password) VALUES ('{name}','{password}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
