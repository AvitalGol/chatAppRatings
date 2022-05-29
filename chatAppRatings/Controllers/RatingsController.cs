using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using chatAppRatings.Data;
using chatAppRatings.Models;

namespace chatAppRatings.Controllers
{
    public class RatingsController : Controller
    {
        private readonly chatAppRatingsContext _context;

        public RatingsController(chatAppRatingsContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rating.ToListAsync());
        }

        public async Task<IActionResult> Search()
        {
            return View(await _context.Rating.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {

            var q = from rating in _context.Rating
                    where rating.FeedBack.Contains(query) || rating.Name.Contains(query)
                    select rating;

            return View(await q.ToListAsync());
        }

        public async Task<IActionResult> Search2(string query)
        {
            if (query == null)
            {
                query = "";
            }

            var q = from rating in _context.Rating
                    where rating.FeedBack.Contains(query) || rating.Name.Contains(query)
                    select rating;

            return PartialView(await q.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumericRating,FeedBack")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.Time = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumericRating,FeedBack,Time")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Search));
            }
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rating == null)
            {
                return Problem("Entity set 'chatAppRatingsContext.Rating'  is null.");
            }
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }

        private bool RatingExists(int id)
        {
          return _context.Rating.Any(e => e.Id == id);
        }
    }
}
