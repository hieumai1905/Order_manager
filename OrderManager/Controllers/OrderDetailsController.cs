using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderManager.Context;
using OrderManager.Entities;

namespace OrderManager.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly MyDbContext _context;

        public OrderDetailsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await myDbContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create(int orderId)
        {
            ViewData["OrderId"] = orderId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OrderDetailId,Price,Quantity,OrderId,ProductId")]
            OrderDetail orderDetail)
        {
            try
            {
                 var orderDetailExists = _context.OrderDetails.SingleOrDefault(x =>
                    x.OrderId == orderDetail.OrderId && x.ProductId.Equals(orderDetail.ProductId));

                if (orderDetailExists != null)
                {
                    // Nếu sản phẩm đã tồn tại, thì cập nhật thông tin số lượng sản phẩm trong OrderDetail
                    orderDetailExists.Quantity += orderDetail.Quantity;
                    _context.OrderDetails.Update(orderDetailExists);
                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, thì thêm mới OrderDetail vào context
                    _context.OrderDetails.Add(orderDetail);
                }

                await _context.SaveChangesAsync();

                // Cập nhật thông tin của Order
                var order = _context.Orders.Single(x => x.OrderId == orderDetail.OrderId);

                var orderDetails =
                    await _context.OrderDetails.Where(x => x.OrderId == orderDetail.OrderId).ToListAsync();
                double volume = 0, mass = 0, total = 0;
                foreach (var item in orderDetails)
                {
                    var product = _context.Products.Single(x => x.ProductId.Equals(item.ProductId));
                    volume += item.Quantity * product.Volume;
                    mass += item.Quantity * product.Mass;
                    total += item.Quantity * product.Price;
                }

                order.Volume = volume;
                order.Mass = mass;
                order.TotalPrice = total;


                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = orderDetail.OrderId });
            }
            catch (Exception ex)
            {
                ViewData["OrderId"] = orderDetail.OrderId;
                ViewData["ProductId"] =
                    new SelectList(_context.Products, "ProductId", "ProductId", orderDetail.ProductId);
                return View(orderDetail);
            }
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            ViewData["OrderId"] = orderDetail.OrderId;
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("OrderDetailId,Price,Quantity,OrderId,ProductId")]
            OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            try
            {
                if (orderDetail.Quantity == 0)
                {
                    _context.OrderDetails.Remove(orderDetail);
                }
                else
                {
                    _context.OrderDetails.Update(orderDetail);
                }

                await _context.SaveChangesAsync();

                // Cập nhật thông tin của Order
                var order = _context.Orders.Single(x => x.OrderId == orderDetail.OrderId);

                var orderDetails =
                    await _context.OrderDetails.Where(x => x.OrderId == orderDetail.OrderId).ToListAsync();
                double volume = 0, mass = 0, total = 0;
                foreach (var item in orderDetails)
                {
                    var product = _context.Products.Single(x => x.ProductId.Equals(item.ProductId));
                    volume += item.Quantity * product.Volume;
                    mass += item.Quantity * product.Mass;
                    total += item.Quantity * product.Price;
                }

                order.Volume = volume;
                order.Mass = mass;
                order.TotalPrice = total;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = orderDetail.OrderId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(orderDetail.OrderDetailId))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["OrderId"] = orderDetail.OrderId;
                    ViewData["ProductId"] =
                        new SelectList(_context.Products, "ProductId", "ProductId", orderDetail.ProductId);
                    return View(orderDetail);
                }
            }
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'MyDbContext.OrderDetails' is null.");
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                var orderId = orderDetail.OrderId;
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
                // Cập nhật thông tin của đơn hàng tương ứng
                var order = _context.Orders.Single(x => x.OrderId == orderId);

                var orderDetails = await _context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
                double volume = 0, mass = 0, total = 0;
                foreach (var item in orderDetails)
                {
                    var product = _context.Products.Single(x => x.ProductId.Equals(item.ProductId));
                    volume += item.Quantity * product.Volume;
                    mass += item.Quantity * product.Mass;
                    total += item.Quantity * product.Price;
                }

                order.Volume = volume;
                order.Mass = mass;
                order.TotalPrice = total;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Orders", new { id = orderDetail.OrderId });
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}