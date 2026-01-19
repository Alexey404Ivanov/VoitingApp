using System.ComponentModel.DataAnnotations;

namespace Askly.Application.DTOs.Users;

public class LoginUserDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}