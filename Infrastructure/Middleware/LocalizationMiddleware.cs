using System.Globalization;

namespace PersonalDataDirectory.Infrastructure.Middleware;

public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var acceptLanguage = context.Request.Headers.AcceptLanguage.ToString();
        
        var kaCulture = new CultureInfo("ka-GE")
        {
            NumberFormat = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                NumberDecimalSeparator = ".",
                PercentDecimalSeparator = "."
            }
        };

        var enCulture = new CultureInfo("en-US")
        {
            NumberFormat = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                NumberDecimalSeparator = ".",
                PercentDecimalSeparator = "."
            }
        };
            
        var ruCulture = new CultureInfo("ru-RU")
        {
            NumberFormat = new NumberFormatInfo
            {
                CurrencyDecimalSeparator = ".",
                NumberDecimalSeparator = ".",
                PercentDecimalSeparator = "."
            }
        };

        var culture = acceptLanguage.ToLower() switch
        {
            "en" => enCulture,
            "ru" => ruCulture,
            "ka" => kaCulture,
            _ => kaCulture
        };

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}