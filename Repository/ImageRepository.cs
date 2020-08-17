using SiteManga.Data;
using SiteManga.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Repository
{
    public class ImageRepository
    {
        private ApplicationDbContext context;
        public ImageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Image> getImagesNotBind()
        {
            List<Image> images = this.context.Images.Where(i => i.manga == null).ToList();
            return images;
        }
    }
}
