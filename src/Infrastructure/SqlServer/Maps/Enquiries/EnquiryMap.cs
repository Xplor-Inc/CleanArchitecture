using ExpressCargo.Core.Constants;
using ExpressCargo.Core.Models.Entities.Enquiries;

namespace ExpressCargo.SqlServer.Maps.Enquiries;
public class EnquiryMap : Map<Enquiry>
{
    public override void Configure(EntityTypeBuilder<Enquiry> entity)
    {
        entity
            .ToTable("Enquiries");

        entity
            .Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

        entity
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.EMAIL_LENGTH);

        entity
            .Property(e => e.Message)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.MAX_LENGTH);

        entity
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.NAME_LENGTH);

        entity
            .Property(e => e.Resolution)
            .HasMaxLength(StaticConfiguration.MAX_LENGTH);

        entity
            .Property(e => e.Subject)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}