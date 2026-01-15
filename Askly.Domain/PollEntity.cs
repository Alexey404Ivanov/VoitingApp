namespace Askly.Domain;

public class PollEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsMultipleChoice { get; private set; }

    // public DateTime CreatedAt { get; set; }

    // public Guid UserId { get; set; }

    // public UserEntity User { get; set; }
    private readonly List<OptionEntity> _options = [];
    public IReadOnlyCollection<OptionEntity> Options => _options;

    // public PollEntity()
    // {
    //     Id = Guid.Empty;
    // }
    
    public PollEntity(string title, bool isMultipleChoice)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsMultipleChoice = isMultipleChoice;
    }

    public void AddOption(OptionEntity option)
    {
        _options.Add(option);
    }
    public void AddOption(string text)
    {
        _options.Add(new OptionEntity(text));
    }
}