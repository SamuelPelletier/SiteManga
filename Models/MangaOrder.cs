using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    public class MangaOrder
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public virtual Manga Manga { get; set; }

        public virtual Order Order { get; set; }
    }
}
