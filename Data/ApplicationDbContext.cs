using Microsoft.EntityFrameworkCore;
using SiteManga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Editor> Editors { get; set; }
    }
}
