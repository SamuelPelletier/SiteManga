using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiteManga.Controllers;
using SiteManga.Data;
using SiteManga.Models;

namespace SiteManga
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,AddressShippingSteelName,AddressShippingPostalCode,AddressShippingCountry,AddressShippingCity,AddressShippingOption1,AddressShippingOption2,AddressInvoiceSteelName,AddressInvoiceCountry,AddressInvoiceCity,AddressInvoicePostalCode,AddressInvoiceOption1,AddressInvoiceOption2")] Order order)
        {
            if (ModelState.IsValid)
            {
                var mangasSession = HttpContext.Session.GetString("mangaOrders");
                List<MangaOrder> mangaOrders = JsonConvert.DeserializeObject<List<MangaOrder>>(mangasSession);
                order.date = DateTime.Now;
                double totalPrice = 0;
                double totalWeight = 0;

                _context.Add(order);
                _context.SaveChanges();
                foreach (MangaOrder mangaOrder in mangaOrders)
                {
                    totalPrice += mangaOrder.Manga.Price * mangaOrder.Quantity;
                    totalWeight += mangaOrder.Manga.Weight * mangaOrder.Quantity;
                    mangaOrder.Manga = _context.Mangas.Find(mangaOrder.Manga.Id);
                    mangaOrder.Order = order;
                    _context.Add(mangaOrder);
                }

                order.TotalPrice = totalPrice;
                order.TotalWeight = totalWeight;

                order.MangaOrders = mangaOrders;
                _context.Update(order);
                _context.SaveChanges();
                HttpContext.Session.SetString("mangaOrders", JsonConvert.SerializeObject(new List<MangaOrder>()));
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index","Cart");
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
