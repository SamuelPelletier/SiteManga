using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public virtual User User { get; set; }

        public virtual Manga Manga { get; set; }
    }
}
