namespace VoitingApp.Domain;

public class PoleEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public List<OptionEntity> Options { get; set; }
    public bool IsMultipleChoice { get; set; }

    public PoleEntity()
    {
        Id = Guid.Empty;
    }
    
    public PoleEntity(Guid id, string title, DateTime createdAt, List<OptionEntity> options, bool isMultipleChoice)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
        Options = options;
        IsMultipleChoice = isMultipleChoice;
    }
}