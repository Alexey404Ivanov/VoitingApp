namespace Askly.Application.DTOs;

public class VoteResultsDto
{
    public Guid OptionId { get; set; }
    public int VotesCount { get; set; }
    public double Ratio { get; set; }
}