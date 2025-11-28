using System.ComponentModel.DataAnnotations;
using VoitingApp.Domain;

namespace VoitingApp.Models;

public class CreatePoleDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public List<CreateOptionDto> Options { get; set; }
    [Required]
    public bool IsMultipleChoice { get; set; }
}