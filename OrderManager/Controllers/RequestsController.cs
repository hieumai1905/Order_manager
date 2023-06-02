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
    public class RequestsController : Controller
    {
        private readonly MyDbContext _context;

        public RequestsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(string? userId)
        {
            if (userId != null)
            {
                var myDbContext = _context.Requests.Include(r => r.Customer).Where(x=>x.CustomerId.Equals(userId));
                return View("Index1", await myDbContext.ToListAsync());
            }
            else
            {
                var myDbContext = _context.Requests.Include(r => r.Customer);
                return View("Index1", await myDbContext.ToListAsync());
            }
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create(string? userId)
        {
            if (userId != null)
            {
                ViewBag.userId = userId;
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,Image,Detail,Status,CustomerId")] Request request)
        {
            var customer = _context.Customers.Single(x => x.CustomerId.Equals(request.CustomerId));
            request.RequestAt = DateTime.Now;
            request.Customer = customer;
            try
            {
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", request.CustomerId);
                return View(request);
            }
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", request.CustomerId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("RequestId,Image,Detail,Status,CustomerId")]
            Request request)
        {
            if (id != request.RequestId)
            {
                return NotFound();
            }

            try
            {
                _context.Requests.Update(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewData["CustomerId"] =
                    new SelectList(_context.Customers, "CustomerId", "CustomerId", request.CustomerId);
                return View(request);
            }
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'MyDbContext.Requests'  is null.");
            }

            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}