using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.NickName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasMany(p => p.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId);

        builder
            .HasMany(p => p.Votes)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId);

        builder
            .HasMany(p => p.RoomsCreated)
            .WithOne(r => r.CreatedByUser)
            .HasForeignKey(r => r.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(user => user.CreatedDate)
            .HasColumnType(PostgreCustomTypes.TimeStampWithTimeZone)
            .IsRequired();
    }
}