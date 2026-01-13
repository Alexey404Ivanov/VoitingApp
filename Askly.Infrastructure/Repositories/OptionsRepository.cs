namespace Askly.Infrastructure.Repositories;

public class OptionsRepository
{
    private readonly AppDbContext _context;
    
    public OptionsRepository(AppDbContext context)
    {
        _context = context;
    }
    
}