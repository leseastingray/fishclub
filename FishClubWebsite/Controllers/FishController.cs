using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using FishClubWebsite.Data;
using FishClubWebsite.Models;

namespace FishClubWebsite.Controllers
{
    public class FishController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public FishController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: Fish
        public async Task<IActionResult> Index()
        {
            return View(await context.Fishes.ToListAsync());
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await context.Fishes
                .FirstOrDefaultAsync(m => m.FishID == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        [Authorize(Roles = "Members")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FishID,FName,FSize,FDiet,FHabitat,FLocation,Date")] Fish fish)
        {
            if (ModelState.IsValid)
            {
                context.Add(fish);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fish);
        }

        // GET: Fish/Edit/5
        [Authorize(Roles = "Members")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await context.Fishes.FindAsync(id);
            if (fish == null)
            {
                return NotFound();
            }
            return View(fish);
        }

        // POST: Fish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FishID,FName,FSize,FDiet,FHabitat,FLocation,Date")] Fish fish)
        {
            if (id != fish.FishID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(fish);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishExists(fish.FishID))
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
            return View(fish);
        }

        // GET: Fish/Delete/5
        [Authorize(Roles = "Members")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await context.Fishes
                .FirstOrDefaultAsync(m => m.FishID == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // POST: Fish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fish = await context.Fishes.FindAsync(id);
            context.Fishes.Remove(fish);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FishExists(int id)
        {
            return context.Fishes.Any(e => e.FishID == id);
        }
    }
}
