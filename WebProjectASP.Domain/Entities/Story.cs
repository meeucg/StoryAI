namespace WebProjectASP.Domain.Entities;

public class Story
{
    public required int Id { get; set; }
    
    public required User Owner { get; set; }
    
    public required string Name { get; set; }
    
    public required List<Scene> Scenes { get; set; }
}