using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using TaskFlowAPI.Data;
=======
using TaskFLowAPI.Data;
>>>>>>> dca66342fd571c4779676ca6b18eb7081b391d3e

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


