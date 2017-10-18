using System.Linq;
using System.Web.Mvc;
using BikersDiary.ForumSystem.Services;
using BikersDiary.ForumSystem.Web.Models.Home;
using Bytes2you.Validation;

namespace BikersDiary.ForumSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        //private readonly IMapper mapper;

        public HomeController(IPostsService postsService)
        {
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();

            this.postsService = postsService;
            //this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var posts = this.postsService
                .GetAll()
                .OrderByDescending(p => p.ModifiedOn)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    AuthorEmail = p.Author.Email,
                    PostedOn = p.CreatedOn.Value
                })
                .ToList();

            return View(posts);
        }

        [HttpPost]
        public ActionResult Index(PostViewModel model)
        {
            //this.postsService.Update()

            return this.RedirectToAction("Index");
        }
    }
}