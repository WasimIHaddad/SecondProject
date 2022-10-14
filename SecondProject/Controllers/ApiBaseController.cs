using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.ModelViews.ModelView;
using System.Linq;
using Tazeez.Common.Extensions;

namespace SecondProject.Controllers
{
    public class ApiBaseController :Controller
    {
        private UserModel _loggedInUser;

        protected UserModel LoggedInUser
        {
            get
            {
                if(_loggedInUser != null)
                {
                    return _loggedInUser;
                }
                Request.Headers.TryGetValue("Authorization", out StringValues Token);
                if (string.IsNullOrWhiteSpace(Token))
                {
                    _loggedInUser = null;
                    return _loggedInUser;
                }
                var ClaimId = User.Claims.FirstOrDefault(c => c.Type == "Id");

                int.TryParse(ClaimId.Value, out int idd);

                if (ClaimId == null || !int.TryParse(ClaimId.Value , out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or expired token");
                }

                var commonManager = HttpContext.RequestServices.GetService(typeof(ICommonManager)) as ICommonManager;
                _loggedInUser = commonManager.GetUserRole(new UserModel { Id = id });

                return _loggedInUser;
                
            }
        }

        public ApiBaseController()
        {

        }
    }
}
