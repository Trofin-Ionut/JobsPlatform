using JobsPlatform.Enums;
using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class JobBenefits : IStuff
    {
        public int? Id { get; set; }
        public int? Identifier { get; set; }
        public int?BenefitID { get; set; }
        public int? JobID { get; set; }

        public JobBenefits(int? id, int? identifier, int? benefitID,int?jobID)
        {
            Id = id;
            Identifier = identifier;
            BenefitID = benefitID;
            JobID= jobID;
        }
    }
}
