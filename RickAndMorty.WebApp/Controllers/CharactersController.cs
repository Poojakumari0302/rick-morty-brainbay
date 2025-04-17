using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RickAndMorty.Shared;
using RickAndMorty.Shared.Models;

namespace RickAndMorty.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly AppDbContext _db;
    private static DateTime _lastFetch = DateTime.MinValue;
    private static bool _dataAdded = false;

    public CharactersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        bool fromDb = true;
        if ((DateTime.Now - _lastFetch).TotalMinutes > 5 || _dataAdded)
        {
            fromDb = false;
            _lastFetch = DateTime.Now;
            _dataAdded = false;
        }

        Response.Headers["from-database"] = fromDb.ToString().ToLower();
        return Ok(await _db.Characters.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Character character)
    {
        _db.Characters.Add(character);
        await _db.SaveChangesAsync();
        _dataAdded = true;
        return Ok(character);
    }

    [HttpGet("origin/{planet}")]
    public async Task<IActionResult> GetByPlanet(string planet)
    {
        var result = await _db.Characters
            .Where(c => c.Origin.ToLower() == planet.ToLower())
            .ToListAsync();
        return Ok(result);
    }
}
