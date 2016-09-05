using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Obsidian.Misc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public bool AllowNull { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            if (!AllowNull && HasNullParameter(context.ActionArguments))
            {
                context.Result = new BadRequestObjectResult("Model can not be null.");
            }
            base.OnActionExecuting(context);
        }

        private bool HasNullParameter(IDictionary<string, object> arguments) =>
            arguments.Any(d => d.Value == null);
    }
}