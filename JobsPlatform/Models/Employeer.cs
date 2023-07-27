using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Employeer:User
    {
        public int? UserID { get; set; }
        public new string? Password { get; set; }
        public Employeer ( string?Name,  int? id,  int?identifier,int? userID,string? password):base( Name, identifier, id,password)
        {
            UserID = userID;
            Password = password;
        }
    }
}
