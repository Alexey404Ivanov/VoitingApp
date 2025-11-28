using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VoitingApp.Domain;

namespace VoitingApp.Infrastructure.DbConfigurations;

public class VoteConfiguration : IEntityTypeConfiguration<VoteEntity>
{
    public void Configure(EntityTypeBuilder<VoteEntity> builder)
    {
        builder.HasKey(v => v.Id);
    }
}