using Microsoft.EntityFrameworkCore;

namespace Capstone.Common.Entities;

[PrimaryKey(nameof(Number),nameof(OriginChamberCode))]
public class Bill
{
    public int Congress { get; set; }
    public string Number { get; set; }
    public DateTime LatestActionDate { get; set; }
    public string LatestActionText { get; set; }
    public string OriginChamber { get; set; }
    public string OriginChamberCode { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Url   { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime UpdatedDateIncludingText { get; set; }
}