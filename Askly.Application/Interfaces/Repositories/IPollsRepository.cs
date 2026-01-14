using Askly.Domain.Entities;

namespace Askly.Application.Interfaces.Repositories;

public interface IPollsRepository
{
    Task<List<PollEntity>> GetAll();
    Task<PollEntity?> GetById(Guid pollId);
    Task<Guid> Create(PollEntity poll);
    Task<bool> Delete(Guid pollId);
    Task<bool> Vote(Guid pollId, List<Guid> optionsIds);
    Task VoteAsync(Guid pollId, List<Guid> optionsIds, Guid anonUserId);
    Task DeleteVote(Guid pollId, List<Guid> optionsIds, Guid anonUserId);
    Task<List<OptionEntity>> GetResults(Guid pollId);
}