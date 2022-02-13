using PRS_Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PRS_Server.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var conn = "PRSDbContextWinHost";
#if DEBUG
conn = "localPRS";
#endif
builder.Services.AddDbContext<PRSDbContext>(x => { x.UseSqlServer(builder.Configuration.GetConnectionString(conn)); });
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
