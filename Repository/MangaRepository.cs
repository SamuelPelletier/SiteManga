using Microsoft.EntityFrameworkCore;
using SiteManga.Data;
using SiteManga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Repository
{
    public class MangaRepository
    {
        private ApplicationDbContext context;
        public MangaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
