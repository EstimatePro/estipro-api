using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EstiPro.Domain.Entities;

namespace EstiPro.Infrastructure.Configurations;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(e => e.Description)
            .HasMaxLength(1000);

        builder
            .Property(e => e.Type)
            .IsRequired();

        builder
            .HasOne(t => t.Room)
            .WithMany(r => r.Tickets)
            .HasForeignKey(t => t.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(tiket => tiket.CreatedDate)
            .HasColumnType(PostgreCustomTypes.TimeStampWithTimeZone)
            .IsRequired();
    }
}
