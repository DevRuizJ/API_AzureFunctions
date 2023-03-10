using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Interface.Security
{
    [DataContract]
    public class AuthDto
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; private set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; private set; }

        [DataMember(Name = "expires_at")]
        public long ExpiresAt { get; private set; }
        public string User { get; set; }
        //public string Name { get; set; }
        //public string LastName { get; set; }
        //public string MotherLastName { get; set; }
        public string Password { get; set; }

        public AuthDto(string accessToken, long expiresAt, string _Usuario, string _pass)
        {
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
            TokenType = "Bearer";
            User = _Usuario;
            //Name = _Nombres;
            //LastName = _ApellidoPaterno;
            //MotherLastName = _ApellidoMaterno;
            Password = _pass;
        }
    }
}
