namespace Askly.Domain;

public class PollEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsMultipleChoice { get; private set; }
    public Guid UserId { get; private set; }
    
    private readonly List<OptionEntity> _options = [];
    public IReadOnlyCollection<OptionEntity> Options => _options;

    private PollEntity() { }
    
    private PollEntity(string title, bool isMultipleChoice, Guid userId)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsMultipleChoice = isMultipleChoice;
        UserId = userId;
    }

    public static PollEntity Create(string title, bool isMultipleChoice, Guid userId)
    {
        return new PollEntity(title, isMultipleChoice, userId);
    }
    
    public OptionEntity AddOption(string text)
    {
        var option = OptionEntity.Create(text, this);
        _options.Add(option);
        return option;
    }
}