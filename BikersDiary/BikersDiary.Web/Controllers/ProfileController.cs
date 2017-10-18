using System.Linq;
using System.Web.Mvc;
using BikersDiary.ForumSystem.Services;
using Bytes2you.Validation;
using PagedList;
using PagedList.Mvc;

namespace BikersDiary.ForumSystem.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService usersService;
                 
        private readonly IPostsService postsService;
                
        private readonly ICommentsService commentsService;

        public ProfileController(IUserService usersService,
            IPostsService postsService, ICommentsService commentsService)
        {
            Guard.WhenArgument(usersService, "usersService").IsNull().Throw();
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();
            Guard.WhenArgument(commentsService, "commentsService").IsNull().Throw();

            this.usersService = usersService;
            this.postsService = postsService;
            this.commentsService = commentsService;
        }
        
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index(int? page, string userName)
        {
            if (page == null)
            {
                page = 1;
            }

            var user = this.usersService.GetUserByName(userName);
            var posts = this.postsService.GetCurrentUserPosts(user)
                .OrderByDescending(p => p.ModifiedOn);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult MyComments(int? page, string userName)
        {
            if (page == null)
            {
                page = 1;
            }

            var user = this.usersService.GetUserByName(userName);
            var comments = this.commentsService.GetCurrentUserComments(user)
                .OrderByDescending(c => c.ModifiedOn);

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
        }
    }
}