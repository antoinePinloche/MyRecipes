﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResult>
    {
        public GetAllUsersQueryRequest() { }
    }
}
