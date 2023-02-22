using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace DapperFantom.Areas.Admin.Models
{
    public class AdminAuth : ActionFilterAttribute
    {
        public override object TypeId => base.TypeId;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }

        public override bool Match(object? obj)
        {
            return base.Match(obj);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string username;
            string id;

            if (context != null)
            {
                if (context.HttpContext.Request.Cookies.Keys != null)
                {
                    try
                    {
                        username = context.HttpContext.Request.Cookies["username"];
                        id = context.HttpContext.Request.Cookies["userid"];
                        if (id == null || username== null)
                        {
                            context.HttpContext.Response.Redirect("/Admin/Login");
                        }

                    }
                    catch (Exception)
                    {

                        context.HttpContext.Response.Redirect("/Admin/Login");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect("/Admin/Login");
                }
            }


            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
