using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishClubWebsite.Models
{
    public class Fish
    {
        // List to hold user comments on fish
        private List<Comment> comments= new List<Comment>();
        public List<Comment> Comments { get { return comments; } }

        // PK for database
        public int FishID { get; set; }

        public string FName { get; set; }
        public string FSize { get; set; }
        public string FDiet { get; set; }
        public string FHabitat { get; set; }
        public string FLocation { get; set; }

        // Date added
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }
}
