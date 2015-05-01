using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Api;

namespace NewSport.Domain.Concrete
{
    public class DefaultEncryptProvider:IEncryptProvider
    {
        public string Encrypt(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            var builder = new StringBuilder();
            foreach (byte hash in hashData)
            {
                builder.Append(hash.ToString());
            }
            return builder.ToString();
        }
        
    }
}
