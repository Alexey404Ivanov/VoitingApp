using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain;

namespace Askly.Infrastructure.DbConfigurations;

public class VoteConfiguration : IEntityTypeConfiguration<VoteEntity>
{
    public void Configure(EntityTypeBuilder<VoteEntity> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.OptionId)
            .IsRequired();
        
        builder.Property(v => v.UserId)
            .IsRequired();
        
        builder.HasOne<PollEntity>()
            .WithMany()
            .HasForeignKey(v => v.PollId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}