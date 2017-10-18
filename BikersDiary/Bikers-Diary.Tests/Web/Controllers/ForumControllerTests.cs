using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;
using BikersDiary.ForumSystem.Data.Model;
using BikersDiary.ForumSystem.Services;
using BikersDiary.ForumSystem.Web.Controllers;
using BikersDiary.ForumSystem.Web.Models.Home;
using TestStack.FluentMVCTesting;

namespace Bikers_Diary.Tests.Web.Controllers
{
    [TestClass]
    public class ForumControllerTests
    {
        private Mock<IPostsService> postsServiceMock;

        private Mock<ICommentsService> commentsServiceMock;

        private Mock<IUserService> usersServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            this.postsServiceMock = new Mock<IPostsService>();
            this.commentsServiceMock = new Mock<ICommentsService>();
            this.usersServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void Constructor_ShouldReturnAnInstance_WhenValidParams()
        {
            //Act
            ForumController forumController = new ForumController(postsServiceMock.Object, 
                commentsServiceMock.Object, usersServiceMock.Object);

            //Assert
            Assert.IsNotNull(forumController);
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenPostsServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ForumController(null, commentsServiceMock.Object, usersServiceMock.Object)
            );
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenCommentsServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ForumController(postsServiceMock.Object, null, usersServiceMock.Object)
            );
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenUsersServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ForumController(postsServiceMock.Object, commentsServiceMock.Object, null)
            );
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView()
        {
            //Arrange
            ForumController forumController = new ForumController(postsServiceMock.Object,
                commentsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            forumController.WithCallTo(f => f.Index(1))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<PostViewModel>>();
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView_WhenNullPage()
        {
            //Arrange
            ForumController forumController = new ForumController(postsServiceMock.Object,
                commentsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            forumController.WithCallTo(f => f.Index(null))
                .ShouldRenderDefaultView()
                .WithModel<PagedList<PostViewModel>>();
        }

        [TestMethod]
        public void Details_ShouldReturnViewWithCorrectModel()
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

            var postViewModel = new PostViewModel()
            {
                Title = post.Title,
                Content = post.Content,
                AuthorEmail = post.Author.Email,
                //PostedOn = post.CreatedOn.Value,
                Comments = post.Comments
            };

            postsServiceMock.Setup(m => m.Find(post.Id)).Returns(post);

            ForumController forumController = new ForumController(postsServiceMock.Object, 
                commentsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            forumController.WithCallTo(p => p.Details(post.Id))
                .ShouldRenderDefaultView()
                .WithModel<PostViewModel>(vm =>
                {
                    Assert.AreEqual(post.Title, vm.Title);
                    Assert.AreEqual(post.Content, vm.Content);
                });
        }

        [TestMethod]
        public void AddPost_ShouldRenderCorrectView()
        {
            ForumController forumController = new ForumController(postsServiceMock.Object,
                commentsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            forumController.WithCallTo(f => f.AddPost())
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void FilteredPosts_ShouldRenderCorrectView_WhenSearchTermIsEmpty()
        {
            //Arrange
            ForumController forumController = new ForumController(postsServiceMock.Object,
                commentsServiceMock.Object, usersServiceMock.Object);

            //Act & Assert
            forumController.WithCallTo(f => f.FilteredPosts(""))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void FilteredPosts_ShouldRenderCorrectView_WhenSearchTermIsNotEmpty()
        {
            //Arrange
            ForumController forumController = new ForumController(postsServiceMock.Object,
                commentsServiceMock.Object, usersServiceMock.Object);

            IQueryable<Post> testPosts = Enumerable.Empty<Post>().AsQueryable();
            postsServiceMock.Setup(p => p.GetPostByTitleOrAuthor("testSearch")).Returns(testPosts);

            //Act & Assert
            forumController.WithCallTo(f => f.FilteredPosts("testSearch"))
                .ShouldRenderPartialView("_FilteredPostsPartial");
        }
    }
}
