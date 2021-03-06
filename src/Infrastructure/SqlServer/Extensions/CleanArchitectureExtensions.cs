using CleanArchitecture.Core.Interfaces.Utility.Security;
using CleanArchitecture.Core.Models.Entities.Users;

namespace CleanArchitecture.SqlServer;
public static class CleanArchitectureExtensions
{
    public static void AddInitialData(this CleanArchitectureContext context, IEncryption encryption)
    {
        context.SeedUsers(encryption);
    }

    private static void SeedUsers(this CleanArchitectureContext context, IEncryption encryption)
    {
        var id = 1;
        var salt = encryption.GenerateSalt();
        var user = new User
        {
            AccountActivateDate     = DateTimeOffset.Now,
            CreatedById             = id,
            CreatedOn               = DateTimeOffset.Now,
            EmailAddress            = "test@app.com",
            FirstName               = "Admin",
            ImagePath               = "no-image.jpg",
            LastName                = "User",
            IsAccountActivated      = true,
            IsActive                = true,
            PasswordHash            = encryption.GenerateHash("1qazxsw2",salt),
            PasswordSalt            = salt,
            Role                    = UserRole.Admin,
            SecurityStamp           = $"{Guid.NewGuid():N}",
            UniqueId                = Guid.NewGuid()
        };

        if (!context.Users.Any())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}