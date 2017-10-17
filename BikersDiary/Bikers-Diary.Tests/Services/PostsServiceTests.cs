namespace Bikers_Diary.Tests.Services
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TelerikAcademy.ForumSystem.Data;
    using TelerikAcademy.ForumSystem.Data.Model;
    using TelerikAcademy.ForumSystem.Data.Repositories;
    using TelerikAcademy.ForumSystem.Data.SaveContext;
    using TelerikAcademy.ForumSystem.Services;

    [TestClass]
    public class PostsServiceTests
    {
        private Mock<IEfRepository<Post>> postsRepoMock;

        private Mock<ISaveContext> contextMock;

        [TestInitialize]
        public void Initialize()
        {
            this.postsRepoMock = new Mock<IEfRepository<Post>>();
            this.contextMock = new Mock<ISaveContext>();
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenContextParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new PostsService(postsRepoMock.Object, null));
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenPostsRepoParameterIsNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new PostsService(null, contextMock.Object));
        }

        [TestMethod]
        public void Constructor_ShouldThrowNullArgumentException_WhenBothParametersAreNull()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => new PostsService(null, null));
        }

        [TestMethod]
        public void Constructor_Should_ReturnInstance_WhenParamsAreNotNull()
        {
            //Act
            PostsService postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Assert
            Assert.IsNotNull(postsService);
        }

        [TestMethod]
        public void Find_ShouldReturnModel_WhenItExists()
        {
            //Arrange
            Guid postId = new Guid();

            this.postsRepoMock.Setup(p => p.Find(postId)).Returns(new Post() { Id = postId });

            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act
            var postModel = postsService.Find(postId);

            //Assert
            Assert.IsNotNull(postModel);
        }

        [TestMethod]
        public void Find_ShouldReturnNull_WhenItDoesNotExists()
        {
            //Arrange
            Guid postId = new Guid();

            postsRepoMock.Setup(p => p.Find(postId)).Returns((Post)null);

            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act
            var postModel = postsService.Find(postId);

            //Assert
            Assert.IsNull(postModel);
        }

        [TestMethod]
        public void AddPost_ShouldAddPostToRepo()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.AddPost(post);

            //Assert
            this.postsRepoMock.Verify(p => p.Add(post), Times.Once);
        }

        [TestMethod]
        public void AddPost_ShouldCallCommit_WhenValidPost()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.AddPost(post);

            //Assert
            this.contextMock.Verify(p => p.Commit(), Times.Once);
        }

        [TestMethod]
        public void AddPost_ShouldThrow_WhenNullArgumentPassed()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                postsService.AddPost(null));
        }

        [TestMethod]
        public void RemovePost_ShouldRemovePostFromRepo()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.RemovePost(post);

            //Assert
            this.postsRepoMock.Verify(p => p.Delete(post), Times.Once);
        }

        [TestMethod]
        public void RemovePost_ShouldCallCommit_WhenValidPost()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.RemovePost(post);

            //Assert
            this.contextMock.Verify(p => p.Commit(), Times.Once);
        }

        [TestMethod]
        public void RemovePost_ShouldThrow_WhenNullArgumentPassed()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
                postsService.RemovePost(null));
        }

        [TestMethod]
        public void GetCurrentUserPosts_ShouldReturnICollection()
        {
            //TODO
        }

        [TestMethod]
        public void GetCurrentUserPosts_ShouldThrow_WhenNullUserPassed()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => postsService.GetCurrentUserPosts(null));
        }

        [TestMethod]
        public void Update_ShouldCallRemoveFromRepo()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.Update(post);

            //Assert
            this.postsRepoMock.Verify(p => p.Update(post), Times.Once);
        }

        [TestMethod]
        public void Update_ShouldCallCommit_WhenValidPost()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();

            //Act
            postsService.Update(post);

            //Assert
            this.contextMock.Verify(p => p.Commit(), Times.Once);
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenNullPostPassed()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => postsService.Update(null));
        }

        [TestMethod]
        public void AddComment_ShouldCallCommit_WhenParamsAreValid()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);
            var user = new User();
            var post = new Post();
            var comment = new Comment();

            //Act
            postsService.AddComment(user, post, comment);

            //Assert
            this.contextMock.Verify(p => p.Commit(), Times.Once);
        }

        [TestMethod]
        public void AddComment_ShouldThrow_WhenUserIsNull()
        {
            //Arrange
            var postsSerive = new PostsService(postsRepoMock.Object, contextMock.Object);
            var post = new Post();
            var comment = new Comment();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => postsSerive.AddComment(null, post, comment));
        }

        [TestMethod]
        public void AddComment_ShouldThrow_WhenPostIsNull()
        {
            //Arrange
            var postsSerive = new PostsService(postsRepoMock.Object, contextMock.Object);
            var user = new User();
            var comment = new Comment();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => postsSerive.AddComment(user, null, comment));
        }

        [TestMethod]
        public void AddComment_ShouldThrow_WhenCommentIsNull()
        {
            //Arrange
            var postsSerive = new PostsService(postsRepoMock.Object, contextMock.Object);
            var user = new User();
            var post = new Post();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => postsSerive.AddComment(user, post, null));
        }

        [TestMethod]
        public void GetAll_ShouldCallAllOnRepo()
        {
            //Arrange
            var postsService = new PostsService(postsRepoMock.Object, contextMock.Object);

            //Act
            postsService.GetAll();

            //Assert
            this.postsRepoMock.Verify(p => p.All, Times.Once);
        }
    }
}
