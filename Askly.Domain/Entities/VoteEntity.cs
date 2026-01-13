namespace Askly.Domain.Entities;

public class VoteEntity
{
    public Guid PollId { get; set; }
    public Guid OptionId { get; set; }
    public Guid AnonUserId { get; set; }
}