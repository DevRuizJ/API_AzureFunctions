using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class TokenManagementConst
    {
        public const string JWT_MNGNT_SECRET = "Axtq_gt$!Bh_2vGT";
        public const string JWT_MNGNT_ENCRYPTION_SECRET = "J7ad,13-19S!12Bg";
        public const string JWT_MNGNT_ISSUER = "https://bluetab";
        public const string JWT_MNGNT_AUDIENCE = "https://primax";
        public const Int32 JWT_MNGNT_ACCESS_Expiration = 1440;
        public const Int32 JWT_MNGNT_REFRESH_Expiration = 2880;
    }
}
