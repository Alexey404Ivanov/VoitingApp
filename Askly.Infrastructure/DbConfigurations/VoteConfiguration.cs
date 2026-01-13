using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain.Entities;

namespace Askly.Infrastructure.DbConfigurations;

public class VoteConfiguration : IEntityTypeConfiguration<VoteEntity>
{
    public void Configure(EntityTypeBuilder<VoteEntity> builder)
    {
        builder.HasKey(v => new { v.PollId, v.OptionId, v.AnonUserId });

        builder.HasIndex(v => new { v.PollId, v.AnonUserId });
    }
}