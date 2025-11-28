namespace VoitingApp.Domain;

public class OptionEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid PoleId { get; set; }
    public PoleEntity Pole { get; set; }
    public List<VoteEntity> Votes { get; set; }
    public int VotesCount => Votes.Count;
    
    public OptionEntity()
    {
        Id = Guid.Empty;
    }
    
    public OptionEntity(Guid id, string text, Guid poleId, PoleEntity pole, List<VoteEntity> votes)
    {
        Id = id;
        Text = text;
        PoleId = poleId;
        Pole = pole;
        Votes = votes;
        // VotesCount = votesCount;
    }
}