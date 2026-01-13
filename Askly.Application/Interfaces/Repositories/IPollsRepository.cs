using Askly.Domain.Entities;

namespace Askly.Application.Interfaces.Repositories;

public interface IPollsRepository
{
    Task<List<PollEntity>> GetAll();
    Task<PollEntity?> GetById(Guid id);
    Task<Guid> Create(PollEntity poll);
    Task<bool> Delete(Guid id);
    Task<bool> Vote(Guid pollId, List<Guid> optionsIds);
}