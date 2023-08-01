
namespace JobsPlatform.Models
{
    public class Employeer : User
    {
        public int? UserID { get; set; }
        public string? Company { get; set; }

        public Employeer(string? Name, int? id, int? userID, string? password, string? company) : base(Name, id, password)
        {
            UserID = userID;
            Company = company;
        }
    }
}
