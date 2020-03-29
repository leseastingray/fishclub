using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FishClubWebsite.Models
{
    public class Comment
    {
        // PK for database
        public int CommentID { get; set; } 
        
        [StringLength(80, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the name of the fish")]
        public string FishName { get; set; }
        public string CommentText { get; set; }

        [Range(1,5)]
        [Required(ErrorMessage = "Please choose a rating for the fish")]
        public int Rating { get; set; }

        // Date added
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        public AppUser Member { get; set; }
    }
}
