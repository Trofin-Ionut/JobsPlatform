using JobsPlatform.BusinessLogic.DataLayer;
using JobsPlatform.Models;
using Microsoft.Data.Sqlite;

namespace JobsPlatform.BusinessLogic.CRUD
{
    public class JobBenefitsCrud
    {
        static string query;
        public static async Task CreateJobBenefitTable()
        {
            await Database.conn.OpenAsync();
            query = "CREATE TABLE IF NOT EXISTS JOB_BENEFITS (id INTEGER PRIMARY KEY AUTOINCREMENT,jobID INTEGER NOT NULL,benefitID INTEGER NOT NULL ,identifier INTEGER NOT NULL,FOREIGN KEY (jobID) REFERENCES JOB(id) ON DELETE CASCADE, FOREIGN KEY (benefitID) REFERENCES BENEFIT(id))";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task ReadJobBenefitTable()
        {
            await Database.conn.OpenAsync();
            query = "SELECT * FROM JOB_BENEFITS";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int? id = Convert.ToInt32(reader["id"]);
                    int?jobID = Convert.ToInt32(reader["jobID"]);
                    int? identifier = Convert.ToInt32(reader["identifier"]);
                    int? bID = Convert.ToInt32(reader["benefitID"]);
                    Database.everything.Add(new JobBenefits(id, identifier, bID,jobID));
                }
            }
            await Database.conn.CloseAsync();
        }
        public static async Task UpdateJobBenefitTable(int? jobID, int? benefitID,int?id)
        {
            await Database.conn.OpenAsync();
            query = $"UPDATE JOB_BENEFITS SET jobID='{jobID}',benefitID={benefitID} WHERE id='{id}'";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task DeleteJobBenefitTable(int? id)
        {
            await Database.conn.OpenAsync();
            query = $"DELETE FROM JOB_BENEFITS WHERE id='{id};";
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
        public static async Task InsertJobBenefit(int? benefitID, int? jobID,int? identifier)
        {
            string query = $"INSERT INTO JOB_BENEFITS(jobID,benefitID,identifier) VALUES ('{jobID}',{benefitID}','{identifier}')";
            await Database.conn.OpenAsync();
            SqliteCommand cmd = new SqliteCommand(query, Database.conn);
            await cmd.ExecuteNonQueryAsync();
            await Database.conn.CloseAsync();
        }
    }
}
