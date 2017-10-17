using System.Collections.Generic;

namespace TelerikAcademy.ForumSystem.Services
{
    using System.Linq;
    using Data.Model;

    public interface ICommentsService
    {
        IQueryable<Comment> GetAll();

        void Update(User user, Comment comment);

        ICollection<Comment> GetCurrentUserComments(User user);
    }
}
