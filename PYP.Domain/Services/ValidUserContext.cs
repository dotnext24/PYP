using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Services
{
    public class ValidUserContext
    {
        public IPrincipal Principal { get; set;}
        public UserWithRoles User { get; set; }
        public bool IsValid()
        {
            return Principal!= null;
        }
    }
}
