using Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Security.Jwt
{
    public interface IJwtGenerator
    {
        Task<AuthDto> CreateToken(CredentialsEntity credentials);
    }
}
