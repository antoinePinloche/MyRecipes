using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Exception;
using System.ComponentModel;

namespace MyRecipes.Authentification.UnitTest
{
    public class DeleteUserCommandTest
    {
        private readonly Mock<IUsersRepository> _usersRepository = new();
        private readonly Mock<ILogger<DeleteUserCommandHandler>> _logger = new();
        private readonly Mock<IServiceProvider> _serviceProvider = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("DeleteUserCommandQuery : WrongParameterException")]
        public async Task DeleteUserCommandTest_WrongParameterException()
        {
            Guid userId = Guid.Empty;

            DeleteUserCommand query = new DeleteUserCommand(userId);
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(_usersRepository.Object, _serviceProvider.Object, _logger.Object);

            var result = await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
            Assert.NotNull(result);
        }

        [Fact]
        [Description("DeleteUserCommandQuery : UserNotFoundException")]
        public async Task DeleteUserCommandTest_UserNotFoundException()
        {
            Guid userId = Guid.NewGuid();

            DeleteUserCommand query = new DeleteUserCommand(userId);
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(_usersRepository.Object, _serviceProvider.Object, _logger.Object);

            _usersRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()));
            await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

    }
}
