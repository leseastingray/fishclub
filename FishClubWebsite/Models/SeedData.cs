using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FishClubWebsite.Data;

namespace FishClubWebsite.Models
{
    public class SeedData
    {
        // Method to seed data in case of empty database
        public static void Seed(IServiceProvider services)
        {
            // Initialize and define context
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            // Make sure database has been created
            context.Database.EnsureCreated();

            // If no fish contained in context
            //  Seed the following Fish data
            if (!context.Fishes.Any())
            {
                // Create new Fish object
                Fish fish = new Fish{
                    FName = "Bluespotted ribbontail ray",
                    FSize = "Small",
                    FDiet = "Invertebrates",
                    FHabitat = "Coral reef",
                    FLocation = "Tropical oceans",
                    Date = DateTime.Parse("3/17/2019")};
                // Add the Fish to the database
                context.Fishes.Add(fish);
                // Save the Fish so that it gets a PK FishID
                context.SaveChanges();

                // Create new Fish object
                fish = new Fish
                {
                    FName = "Giant moray",
                    FSize = "Large",
                    FDiet = "Large fish",
                    FHabitat = "Lagoon",
                    FLocation = "Southern oceans",
                    Date = DateTime.Parse("3/5/2019")
                };
                // Add the Fish to the database
                context.Fishes.Add(fish);
                // Save the Fish so that it gets a PK FishID
                context.SaveChanges();

                // Create new Fish object
                fish = new Fish
                {
                    FName = "Banded sea krait",
                    FSize = "Medium",
                    FDiet = "Eels",
                    FHabitat = "Coasts",
                    FLocation = "Tropical oceans",
                    Date = DateTime.Parse("1/11/2019")
                };
                // Add the Fish to the database
                context.Fishes.Add(fish);
                // Save the Fish so that it gets a PK FishID
                context.SaveChanges();

                // Create new Fish object
                fish = new Fish
                {
                    FName = "Painted frogfish",
                    FSize = "Small",
                    FDiet = "Small fish",
                    FHabitat = "Rocks and coral reefs",
                    FLocation = "Subtropical oceans",
                    Date = DateTime.Parse("2/14/2019")
                };
                // Add the Fish to the database
                context.Fishes.Add(fish);
                // Save the Fish so that it gets a PK FishID
                context.SaveChanges();
            }
        }
    }
}
