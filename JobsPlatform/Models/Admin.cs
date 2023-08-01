
namespace JobsPlatform.Models
{
    public class Admin : User
    {
        public int? UserID { get; set; }

        public Admin(string? name, int? id, int? userID, string? password) : base(name, id, password)
        {
            UserID = userID;
            Password = password;
        }
    }
}
