using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BikersDiary.ForumSystem.Data.Model;
using BikersDiary.ForumSystem.Services;
using BikersDiary.ForumSystem.Web.Areas.Admin.Controllers;
using TestStack.FluentMVCTesting;

namespace Bikers_Diary.Tests.Web.Controllers
{
    [TestClass]
    public class PostsAdminControllerTests
    {
        private Mock<IPostsService> postsServiceMock;

        private Mock<IUserService> usersServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            this.postsServiceMock = new Mock<IPostsService>();
            this.usersServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView()
        {
            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            postsAdmin.WithCallTo(p => p.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Details_ShouldRenderCorrectViewWithCorrectModel()
        {
            //Arrange
            var user = new User();
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "test controller",
                Author = user,
                AuthorId = user.Id,
                Comments = new List<Comment>()
            };

            postsServiceMock.Setup(m => m.Find(post.Id)).Returns(post);

            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            postsAdmin.WithCallTo(p => p.Details(post.Id))
                .ShouldRenderDefaultView()
                .WithModel(post);
        }

        [TestMethod]
        public void Create_ShouldRenderCorrectView()
        {
            //Arrange
            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            postsAdmin.WithCallTo(p => p.Create()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Create_ShouldRedirectToCorrectRoute()
        {
            //Arrange
            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            postsAdmin.WithCallTo(p => p.Create(new Post())).ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void Edit_ShouldRenderCorrectViewWithCorrectModel()
        {
            //Arrange
            var user = new User();
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "test controller",
                Author = user,
                AuthorId = user.Id,
                Comments = new List<Comment>()
            };

            postsServiceMock.Setup(m => m.Find(post.Id)).Returns(post);

            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            postsAdmin.WithCallTo(p => p.Edit(post.Id))
                .ShouldRenderDefaultView()
                .WithModel(post);
        }

        //[TestMethod]
        //public void Edit_ShouldRedirectToCorrectRoute_WhenNoPostIsFound()
        //{
        //    var user = new User();
        //    var post = new Post()
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "title",
        //        Content = "test controller",
        //        Author = user,
        //        AuthorId = user.Id,
        //        Identifier = 0,
        //        Comments = new List<Comment>()
        //    };

        //    postsServiceMock.Setup(m => m.Find(post.Id)).Returns((Post)null);

        //    PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

        //    //Act
        //    postsAdmin.WithCallTo(p => p.Edit(post.Id));
        //}

        [TestMethod]
        public void Delete_ShouldRenderCorrectViewWithCorrectModel()
        {
            var user = new User();
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Content = "test controller",
                Author = user,
                AuthorId = user.Id,
                Comments = new List<Comment>()
            };

            postsServiceMock.Setup(m => m.Find(post.Id)).Returns(post);

            PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            postsAdmin.WithCallTo(p => p.Delete(post.Id))
                .ShouldRenderDefaultView()
                .WithModel(post);
        }

        //[TestMethod]
        //public void Delete_ShouldRedirectToCorrectRoute()
        //{
        //    var user = new User();
        //    var post = new Post()
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = "title",
        //        Content = "test controller",
        //        Author = user,
        //        AuthorId = user.Id,
        //        Identifier = 0,
        //        Comments = new List<Comment>()
        //    };

        //    postsServiceMock.Setup(m => m.Find(post.Id)).Returns(post);

        //    PostsController postsAdmin = new PostsController(postsServiceMock.Object, usersServiceMock.Object);

        //    postsAdmin.WithCallTo(p => p.Delete(post.Id))
        //        .ShouldRedirectToRoute("");
        //}
    }
}
