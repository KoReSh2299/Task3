using DZen.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTask3
{
    public static class HMACGenerator
    {
        public static string GenerateRandomKey(int countBytes)
        {
            byte[] key = new byte[countBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                
            }
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }

        public static string GenerateHMACSHA256(string key, string message)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
