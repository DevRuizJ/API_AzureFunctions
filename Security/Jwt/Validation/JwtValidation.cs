using Application.Common;
using Application.Interface.Security.Jwt;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Security.Jwt.Validation
{
    public class JwtValidation : IJwtValidation
    {
        public bool access_token { get; }
        public string token_type { get; }
        public string expires_at { get; }

        public async Task<bool> ValidateJwt(string jwt)
        {
            IDictionary<string, object> claims = null;

            var isValid = false;
            try
            {
                claims = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(TokenManagementConst.JWT_MNGNT_SECRET)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(jwt);

                isValid = true;
            }
            catch (Exception ex) 
            {
                isValid= false;
            }

            return isValid;
        }
    }
}
