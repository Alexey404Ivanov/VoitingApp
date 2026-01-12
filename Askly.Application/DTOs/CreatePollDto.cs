using System.ComponentModel.DataAnnotations;
using Askly.Domain;

namespace Askly.Application.DTOs;

public class CreatePollDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public List<CreateOptionDto> Options { get; set; }
    [Required]
    public bool IsMultipleChoice { get; set; }
}