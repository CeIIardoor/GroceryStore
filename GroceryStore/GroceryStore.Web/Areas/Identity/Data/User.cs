using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GroceryStore.Web.Areas.Identity.Data.GroceryStore;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? GetFullName()
    {
        return FirstName + " " + LastName;
    }

}

