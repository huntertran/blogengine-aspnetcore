namespace BlogEngine.Services.Tests
{
    using Models;
    using Moq;
    using Post;
    using Repository.Generic;
    using System;
    using System.Linq.Expressions;
    using Xunit;

    public class PostServiceTests
    {
        [Fact]
        public void ShouldReturnIntCountPost()
        {
            // Arrange
            var expected = 5;
            var mockGenericRepo = new Mock<IGenericRepository<Post>>();
            mockGenericRepo.Setup(s => s.Count(It.IsAny<Expression<Func<Post, bool>>>())).Returns(expected);

            var postService = new PostService(mockGenericRepo.Object);

            // Act
            var act = postService.Count();

            // Assert
            Assert.Equal(expected, act);
        }
    }
}
