namespace Askly.Domain.Entities;

public class OptionEntity
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    
    public Guid PollId { get; private set; }
    public PollEntity Poll { get; private set; }
    
    // public List<VoteEntity> Votes { get; set; }
    public int VotesCount = 0;
    
    private OptionEntity() { }
    
    internal OptionEntity(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        // Votes = votes;
        // VotesCount = votesCount;
    }
}