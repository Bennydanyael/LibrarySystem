using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LibraryAppAPI.Helpers
{
    public class HashingHelper
    {
        public static string HashUsingPbkdf2(string _password, string _salt)
        {
            var _bytes = new Rfc2898DeriveBytes(_password, Convert.FromBase64String(_salt), 1000, HashAlgorithmName.SHA256);
            var _deriveRandomKey = _bytes.GetBytes(32);
            var _hash = Convert.ToBase64String(_deriveRandomKey);
            return _hash;
        }
    }
}
