using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TelerikAcademy.ForumSystem.Services;
using TelerikAcademy.ForumSystem.Web.Controllers;
using TelerikAcademy.ForumSystem.Web.Models.Home;
using TestStack.FluentMVCTesting;

namespace Bikers_Diary.Tests.Web.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IPostsService> postsServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            this.postsServiceMock = new Mock<IPostsService>();
        }

        [TestMethod]
        public void Constructor_ShouldReturnAnInstance_WhenValidParams()
        {
            //Act
            HomeController homeController = new HomeController(postsServiceMock.Object);

            //Assert
            Assert.IsNotNull(homeController);
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenNullPostService()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new HomeController(null)
            );
        }

        [TestMethod]
        public void Index_ShouldRenderCorrectView()
        {
            //Arrange
            HomeController homeController = new HomeController(postsServiceMock.Object);

            //Act & Assert
            homeController.WithCallTo(h => h.Index()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void Index_ShouldRedirectToCorrectRoute()
        {
            //Arrange
            HomeController homeController = new HomeController(postsServiceMock.Object);

            //Act & Assert
            homeController.WithCallTo(h => h.Index(new PostViewModel())).ShouldRedirectToRoute("");
        }
    }
}
