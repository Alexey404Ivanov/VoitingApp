namespace VoitingApp.Domain;

public class PoleEntity
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OptionEntity> Options { get; set; }
    public bool IsMultipleChoice { get; set; }

    public PoleEntity()
    {
        Id = Guid.Empty;
    }
    
    public PoleEntity(Guid id, string question, DateTime createdAt, List<OptionEntity> options, bool isMultipleChoice)
    {
        Id = id;
        Question = question;
        CreatedAt = createdAt;
        Options = options;
        IsMultipleChoice = isMultipleChoice;
    }
}