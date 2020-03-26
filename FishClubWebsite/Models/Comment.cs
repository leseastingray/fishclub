using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishClubWebsite.Models
{
    public class Comment
    {
        // PK for database
        public int CommentID { get; set; } 

        public string CommentText { get; set; }
        public AppUser Member { get; set; }
    }
}
