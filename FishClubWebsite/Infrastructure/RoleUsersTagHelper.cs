using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FishClubWebsite.Models;

namespace FishClubWebsite.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUsersTagHelper(UserManager<AppUser> umgr,
        RoleManager<IdentityRole> rmgr)
        {
            userManager = umgr;
            roleManager = rmgr;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
        TagHelperOutput output)
        {
            // list to hold names
            List<string> names = new List<string>();
            // initialize and find role by ID
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            // if role not null
            if (role != null)
            {
                // for each user in Users list from userManager
                foreach (var user in userManager.Users)
                {
                    // if user not null and is in specified role
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        // add user's username to names list
                        names.Add(user.UserName);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ?
            "No Users" : string.Join(", ", names));
        }
    }
}
