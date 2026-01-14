using Askly.Application.Interfaces.Repositories;
using Askly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Askly.Infrastructure.Repositories;

public class PollsRepository : IPollsRepository
{
    private readonly AppDbContext _context;
    
    public PollsRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<PollEntity>> GetAll()
    {
        var polls = await _context.Poles
            .AsNoTracking()
            .Include(p => p.Options)
            .ToListAsync();
        
        return polls;
    }
    
    public async Task<PollEntity?> GetById(Guid pollId)
    {
        var poll = await _context.Poles
            .AsNoTracking()
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == pollId);

        return poll;
    }

    public async Task<Guid> Create(PollEntity poll)
    {
        _context.Poles.Add(poll);
        await _context.SaveChangesAsync();
        
        return poll.Id;
    }

    public async Task<bool> Delete(Guid pollId)
    {
        var poll = await _context.Poles
            .Include(p => p.Options)    
            .FirstOrDefaultAsync(p => p.Id == pollId);
        
        if (poll == null) return false;
        
        _context.Poles.Remove(poll);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Vote(Guid pollId, List<Guid> optionsIds)
    {
        await _context.Options
            .Where(o => o.PollId == pollId)
            .Where(o => optionsIds.Contains(o.Id))
            .ExecuteUpdateAsync(s =>
                s.SetProperty(
                    o => o.VotesCount,
                    o => o.VotesCount + 1
                ));
        
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task VoteAsync(Guid pollId, List<Guid> optionIds, Guid anonUserId)
    {
        // удаляем старые голоса пользователя
        await _context.Votes
            .Where(v => v.PollId == pollId && v.AnonUserId == anonUserId)
            .ExecuteDeleteAsync();
    
        // добавляем новые
        var votes = optionIds.Select(optionId => new VoteEntity
        {
            PollId = pollId,
            OptionId = optionId,
            AnonUserId = anonUserId
        });
    
        await _context.Votes.AddRangeAsync(votes);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVote(Guid pollId, List<Guid> optionIds, Guid anonUserId)
    {
        var votesToDelete = await _context.Votes
            .Where(v => v.PollId == pollId && v.AnonUserId == anonUserId && optionIds.Contains(v.OptionId))
            .ToListAsync();
        
        _context.Votes.RemoveRange(votesToDelete);
        await _context.SaveChangesAsync();
    }

    
    public async Task<List<Guid>> GetUserVotesAsync(Guid pollId, Guid anonUserId)
    {
        return await _context.Votes
            .Where(v => v.PollId == pollId && v.AnonUserId == anonUserId)
            .Select(v => v.OptionId)
            .ToListAsync();
    }

    public async Task<int> GetVotedUsersCount(Guid pollId)
    {
        return await _context.Votes
            .AsNoTracking()
            .Where(v => v.PollId == pollId)
            .Select(v => v.AnonUserId)
            .Distinct()
            .CountAsync();
    }
    public async Task<List<Tuple<Guid, int>>> GetResults(Guid pollId)
    {
        return await _context.Votes
            .AsNoTracking()
            .Where(v => v.PollId == pollId)
            .GroupBy(v => v.OptionId)
            .Select(g => new Tuple<Guid, int>(g.Key, g.Count()))
            .ToListAsync();
    }
}