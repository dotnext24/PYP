using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Services
{
    public class CryptoService : ICryptoService
    {
        public string EncryptPassword(string password, string salt)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }


           
        }

        public string GenerateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
    }
}
