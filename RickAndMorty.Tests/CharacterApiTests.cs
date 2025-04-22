// RickAndMorty.Tests/CharacterServiceTests.cs
using Microsoft.EntityFrameworkCore.InMemory;
using RickAndMorty.Shared;
using System.Net.Http.Json;
using RickAndMorty.Shared.Models;
using Moq;
using RickAndMorty.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace RickAndMorty.Tests;

public class CharactersApiTests : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public CharactersApiTests(CustomWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCharacters_ReturnsOk_WithFromDatabaseHeader()
    {
        var response = await _client.GetAsync("/api/characters");

        response.EnsureSuccessStatusCode();
        Assert.True(response.Headers.Contains("from-database"));
    }

    [Fact]
    public async Task PostCharacter_AddsCharacterSuccessfully()
    {
        var newCharacter = new Character
        {
            Name = "Test Rick",
            Status = "Alive",
            Species = "Human",
            Gender = "Male",
            Origin = "Test Planet",
            Image = "http://test/image.png"
        };

        var result = await _client.PostAsJsonAsync("/api/characters", newCharacter);
        result.EnsureSuccessStatusCode();

        var getResponse = await _client.GetFromJsonAsync<List<Character>>("/api/characters");
        Assert.NotNull(getResponse);
        Assert.Contains(getResponse, c => c.Name == "Test Rick");

    }
}