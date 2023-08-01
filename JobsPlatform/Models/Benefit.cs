
namespace JobsPlatform.Models
{
    public class Benefit
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Benefit(int? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
