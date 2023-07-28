using Microsoft.Data.Sqlite;
using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public static class EmployeerCrud
    {
        static string? query;
        public static async Task CreateEmployeerTable()
        {
            await Database.conn.OpenAsync();   
            query = "CREATE TABLE IF NOT EXISTS EMPLOYEER (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(30) NOT NULL,identifier INTEGER NOT NULL,_password VARCHAR(30),company VARCHAR(30) NOT NULL, userID INTEGER NOT NULL ,FOREIGN KEY (userID) REFERENCES USER(id) ON DELETE CASCADE)";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadEmployeerTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM EMPLOYEER";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["id"]); 
                    string? name = Convert.ToString(reader["name"]);
                    int? identifier = Convert.ToInt32(reader["identifier"]);
                    int? uID = Convert.ToInt32(reader["userID"]);
                    string? password = Convert.ToString(reader["_password"]);
                    string? company = Convert.ToString(reader["company"]);
                    Database.everything.Add(new Employeer(name, id, identifier, uID,password));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateEmployeerTable(string? name, int? userID, int? employeerID,string? password)
        {
            await Database.conn.OpenAsync();
            query = $"UPDATE EMPLOYEER SET name='{name}', userID='{userID}',_password={password} WHERE id='{employeerID}'";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteEmployeerTable(int? employeerID)
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM EMPLOYEER WHERE id='{employeerID};";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertEmployeer(string? name, int? identifier, int? userID,string?password)
        {
            string query = $"INSERT INTO EMPLOYEER(name,identifier,userID,_password) VALUES ('{name}','{identifier}','{userID}',{password}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
