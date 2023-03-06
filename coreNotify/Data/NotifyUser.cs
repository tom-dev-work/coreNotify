using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoreNotify.Areas.Identity.Data;

// Add profile data for application users by adding properties to the NotifyUser class
public class NotifyUser : IdentityUser
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}
public class ApplicationRole : IdentityRole
{

}
