using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Shared;

public static class CommonAuthentication
{
    private static readonly byte[] fixedKey = Encoding.UTF8.GetBytes("your-secret-key-here"); // store securely

    public static void CreatePasswordHash(string password, out byte[] passwordHash)
    {
        using (var hmac = new HMACSHA512(fixedKey))
        {
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] storedHash)
    {
        using (var hmac = new HMACSHA512(fixedKey))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }



}
