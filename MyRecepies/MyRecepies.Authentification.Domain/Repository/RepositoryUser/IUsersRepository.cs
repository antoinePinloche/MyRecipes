using MyRecepies.Authentification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Domain.Repository.RepositoryUser
{
    public interface IUsersRepository : IRepository<User, Guid>
    {
    }
}
