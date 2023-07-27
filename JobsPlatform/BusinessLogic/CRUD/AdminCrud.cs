using Microsoft.Data.Sqlite;
using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public static class AdminCrud
    {
        static string query;
        public static async Task CreateAdminTable()
        {
            await Database.conn.OpenAsync();
            query = "CREATE TABLE IF NOT EXISTS ADMINS (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(30) NOT NULL,identifier INTEGER NOT NULL,userID INTEGER NOT NULL,FOREIGN KEY (userID) REFERENCES USER(id) ON DELETE CASCADE)";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadAdminTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM ADMINS";
            SqliteCommand cmd= new SqliteCommand(query,Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read()) 
                {
                    int? id = Convert.ToInt32(reader["id"]); //Nullable<type> object requires this slightly longer way of writing all these
                    string? name = Convert.ToString(reader["name"]);
                    int? identifier = Convert.ToInt32(reader["identifier"]);
                    int? uID= Convert.ToInt32(reader["userID"]);
                    Database.everything.Add(new Admin(name, id, identifier,uID));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateAdminTable(string? name,int?userID,int?adminID)
        {
            await Database.conn.OpenAsync();
            query = $"UPDATE ADMINS SET name='{name}', userID='{userID}' WHERE id='{adminID}'";
            SqliteCommand cmd = new SqliteCommand(query,Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteAdminTable(int? adminID) 
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM ADMINS WHERE id='{adminID};";
            SqliteCommand cmd = new SqliteCommand(query,Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertAdmin(string? name, int? identifier, int? userID)
        {
            string query = $"INSERT INTO ADMINS(name,identifier,userID) VALUES ('{name}','{identifier}','{userID}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
