using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public virtual User User { get; set; }
    }
}
