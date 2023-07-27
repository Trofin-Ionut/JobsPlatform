using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Admin : User
    {
        public int? UserID { get; set; }
        public new string? Password { get; set; }

        public Admin(string? name, int? id,int? identifier,int?userID,string?password) : base(name, identifier,id,password)
        {
            UserID= userID;
            Password= password;
        }
    }
}
