using System.Collections.Generic;
using System.Reflection;
using Bikers_Diary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bikers_Diary.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Data;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Bikers_Diary.Data.DbContext";
        }

        protected override void Seed(DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var userManager = new UserManager<User>(new UserStore<User>(context));
            var passwordHasher = new PasswordHasher();
            var userNames = new List<string>();
            userNames.Add("joro@gmail.com");
            userNames.Add("test@abv.bg");
            userNames.Add("ivan@gmail.com");

            //for (int i = 0; i < userNames.Count; i++)
            //{
            //    if (!context.Users.Any(u => u.UserName == userNames[i]))
            //    {
            //        var user = new User
            //        {
            //            UserName = userNames[i],
            //            Email = userNames[i],
            //            PasswordHash = passwordHasher.HashPassword("P@ssw0rd123")
            //        };

            //        userManager.Create(user);
            //        userManager.AddToRole(user.Id, "user");
            //    }
            //}
        

        }
    }
}
