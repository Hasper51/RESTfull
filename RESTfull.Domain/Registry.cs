namespace RESTfull.Domain
{
    public class Registry
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<Discipline> Disciplines { get; set; } = null!;
    }
}
