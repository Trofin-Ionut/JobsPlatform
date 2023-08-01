

namespace JobsPlatform.Models
{
    public class UserJobs
    {
        public int? Id { get; set; }
        public int? JobID { get; set; }
        public int? UserID { get; set; }

        public UserJobs(int? id, int? jobID, int? userID)
        {
            Id = id;
            JobID = jobID;
            UserID = userID;
        }

        public UserJobs()
        {
        }
    }
}
