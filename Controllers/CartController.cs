using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteManga.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SiteManga.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.MangaOrders = JsonConvert.DeserializeObject<List<MangaOrder>>(HttpContext.Session.GetString("mangaOrders"));
            return View();
        }

        public IActionResult Add(Manga manga)
        {
            var mangasSession = HttpContext.Session.GetString("mangaOrders");
            List<MangaOrder> mangaOrders = new List<MangaOrder>();
            if (mangasSession != null)
            {
                mangaOrders = JsonConvert.DeserializeObject<List<MangaOrder>>(mangasSession);
            }
            
            bool mangaFind = false;
            foreach(MangaOrder mangaOrder in mangaOrders)
            {
                if(mangaOrder.Manga.Id == manga.Id)
                {
                    mangaFind = true;
                    mangaOrder.Quantity = mangaOrder.Quantity + 1;
                }
            }
            if(mangaFind == false)
            {
                MangaOrder mangaOrder = new MangaOrder();
                mangaOrder.Manga = manga;
                mangaOrder.Quantity = 1;
                mangaOrders.Add(mangaOrder);
            }
            HttpContext.Session.SetString("mangaOrders", JsonConvert.SerializeObject(mangaOrders));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddQuantity(Manga manga)
        {
            var mangasSession = HttpContext.Session.GetString("mangaOrders");
            List<MangaOrder>  mangaOrders = JsonConvert.DeserializeObject<List<MangaOrder>>(mangasSession);

            foreach (MangaOrder sessionMangaOrder in mangaOrders)
            {
                if (sessionMangaOrder.Manga.Id == manga.Id)
                {
                    sessionMangaOrder.Quantity = sessionMangaOrder.Quantity + 1;
                    HttpContext.Session.SetString("mangaOrders", JsonConvert.SerializeObject(mangaOrders));
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveQuantity(Manga manga)
        {
            var mangasSession = HttpContext.Session.GetString("mangaOrders");
            List<MangaOrder> mangaOrders = JsonConvert.DeserializeObject<List<MangaOrder>>(mangasSession);

            foreach (MangaOrder sessionMangaOrder in mangaOrders)
            {
                if (sessionMangaOrder.Manga.Id == manga.Id)
                {
                    sessionMangaOrder.Quantity = sessionMangaOrder.Quantity - 1;
                    if (sessionMangaOrder.Quantity == 0)
                    {
                        mangaOrders.Remove(sessionMangaOrder);
                    }
                    HttpContext.Session.SetString("mangaOrders", JsonConvert.SerializeObject(mangaOrders));
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
