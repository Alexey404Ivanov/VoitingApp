using System.ComponentModel.DataAnnotations;

namespace Askly.Application.DTOs.Users;

public class RegisterUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}