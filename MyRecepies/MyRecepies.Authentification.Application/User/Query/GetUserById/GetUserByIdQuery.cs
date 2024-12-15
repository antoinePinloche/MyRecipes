using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.User.Query.GetUserById
{
    public class GetUserByIdQuery
    {
        public Guid Guid { get; set; }
        public GetUserByIdQuery(Guid id) 
        {
            
        }
    }
}
