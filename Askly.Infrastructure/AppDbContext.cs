using Microsoft.EntityFrameworkCore;
using VoitingApp.Domain;
using VoitingApp.Infrastructure.DbConfigurations;
namespace VoitingApp.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PoleEntity> Poles { get; set; }
    public DbSet<VoteEntity> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PoleConfiguration());
        modelBuilder.ApplyConfiguration(new VoteConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}