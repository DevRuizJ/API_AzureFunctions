using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Security.Jwt.Configuration
{
    public sealed class TokenManagement
    {
        [DataMember(Name = "Secret")]
        public string Secret { get; set; } = TokenManagementConst.JWT_MNGNT_SECRET;

        [DataMember(Name = "EncryptionSecret")]
        public string EncryptionSecret { get; set; } = TokenManagementConst.JWT_MNGNT_ENCRYPTION_SECRET;

        [DataMember(Name = "Issuer")]
        public string Issuer { get; set; } = TokenManagementConst.JWT_MNGNT_ISSUER;

        [DataMember(Name = "Audience")]
        public string Audience { get; set; } = TokenManagementConst.JWT_MNGNT_AUDIENCE;

        [DataMember(Name = "AccessExpiration")]
        public int AccessExpiration { get; set; } = TokenManagementConst.JWT_MNGNT_ACCESS_Expiration;

        [DataMember(Name = "RefreshExpiration")]
        public int RefreshExpiration { get; set; } = TokenManagementConst.JWT_MNGNT_REFRESH_Expiration;
    }
}
