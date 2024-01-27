using UniversalNFT.dev.API.Services.Images;
using UniversalNFT.dev.API.Services.Mapping;
using UniversalNFT.dev.API.Services.Providers;
using UniversalNFT.dev.API.Services.XRPL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(service => service.EnableAnnotations());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<XRPLService>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddSingleton<OnXRPService>();
builder.Services.AddSingleton<RulesEngine>();

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
