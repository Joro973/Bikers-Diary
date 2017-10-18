namespace BikersDiary.ForumSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Model;

    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "info@telerikacademy.com";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);

            base.Seed(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleName = "Admin";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = roleName };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, roleName);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            Guid seedPostId = Guid.Empty;
            if (!context.Posts.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var post = new Post()
                    {
                        Title = "Post " + i,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet lobortis nibh. Nullam bibendum, tortor quis porttitor fringilla, eros risus consequat orci, at scelerisque mauris dolor sit amet nulla. Vivamus turpis lorem, pellentesque eget enim ut, semper faucibus tortor. Aenean malesuada laoreet lorem.",
                        Author = context.Users.First(x => x.Email == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };

                    context.Posts.Add(post);

                    if (i == 0)
                    {
                        seedPostId = post.Id;
                    }
                }
            }

            Console.WriteLine(context.Posts.Count());
            if (!context.Comments.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var comment = new Comment()
                    {
                        Content =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. . .",
                        Author = context.Users.First(x => x.Email == AdministratorUserName),
                        AuthorId = context.Users.First(u => u.Email == AdministratorUserName).Id,
                        PostId = seedPostId,
                        CreatedOn = DateTime.Now
                    };

                    context.Comments.Add(comment);
                }
            }
        }
    }
}
