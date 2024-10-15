namespace Capstone.Collector.Models;

public class CongressMemberDtoComparer : IEqualityComparer<CongressMemberDto>
{
    public bool Equals(CongressMemberDto? x, CongressMemberDto? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        return x.BioguideId.Equals(y.BioguideId);
    }

    public int GetHashCode(CongressMemberDto obj)
    {
        return obj.BioguideId.GetHashCode();
    }
}