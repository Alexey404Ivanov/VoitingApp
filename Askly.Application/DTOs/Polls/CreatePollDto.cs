using System.ComponentModel.DataAnnotations;

namespace Askly.Application.DTOs.Polls;

public class CreatePollDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public List<CreateOptionDto> Options { get; set; }
    [Required]
    public bool IsMultipleChoice { get; set; }
}