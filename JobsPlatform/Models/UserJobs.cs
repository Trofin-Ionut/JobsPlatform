using JobsPlatform.Enums;
using JobsPlatform.Interfaces;
using System.Data.Common;

namespace JobsPlatform.Models
{
    public class UserJobs : SealedStuff
    {
        public new int? Id { get; set; }
        public new int? Identifier{get;set; }
        public  int? JobID { get; set; }
        public int? UserID { get; set; }

        public UserJobs(int? id, int? identifier, int? jobID, int? userID)
        {
            Id = id;
            Identifier = identifier;
            JobID = jobID;
            UserID = userID;
        }
    }
}
