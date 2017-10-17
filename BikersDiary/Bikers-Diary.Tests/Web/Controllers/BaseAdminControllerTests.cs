namespace Bikers_Diary.Tests.Web.Controllers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TelerikAcademy.ForumSystem.Services;
    using TelerikAcademy.ForumSystem.Web.Areas.Admin.Controllers;

    [TestClass]
    public class BaseAdminControllerTests
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
        public void Constructor_ShoulReturnInstance_WhenParamsAreNotNull()
        {
            BaseAdminController baseAdmin = new BaseAdminController(postsServiceMock.Object, usersServiceMock.Object);

            Assert.IsNotNull(baseAdmin);
        }


        [TestMethod]
        public void Constructor_ShouldThrow_WhenPostsServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new BaseAdminController(null, usersServiceMock.Object)
            );
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenUsersServiceIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new BaseAdminController(postsServiceMock.Object, null)
            );
        }
    }
}
