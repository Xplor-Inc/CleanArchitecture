using CleanArchitecture.Core.Constants;
using CleanArchitecture.Core.Models.Entities.Audits;

namespace CleanArchitecture.SqlServer.Maps.Audits;
public class UserLoginMap : Map<UserLogin>
{
    public override void Configure(EntityTypeBuilder<UserLogin> entity)
    {
        entity
            .ToTable("UserLogins");

        entity
            .Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

        entity
            .Property(e => e.Browser)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Device)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.IP)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.OperatingSystem)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.ServerName)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}