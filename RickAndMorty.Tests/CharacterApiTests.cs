// RickAndMorty.Tests/CharacterServiceTests.cs
using Microsoft.EntityFrameworkCore;
using RickAndMorty.Shared;
using RickAndMorty.Shared.Models;
using RickAndMorty.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace RickAndMorty.Tests;

public class CharacterApiTests
{
    private readonly AppDbContext _dbContext;

    public CharacterApiTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        _dbContext = new AppDbContext(options);
        SeedTestData();
    }

    private void SeedTestData()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        var characters = new List<Character>
        {
            new() { Id = 1, Name = "Rick", Status = "Alive", Species = "Human", Gender = "Male", Origin = "Earth", Image= "https://rickandmortyapi.com/api/character/avatar/1.jpeg"},
            new() { Id = 2, Name = "Morty", Status = "Alive", Species = "Human", Gender = "Male", Origin = "Earth", Image= "https://rickandmortyapi.com/api/character/avatar/2.jpeg"},
            new() { Id = 3, Name = "Summer", Status = "Alive", Species = "Human", Gender = "Female", Origin = "Earth", Image= "https://rickandmortyapi.com/api/character/avatar/3.jpeg"},
            new() { Id = 4, Name = "Adjudicator Rick", Status = "Dead", Species = "Human", Gender = "Male", Origin = "unknown", Image= "https://rickandmortyapi.com/api/character/avatar/8.jpeg"}
        };

        _dbContext.Characters.AddRange(characters);
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task Index_ReturnsOnlyAliveCharacters()
    {
        var controller = new CharactersController(_dbContext);
        var result = await controller.Get() as ViewResult;
        var model = result?.Model as List<Character>;

        Assert.NotNull(model);
        Assert.Equal(3, model.Count);
        Assert.DoesNotContain(model, c => c.Status != "Alive");
    }


}