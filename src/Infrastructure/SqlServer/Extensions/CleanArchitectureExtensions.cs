using ExpressCargo.Core.Interfaces.Utility.Security;
using ExpressCargo.Core.Models.Entities.Users;

namespace ExpressCargo.SqlServer;
public static class ExpressCargoExtensions
{
    public static void AddInitialData(this ExpressCargoContext context, IEncryption encryption)
    {
        context.SeedUsers(encryption);
    }

    private static void SeedUsers(this ExpressCargoContext context, IEncryption encryption)
    {
        var id = Guid.NewGuid();
        var salt = encryption.GenerateSalt();
        var user = new User
        {
            AccountActivateDate     = DateTimeOffset.Now,
            CreatedById             = id,
            CreatedOn               = DateTimeOffset.Now,
            EmailAddress            = "test@app.com",
            FirstName               = "Admin",
            Id                      = id,
            ImagePath               = "no-image.jpg",
            LastName                = "User",
            IsAccountActivated      = true,
            IsActive                = true,
            PasswordHash            = encryption.GenerateHash("1qazxsw2",salt),
            PasswordSalt            = salt,
            Role                    = UserRole.Admin,
            SecurityStamp           = Guid.NewGuid().ToString("N")
        };

        if (!context.Users.Any())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}