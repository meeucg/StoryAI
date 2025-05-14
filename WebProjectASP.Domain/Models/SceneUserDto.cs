namespace WebProjectASP.Domain.Models;

public record SceneUserDto
{
    public required string Description { get; set; }
    public required string Img { get; set; }
    public required List<string> Options { get; set; }
}