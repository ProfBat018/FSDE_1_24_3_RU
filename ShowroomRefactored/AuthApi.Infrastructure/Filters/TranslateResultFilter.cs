using AuthApi.Infrastructure.Helpers;

namespace AuthApi.Infrastructure.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

public class TranslateResultFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var culture = CultureInfo.CurrentUICulture?.Name ?? CultureInfo.CurrentCulture?.Name ?? "az";

        object? value = null;

        if (context.Result is ObjectResult objResult)
        {
            value = objResult.Value;
        }
        else if (context.Result is JsonResult jsonResult)
        {
            value = jsonResult.Value;
        }

        if (value != null)
        {
            TranslationHelper.ApplyTranslationsToResult(value, culture);
        }

        base.OnResultExecuting(context);
    }
}
