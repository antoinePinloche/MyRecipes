using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery : IRequest<Domain.Entities.User>
    {
        public Guid Guid { get; set; }
        public GetUserByIdQuery(Guid id) 
        {
            Guid = id;
        }
    }
}
