using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace WebApp.Extensions {
    public class CustomAuthorization {
        public static bool ValidaClaimsUsuario (HttpContext context, string claimType, string claimValue) {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any (c => c.Type == claimType && c.Value.Contains (claimValue));

        }

    }

    public class ClaimAuthorizeAttribute : TypeFilterAttribute {
        public ClaimAuthorizeAttribute (string claimType, string claimValue) : base (typeof (RequesitoClaimFilter)) {
            Arguments = new object[] {
                new Claim (claimType, claimValue)
            };

        }
    }
    public class RequesitoClaimFilter : IAuthorizationFilter {

        readonly Claim _claim;

        public RequesitoClaimFilter (Claim claim) {
            _claim = claim;
        }

        public void OnAuthorization (AuthorizationFilterContext context) {
            if (!context.HttpContext.User.Identity.IsAuthenticated) {
                var route = new RouteValueDictionary (new {
                    area = "Identity",
                        page = "/Account/Login",
                        ReturnUrl = context.HttpContext.Request.Path.ToString ()

                });
                context.Result = new RedirectToRouteResult (route);
                return;
            }
            if (!CustomAuthorization.ValidaClaimsUsuario (context.HttpContext, _claim.Type, _claim.Value)) {
                context.Result = new StatusCodeResult (403);
            }
        }
    }

}