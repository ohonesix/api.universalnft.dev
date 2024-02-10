using AspNetCoreRateLimit;
using SixLabors.ImageSharp;
using UniversalNFT.dev.API.Facades;
using UniversalNFT.dev.API.Services.CacheCleanup;
using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.NFT;
using UniversalNFT.dev.API.Services.Providers;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.XRPL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(service => service.EnableAnnotations());

// DI
builder.Services.AddSingleton<IHttpFacade, HttpFacade>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IXRPLService, XRPLService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IOnXRPService, OnXRPService>();
builder.Services.AddSingleton<IRulesEngine, RulesEngine>();
builder.Services.AddScoped<INFTService, NFTService>();

// Image cache folder clean up job
builder.Services.Configure<CacheFolderWatcherSettings>(builder.Configuration.GetSection("CacheFolderWatcher"));
builder.Services.AddHostedService<CleanUpTask>();

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

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.UseStaticFiles();

app.Run();
