using System.Reflection;
using AspNetCoreRateLimit;
using UniversalNFT.dev.API.Facades;
using UniversalNFT.dev.API.Services.AppSettings;
using UniversalNFT.dev.API.Services.ImageCacheCleanup;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.NFT;
using UniversalNFT.dev.API.Services.Providers;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;
using UniversalNFT.dev.API.SwaggerConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);

    options.EnableAnnotations();
    options.SchemaFilter<SwaggerTryItOutDefaultValue>();
});

// DI
builder.Services.Configure<XRPLSettings>(builder.Configuration.GetSection("XRPLSettings"));
builder.Services.Configure<ServerSettings>(builder.Configuration.GetSection("ServerSettings"));
builder.Services.AddSingleton<IHttpFacade, HttpFacade>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IXRPLService, XRPLService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IOnXRPService, OnXRPService>();
builder.Services.AddSingleton<IRulesEngine, RulesEngine>();
builder.Services.AddScoped<INFTService, NFTService>();

// Image cache folder clean up job
builder.Services.Configure<CacheFolderWatcherSettings>(builder.Configuration.GetSection("CacheFolderWatcher"));
builder.Services.AddHostedService<ImageCleanupTask>();

builder.Services.AddOptions();
builder.Services.AddMemoryCache();

// IP Rate limiting
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

app.UseIpRateLimiting();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseStaticFiles();

// Redirect from the root URL to /swagger.
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription(); ;
app.MapGet("/about", () => Results.Redirect("https://universalnft.dev")).ExcludeFromDescription(); ;

// You may want to limit where this can be called from if you host your own
// so it only works for your services. Or be nice and leave it open to others :)
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
