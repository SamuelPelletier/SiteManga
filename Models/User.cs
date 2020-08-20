using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
