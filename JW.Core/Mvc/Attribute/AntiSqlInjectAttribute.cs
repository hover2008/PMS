using JW.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JW.Core.Mvc.Attribute
{
    /// <summary>
    /// 过滤表现层请求参数含有T-SQL危险关键字
    /// </summary>
    public class AntiSqlInjectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionDescriptor.Parameters;
            foreach (var p in actionParameters)
            {
                if (p.ParameterType == typeof(string))
                {
                    if (filterContext.ActionArguments.ContainsKey(p.Name) && filterContext.ActionArguments[p.Name] != null)
                    {
                        filterContext.ActionArguments[p.Name] = (filterContext.ActionArguments[p.Name].ToString().FilterSql());
                    }
                }
            }
        }
    }
}
