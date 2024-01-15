namespace RESTfull.Domain;

public class Discipline
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Attestation { get; set; } = String.Empty;
    public int Hours { get; set; }
    public List<Section> Sections { get; set; } =  new List<Section>();
    public Guid RegistryId;
    public Registry Registry { get; set; } = null!;
    public void AddSection(Section section)
    {
        Sections.Add(section);
    }

    // Добавлен метод для удаления секции по индексу
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < Sections.Count)
        {
            Sections.RemoveAt(index);
        }
    }
}