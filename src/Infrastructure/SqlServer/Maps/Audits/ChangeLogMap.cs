using CleanArchitecture.Core.Models.Entities.Audits;

namespace CleanArchitecture.SqlServer.Maps.Audits;
public class ChangeLogMap : Map<ChangeLog>
{
    public override void Configure(EntityTypeBuilder<ChangeLog> entity)
    {
        entity
            .ToTable("ChangeLogs");
    }
}