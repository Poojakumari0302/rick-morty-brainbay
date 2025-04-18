namespace RickAndMorty.Shared.Models;
using System.ComponentModel.DataAnnotations;

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required string Species { get; set; }
    public required string Gender { get; set; }
    public required string Origin { get; set; }
    public required string Image { get; set; }
}
