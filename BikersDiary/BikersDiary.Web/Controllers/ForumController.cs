namespace BikersDiary.ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Model;
    using Services;
    using Models.Home;
    using Bytes2you.Validation;
    using PagedList;

    public class ForumController : Controller
    {
        private readonly IUserService usersSrvice;

        private readonly IPostsService postsService;

        private readonly ICommentsService commentsService;

        public ForumController(IPostsService postsService, 
            ICommentsService commentsService, IUserService usersService)
        {
            Guard.WhenArgument(postsService, "postsService").IsNull().Throw();
            Guard.WhenArgument(commentsService, "commentsService").IsNull().Throw();
            Guard.WhenArgument(usersService, "usersService").IsNull().Throw();

            this.postsService = postsService;
            this.commentsService = commentsService;
            this.usersSrvice = usersService;
        }

        // GET: Forum
        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }

            var posts = postsService
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

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(Guid id)
        {
            var post = postsService.Find(id);

            var viewModel = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorEmail = post.Author.Email,
                Comments = post.Comments
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddPost(Post post)
        {
            var user = this.usersSrvice.GetUserByName(this.User.Identity.Name);
            user.Posts.Add(post);

            this.postsService.AddPost(post);

            return Redirect("/Forum");
        }

        [HttpPost]
        public ActionResult FilteredPosts(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return this.Index(null);
            }
            else
            {
                var filteredPosts = this.postsService.GetPostByTitleOrAuthor(searchTerm).
                    OrderByDescending(p => p.ModifiedOn)
                    .ToList();

                return this.PartialView("_FilteredPostsPartial", filteredPosts);    
            }
        }

        [HttpPost]
        public ActionResult AddComment(string userName, Guid postId, Comment comment)
        {
            var user = this.usersSrvice.GetUserByName(this.User.Identity.Name);
            comment.Author = user;
            comment.AuthorId = user.Id;
            user.Comments.Add(comment);

            var post = this.postsService.Find(postId);
            post.Comments.Add(comment);
            comment.PostId = post.Id;

            this.commentsService.AddComment(comment);

            return Redirect("/Forum");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task AddComment(Comment comment)
        //{
        //    if (comment.AuthorId != null && comment.Content != null)
        //    {
        //        var userName = this.User.Identity.Name;
        //        var user = this.usersSrvice.GetUserByName(userName);
        //    }
        //}


    }
}