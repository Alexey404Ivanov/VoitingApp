namespace Askly.Domain;

public class OptionEntity
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    
    public Guid PollId { get; private set; }
    public PollEntity Poll { get; private set; }
    
    public int VotesCount { get; private set; }
    
    private OptionEntity() { }
    
    private OptionEntity(string text, PollEntity poll)
    {
        Id = Guid.Empty;
        Text = text;
        PollId = poll.Id;
        Poll = poll;
        VotesCount = 0;
    }
    
    internal static OptionEntity Create(string text, PollEntity poll)
    {
        return new OptionEntity(text, poll);
    }
}