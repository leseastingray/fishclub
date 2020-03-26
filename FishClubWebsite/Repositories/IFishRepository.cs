using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FishClubWebsite.Models;

namespace FishClubWebsite.Repositories
{
    public interface IFishRepository
    {
        List<Fish> GetAllFish();
        Fish GetFishByName(string name);
        Fish GetFishById(int id);
        List<Fish> GetFishBySize(string size);
        List<Fish> GetFishByLocation(string location);
        List<Fish> GetFishByHabitat(string habitat);

        int AddFish(Fish fish);
        int EditFish(Fish fish);
        int DeleteFish(int id);
    }
}
