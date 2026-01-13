using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain.Entities;

namespace Askly.Infrastructure.DbConfigurations;

public class PollConfiguration : IEntityTypeConfiguration<PollEntity>
{
    public void Configure(EntityTypeBuilder<PollEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired();
        
        builder
            .HasMany(p => p.Options)
            .WithOne(o => o.Poll)
            .HasForeignKey(o => o.PollId);
        
        builder
            .Navigation(p => p.Options)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}