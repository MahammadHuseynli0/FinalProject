using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NanoNexus.Core.EnumForCore;
using NanoNexus.Core.Models;
using NanoNexus.Data.DAL;

namespace NanoNexus.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super Admin")]

    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public OrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var orders = _appDbContext.Orders.ToList();
            return View(orders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Order order = await _appDbContext.Orders.Include(x => x.OrderItems).FirstAsync(x => x.Id == id);

            return View(order);

        }

        public async Task<IActionResult> Accept(int id)
        {
            Order order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
                return NotFound();

            order.OrderStatus = OrderStatus.Accepted;
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("index", "order");
        }

        public async Task<IActionResult> Reject(int id)
        {
            Order order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
                return NotFound();

            order.OrderStatus = OrderStatus.Rejected;
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("index", "order");
        }
    }
}
