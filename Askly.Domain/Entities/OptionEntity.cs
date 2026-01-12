namespace Askly.Domain.Entities;

public class OptionEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid PollId { get; set; }
    public PollEntity Poll { get; set; }
    public List<VoteEntity> Votes { get; set; }
    public int VotesCount => Votes.Count;
    
    public OptionEntity()
    {
        Id = Guid.Empty;
    }
    
    public OptionEntity(Guid id, string text, Guid pollId, PollEntity poll, List<VoteEntity> votes)
    {
        Id = id;
        Text = text;
        PollId = pollId;
        Poll = poll;
        Votes = votes;
        // VotesCount = votesCount;
    }
}