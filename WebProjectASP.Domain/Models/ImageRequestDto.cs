namespace WebProjectASP.Domain.Models;

public record ImageRequestDto
{
    public required string Prompt { get; set; }
    public required string Model { get; set; }
}