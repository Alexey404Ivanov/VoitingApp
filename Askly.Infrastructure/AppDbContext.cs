using Microsoft.EntityFrameworkCore;
using Askly.Domain;
using Askly.Infrastructure.DbConfigurations;

namespace Askly.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PollEntity> Poles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<VoteEntity> Votes { get; set; }
    public DbSet<OptionEntity> Options { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PollsConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new VotesConfiguration());
        modelBuilder.ApplyConfiguration(new OptionsConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}