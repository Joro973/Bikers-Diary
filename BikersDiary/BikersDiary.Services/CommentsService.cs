namespace BikersDiary.ForumSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Bytes2you.Validation;
    using Data.Model;
    using Data.Repositories;
    using Data.SaveContext;

    public class CommentsService : ICommentsService
    {
        private readonly IEfRepository<Comment> commentsRepo;
        private readonly ISaveContext context;

        public CommentsService(IEfRepository<Comment> commentsRepo, ISaveContext context)
        {
            Guard.WhenArgument(commentsRepo, "commentsRepo").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.commentsRepo = commentsRepo;
            this.context = context;
        }

        public IQueryable<Comment> GetAll()
        {
            return this.commentsRepo.All;
        }

        public void Update(User user, Comment comment)
        {
            //TODO fix logic
            Guard.WhenArgument(user, "user").IsNull().Throw();
            Guard.WhenArgument(comment, "comment").IsNull().Throw();

            this.commentsRepo.Update(comment);
            this.context.Commit();
        }

        public void AddComment(Comment comment)
        {
            this.commentsRepo.Add(comment);
            this.context.Commit();
        }

        public ICollection<Comment> GetCurrentUserComments(User user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();

            return user.Comments;
        }
    }
}
