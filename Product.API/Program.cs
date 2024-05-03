
using Microsoft.Extensions.FileProviders;
using Product.API.Extensions;
using Product.API.Middleware;
using Product.Infrastructure;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAPIReguestration();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InfraStructureConfigration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("errors/{0}");
app.UseCors();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
