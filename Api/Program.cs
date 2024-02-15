using System.Net.Http.Headers;
using System.Reflection;
using Lora.Api.Clients.Interfaces;
using Lora.Api.Services;
using Lora.Api.Services.Interfaces;
using Microsoft.OpenApi.Models;
using RestEase;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton(RestClient.For<IGithubClient>("https://api.github.com", async (request, cancelationToken) => {
    await Task.Run(() => request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", builder.Configuration.GetValue<string>("GH_TOKEN")));
}));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1",
        Title = "Lora Smart Contact Endpoint",
        Description = "API-gateway for smart contact to delivery services into smart contact"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
