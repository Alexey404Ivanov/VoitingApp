using Askly.Application.DTOs.Polls;

namespace Askly.Application.Interfaces.Services;

public interface IPollsService
{
    Task<PollDto> GetById(Guid pollId, Guid anonUserId);
    Task<Guid> Create(string title, List<CreateOptionDto> options, bool isMultipleChoice);
    Task<List<PollDto>> GetAll();
    Task DeletePoll(Guid id);
    Task Vote(Guid id, List<Guid> optionsIds);
    Task VoteAsync(Guid id, List<Guid> optionsIds, Guid anonUserId);
    Task DeleteVote(Guid pollId, List<Guid> optionsIds, Guid anonUserId);
    Task<List<VoteResultsDto>> GetResults(Guid pollId);
    // void DeleteVote(Guid pollId, List<Guid> optionsIds);
    // PollResultsDto ShowResults(Guid pollId);
}