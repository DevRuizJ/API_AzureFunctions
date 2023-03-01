using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Security.Jwt
{
    public interface IJwtValidation
    {
        Task<bool> ValidateJwt(string jwt);
    }
}
