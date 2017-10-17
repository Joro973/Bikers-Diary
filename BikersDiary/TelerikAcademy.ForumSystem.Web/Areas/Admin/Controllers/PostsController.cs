namespace TelerikAcademy.ForumSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Model;
    using Services;

    public class PostsController : BaseAdminController
    {
        public PostsController(IPostsService postsService, IUserService usersService)
            :base(postsService, usersService)
        {
      
        }

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var posts = this.postsService.GetAll().Include(p => p.Author);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(Guid id)
        {
            Post post = this.postsService.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.usersService.GetAll(), "Id", "Email");
            return View();
        }

        // POST: Admin/Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,AuthorId,Identifier,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                this.postsService.AddPost(post);

                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.usersService.GetAll(), "Id", "Email", post.AuthorId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(Guid id)
        {
            Post post = this.postsService.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuthorId = new SelectList(this.usersService.GetAll(), "Id", "Email", post.AuthorId);

            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AuthorId,Identifier,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Post post)
        {
            if (ModelState.IsValid)
            {
                var foundPost = this.postsService.Find(post.Id);
                foundPost.Title = post.Title;
                foundPost.Content = post.Content;
                foundPost.AuthorId = post.AuthorId;
                foundPost.IsDeleted = post.IsDeleted;
                foundPost.DeletedOn = post.DeletedOn;
                foundPost.CreatedOn = post.CreatedOn;

                this.postsService.Update(foundPost);

                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.usersService.GetAll(), "Id", "Email", post.AuthorId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(Guid id)
        {
            Post post = this.postsService.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = this.postsService.Find(id);
            this.postsService.RemovePost(post);;
            return RedirectToAction("Index");
        }
    }
}
