using System.ComponentModel.DataAnnotations;

namespace Askly.Application.DTOs;

public class CreateOptionDto
{
    [Required]
    public string Text { get; set; }
}