namespace Askly.Domain;

public class VoteEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid PollId { get; private set; }
    public Guid OptionId { get; private set; }
    
    private VoteEntity() { }
    private VoteEntity(Guid userId, Guid pollId, Guid optionId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        PollId = pollId;
        OptionId = optionId;
    }
    
    public static VoteEntity Create(Guid userId, Guid pollId, Guid optionId)
    { 
        return new VoteEntity(userId, pollId, optionId);
    }
}