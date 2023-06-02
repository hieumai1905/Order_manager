using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManager.Context;
using OrderManager.Entities;

namespace OrderManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Products.Include(p => p.Category).Include(p => p.WorkShop);
            return View("Index1", await myDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.WorkShop)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create(int? categoryId)
        {
            if (categoryId != null)
            {
                ViewData["CategoryIds"] = categoryId;
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "NameCategory");
            }

            ViewData["WorkShopId"] = new SelectList(_context.WorkShops, "WorkShopId", "NameWorkShop");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ProductId,Name,Price,Status,Photo,Description, Volume, Mass,WorkShopId,CategoryId")]
            Product product, int? id)
        {
            var category = await _context.Categories.SingleAsync(c => c.CategoryId == product.CategoryId);
            var shop = await _context.WorkShops.SingleAsync(x => x.WorkShopId == product.WorkShopId);
            product.WorkShop = shop;
            product.Category = category;
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                if (id != null)
                {
                    ViewData["CategoryIds"] = id;
                }
                else
                {
                    ViewData["CategoryId"] =
                        new SelectList(_context.Categories, "CategoryId", "NameCategory", product.CategoryId);
                }

                ViewData["WorkShopId"] =
                    new SelectList(_context.WorkShops, "WorkShopId", "NameWorkShop", product.WorkShopId);
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] =
                new SelectList(_context.Categories, "CategoryId", "NameCategory", product.CategoryId);
            ViewData["WorkShopId"] =
                new SelectList(_context.WorkShops, "WorkShopId", "NameWorkShop", product.WorkShopId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind("ProductId,Name,Price,Status,Photo,Description, Volume, Mass,WorkShopId,CategoryId")]
            Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }


            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }

                ViewData["CategoryId"] =
                    new SelectList(_context.Categories, "CategoryId", "NameCategory", product.CategoryId);
                ViewData["WorkShopId"] =
                    new SelectList(_context.WorkShops, "WorkShopId", "NameWorkShop", product.WorkShopId);
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.WorkShop)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyDbContext.Products'  is null.");
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}