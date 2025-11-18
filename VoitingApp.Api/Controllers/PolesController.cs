using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VoitingApp.Domain;
using VoitingApp.Models;

namespace VoitingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolesController : ControllerBase
{
    private readonly IPolesRepository _polesRepository;
    private readonly IMapper _mapper;

    public PolesController(IPolesRepository polesRepository, IMapper mapper)
    {
        _polesRepository = polesRepository;
        _mapper = mapper;
    }
    
    [HttpGet("{poleId}", Name = nameof(GetPoleById))]
    [Produces("application/json")]
    public ActionResult<PoleDto> GetPoleById([FromRoute] Guid poleId)
    {
        var pole = _polesRepository.FindById(poleId);
        if (pole == null)
            return NotFound();
        var dto = _mapper.Map<PoleDto>(pole);
        return Ok(dto);
    }
    
    [HttpPost]
    [Produces("application/json", "application/xml")]
    public ActionResult<Guid> CreatePole([FromBody] CreatePoleDto? poleDto)
    {
        if (poleDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var poleEntity = _mapper.Map<PoleEntity>(poleDto);
        var insertedPole = _polesRepository.Insert(poleEntity);
        return CreatedAtRoute(nameof(GetPoleById), new{poleId=insertedPole.Id}, insertedPole.Id);
    }
    
    
    // [HttpGet]
    // public ActionResult<IEnumerable<PoleEntity>> GetPoles()
    // {
    //     return "Hello, World!";
    // }
}
