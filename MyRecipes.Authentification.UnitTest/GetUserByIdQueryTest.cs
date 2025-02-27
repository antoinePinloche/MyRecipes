using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Authentification.Application.User.Query.GetUserById;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Authentification.UnitTest
{
    public class GetUserByIdQueryTest
    {
        private readonly Mock<IUsersRepository> _usersRepository = new();
        private readonly Mock<ILogger<GetUserByIdQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("GetUserByIdQuery : WrongParameterException")]
        public async Task GetUserByIdQueryTest_WrongParameterException()
        {
            Guid id = Guid.Empty;
            GetUserByIdQuery query = new GetUserByIdQuery(id);
            GetUserByIdQueryHandler handler = new GetUserByIdQueryHandler(_usersRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetUserByIdQuery : UserNotFoundException")]
        public async Task GetUserByIdQueryTest_UserNotFoundException()
        {
            Guid id = Guid.NewGuid();
            GetUserByIdQuery query = new GetUserByIdQuery(id);
            GetUserByIdQueryHandler handler = new GetUserByIdQueryHandler(_usersRepository.Object, _logger.Object);

            await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

        [Fact]
        [Description("GetUserByIdQuery : Ok")]
        public async Task GetUserByIdQueryTest_Ok()
        {
            Guid id = Guid.NewGuid();
            GetUserByIdQuery query = new GetUserByIdQuery(id);
            GetUserByIdQueryHandler handler = new GetUserByIdQueryHandler(_usersRepository.Object, _logger.Object);

            _usersRepository.Setup(s => s.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new Domain.Entities.User() { UserName = "UserNameTest", Email = "test@gmail.com" });

            var result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Equal("UserNameTest", result.UserName);
            Assert.Equal("test@gmail.com", result.Email);
        }
    }
}
