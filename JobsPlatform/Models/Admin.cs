using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Admin : User
    {
        public int? UserID { get; set; }
        public Admin(string? name, int? id,int? identifier,int?userID) : base(name, identifier,id)
        {
            UserID= userID; 
        }
    }
}
