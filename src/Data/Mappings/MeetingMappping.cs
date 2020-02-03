using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings {
    public class MeetingMappping : IEntityTypeConfiguration<Meeting> {
        public void Configure (EntityTypeBuilder<Meeting> builder) {
            builder.ToTable ("Meeting");
            builder.Property (p => p.Data)
                .IsRequired ();

            builder.Property (p => p.Email)
                .IsRequired ();

        }
    }
}