using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain;

namespace Askly.Infrastructure.DbConfigurations;

public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
{
    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Text)
            .IsRequired()
            .HasMaxLength(75);
        
        builder.Property(o => o.VotesCount)
            .IsRequired();
    }
}