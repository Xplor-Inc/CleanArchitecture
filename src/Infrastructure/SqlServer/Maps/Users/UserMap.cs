using ExpressCargo.Core.Constants;
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.SqlServer.Maps.Users;
public class UserMap : Map<User>
{
    public override void Configure(EntityTypeBuilder<User> entity)
    {
        entity
            .ToTable("Users");

        entity
            .Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

        entity
            .Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.NAME_LENGTH);

        entity
            .Property(e => e.SecurityStamp)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.NAME_LENGTH);

        entity
            .HasIndex(e => new { e.EmailAddress, e.DeletedOn })
            .HasFilter("[DeletedOn] IS NULL")
            .IsUnique();

        entity
            .Property(e => e.EmailAddress)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.EMAIL_LENGTH);

        entity
            .Property(e => e.PasswordHash)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.PasswordSalt)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Role)
            .IsRequired();

        entity
            .Property(e => e.ImagePath)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .HasOne(d => d.CreatedBy)
            .WithMany(p => p.UsersCreatedBy)
            .HasForeignKey(d => d.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        entity
            .HasOne(d => d.DeletedBy)
            .WithMany(p => p.UsersDeletedBy)
            .HasForeignKey(d => d.DeletedById)
            .OnDelete(DeleteBehavior.Restrict);

        entity
            .HasOne(d => d.UpdatedBy)
            .WithMany(p => p.UsersUpdatedBy)
            .HasForeignKey(d => d.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}