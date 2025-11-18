using System.ComponentModel.DataAnnotations;
using VoitingApp.Domain;

namespace VoitingApp.Models;

public class CreatePoleDto
{
    [Required]
    public string Question { get; set; }
    [Required]
    public List<OptionEntity> Options { get; set; }
}