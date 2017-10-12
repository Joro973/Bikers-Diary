using System.Linq;
using Bikers_Diary.Data;
using Bikers_Diary.Models;

namespace Bikers_Diary.Controllers.Forum
{
    using System.Web.Mvc;

    public class ForumController : BaseController
    {
        public ForumController(IDiaryData data) 
            : base(data)
        {
        }

        public ForumController(IDiaryData data, User user) : base(data, user)
        {
        }

        public ActionResult ForumView()
        {
            var posts = this.Data.Posts.All()
                .OrderByDescending(p => p.DatePublished);
            return this.View(posts);
        }

        public ActionResult ForumAllView()
        {
            var posts = this.Data.Posts.All()
                .OrderByDescending(p => p.DatePublished);

            ViewBag.Posts = posts;
            return this.View(posts);
        }
    }
}