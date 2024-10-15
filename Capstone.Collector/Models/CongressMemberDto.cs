namespace Capstone.Collector.Models;

public class CongressMemberDto
{
	public string? BioguideId { get; set; } = String.Empty;
	public string? Name { get; set; } = String.Empty;
	public string? State { get; set; } =String.Empty;
	public string? Url { get; set; } =String.Empty;
	public CongressMemberDepiction? Depiction { get; set; } = new CongressMemberDepiction();
	public DateTime UpdatedDate { get; set; }

}


