using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishClubWebsite.Models
{
    // for users to login to their accounts via the Login View
    public class LoginViewModel
    {
        [Required]
        [UIHint("Email")]
        public string Email { get; set; }
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
