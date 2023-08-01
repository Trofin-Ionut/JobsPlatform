
namespace JobsPlatform.Models
{
    public class JobBenefits
    {
        public new int? Id { get; set; }
        public int? BenefitID { get; set; }
        public int? JobID { get; set; }

        public JobBenefits(int? id, int? benefitID, int? jobID)
        {
            Id = id;
            BenefitID = benefitID;
            JobID = jobID;
        }
    }
}
