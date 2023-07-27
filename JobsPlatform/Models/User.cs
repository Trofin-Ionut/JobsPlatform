using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public  class User:IStuff
    {
        public string? Name { get; set; }
        public int? Id { get; set; }
        public int? Identifier { get; set; }

        public User(string? name, int? identifier, int? id)
        {
            Name= name;
            Id=id;
            Identifier= identifier;
        }
    }
}
