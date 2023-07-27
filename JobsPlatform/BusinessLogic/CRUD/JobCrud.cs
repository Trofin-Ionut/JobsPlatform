using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.Data.Sqlite;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public class JobCrud
    {
        static string? query;
        public static async Task CreateJobTable()
        {
            await Database.conn.OpenAsync();
            string query = "CREATE TABLE IF NOT EXISTS JOB (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(30) NOT NULL, industry VARCHAR(30) NOT NULL, minSalary INTEGER, maxSalary INTEGER,identifier INTEGER NOT NULL)";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateJobTable(string? name,string? industry,int?minSalary,int?maxSalary, int? jobID)
        {
            try
            {
                await Database.conn.OpenAsync();
                query = $"UPDATE JOB SET name='{name}',industry='{industry}',minSalary='{minSalary}',maxSalary='{maxSalary}' WHERE id='{jobID}'";
                SqliteCommand cmd = new SqliteCommand(query, Database.conn);
                await cmd.ExecuteNonQueryAsync();
                await Database.conn.CloseAsync();
            }
            catch(Exception) { }
        }
        public static async Task ReadJobTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM JOB";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["id"]); 
                    string? name = Convert.ToString(reader["name"]);
                    string? industry = Convert.ToString(reader["industry"]);
                    int?minSalary= Convert.ToInt32(reader["minSalary"]);
                    int? maxSalary = Convert.ToInt32(reader["maxSalary"]);
                    int? identifier = Convert.ToInt32(reader["identifier"]);
                    Database.everything.Add(new Job(name, id, industry,minSalary,maxSalary,identifier));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteJobTable(int? jobID)
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM JOB WHERE id='{jobID};";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertJob(string? name,int?id,string? industry,int?minSalary,int? maxSalary ,int? identifier)
        {
            string query = $"INSERT INTO JOB(name,industry,minSalary,maxSalary,benefits,identifier) VALUES ('{name}','{industry}','{minSalary}','{maxSalary}','{identifier}');";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
