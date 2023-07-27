using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Employeer:User
    {
        public int? UserID { get; set; }
        public Employeer ( string?Name,  int? id,  int?identifier,int? userID):base( Name, identifier, id)
        {
            UserID = userID;
        }
    }
}
