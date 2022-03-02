using ExpressCargo.Core.Interfaces.Utility.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ExpressCargo.Core.Utilities.Security;
public class Encryption : IEncryption
{
    public string GenerateSalt()
    {
        ushort bits = 256;

        var salt = new byte[bits / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }
    public string GenerateHash(string value, string salt)
    {
        if(value == null) { throw new ArgumentNullException(nameof(value)); }
        if (salt == null) { throw new ArgumentNullException(nameof(salt)); }

        int iterationCount = 10000;
        ushort bits = 256;

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: value,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: iterationCount,
            numBytesRequested: bits / 8
        ));
        return hashed;
    }
}