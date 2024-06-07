using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Application_Expense_Tracker.Areas.Identity.Data;
using Web_Application_Expense_Tracker.Models;

namespace Web_Application_Expense_Tracker.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var userCategories = _context.Categories
                .Where(c => c.Users.Any(u => u.Id == userId));
            return View(await userCategories.ToListAsync());
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            ViewBag.EmojiList = new List<string>
            {
                "💵", "👕", "📄", "✈️", "🥐", "🚗", "🍷", "🍼", "🎁", "❤️",
                "🏥", "🏠", "🐕", "👶", "🪑", "🧹", "🧴", "🎆"
            };
            return View(new Category());
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                category.Users.Add(user);
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            ViewBag.EmojiList = new List<string>
            {
                "💵", "👕", "📄", "✈️", "🥐", "🚗", "🍷", "🍼", "🎁", "❤️",
                "🏥", "🏠", "🐕", "👶", "🪑", "🧹", "🧴", "🎆"
            };

            var userId = _userManager.GetUserId(User);
            var category = await _context.Categories
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.CategoryId == id && c.Users.Any(u => u.Id == userId));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var existingCategory = await _context.Categories
                    .Include(c => c.Users)
                    .FirstOrDefaultAsync(c => c.CategoryId == id && c.Users.Any(u => u.Id == userId));

                if (existingCategory == null)
                {
                    return NotFound();
                }

                try
                {
                    existingCategory.Title = category.Title;
                    existingCategory.Icon = category.Icon;
                    existingCategory.Type = category.Type;
                    _context.Update(existingCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories' is null.");
            }

            var userId = _userManager.GetUserId(User);
            var category = await _context.Categories
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.CategoryId == id && c.Users.Any(u => u.Id == userId));

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Categories?.Any(e => e.CategoryId == id && e.Users.Any(u => u.Id == userId)) ?? false;
        }
    }
}
