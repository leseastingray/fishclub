using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Xunit;
using FishClubWebsite.Models;
using FishClubWebsite.Controllers;
using FishClubWebsite.Repositories;

namespace FishClubWebsite.Tests
{
    public class FishTests
    {
        FishController controller;
        FakeFishRepository repo;

        // Constructor for the tests' Arrange step
        public FishTests()
        {
            // Instantiate new FakeFishRepository
            repo = new FakeFishRepository();
            // Instantiate FishController with FakeFish repo
            controller = new FishController(repo);
        }
        // Repository Tests
        [Fact]
        // Test get all Fish
        public void DoesGetAllFish()
        {
            // Act
            List<Fish> testList = repo.GetAllFish();
            // Assert
            Assert.Equal("Painted frogfish", testList[2].FName);
            Assert.Equal("Coasts", testList[1].FHabitat);
            Assert.Equal("Small", testList[0].FSize);
        }
        [Fact]
        // Test get Fish by fish name
        public void DoesGetFishByName()
        {
            // Act
            Fish testFish = repo.GetFishByName("Painted frogfish");
            // Assert
            Assert.Equal("Small fish", testFish.FDiet);
        }
        [Fact]
        // Test get List of Fish by location
        public void DoesGetFishesByLocation()
        {
            // Act
            List<Fish> testList = repo.GetFishByLocation("Tropical oceans");
            // Assert
            Assert.Equal("Bluespotted ribbontail ray", testList[0].FName);
            Assert.Equal("Banded sea krait", testList[1].FName);
        }
        [Fact]
        // Test Add Comment to Fish
        public void DoesAddCommentToFish()
        {
            // Arrange
            Fish testFish = repo.GetFishByName("Bluespotted ribbontail ray");
            int testFishId = testFish.FishID;

            // Act
            Comment testComment = new Comment
            {
                FishName = "Bluespotted ribbontail ray",
                CommentText = "Super cute and beautiful fish!",
                Rating = 5
            };
            repo.AddComment(testFishId, testComment);
            // Assert
            Assert.Equal(5, testFish.Comments[0].Rating);
        }
    }
}