using Askly.Application.DTOs;
namespace Askly.Application.Services;

public interface IPollService
{
    PollDto GetById(Guid pollId);
    Guid Create(CreatePollDto pollDto);
    IEnumerable<PollDto> GetAll();
    void DeletePoll(Guid pollId);
    void DeleteVote(Guid pollId, List<Guid> optionsIds);
    void Vote(Guid pollId, List<Guid> optionsIds);
    PollResultsDto ShowResults(Guid pollId);
}