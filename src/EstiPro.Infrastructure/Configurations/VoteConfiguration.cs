using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Configurations;

internal sealed class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder
            .HasKey(v => v.Id);

        builder
            .Property(v => v.Mark)
            .IsRequired();

        builder
            .Property(v => v.CreatedDate)
            .IsRequired();

        builder
            .HasOne(v => v.User)
            .WithMany(p => p.Votes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(v => v.Ticket)
            .WithMany(t => t.Votes)
            .HasForeignKey(v => v.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(vote => vote.CreatedDate)
            .HasColumnType(PostgreCustomTypes.TimeStampWithTimeZone)
            .IsRequired();
    }
}
