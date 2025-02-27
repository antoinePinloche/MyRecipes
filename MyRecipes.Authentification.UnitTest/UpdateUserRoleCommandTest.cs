using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Application.User.Command.UpdateUserRole;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.UnitTest
{
    public class UpdateUserRoleCommandTest
    {
        private readonly Mock<IUsersRepository> _usersRepository = new();
        private readonly Mock<ILogger<UpdateUserRoleCommandHandler>> _logger = new();
        private readonly Mock<IServiceProvider> _serviceProvider = new();
        private CancellationToken _cancellationToken = CancellationToken.None;

        [Fact]
        [Description("UpdateUserRoleCommand : WrongParameterException Id")]
        public async Task UpdateUserRoleCommandTest_WrongParameterException_ID()
        {
            Guid userId = Guid.Empty;
            string UserRole = string.Empty;

            UpdateUserRoleCommand query = new UpdateUserRoleCommand(userId, UserRole, false);
            UpdateUserRoleCommandHandler handler = new UpdateUserRoleCommandHandler(_usersRepository.Object, _serviceProvider.Object, _logger.Object);

            var result = await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
            Assert.NotNull(result);
        }

        [Fact]
        [Description("UpdateUserRoleCommand : WrongParameterException UserRole")]
        public async Task UpdateUserRoleCommandTest_WrongParameterException_UserRole()
        {
            Guid userId = Guid.NewGuid();
            string UserRole = string.Empty;

            UpdateUserRoleCommand query = new UpdateUserRoleCommand(userId, UserRole, false);
            UpdateUserRoleCommandHandler handler = new UpdateUserRoleCommandHandler(_usersRepository.Object, _serviceProvider.Object, _logger.Object);

            var result = await Assert.ThrowsAsync<WrongParameterException>(async () => await handler.Handle(query, _cancellationToken));
            Assert.NotNull(result);
        }

        [Fact]
        [Description("UpdateUserRoleCommand : UserNotFoundException")]
        public async Task UpdateUserRoleCommandTest_UserNotFoundException()
        {
            Guid userId = Guid.NewGuid();
            string UserRole = "Admin";

            UpdateUserRoleCommand query = new UpdateUserRoleCommand(userId, UserRole, false);
            UpdateUserRoleCommandHandler handler = new UpdateUserRoleCommandHandler(_usersRepository.Object, _serviceProvider.Object, _logger.Object);

            var result = await Assert.ThrowsAsync<UserNotFoundException>(async () => await handler.Handle(query, _cancellationToken));
            Assert.NotNull(result);
        }
    }
}
