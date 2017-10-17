using System.Web.Mvc;
using Bytes2you.Validation;
using PagedList;
using TelerikAcademy.ForumSystem.Services;
using PagedList.Mvc;

namespace TelerikAcademy.ForumSystem.Web.Controllers
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
            var posts = this.postsService.GetCurrentUserPosts(user);

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
            var comments = this.commentsService.GetCurrentUserComments(user);

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(comments.ToPagedList(pageNumber, pageSize));
        }
    }
}