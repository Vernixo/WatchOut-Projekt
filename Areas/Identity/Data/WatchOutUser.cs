using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WatchOut.Areas.Identity.Data;

// Add profile data for application users by adding properties to the WatchOutUser class
public class WatchOutUser : IdentityUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsAdmin { get; set; }
}

