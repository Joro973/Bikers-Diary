using Bikers_Diary.Models;

namespace Bikers_Diary.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class DbContext : IdentityDbContext<User>
    {
        public DbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }
    }
}
