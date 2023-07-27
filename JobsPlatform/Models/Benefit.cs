using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Benefit : IStuff
    {
        public int? Id { get; set; }
        public int? Identifier { get; set; }
        public string? Name { get; set; }

        public Benefit(int? id,int? identifier,string? name) 
        {
            Id = id;
            Identifier = identifier;
            Name = name;
        }
    }
}
