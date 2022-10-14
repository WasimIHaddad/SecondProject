using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.ModelViews.ModelView;
using Serilog;
using System;
using System.Linq;
using Tazeez.Common.Extensions;

namespace SecondProject.Attributes
{
    public class SecondProjectAuthorizeAttribute : Attribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            try
            {
                var roleManager = context.HttpContext.RequestServices.GetService(typeof(IRoleManager)) as IRoleManager;
                var StringId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                int.TryParse(StringId, out int id);
                var user = new UserModel { Id = id };
                if (roleManager.CheckAccess(user))
                {
                    return;
                }
                throw new Exception("UnAuthorized");
            }
            catch (RetryLimitExceededException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error Occured Please contact system Administrator");

            }
            catch (InvalidOperationException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error Occured Please contact system Administrator");

            }
            catch (DependencyResolutionException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error Occured Please contact system Administrator");

            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error Occured Please contact system Administrator");


            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                context.Result = new UnauthorizedResult();
                return;

            }
        }
    }
}
