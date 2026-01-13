using Askly.Application.Interfaces.Repositories;
using Askly.Application.DTOs;
using Askly.Domain.Entities;
using Askly.Application.Exceptions;
using AutoMapper;

namespace Askly.Application.Services;

public class PollService : IPollService
{
    private readonly IPollsRepository _repo;
    private readonly IMapper _mapper;
    
    public PollService(IPollsRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<PollDto> GetById(Guid pollId)
    {
        var poll = await _repo.GetById(pollId);
        return poll == null ? throw new PollNotFoundException(pollId) : _mapper.Map<PollDto>(poll);
    }
    
    public async Task<Guid> Create(CreatePollDto pollDto)
    {
        var poll = new PollEntity(pollDto.Title, pollDto.IsMultipleChoice);
        foreach (var optionDto in pollDto.Options)
            poll.AddOption(optionDto.Text);
        
        return await _repo.Create(poll);
    }
    
    public async Task<List<PollDto>> GetAll()
    {
        var polls = await _repo.GetAll();
        return _mapper.Map<List<PollDto>>(polls);
    }
    
    public async Task DeletePoll(Guid id)
    {
        // var poll = await _repo.GetIfExists(id);
        // if (poll == null)
        //     throw new PollNotFoundException(id);
        var isDeletedSucceed = await _repo.Delete(id);
        if (!isDeletedSucceed)
            throw new PollNotFoundException(id);
    }
    
    public async Task Vote(Guid id, List<Guid> optionsIds)
    {
        var isVoteSucceed = await _repo.Vote(id, optionsIds);
        if (!isVoteSucceed)
            throw new PollNotFoundException(id);
    }
    
    // public void DeleteVote(Guid pollId, List<Guid> optionsIds)
    // {
    //     if (_repo.FindById(pollId) == null)
    //         throw new PollNotFoundException(pollId);
    //     _repo.UpdateVotes(pollId, optionsIds, true);
    // }
    //
    
    //
    // public PollResultsDto ShowResults(Guid pollId)
    // {
    //     var poll = _repo.FindById(pollId);
    //     return poll == null ? throw new PollNotFoundException(pollId) : _mapper.Map<PollResultsDto>(poll);
    // }
}