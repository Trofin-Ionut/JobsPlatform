using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.Data.Sqlite;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public static class BenefitCrud
    {
        static string query;
        public static async Task CreateBenefitTable()
        {
            await Database.conn.OpenAsync();
            query = "CREATE TABLE IF NOT EXISTS BENEFIT (id INTEGER PRIMARY KEY AUTOINCREMENT,name VARCHAR(30) NOT NULL)";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadBenefitTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM Benefit";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["id"]);
                    string? name = Convert.ToString(reader["name"]);
                    Database.benefits.Add(new Benefit(id, name));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateBenefitTable(string? name, int? BenefitID)
        {
            await Database.conn.OpenAsync();
            query = $"UPDATE BENEFIT SET name='{name}' WHERE id='{BenefitID}'";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteBenefitTable(int? benefitID)
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM BENEFIT WHERE id='{benefitID};";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertBenefit(string? name)
        {
            string query = $"INSERT INTO BENEFIT(name) VALUES ('{name}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
