using System.ComponentModel.DataAnnotations;

namespace Askly.Application.DTOs.Polls;

public class CreateOptionDto
{
    [Required]
    public string Text { get; set; }
}