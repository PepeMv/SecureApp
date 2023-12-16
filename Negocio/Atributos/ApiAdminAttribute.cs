using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Negocio.Clases;

namespace Negocio.Atributos
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ApiAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationRol = 
                context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "typ")?.Value ?? null;

            var isAdmin = authorizationRol != null && authorizationRol == Roles.ADMIN;

            if (!isAdmin)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
