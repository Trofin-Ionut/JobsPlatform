
namespace JobsPlatform.Models
{
    public class User
    {
        public string? Name { get; set; }
        public int? Id { get; set; }
        public string? Password { get; set; }

        public User(string? name, int? id, string? password)
        {
            Name = name;
            Id = id;
            Password = password;
        }

        public User()
        {
        }
    }
}
