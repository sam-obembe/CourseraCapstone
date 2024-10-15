namespace Capstone.Collector.Models;

public record CongressDto
{
    public string EndYear { get; set; } = string.Empty;
    public string StartYear { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; } 
    public List<CongressSession> Sessions { get; set; } = new List<CongressSession>();
}
