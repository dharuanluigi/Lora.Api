using System.Net.Http.Headers;
using Lora.Api.Clients.Interfaces;
using Lora.Api.Services;
using Lora.Api.Services.Interfaces;
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
builder.Services.AddSwaggerGen();

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
