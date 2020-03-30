using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FishClubWebsite.Models;
using FishClubWebsite.Data;

namespace FishClubWebsite.Repositories
{

    // The repository object will be injected into a Controller by a transient service, configured in Startup
    public class FishRepository : IFishRepository
    {
        private readonly ApplicationDbContext context;

        public FishRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        // Add method, takes Fish object as parameter
        public int AddFish(Fish fish)
        {
            // Add the fish to database and update
            context.Fishes.Update(fish);
            // Return context and save the fish for PK FishID
            return context.SaveChanges();
        }
        // Delete method, takes FishID as parameter
        public int DeleteFish(int id)
        {
            // find first fish with its FishID being id
            var fishFromDb = context.Fishes.First(f => f.FishID == id);
            // remove from database
            context.Remove(fishFromDb);
            // return and save changes
            return context.SaveChanges();
        }
        // Edit method, takes Fish object as parameter
        public int EditFish(Fish fish)
        {
            context.Fishes.Update(fish);
            return context.SaveChanges();
        }

        // Retrieval methods
        // Get list of all Fish
        public List<Fish> GetAllFish()
        {
            return context.Fishes.ToList();
        }
        // Get individual Fish by FishID
        public Fish GetFishById(int id)
        {
            return context.Fishes.First(f => f.FishID == id);
        }
        // Get individual Fish by name
        public Fish GetFishByName(string name)
        {
            return context.Fishes.First(f => f.FName == name);
        }
        // Get list of Fish by habitat
        public List<Fish> GetFishByHabitat(string habitat)
        {
            return (from f in context.Fishes
                    where f.FHabitat.Contains(habitat)
                    select f).ToList();
        }
        // Get list of Fish by Location
        public List<Fish> GetFishByLocation(string location)
        {
            return (from f in context.Fishes
                    where f.FLocation.Contains(location)
                    select f).ToList();
        }
        // Get list of Fish by size
        public List<Fish> GetFishBySize(string size)
        {
            return (from f in context.Fishes
                    where f.FSize.Contains(size)
                    select f).ToList();
        }
        // Add comment associated with Fish
        public void AddComment(int fishID, Comment comment)
        {
            // Get fish object from fishid parameter
            Fish fish = GetFishById(fishID);
            // Add to the Fish comment from comment parameter
            fish.Comments.Add(comment);
            // Update database and save changes
            context.Fishes.Update(fish);
            context.SaveChanges();
        }
        // Get list of comments associated with Fish
        public List<Comment> GetCommentsByFishID(int fishID)
        {
            // Get fish object from fishid parameter
            Fish fish = GetFishById(fishID);
            // Get list of comments associated with that fish object
            return fish.Comments.ToList();
        }
    }
}
