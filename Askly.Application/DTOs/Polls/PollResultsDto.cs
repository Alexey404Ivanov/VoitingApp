namespace Askly.Application.DTOs;

public class PollResultsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<VoteResultsDto> Options { get; set; }
}