namespace Askly.Domain;

public class VoteEntity
{
    public Guid PollId { get; set; }
    public Guid OptionId { get; set; }
    public Guid AnonUserId { get; set; }
}