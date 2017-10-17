namespace Bikers_Diary.Tests.Services
{
    using System;
    using TelerikAcademy.ForumSystem.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TelerikAcademy.ForumSystem.Data.Model;
    using TelerikAcademy.ForumSystem.Data.Repositories;
    using TelerikAcademy.ForumSystem.Data.SaveContext;

    [TestClass]
    public class CommentsServiceTests
    {
        private Mock<IEfRepository<Comment>> commentsRepoMock;

        private Mock<ISaveContext> contextMock;

        [TestInitialize]
        public void Initialize()
        {
            this.commentsRepoMock = new Mock<IEfRepository<Comment>>();
            this.contextMock = new Mock<ISaveContext>();
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenContextParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommentsService(commentsRepoMock.Object, null));
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenPostsRepoParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new CommentsService(null, contextMock.Object));
        }

        [TestMethod]
        public void Update_ShouldThrowNullArgumentException_WhenUserIsNull()
        {
            //Arrange
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);
            var comment = new Comment();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => commentsService.Update(null, comment));
        }

        [TestMethod]
        public void Update_ShouldCallUpdateOnRepo_WhenValidUserAndComment()
        {
            //Arrange
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);
            var user = new User();
            var comment = new Comment();

            //Act
            commentsService.Update(user, comment);

            //Assert
            this.commentsRepoMock.Verify(c => c.Update(comment), Times.Once);
        }

        [TestMethod]
        public void Update_ShouldCallCommit_WhenValidUserAndComment()
        {
            //Arrange
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);
            var user = new User();
            var comment = new Comment();

            //Act
            commentsService.Update(user, comment);

            //Assert
            this.contextMock.Verify(c => c.Commit(), Times.Once);
        }

        [TestMethod]
        public void Update_ShouldThrowNullArgumentException_WhenCommentIsNull()
        {
            //Arrange
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);
            var user = new User();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => commentsService.Update(user, null));
        }

        [TestMethod]
        public void GetAll_ShouldCallAllOnRepo()
        {
            //Arrange
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);

            //Act
            commentsService.GetAll();

            //Assert
            this.commentsRepoMock.Verify(c => c.All, Times.Once);
        }

        [TestMethod]
        public void GetCurrentUserComments_ShouldThrow_WhenUserIsNull()
        {
            var commentsService = new CommentsService(commentsRepoMock.Object, contextMock.Object);

            Assert.ThrowsException<ArgumentNullException>(() =>
                commentsService.GetCurrentUserComments(null)
            );
        }
    }
}
