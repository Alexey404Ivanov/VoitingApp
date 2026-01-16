using Askly.Application.Interfaces.Repositories;
using Askly.Application.DTOs;
using Askly.Application.DTOs.Polls;
using Askly.Domain;
using Askly.Application.Exceptions;
using Askly.Application.Interfaces.Services;
using AutoMapper;

namespace Askly.Application.Services;

public class PollsService : IPollsService
{
    private readonly IPollsRepository _repo;
    private readonly IMapper _mapper;
    
    public PollsService(IPollsRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<PollDto> GetById(Guid pollId, Guid anonUserId)
    {
        // var votedOptions = (await _repo.GetVotesAsync(pollId))
        //     .Where(v => v.AnonUserId == anonUserId)
        //     .Select(v => v.OptionId)
        //     .ToList();
        
        var poll = await _repo.GetById(pollId);
        var votedOptions = await _repo.GetVotedOptionIds(pollId, anonUserId);
        
        // if (votedOptions.Count == 0)
        //     return _mapper.Map<PollDto>(poll);
        //
        var dto = _mapper.Map<PollDto>(poll);
        dto.UserVotes = votedOptions;
        return dto;
    }
    
    public async Task<Guid> Create(string title, List<CreateOptionDto> options, bool isMultipleChoice)
    {
        var poll = PollEntity.Create(title, isMultipleChoice);
        foreach (var optionDto in options)
            poll.AddOption(optionDto.Text);
        
        return await _repo.Add(poll);
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
    
    public async Task VoteAsync(Guid id, List<Guid> optionsIds, Guid anonUserId)
    {
        await _repo.VoteAsync(id, optionsIds, anonUserId);
    }

    public async Task DeleteVote(Guid pollId, List<Guid> optionsIds, Guid anonUserId)
    {
        await _repo.DeleteVote(pollId, optionsIds, anonUserId);
    }
    
    public async Task<List<VoteResultsDto>> GetResults(Guid pollId)
    {
        var votedOptions = await _repo.GetResults(pollId);
        var votedUsersCount = await _repo.GetVotedUsersCount(pollId);
        var allOptionGuids = (await _repo.GetById(pollId))!.Options.Select(x => x.Id).ToList();
        var votedOptionGuids = votedOptions.Select(t => t.Item1).ToHashSet();
        var results = new List<VoteResultsDto>();
        foreach (var optionGuid in allOptionGuids)
        {
            if (!votedOptionGuids.Contains(optionGuid))
                results.Add(new VoteResultsDto
                {
                    OptionId = optionGuid,
                    VotesCount = 0,
                    Ratio = 0
                });
            else 
            {
                var tuple = votedOptions.First(t => t.Item1 == optionGuid);
                results.Add(new VoteResultsDto
                {
                    OptionId = tuple.Item1,
                    VotesCount = tuple.Item2,
                    Ratio = Math.Round((double)tuple.Item2 / votedUsersCount * 100, 1)
                });
            }
        }
        return results;
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