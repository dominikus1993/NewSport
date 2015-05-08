using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSport.WebApi.Models
{
    public class RegisterViewModel
    {
        public String UserName { get; set; }

        public String Email { get; set; }

        public String ConfirmEmail { get; set; }

        public String Password { get; set; }

        public String ConfirmPassword { get; set; }
    }
}
