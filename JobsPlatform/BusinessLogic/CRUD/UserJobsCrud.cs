using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.Sqlite;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public class UserJobsCrud
    {
        static string query;
        public static async Task CreateUserJobsTable()
        {
            await Database.conn.OpenAsync();
            query = "CREATE TABLE IF NOT EXISTS USER_JOBS (id INTEGER PRIMARY KEY AUTOINCREMENT,userID INTEGER NOT NULL,jobID INTEGER NOT NULL,FOREIGN KEY (userID) REFERENCES USER(id) ON DELETE CASCADE,FOREIGN KEY (jobID) REFERENCES JOB(id) ON DELETE CASCADE)";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadUserJobsTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM USER_JOBS";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["id"]);
                    int? userID = Convert.ToInt32(reader["userID"]);
                    int? jID = Convert.ToInt32(reader["jobID"]);
                    Database.userJobs.Add(new UserJobs(id, jID, userID));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateUserJobsTable(int? userJobsID, int? userID, int? jobID)
        {
            await Database.conn.OpenAsync();
            query = $"UPDATE USER_JOBS SET userID='{userID}', jobID='{jobID}' WHERE id='{userJobsID}'";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteUserJobsTable(int? userJobsID)
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM USER_JOBS WHERE id='{userJobsID};";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertUserJobs(int? jobID, int? userID)
        {
            string query = $"INSERT INTO USER_JOBS(userID,jobID) VALUES ('{userID}','{jobID}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
