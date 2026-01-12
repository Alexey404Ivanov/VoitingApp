namespace Askly.Domain.Entities;

public class VoteEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    
    public Guid OptionId { get; set; }
    public OptionEntity Option { get; set; }
    
    public VoteEntity()
    {
        Id = Guid.NewGuid();
    }
    
    public VoteEntity(Guid id, Guid userId, Guid optionId)
    {
        Id = id;
        UserId = userId;
        OptionId = optionId;
    }
}