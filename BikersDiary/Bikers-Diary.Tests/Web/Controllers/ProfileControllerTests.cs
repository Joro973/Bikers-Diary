using System;
using System.Collections.Generic;
using BikersDiary.ForumSystem.Data.Model;
using BikersDiary.ForumSystem.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace Bikers_Diary.Tests.Web.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using BikersDiary.ForumSystem.Services;

    [TestClass]
    public class ProfileControllerTests
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
        public void Constructor_ShouldReturnInstance_WhenParamsAreNotNull()
        {
            //Act
            ProfileController profileController = new ProfileController(usersServiceMock.Object, 
                postsServiceMock.Object, commentsServiceMock.Object);

            //Assert
            Assert.IsNotNull(profileController);
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenUserServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProfileController(null, postsServiceMock.Object, commentsServiceMock.Object)
            );
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenPostsServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProfileController(usersServiceMock.Object, null, commentsServiceMock.Object)
            );
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenCommentsServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ProfileController(usersServiceMock.Object, postsServiceMock.Object, null)
            );
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView()
        {
            //Arrange
            var user = new User();

            this.usersServiceMock.Setup(u => u.GetUserByName(user.UserName)).Returns(user);
            this.postsServiceMock.Setup(p => p.GetCurrentUserPosts(user)).Returns(new List<Post>());

            ProfileController profileController = new ProfileController(usersServiceMock.Object,
                postsServiceMock.Object, commentsServiceMock.Object);

            //Act & Assert
            profileController.WithCallTo(p => p.Index(1, user.UserName))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView_WhenNullPage()
        {
            //Arrange
            var user = new User();

            this.usersServiceMock.Setup(u => u.GetUserByName(user.UserName)).Returns(user);
            this.postsServiceMock.Setup(p => p.GetCurrentUserPosts(user)).Returns(new List<Post>());

            ProfileController profileController = new ProfileController(usersServiceMock.Object,
                postsServiceMock.Object, commentsServiceMock.Object);

            //Act & Assert
            profileController.WithCallTo(p => p.Index(null, user.UserName))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void MyComments_ShouldRenderCorrectView()
        {
            //Arrange
            //Arrange
            var user = new User();

            this.usersServiceMock.Setup(u => u.GetUserByName(user.UserName)).Returns(user);
            this.commentsServiceMock.Setup(c => c.GetCurrentUserComments(user)).Returns(new List<Comment>());

            ProfileController profileController = new ProfileController(usersServiceMock.Object,
                postsServiceMock.Object, commentsServiceMock.Object);

            //Act & Assert
            profileController.WithCallTo(p => p.MyComments(2, user.UserName))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void MyComments_ShouldRenderCorrectView_WhenNullPage()
        {
            //Arrange
            //Arrange
            var user = new User();

            this.usersServiceMock.Setup(u => u.GetUserByName(user.UserName)).Returns(user);
            this.commentsServiceMock.Setup(c => c.GetCurrentUserComments(user)).Returns(new List<Comment>());

            ProfileController profileController = new ProfileController(usersServiceMock.Object,
                postsServiceMock.Object, commentsServiceMock.Object);

            //Act & Assert
            profileController.WithCallTo(p => p.MyComments(null, user.UserName))
                .ShouldRenderDefaultView();
        }
    }
}
