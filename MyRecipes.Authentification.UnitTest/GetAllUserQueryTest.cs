using Microsoft.Extensions.Logging;
using Moq;
using MyRecipes.Authentification.Application.User.Query.GetAllUsers;
using MyRecipes.Authentification.Application.User.Query.GetUserById;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyRecipes.Authentification.UnitTest
{
    public class GetAllUserQueryTest
    {
        private readonly Mock<IUsersRepository> _usersRepository = new();
        private readonly Mock<ILogger<GetAllUsersQueryHandler>> _logger = new();
        private CancellationToken _cancellationToken = CancellationToken.None;
        
        [Fact]
        [Description("GetAllUsersQuery : Ok")]
        public async Task GetAllUsersQueryTest_Ok()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            GetAllUsersQueryHandler handler = new GetAllUsersQueryHandler(_usersRepository.Object, _logger.Object);

            _usersRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(
                new List<Domain.Entities.User>()
                {
                    new Domain.Entities.User() { UserName = "UserNameTest", Email = "test@gmail.com" },
                    new Domain.Entities.User() { UserName = "UserNameTest2", Email = "test2@gmail.com" }
                });

            var result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.NotEmpty(result.users);
            Assert.Equal(2, result.users.Count);
            Assert.Equal("UserNameTest", result.users[0].UserName);
            Assert.Equal("test@gmail.com", result.users[0].Email);
            Assert.Equal("UserNameTest2", result.users[1].UserName);
            Assert.Equal("test2@gmail.com", result.users[1].Email);
        }

        [Fact]
        [Description("GetAllUsersQuery : Ok Empty List")]
        public async Task GetAllUsersQueryTest_OkEmptyList()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            GetAllUsersQueryHandler handler = new GetAllUsersQueryHandler(_usersRepository.Object, _logger.Object);

            _usersRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(
                new List<Domain.Entities.User>());

            var result = await handler.Handle(query, _cancellationToken);
            Assert.NotNull(result);
            Assert.Empty(result.users);
        }
    }
}
