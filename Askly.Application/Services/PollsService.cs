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
    private readonly IPollsRepository _pollsRepository;
    private readonly IVotesRepository _votesRepository;
    private readonly IMapper _mapper;
    
    public PollsService(
        IPollsRepository pollsRepository,
        IVotesRepository votesRepository,
        IMapper mapper)
    {
        _pollsRepository = pollsRepository;
        _votesRepository = votesRepository;
        _mapper = mapper;
    }
    
    public async Task<PollDto> GetById(Guid pollId, Guid userId)
    {
        // var votedOptions = (await _repo.GetVotesAsync(pollId))
        //     .Where(v => v.AnonUserId == anonUserId)
        //     .Select(v => v.OptionId)
        //     .ToList();
        
        var poll = await _pollsRepository.GetById(pollId);
        var votedOptions = await _votesRepository.GetUserVotedOptionIds(pollId, userId);
        
        // if (votedOptions.Count == 0)
        //     return _mapper.Map<PollDto>(poll);
        //
        var dto = _mapper.Map<PollDto>(poll);
        dto.UserVotes = votedOptions;
        return dto;
    }
    
    public async Task<Guid> Create(
        string title,
        List<CreateOptionDto> options,
        bool isMultipleChoice,
        Guid userId)
    {
        var poll = PollEntity.Create(title, isMultipleChoice, userId);
        
        foreach (var optionDto in options)
            poll.AddOption(optionDto.Text);
        
        return await _pollsRepository.Add(poll);
    }
    
    public async Task<List<PollDto>> GetAll()
    {
        var polls = await _pollsRepository.GetAll();
        return _mapper.Map<List<PollDto>>(polls);
    }
    
    public async Task DeletePoll(Guid id)
    {
        // var poll = await _repo.GetIfExists(id);
        // if (poll == null)
        //     throw new PollNotFoundException(id);
        var isDeletedSucceed = await _pollsRepository.Delete(id);
        if (!isDeletedSucceed)
            throw new PollNotFoundException(id);
    }
    
    // public async Task Vote(Guid id, List<Guid> optionsIds)
    // {
    //     var isVoteSucceed = await _pollsRepository.Vote(id, optionsIds);
    //     if (!isVoteSucceed)
    //         throw new PollNotFoundException(id);
    // }
    
    public async Task VoteAsync(Guid pollId, List<Guid> optionsIds, Guid userId)
    {
        await _votesRepository.VoteAsync(pollId, optionsIds, userId);
    }

    public async Task DeleteVote(Guid pollId, Guid userId)
    {
        await _votesRepository.DeleteVote(pollId, userId);
    }
    
    public async Task<List<VoteResultsDto>> GetResults(Guid pollId)
    {
        var votedOptions = await _votesRepository.GetResults(pollId);
        var votedUsersCount = await _votesRepository.GetVotedUsersCount(pollId);
        var allOptionGuids = (await _pollsRepository.GetById(pollId))!.Options.Select(x => x.Id).ToList();
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