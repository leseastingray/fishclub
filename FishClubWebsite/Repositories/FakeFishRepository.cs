using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishClubWebsite.Models;
using FishClubWebsite.Repositories;

namespace FishClubWebsite
{
    public class FakeFishRepository : IFishRepository
    {
        // List to hold fish objects
        List<Fish> fishes = new List<Fish>();

        // Constructor with seed data
        public FakeFishRepository()
        {
            // Create new Fish object
            Fish fish = new Fish
            {
                FishID = 0,
                FName = "Bluespotted ribbontail ray",
                FSize = "Small",
                FDiet = "Invertebrates",
                FHabitat = "Coral reef",
                FLocation = "Tropical oceans",
                Date = DateTime.Parse("3/17/2019")
            };
            // Add new Fish object to the List
            fishes.Add(fish);

            // Create new Fish object
            Fish fish1 = new Fish
            {
                FishID = 1,
                FName = "Banded sea krait",
                FSize = "Medium",
                FDiet = "Eels",
                FHabitat = "Coasts",
                FLocation = "Tropical oceans",
                Date = DateTime.Parse("1/11/2019")
            };
            // Add new Fish object to the List
            fishes.Add(fish1);

            // Create new Fish object
            Fish fish2 = new Fish
            {
                FishID = 2,
                FName = "Painted frogfish",
                FSize = "Small",
                FDiet = "Small fish",
                FHabitat = "Rocks and coral reefs",
                FLocation = "Subtropical oceans",
                Date = DateTime.Parse("2/14/2019")
            };
            // Add new Fish object to the List
            fishes.Add(fish2);
        }

        public int AddFish(Fish fish)
        {
            fishes.Add(fish);
            return fish.FishID;
        }

        public int DeleteFish(int id)
        {
            // find first fish with its FishID being id
            var fishFromList = fishes.First(f => f.FishID == id);
            return id;
        }

        public int EditFish(Fish fish)
        {
            throw new NotImplementedException();
        }

        public List<Fish> GetAllFish()
        {
            return fishes;
        }

        public List<Fish> GetFishByHabitat(string habitat)
        {
            return (from f in fishes
                    where f.FHabitat.Contains(habitat)
                    select f).ToList();
        }

        public Fish GetFishById(int id)
        {
            // find first fish with its FishID being id
            Fish fishFromList = fishes.First(f => f.FishID == id);
            return fishFromList;
        }

        public List<Fish> GetFishByLocation(string location)
        {
            return (from f in fishes
                    where f.FLocation.Contains(location)
                    select f).ToList();
        }

        public Fish GetFishByName(string name)
        {
            return fishes.First(f => f.FName == name);
        }

        public List<Fish> GetFishBySize(string size)
        {
            return (from f in fishes
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
