using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "BrenoTrindade API");
    swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();