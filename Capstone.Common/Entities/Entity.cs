namespace Capstone.Common.Entities;

public class Entity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
}