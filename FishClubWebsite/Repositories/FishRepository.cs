using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FishClubWebsite.Models;

namespace FishClubWebsite.Repositories
{

    // The repository object will be injected into a Controller by a transient service, configured in Startup
    public class FishRepository : IFishRepository
    {
        public int AddFish(Fish fish)
        {
            throw new NotImplementedException();
        }

        public int DeleteFish(int id)
        {
            throw new NotImplementedException();
        }

        public int EditFish(Fish fish)
        {
            throw new NotImplementedException();
        }

        public List<Fish> GetAllFish()
        {
            throw new NotImplementedException();
        }

        public List<Fish> GetFishByHabitat(string habitat)
        {
            throw new NotImplementedException();
        }

        public Fish GetFishById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Fish> GetFishByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public Fish GetFishByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Fish> GetFishBySize(string size)
        {
            throw new NotImplementedException();
        }
    }
}
