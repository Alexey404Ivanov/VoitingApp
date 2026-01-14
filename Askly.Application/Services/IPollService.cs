using Askly.Application.DTOs;
namespace Askly.Application.Services;

public interface IPollService
{
    Task<PollDto> GetById(Guid pollId);
    Task<Guid> Create(CreatePollDto pollDto);
    Task<List<PollDto>> GetAll();
    Task DeletePoll(Guid id);
    Task Vote(Guid id, List<Guid> optionsIds);
    Task VoteAsync(Guid id, List<Guid> optionsIds, Guid anonUserId);
    Task DeleteVote(Guid pollId, List<Guid> optionsIds, Guid anonUserId);
    Task<List<VoteResultsDto>> GetResults(Guid pollId);
    // void DeleteVote(Guid pollId, List<Guid> optionsIds);
    // PollResultsDto ShowResults(Guid pollId);
}