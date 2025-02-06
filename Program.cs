using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PersonalDataDirectory.Extensions;
using PersonalDataDirectory.Infrastructure.Data;
using PersonalDataDirectory.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(webRootPath))
    Directory.CreateDirectory(webRootPath);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("ka-GE");

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Logging.AddConsole();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.RegisterServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

app.UseRequestLocalization(x =>
{
    x.DefaultRequestCulture = new RequestCulture("en-US");
    x.SupportedCultures = new List<CultureInfo> { new("ka-GE"), new("en-US"), new("ru-RU") };
    x.SupportedUICultures = new List<CultureInfo> { new("ka-GE"), new("en-US"), new("ru-RU") };
});

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();