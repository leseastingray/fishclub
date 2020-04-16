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
using FishClubWebsite.Repositories;

namespace FishClubWebsite.Controllers
{
    public class FishController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        public IFishRepository fishRepo;

        // Constructor with repository
        public FishController(IFishRepository fishRepo)
        {
            this.fishRepo = fishRepo;
        }
        
        // Constructor with context and userManager, default from scaffolding
        /*public FishController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        */
        // GET: Fish
        public IActionResult Index()
        {
            List<Fish> fishes = fishRepo.GetAllFish();
            return View(fishes);
        }

        // GET: Fish/Details/5
        public IActionResult Details(int id)
        {
            Fish fish = fishRepo.GetFishById(id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        [Authorize]
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
                fishRepo.AddFish(fish);
                //await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fish);
        }

        // GET: Fish/Edit/5
        [Authorize]
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
        [Authorize]
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

        // Get Add Comment
        [Authorize]
        public IActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        // Post Add Comment
        public RedirectToActionResult AddComment(int id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                fishRepo.AddComment(id, comment);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("AddComment", fishRepo.GetFishById(id));
        }
        // GET: Comments
        public IActionResult Comments(int id)
        {
            List<Comment> comments = fishRepo.GetCommentsByFishID(id);
            return View(comments);
        }

        // Search View
        public IActionResult Search()
        {
            return View();
        }

        // Search View
        public IActionResult SearchResults()
        {
            return View();
        }

        // Search by Name
        [HttpPost]
        public RedirectToActionResult SearchByName(string id)
        {
            var fishes = from f in fishRepo.GetAllFish()
                         select f;

            if (!String.IsNullOrEmpty(id))
            {
                fishes = fishes.Where(f => f.FName.Contains(id));
            }

            ViewData["results"] = fishes;
            return RedirectToAction("SearchResults", ViewData["results"]);
        }
    }
}
