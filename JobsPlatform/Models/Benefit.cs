using JobsPlatform.Interfaces;

namespace JobsPlatform.Models
{
    public class Benefit : SealedStuff
    {
        public new int? Id { get; set; }
        public new int? Identifier { get; set; }
        public new string? Name { get; set; }

        public Benefit(int? id,int? identifier,string? name) 
        {
            Id = id;
            Identifier = identifier;
            Name = name;
        }
    }
}
