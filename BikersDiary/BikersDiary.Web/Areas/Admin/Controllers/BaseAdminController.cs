using Bytes2you.Validation;

namespace BikersDiary.ForumSystem.Web.Areas.Admin.Controllers
{
    using Services;
    using System.Web.Mvc;

    [Authorize(Roles = "Admin")]
    public class BaseAdminController : Controller
    {
        protected readonly IPostsService postsService;

        protected readonly IUserService usersService;

        public BaseAdminController(IPostsService postsService, IUserService usersService)
        { 
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();
            Guard.WhenArgument(usersService, "usersService").IsNull().Throw();

            this.postsService = postsService;
            this.usersService = usersService;
        }
    }
}