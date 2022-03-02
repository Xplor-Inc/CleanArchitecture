using ExpressCargo.Core.Constants;
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.SqlServer.Maps.Users;
public class AccountRecoveryMap : Map<AccountRecovery>
{
    public override void Configure(EntityTypeBuilder<AccountRecovery> entity)
    {
        entity
            .ToTable("AccountRecoveries");

        entity
            .Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

        entity
            .Property(e => e.ResetLink)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}