using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TelerikAcademy.ForumSystem.Data;
using TelerikAcademy.ForumSystem.Data.Model;
using TelerikAcademy.ForumSystem.Data.Repositories;
using TelerikAcademy.ForumSystem.Data.SaveContext;

namespace Bikers_Diary.Tests.Data
{
    [TestClass]
    public class EfRepositoryTests
    {
        private Mock<MsSqlDbContext> dbContextMock;

        [TestInitialize]
        public void Initialize()
        {
            this.dbContextMock = new Mock<MsSqlDbContext>();
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenContextIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new EfRepository<Post>(null)
            );
        }

        [TestMethod]
        public void Create_ShouldReturnAnInstance()
        {
            //Act
            var result = MsSqlDbContext.Create();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SaveContext_Constructor_ShouldThrow_WhenNullContext()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SaveContext(null)
            );
        }

        [TestMethod]
        public void SaveContext_SaveChanges_ShouldCallCommitOnContext()
        {
            //Arrange
            var saveContext = new SaveContext(this.dbContextMock.Object);

            //Act
            saveContext.Commit();

            //Assert
            this.dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void ConstructorForPostsRepo_ShouldSetDbSet_WhenContextIsNotNull()
        {
            //Act
            var repo = new EfRepository<Post>(this.dbContextMock.Object);

            //Assert
            this.dbContextMock.Verify(c => c.Set<Post>(), Times.Once);
        }

        //[TestMethod]
        //public void GetPosts_ShouldReturnCorrectInstance()
        //{
        //    var data = new MsSqlDbContext();

        //    var postsRepoMock = new Mock<IEfRepository<Post>>();
        //    var postsRepo = data.Posts;

        //    Assert.AreEqual(postsRepo, postsRepoMock.Object);
        //}
    }
}
