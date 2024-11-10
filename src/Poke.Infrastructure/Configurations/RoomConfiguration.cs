using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poke.Domain.Entities;

namespace Poke.Infrastructure.Configurations;

internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder
            .HasKey(room => room.Id);

        builder
            .Property(room => room.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(room => room.CreatedDate)
            .HasColumnType(PostgreCustomTypes.TimeStampWithTimeZone)
            .IsRequired();
    }
}
