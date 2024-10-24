namespace Capstone.Collector.Models;

public class BillDto
{
    public int Congress { get; set; }
    public string Number { get; set; }
    public BillDtoLatestAction LatestAction { get; set; }
    public string OriginChamber { get; set; }
    public string OriginChamberCode { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Url   { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime UpdatedDateIncludingText { get; set; }
}