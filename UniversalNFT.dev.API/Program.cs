using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.Rules;
using UniversalNFT.dev.API.Services.Providers;
using UniversalNFT.dev.API.Services.XRPL;
using UniversalNFT.dev.API.Services.NFT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(service => service.EnableAnnotations());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IXRPLService, XRPLService>();
builder.Services.AddSingleton<IImageService, ImageService>();
builder.Services.AddSingleton<IOnXRPService, OnXRPService>();
builder.Services.AddSingleton<IRulesEngine, RulesEngine>();
builder.Services.AddScoped<INFTService, NFTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
