using Askly.Domain;
using Askly.Domain.Entities;

namespace Askly.Application.Interfaces.Repositories;

public interface IPollsRepository
{
    PollEntity Create(PollEntity poll);
    PollEntity? FindById(Guid pollId);
    IEnumerable<PollEntity> GetAll();
    void Delete(Guid pollId);
    void UpdateVotes(Guid pollId, List<Guid> optionsIds, bool isVoteReset);
}