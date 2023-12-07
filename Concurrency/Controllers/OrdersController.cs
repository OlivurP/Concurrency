using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concurrency.Data;
using Concurrency.Models;

namespace Concurrency.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ConcurrencyContext _context;
        public Order Order { get; set; }
        public static List<Seat> SeatList { get; set; } 

        public OrdersController(ConcurrencyContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return _context.Order != null ? 
                          View(await _context.Order.Include(x => x.Seat).ToListAsync()) :
                          Problem("Entity set 'ConcurrencyContext.Order'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int id)
        {
            Order = new Order();
            SeatList = _context.Seat.ToList();
            _context.Order.Add(Order);
            Order.Movie = await _context.Movie.FindAsync(id);
            Order.Movie.Seats = _context.Seat.ToList();
            return View(Order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Seat,RowVersion")] Order order, [Bind("RowVersion")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (Seat s in _context.Seat.ToList())
                    {
                        if (order.Seat.Name == s.Name)
                        {
                            order.Seat = s;
                            order.Seat.Taken = true;
                            break;
                        }
                    }
                    foreach(Seat s in SeatList)
                    {
                        if(s.Name == order.Seat.Name)
                        {
                            _context.Entry(order.Seat).Property(x => x.RowVersion).OriginalValue = s.RowVersion;
                        }
                    }
                    _context.Update(order.Seat);
                    _context.Entry(order.Seat).Property(x => x.RowVersion).IsModified = false;
                    _context.Update(order);
                    _context.Entry(order).Property(x => x.RowVersion).OriginalValue = order.RowVersion;
                    _context.Entry(order).Property(x => x.RowVersion).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Seat,Movie,RowVersion")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    _context.Entry(order).Property(x => x.RowVersion).OriginalValue = order.RowVersion;
                    _context.Entry(order).Property(x => x.RowVersion).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
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
            if (_context.Order == null)
            {
                return Problem("Entity set 'ConcurrencyContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
