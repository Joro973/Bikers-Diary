using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TelerikAcademy.ForumSystem.Data.Model;
using TelerikAcademy.ForumSystem.Data.Repositories;
using TelerikAcademy.ForumSystem.Data.SaveContext;
using TelerikAcademy.ForumSystem.Services;

namespace Bikers_Diary.Tests.Services
{
    [TestClass]
    public class UsersServiceTests
    {
        private Mock<IEfRepository<User>> usersRepoMock;

        private Mock<ISaveContext> contextMock;

        [TestInitialize]
        public void Initialize()
        {
            this.usersRepoMock = new Mock<IEfRepository<User>>();
            this.contextMock = new Mock<ISaveContext>();
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenContextParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new UserService(this.usersRepoMock.Object, null));
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenPostsRepoParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new UserService(null, contextMock.Object));
        }

        [TestMethod]
        public void GetAll_ShouldCallAllOnRepo()
        {
            //Arrange
            var userService = new UserService(usersRepoMock.Object, contextMock.Object);

            //Act
            userService.GetAll();

            //Assert
            this.usersRepoMock.Verify(r => r.All, Times.Once);
        }

        [TestMethod]
        public void GetUserByName_ShouldThrow_WhenUserNotFound()
        {
            //Arrange
            var userService = new UserService(usersRepoMock.Object, contextMock.Object);
            var mockedName = "joro";

            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => userService.GetUserByName(mockedName));
        }

    }
}
