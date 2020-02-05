using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace WebApp.Extensions {
    public static class RazorExtension {
        public static string GetAntiforgeryToken (this HttpContext httpContext) {
            var antiforgery = (IAntiforgery) httpContext.RequestServices.GetService (typeof (IAntiforgery));
            // var tokenSet = antiforgery.GetAndStoreTokens (httpContext);
            // string fieldName = tokenSet.FormFieldName;
            // string requestToken = tokenSet.RequestToken;
            // return requestToken;

            return antiforgery.GetAndStoreTokens (httpContext).RequestToken;
        }

    }
}