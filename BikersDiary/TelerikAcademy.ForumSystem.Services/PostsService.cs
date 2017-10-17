namespace TelerikAcademy.ForumSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Model;
    using Data.Repositories;
    using Data.SaveContext;
    using Bytes2you.Validation;

    public class PostsService : IPostsService
    {
        private readonly IEfRepository<Post> postsRepo;
        private readonly ISaveContext context;

        public PostsService(IEfRepository<Post> postsRepo, ISaveContext context)
        {
            Guard.WhenArgument(postsRepo, "postsRepo").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.postsRepo = postsRepo;
            this.context = context;
        }

        public IQueryable<Post> GetAll()
        {
            return this.postsRepo.All;
        }

        public Post Find(Guid id)
        {
            return this.postsRepo.Find(id);
        }

        public void AddPost(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsRepo.Add(post);
            this.context.Commit();
        }

        public void RemovePost(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsRepo.Delete(post);
            this.context.Commit();
        }

        public ICollection<Post> GetCurrentUserPosts(User user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();

            return user.Posts;
        }

        public void Update(Post post)
        {
            Guard.WhenArgument(post, "post").IsNull().Throw();

            this.postsRepo.Update(post);
            this.context.Commit();
        }

        public void AddComment(User user, Post post, Comment comment)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();
            Guard.WhenArgument(post, "post").IsNull().Throw();
            Guard.WhenArgument(comment, "comment").IsNull().Throw();

            post.Comments.Add(comment);
            user.Comments.Add(comment);
            this.context.Commit();
        }
    }
}
