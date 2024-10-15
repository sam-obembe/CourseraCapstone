using System.ComponentModel.DataAnnotations;

namespace Capstone.Common.Entities;

public class CongressMember:Entity
{
    [Key]
    public string BioGuideId { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Url { get; set; }
    public string Attribution { get; set; }
    public string ImageUrl { get; set; }
}