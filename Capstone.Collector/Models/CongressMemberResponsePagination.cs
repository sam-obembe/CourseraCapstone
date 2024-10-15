namespace Capstone.Collector.Models;

public record CongressMemberResponsePagination
{
    public int Count { get; set; }
    public string Next { get; set; }=String.Empty;
}