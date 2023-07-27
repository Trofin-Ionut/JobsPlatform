﻿using JobsPlatform.BusinessLogic.DataLayer;
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
            
            string query = "CREATE TABLE IF NOT EXISTS USER (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(30) NOT NULL,identifier INTEGER)";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateUserTable(string? name, int? userID)
        {
            query = $"UPDATE USER SET name='{name}' WHERE id='{userID}";
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
                    int? identifier = Convert.ToInt32(reader["identifier"]);
                    Database.everything.Add(new User(name, id, identifier));
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
        public static async Task InsertUser(string? name, int? identifier)
        {
            string query = $"INSERT INTO USER(name,identifier,jobID) VALUES ('{name}','{identifier}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
