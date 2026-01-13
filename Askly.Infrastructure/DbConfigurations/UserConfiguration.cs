using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Askly.Domain.Entities;

namespace Askly.Infrastructure.DbConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder
            .Property(u => u.Login)
            .IsRequired();
        
        builder
            .Property(u => u.Password)
            .IsRequired();
        
        // builder
        //     .HasMany(u => u.CreatedPolls)
        //     .WithOne(p => p.User)
        //     .HasForeignKey(p => p.UserId);
        
        // builder
        //     .HasMany(u => u.Votes)
        //     .WithOne(v => v.User)
        //     .HasForeignKey(v => v.UserId);
    }
}