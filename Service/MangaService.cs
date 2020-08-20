using SiteManga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Service
{
    public static  class MangaService
    {
        public static List<Manga> findAllMangaInOrderWithUser(User user)
        {
            List<Manga> mangas = new List<Manga>();
            foreach(Order order in user.Orders)
            {
                foreach(MangaOrder mangaOrder in order.MangaOrders)
                {
                    mangas.Add(mangaOrder.Manga);
                }
            }

            return mangas.Distinct().ToList();
        }
    }
}
