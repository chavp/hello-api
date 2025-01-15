using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MooDeng.Parties.IServices;
using MooDeng.Parties.Mappings;
using MooDeng.Parties.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPartiesService, PartiesService>();

builder.Services.AddDbContextFactory<PartiesContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("parties_db")));

builder.Services.AddCors(policy => {

    policy.AddPolicy("Policy_Name", builder =>
      builder.WithOrigins("https://*:7152")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()

 );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Policy_Name");

app.Run();
