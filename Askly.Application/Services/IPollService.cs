using Askly.Application.DTOs;
namespace Askly.Application.Services;

public interface IPollService
{
    Task<PollDto> GetById(Guid pollId);
    Task<Guid> Create(CreatePollDto pollDto);
    Task<List<PollDto>> GetAll();
    Task DeletePoll(Guid id);
    Task Vote(Guid id, List<Guid> optionsIds);
    // void DeleteVote(Guid pollId, List<Guid> optionsIds);
    // PollResultsDto ShowResults(Guid pollId);
}