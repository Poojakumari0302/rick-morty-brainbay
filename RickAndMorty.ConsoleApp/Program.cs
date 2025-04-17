using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using RickAndMorty.Shared;
using RickAndMorty.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;

var services = new ServiceCollection();
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=characters.db"));
services.AddHttpClient();

var provider = services.BuildServiceProvider();
var dbContext = provider.GetRequiredService<AppDbContext>();
await dbContext.Database.EnsureCreatedAsync();

var clientFactory = provider.GetRequiredService<IHttpClientFactory>();
var httpClient = clientFactory.CreateClient();

var response = await httpClient.GetFromJsonAsync<ApiResponse>("https://rickandmortyapi.com/api/character");

if (response?.Results is not null)
{
    dbContext.Characters.RemoveRange(dbContext.Characters);
    var aliveCharacters = response.Results
        .Where(c => c.Status == "Alive")
        .Select(c => new Character
        {
            Id = c.Id,
            Name = c.Name,
            Status = c.Status,
            Species = c.Species,
            Gender = c.Gender,
            Origin = c.Origin.Name,
            Image = c.Image
        });

    dbContext.Characters.AddRange(aliveCharacters);
    await dbContext.SaveChangesAsync();
}

record ApiResponse(List<ApiCharacter> Results);
record ApiCharacter(int Id, string Name, string Status, string Species, string Gender, Origin Origin, string Image);
record Origin(string Name);
