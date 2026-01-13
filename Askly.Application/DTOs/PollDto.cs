namespace Askly.Application.DTOs;

public class PollDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public List<OptionDto> Options { get; init; }
    public bool IsMultipleChoice { get; init; }
}