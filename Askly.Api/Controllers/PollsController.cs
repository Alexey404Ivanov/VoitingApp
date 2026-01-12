using Microsoft.AspNetCore.Mvc;
using Askly.Application.DTOs;

namespace Askly.Api.Controllers;

public class PollsController: Controller
{
    private readonly HttpClient _client;

    public PollsController(HttpClient client)
    {
        _client = client;
    }

    [HttpGet("/polls")]
    public async Task<IActionResult> Index()
    {
        var polls = await _client.GetFromJsonAsync<List<PollDto>>(
            "http://localhost:5000/api/polls");

        return View("Index", polls);
    }

    [HttpGet("/polls/{pollId:guid}")]
    public async Task<IActionResult> Details([FromRoute] Guid pollId)
    {
        var poll = await _client.GetFromJsonAsync<PollDto>(
            $"http://localhost:5000/api/polls/{pollId}");

        return View("Details", poll);
    }
}