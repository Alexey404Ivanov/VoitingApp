using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoitingApp.Domain;

namespace VoitingApp.Infrastructure.DbConfigurations;

public class PoleConfiguration : IEntityTypeConfiguration<PoleEntity>
{
    public void Configure(EntityTypeBuilder<PoleEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired();
        
        builder
            .HasMany(p => p.Options)
            .WithOne(o => o.Pole)
            .HasForeignKey(o => o.PoleId);
    }
}