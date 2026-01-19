using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain;

namespace Askly.Infrastructure.DbConfigurations;

public class PollsConfiguration : IEntityTypeConfiguration<PollEntity>
{
    public void Configure(EntityTypeBuilder<PollEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserId)
            .IsRequired();
        
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(75);
        
        builder
            .HasMany(p => p.Options)
            .WithOne(o => o.Poll)
            .HasForeignKey(o => o.PollId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .Navigation(p => p.Options)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}