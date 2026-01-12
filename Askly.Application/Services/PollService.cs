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
    
    public PollDto GetById(Guid pollId)
    {
        var poll = _repo.FindById(pollId);
        return poll == null ? throw new PollNotFoundException(pollId) : _mapper.Map<PollDto>(poll);
    }
    
    public Guid Create(CreatePollDto pollDto)
    {
        var entity = _mapper.Map<PollEntity>(pollDto);
        return _repo.Create(entity).Id;
    }
    
    public IEnumerable<PollDto> GetAll()
    {
        var polls = _repo.GetAll();
        return _mapper.Map<IEnumerable<PollDto>>(polls);
    }
    
    public void DeletePoll(Guid pollId)
    {
        if (_repo.FindById(pollId) == null)
            throw new PollNotFoundException(pollId);
        _repo.Delete(pollId);
    }
    
    public void DeleteVote(Guid pollId, List<Guid> optionsIds)
    {
        if (_repo.FindById(pollId) == null)
            throw new PollNotFoundException(pollId);
        _repo.UpdateVotes(pollId, optionsIds, true);
    }
    
    public void Vote(Guid pollId, List<Guid> optionsIds)
    {
        if (_repo.FindById(pollId) == null)
            throw new PollNotFoundException(pollId);
        _repo.UpdateVotes(pollId, optionsIds, false);
    }
    
    public PollResultsDto ShowResults(Guid pollId)
    {
        var poll = _repo.FindById(pollId);
        return poll == null ? throw new PollNotFoundException(pollId) : _mapper.Map<PollResultsDto>(poll);
    }
}