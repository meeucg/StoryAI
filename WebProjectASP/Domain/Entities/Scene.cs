namespace WebProjectASP.Domain.Entities;

public class Scene
{
    public required int Id { get; set; }
    
    public required string Description { get; set; }
    
    public required string Img { get; set; }
    
    public required List<string> Options { get; set; }
    
    public required List<string> ContinuityNotes { get; set; }
    
    public required int Position { get; set; }
}