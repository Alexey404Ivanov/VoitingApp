namespace VoitingApp.Domain;

public class PoleEntity
{
    public Guid Id { get; }
    public string Question { get; }
    public DateTime CreatedAt { get; }
    public List<OptionEntity> Options { get; }
    
    public PoleEntity(Guid id, string question, DateTime createdAt, List<OptionEntity> options)
    {
        Id = id;
        Question = question;
        CreatedAt = createdAt;
        Options = options;
    }
}