namespace Askly.Application.DTOs;

public class PollDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<OptionDto> Options { get; set; }
    public bool IsMultipleChoice { get; set; }
}