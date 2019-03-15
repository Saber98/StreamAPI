using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StreamApi
{
    public class JwtAuthenticationFilter : AuthorizationFilterAttribute
    {
        private static string _lastAuthError = string.Empty;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!IsUserAuthorized(actionContext))
            {
                ShowAuthenticationError(actionContext);   
                return;
            }

            if (actionContext.RequestContext.Principal != null)
            {
                actionContext.RequestContext.Principal = Thread.CurrentPrincipal;
            }
            base.OnAuthorization(actionContext);
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            var authHeader = FetchFromHeader(actionContext); // fetch authorization token from header

            if (authHeader != null)
            {
                var auth = new AuthenticationModule();
                JwtSecurityToken userPayloadToken = auth.GenerateUserClaimFromJwt(authHeader);
                
                if (userPayloadToken != null)
                {
                    //TODO:  Add code to check database for existing, non-expired key.
                    var identity = auth.PopulateUserIdentity(userPayloadToken);
                    // string[] roles = { "All" };
                    var genericPrincipal = new GenericPrincipal(identity, identity.Roles.ToArray());
                    Thread.CurrentPrincipal = genericPrincipal;
                    var authenticationIdentity = Thread.CurrentPrincipal.Identity as JwtAuthenticationIdentity;
                    if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.UserName))
                    {
                        authenticationIdentity.UserId = identity.UserId;
                        authenticationIdentity.UserName = identity.UserName;
                    }
                    
                    return true;
                }
                else
                {
                    _lastAuthError = auth.GetLastValidationError();
                }

            }
            return false;
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            // var responseDTO = new ResponseDTO() { Code = 401, Message = "Unable to access, Please login again" };
            filterContext.Response =
                filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, _lastAuthError);
        }
    }
}