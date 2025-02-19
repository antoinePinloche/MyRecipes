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

            await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
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

        [Fact]
        [Description("DeleteUserCommandQuery : Ok")]
        public async Task DeleteUserCommandTest_OK()
        {
            Guid userId = Guid.NewGuid();

            DeleteUserCommand query = new DeleteUserCommand(userId);


            var serviceCollection = new ServiceCollection();

            Mock<UserManager<Domain.Entities.User>> userManager = new Mock<UserManager<Domain.Entities.User>>();
            userManager.Setup(s => s.GetRolesAsync(It.IsAny<Domain.Entities.User>()))
                .ReturnsAsync(
                    new List<string>()
                    {
                        "Administrator"
                    }
                );
            // Add any DI stuff here:
            serviceCollection.AddSingleton<UserManager<Domain.Entities.User>>(userManager.Object);

            // Create the ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // serviceScopeMock will contain my ServiceProvider
            var serviceScopeMock = new Mock<IServiceScope>();
            serviceScopeMock.SetupGet<IServiceProvider>(s => s.ServiceProvider)
                .Returns(serviceProvider);

            // serviceScopeFactoryMock will contain my serviceScopeMock
            var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            serviceScopeFactoryMock.Setup(s => s.CreateScope())
                .Returns(serviceScopeMock.Object);

            _usersRepository.Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                new Domain.Entities.User()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Test",
                    Email = "Test"
                });
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(_usersRepository.Object, serviceProvider, _logger.Object);
            await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
        }

    }
}
