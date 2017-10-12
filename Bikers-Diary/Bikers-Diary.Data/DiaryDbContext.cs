using System.Data.Entity;
using Bikers_Diary.Models;

namespace Bikers_Diary.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class DiaryDbContext : IdentityDbContext<User>
    {
        public DiaryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }

        public static DiaryDbContext Create()
        {
            return new DiaryDbContext();
        }
    }
}
