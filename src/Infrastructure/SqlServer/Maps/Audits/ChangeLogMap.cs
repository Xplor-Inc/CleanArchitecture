using ExpressCargo.Core.Models.Entities.Audits;

namespace ExpressCargo.SqlServer.Maps.Audits;
public class ChangeLogMap : Map<ChangeLog>
{
    public override void Configure(EntityTypeBuilder<ChangeLog> entity)
    {
        entity
            .ToTable("ChangeLogs");

        entity
            .Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

    }
}