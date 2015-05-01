using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSport.Domain.Api
{
    public interface IEncryptProvider
    {
        string Encrypt(string data);
    }
}
