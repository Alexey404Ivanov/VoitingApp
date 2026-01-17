using Askly.Application.Interfaces.Repositories;
using Askly.Domain;
using Microsoft.EntityFrameworkCore;

namespace Askly.Infrastructure.Repositories;

public class VotesRepository : IVotesRepository
{
    private readonly AppDbContext _context;
    
    public VotesRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Guid>> GetUserVotedOptionIds(Guid pollId, Guid userId)
    {
        return await _context.Votes
            .Where(v => v.PollId == pollId && v.UserId == userId)
            .Select(v => v.OptionId)
            .ToListAsync();
    }

    public async Task VoteAsync(Guid pollId, List<Guid> optionIds, Guid userId)
    {
        // удаляем старые голоса пользователя
        await _context.Votes
            .Where(v => v.PollId == pollId && v.UserId == userId)
            .ExecuteDeleteAsync();
    
        // добавляем новые
        var votes = optionIds
            .Select(optionId => 
                VoteEntity.Create(userId, pollId, optionId));
        
    
        await _context.Votes.AddRangeAsync(votes);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteVote(Guid pollId, Guid userId)
    {
        await _context.Votes
            .Where(v => v.PollId == pollId && v.UserId == userId)
            .ExecuteDeleteAsync();
        
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetVotedUsersCount(Guid pollId)
    {
        return await _context.Votes
            .AsNoTracking()
            .Where(v => v.PollId == pollId)
            .Select(v => v.UserId)
            .Distinct()
            .CountAsync();
    }
    public async Task<List<Tuple<Guid, int>>> GetResults(Guid pollId)
    {
        // return await _context.Options
        //     .AsNoTracking()
        //     .Where(o => o.PollId == pollId)
        //     .Select(o => new Tuple<Guid, int>(o.Id, o.VotesCount))
        //     .ToListAsync();
        
        return await _context.Votes
            .AsNoTracking()
            .Where(v => v.PollId == pollId)
            .GroupBy(v => v.OptionId)
            .Select(g => new Tuple<Guid, int>(g.Key, g.Count()))
            .ToListAsync();
    }
}