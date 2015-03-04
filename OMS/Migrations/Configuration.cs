namespace OMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using OMS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<OMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OMS.Models.ApplicationDbContext context)
        {

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);

            // Adding Default Admin User
            var user = new ApplicationUser() { UserName = "Administrator", Email = "mohit29121991@gmail.com", PhoneNumber = "9915505025" };
            IdentityResult result = userManager.Create(user, "4$p1r3%63n3$1$");
            if (result.Succeeded)
            {
                // Adding Claims for Newly Created User
                result = userManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "Admin"));
            }
        }
    }
}
