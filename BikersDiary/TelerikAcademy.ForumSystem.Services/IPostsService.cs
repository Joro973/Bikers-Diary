namespace TelerikAcademy.ForumSystem.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Data.Model;

    public interface IPostsService
    {
        IQueryable<Post> GetAll();

        Post Find(Guid id);

        void Update(Post post);

        ICollection<Post> GetCurrentUserPosts(User user);

        void AddComment(User user, Post post, Comment comment);

        void AddPost(Post post);

        void RemovePost(Post post);
    }
}