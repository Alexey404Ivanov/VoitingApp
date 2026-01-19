using Askly.Application.DTOs.Polls;
using Askly.Application.Exceptions;
using Askly.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Askly.Api.Controllers.Polls;

[ApiController]
[Route("api/polls")]
public class PollsApiController : ControllerBase
{
    private readonly IPollsService _service;

    public PollsApiController(IPollsService service)
    {
        _service = service;
    }
    
    [Authorize]
    [HttpGet("{pollId:guid}", Name = nameof(GetById))]
    [Produces("application/json")]
    public async Task<ActionResult<PollDto>> GetById([FromRoute] Guid pollId)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        // var anonUserId = (Guid)HttpContext.Items["AnonUserId"]!;
        try
        {
            var poll = await _service.GetById(pollId, userId);
            return Ok(poll);
        }
        catch (PollNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
        // var pole = _repo.FindById(poleId);
        // if (pole == null)
        //     return NotFound();
        // var dto = _mapper.Map<PoleDto>(pole);
        // return Ok(dto);
    }
    
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<List<PollDto>>> GetAll()
    {
        var polls = await _service.GetAll();
        return Ok(polls);
    }
    
    [Authorize]
    [HttpPost]
    [Produces("application/json", "application/xml")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePollDto? pollDto)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        
        if (pollDto == null)
            return BadRequest();
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var createdPollId = await _service.Create(pollDto.Title, pollDto.Options, pollDto.IsMultipleChoice, userId);
        return Ok(createdPollId);
    }
    
    [Authorize]
    [HttpPost("{pollId:guid}/vote")]
    [Produces("application/json")]
    public async Task<IActionResult> Vote([FromRoute] Guid pollId, [FromBody] List<Guid> optionsIds)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);

        await _service.VoteAsync(
            pollId,
            optionsIds,
            userId);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{pollId:guid}")]
    public async Task<ActionResult> DeletePoll([FromRoute] Guid pollId)
    {
        try
        {
            await _service.DeletePoll(pollId);
            return NoContent();
        }
        catch (PollNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{pollId:guid}/vote")]
    [Produces("application/json")]
    public async Task<ActionResult> DeleteVote([FromRoute] Guid pollId)
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        try
        {
            await _service.DeleteVote(pollId, userId);
            return NoContent();
        }
        catch (PollNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }


    [Authorize]
    [HttpGet("{pollId:guid}/results")]
    [Produces("application/json")]
    public async Task<ActionResult<List<VoteResultsDto>>> ShowResults([FromRoute] Guid pollId)
    {
        try
        {
            var resultsDto = await _service.GetResults(pollId);
            return Ok(resultsDto);
        }
        catch (PollNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
