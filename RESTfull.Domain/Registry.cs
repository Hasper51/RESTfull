using static System.Collections.Specialized.BitVector32;

namespace RESTfull.Domain
{
    public class Registry
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<Discipline> Disciplines { get; set; } = new List<Discipline>();
       
        public void AddDiscipline(Discipline discipline)
        {
            Disciplines.Add(discipline);
        }
    }
}
