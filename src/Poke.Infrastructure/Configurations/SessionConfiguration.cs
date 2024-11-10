using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Domain.Entities;

namespace Poke.Infrastructure.Configurations;

internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.UserRole)
            .IsRequired();

        builder
            .HasOne(s => s.User)
            .WithMany(p => p.Sessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(s => s.Room)
            .WithMany(r => r.Sessions)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(session => session.CreatedDate)
            .HasColumnType(PostgreCustomTypes.TimeStampWithTimeZone)
            .IsRequired();
    }
}
