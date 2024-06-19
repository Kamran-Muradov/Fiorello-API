using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using FiorelloAPI.Data;
using FiorelloAPI.Helpers;
using FiorelloAPI.Services;
using FiorelloAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//builder.Services.AddControllers().AddJsonOptions(x =>
//    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISocialService, SocialService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();