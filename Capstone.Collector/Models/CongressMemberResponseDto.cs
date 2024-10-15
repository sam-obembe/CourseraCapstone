namespace Capstone.Collector.Models;

public record CongressMemberResponseDto
{
    public IEnumerable<CongressMemberDto> Members { get; set; } = new List<CongressMemberDto>();
    public CongressMemberResponsePagination Pagination { get; set; }
}