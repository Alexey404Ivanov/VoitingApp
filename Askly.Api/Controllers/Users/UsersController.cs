using Askly.Application.DTOs.Users;
using Askly.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Askly.Api.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly IUsersService _service;
    
    public UsersController(IUsersService service)
    {
        _service = service;
    }
    
    
    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto? userDto)
    {
        if (userDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        await _service.Register(userDto.UserName, userDto.Email, userDto.Password);
        return Ok();
    }
}