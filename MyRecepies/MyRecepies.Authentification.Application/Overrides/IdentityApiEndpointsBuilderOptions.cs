using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.Overrides
{
    public class IdentityApiEndpointsBuilderOptions
    {
        public bool IncludeRegisterPost { get; set; }
        public bool IncludeLoginPost { get; set; }
        public bool IncludeRefreshPost { get; set; }
        public bool IncludeConfirmEmailGet { get; set; }
        public bool IncludeResendConfirmationEmailPost { get; set; }
        public bool IncludeForgotPasswordPost { get; set; }
        public bool IncludeResetPasswordPost { get; set; }
        public bool IncludeManageGroup { get; set; }
        public bool Include2faPost { get; set; }
        public bool IncludegInfoGet { get; set; }
        public bool IncludeInfoPost { get; set; }
    }
}
