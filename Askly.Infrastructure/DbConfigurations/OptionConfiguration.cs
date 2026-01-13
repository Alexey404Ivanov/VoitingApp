using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain.Entities;

namespace Askly.Infrastructure.DbConfigurations;

public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
{
    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Text)
            .IsRequired();
        
        // builder
        //     .HasMany(o => o.Votes)
        //     .WithOne(v => v.Option)
        //     .HasForeignKey(v => v.OptionId);
    }
}